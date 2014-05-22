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

        public GamePlayController() {
            turnService = Container.get<TurnService>();
        }

        public void Awake() {

            turnService.currentPlayer.cards.ForEach((card) => {
                var cardPrefab = Resources.Load("Card");

                var cardObject = Instantiate(cardPrefab) as GameObject;

                PlayLocalController cardCtrl = (PlayLocalController)cardObject.GetComponent<PlayLocalController>();

                cardCtrl.inactiveTex = (Texture2D) Resources.Load(String.Format("Images/Card/A/A{0}", card.id[1]));

                cardCtrl.activeTex = (Texture2D) Resources.Load(String.Format("Images/Card/A/A{0}", card.id[1]));

                cardObject.guiTexture.texture = cardCtrl.inactiveTex;

            });

            

        }
    }
}
