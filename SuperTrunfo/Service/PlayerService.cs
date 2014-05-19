using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperTrunfo
{
    interface PlayerService{

        Player createPlayer();

        void play(Player player);

    }
}
