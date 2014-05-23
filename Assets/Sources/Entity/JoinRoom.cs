using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperTrunfo
{
    class JoinRoom {

        public Player player;
        public Room room;

        public JoinRoom(Player player, Room room) {
            this.player = player;
            this.room = room;
        }

        public JoinRoom()
        {
        }
    }
}
