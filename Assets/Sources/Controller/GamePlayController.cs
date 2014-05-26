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

                if(turnService.currentPlayer != localPlayer || turnService.currentProperty == Property.NONE){
                    // 
                    UnityEngine.Debug.Log("Clicked in " + card.id + " but not your turn");
                    return;
                }

                turnService.play(card, localPlayer);

                showCards();

            }, this);

            gameObserver.addListener(Events.TURN_WINNER, (playerMessage) => {
                
                if(playerMessage.Equals(localPlayer)){
                    showMenu();
                }
            });

            gameObserver.addListener(Events.NEXT_PLAYERS_TURN, (playerMessage) => {
                
                if(playerMessage.Equals(localPlayer)){
                    showMenu();
                }
            });

            gameObserver.addListener(Events.SELECT_PROPERTY, (property) => {
				
				showMenu();
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
            }
        }

        private void showMenu() {

            try {
	                GameObject[] buttons = GameObject.FindGameObjectsWithTag("Sidebar");
	
	                foreach(GameObject button in buttons){
	                    if (!button.name.ToLower().Contains(turnService.currentProperty.ToString().ToLower())
								&& turnService.currentProperty != Property.NONE) { 
	                        button.GetComponent<GUITexture>().enabled = false;
	                    } else {
	
	                        button.GetComponent<GUITexture>().enabled = true;
	
	                    }
	               }
				} catch(Exception e){}
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

            Card cardOnTop = localPlayer.cards[localPlayer.cards.Count -1];

            var cardObject = Instantiate(cardPrefab) as GameObject;

            PlayLocalController cardCtrl = (PlayLocalController)cardObject.GetComponent<PlayLocalController>();

            cardCtrl.inactiveTex = (Texture2D) Resources.Load(String.Format("Images/Card/{0}/{1}",cardOnTop.id[0],cardOnTop.id));

            cardCtrl.activeTex = (Texture2D) Resources.Load(String.Format("Images/Card/{0}/{1}", cardOnTop.id[0], cardOnTop.id));

            cardCtrl.eventMessage = cardOnTop;

            cardObject.guiTexture.texture = cardCtrl.inactiveTex;
				
			var index = turnService.currentPlayer.cards.IndexOf(cardOnTop);

            cardObject.transform.position = new Vector3(0.1F, cardObject.transform.position.y, cardObject.transform.position.z);

            turnService.cardsOnTable.Keys.ToList().TrueForAll((cardOnTable) => {

                cardObject = Instantiate(cardOnTablePrefab) as GameObject;

                cardCtrl = (PlayLocalController)cardObject.GetComponent<PlayLocalController>();

                cardCtrl.inactiveTex = (Texture2D)Resources.Load(String.Format("Images/Card/{0}/{1}", cardOnTable.id[0], cardOnTable.id));

                cardCtrl.activeTex = (Texture2D)Resources.Load(String.Format("Images/Card/{0}/{1}", cardOnTable.id[0], cardOnTable.id));

                cardCtrl.eventMessage = cardOnTable;

                cardObject.guiTexture.texture = cardCtrl.inactiveTex;

                index = turnService.cardsOnTable.Keys.ToList().IndexOf(cardOnTable);

                var x = 0.1F + index * 0.2F;

                cardObject.transform.position = new Vector3(x, cardObject.transform.position.y, cardObject.transform.position.z);
              	
				return index < 4;
				
            });
		}
    }
}
