﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperTrunfo
{
    class Play {

        public Player player;

        public Card   selectedCard;

        public Play() {}

        public Play(Player player, Card card) {
            this.player = player;
            this.selectedCard = card;
        }
    }
}
