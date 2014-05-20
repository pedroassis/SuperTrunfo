using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTrunfo
{
    class Room {
        public List<Player> players;

        public String       name;

        public String       id;

        public Room() { }
        public Room(String id, String name, List<Player> players) {
            this.id = id;
            this.name = name;
            this.players = players;
        }
    }
}
