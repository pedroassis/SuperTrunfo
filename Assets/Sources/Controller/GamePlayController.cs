using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SuperTrunfo
{
    class GamePlayController : MonoBehaviour
    {

        private TurnService turnService;
        private GameObserver gameObserver;

        public Player localPlayer;

        public GamePlayController() {
            turnService  = Container.get<TurnService>();

            gameObserver = Container.get<GameObserver>();

            gameObserver.addListener("GUI.CardClick", (cardObject) => {

                Card card = cardObject as Card;

                if(turnService.currentPlayer != localPlayer){
                    // 
                    UnityEngine.Debug.Log("Clicked in " + card.id + " but not your turn");
                    return;
                }

                turnService.play(card, localPlayer);

                showCards();

                UnityEngine.Debug.Log("Clicked in " + card.id);

            }, this);

            gameObserver.addListener(Events.TURN_WINNER, (playerMessage) => {
                
                if(playerMessage.Equals(localPlayer)){
                    showMenu();
                }
            });

            gameObserver.addListener(Events.SELECT_PROPERTY, (property) => {
                
                if(turnService.currentPlayer == localPlayer){
                    setProperty((Property) property);
                }
            });

            //showMenu();

            bindPropertyListeners();
            
        }

        public void Awake() {

            showCards();

        }

        public void OnDestroy () {
            Debug.Log(this.GetType().Name + " was destroyed");

            gameObserver.removeListeners(this);
	    }

        private void setProperty(Property property){
            if(localPlayer == turnService.currentPlayer && turnService.currentProperty == Property.NONE){
                turnService.selectProperty(property, localPlayer);

                GameObject[] buttons = GameObject.FindGameObjectsWithTag("Sidebar");

                foreach(GameObject button in buttons){
                    if (!button.name.ToLower().Contains(turnService.currentProperty.ToString().ToLower()))
                        button.GetComponent<GUITexture>().enabled = false;
                }
            }
        }

        private void showMenu() {

            GameObject[] buttons = GameObject.FindGameObjectsWithTag("Sidebar");

            foreach(GameObject button in buttons){
                button.GetComponent<GUITexture>().enabled = true;
            }
        }

        private void bindPropertyListeners(){        

            gameObserver.addListener("GUI.Set.Power", (cardObject) => {

                setProperty(Property.POWER);

            }, this);

            gameObserver.addListener("GUI.Set.Hability", (cardObject) => {

                setProperty(Property.HABILITY);

            }, this);

            gameObserver.addListener("GUI.Set.Inteligence", (cardObject) => {

                setProperty(Property.INTELIGENCE);

            }, this);

            gameObserver.addListener("GUI.Set.Equipment", (cardObject) => {

                setProperty(Property.EQUIPMENT);

            }, this);

            gameObserver.addListener("GUI.Set.Velocity", (cardObject) => {

                setProperty(Property.VELOCITY);

            }, this);
        }
		
		private void showCards(){

            var cardPrefab = Resources.Load("Card");

            var cardOnTablePrefab = Resources.Load("CardOnTable");

            localPlayer = turnService.currentPlayer;
			
			GameObject[] cardsGame = GameObject.FindGameObjectsWithTag("Card");
			
			foreach(var card in cardsGame){
				Destroy(card);
			}

            localPlayer.cards.TrueForAll((card) => {

                var cardObject = Instantiate(cardPrefab) as GameObject;

                PlayLocalController cardCtrl = (PlayLocalController)cardObject.GetComponent<PlayLocalController>();

                cardCtrl.inactiveTex = (Texture2D) Resources.Load(String.Format("Images/Card/{0}/{1}", card.id[0], card.id));

                cardCtrl.activeTex = (Texture2D) Resources.Load(String.Format("Images/Card/{0}/{1}", card.id[0], card.id));

                cardCtrl.eventMessage = card;

                cardObject.guiTexture.texture = cardCtrl.inactiveTex;
				
				var index = turnService.currentPlayer.cards.IndexOf(card);

                var x = 0.1F + index * 0.2F;

                cardObject.transform.position = new Vector3(x, cardObject.transform.position.y, cardObject.transform.position.z);
              	
				return index < 4;
				
            });

            turnService.cardsOnTable.Keys.ToList().TrueForAll((card) => {

                var cardObject = Instantiate(cardOnTablePrefab) as GameObject;

                PlayLocalController cardCtrl = (PlayLocalController)cardObject.GetComponent<PlayLocalController>();

                cardCtrl.inactiveTex = (Texture2D) Resources.Load(String.Format("Images/Card/{0}/{1}", card.id[0], card.id));

                cardCtrl.activeTex = (Texture2D) Resources.Load(String.Format("Images/Card/{0}/{1}", card.id[0], card.id));

                cardCtrl.eventMessage = card;

                cardObject.guiTexture.texture = cardCtrl.inactiveTex;
				
				var index = turnService.currentPlayer.cards.IndexOf(card);

                var x = 0.1F + index * 0.2F;

                cardObject.transform.position = new Vector3(x, cardObject.transform.position.y, cardObject.transform.position.z);
              	
				return index < 4;
				
            });
		}
    }
}
