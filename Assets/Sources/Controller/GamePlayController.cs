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
            
        }

        public void Awake() {

            showCards();

        }

        public void OnDestroy () {
            Debug.Log(this.GetType().Name + " was destroyed");

            gameObserver.removeListeners(this);
	    }
		
		private void showCards(){
			
            var cardPrefab = Resources.Load("Card");

            localPlayer = turnService.currentPlayer;
			
			GameObject[] cardsGame = GameObject.FindGameObjectsWithTag("Card");
			
			foreach(var card in cardsGame){
				Destroy(card);
			}

            localPlayer.cards.TrueForAll((card) => {

                var cardObject = Instantiate(cardPrefab) as GameObject;

                PlayLocalController cardCtrl = (PlayLocalController)cardObject.GetComponent<PlayLocalController>();

                cardCtrl.inactiveTex = (Texture2D) Resources.Load(String.Format("Images/Card/A/A{0}", card.id[1]));

                cardCtrl.activeTex = (Texture2D) Resources.Load(String.Format("Images/Card/A/A{0}", card.id[1]));

                cardCtrl.eventMessage = card;

                cardObject.guiTexture.texture = cardCtrl.inactiveTex;
				
				var index = turnService.currentPlayer.cards.IndexOf(card);

                var x = 0.1F + index * 0.2F;

                cardObject.transform.position = new Vector3(x, cardObject.transform.position.y, cardObject.transform.position.z);
              	
				return index < 6;
				
            });
		}
    }
}
