﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperTrunfo
{
    interface PlayerService{

        public Player createPlayer();

        public void play(Player player);

    }
}
