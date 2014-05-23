using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperTrunfo
{
    class Player
    {
        public String       id;

        public List<Card>   cards;

        public Card         currentCard;

        public PlayerType  playerType;

        public Player(String id, List<Card> cards, Card currentCard, PlayerType playerType) {
            this.id = id;
            this.cards = cards;
            this.currentCard = currentCard;
            this.playerType = playerType;
        }

        public Player() {
        }

    }
}
