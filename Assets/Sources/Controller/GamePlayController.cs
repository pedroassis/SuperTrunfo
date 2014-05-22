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

                if(){
                
                }

                Card card = cardObject as Card;

                UnityEngine.Debug.Log("Clicked in " + card.id);
            });
            
        }

        public void Awake() {
            var cardPrefab = Resources.Load("Card");

            localPlayer = turnService.currentPlayer;

            turnService.currentRoom.players.ForEach((player) => player.cards.ForEach((card) => {

                var cardObject = Instantiate(cardPrefab) as GameObject;

                PlayLocalController cardCtrl = (PlayLocalController)cardObject.GetComponent<PlayLocalController>();

                cardCtrl.inactiveTex = (Texture2D) Resources.Load(String.Format("Images/Card/A/A{0}", card.id[1]));

                cardCtrl.activeTex = (Texture2D) Resources.Load(String.Format("Images/Card/A/A{0}", card.id[1]));

                cardCtrl.eventMessage = card;

                cardObject.guiTexture.texture = cardCtrl.inactiveTex;

                var x = 0.1F + turnService.currentPlayer.cards.IndexOf(card) * 0.2F;

                cardObject.transform.position = new Vector3(x, cardObject.transform.position.y, cardObject.transform.position.z);
              
            }));

            

        }
    }
}
