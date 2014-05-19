using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperTrunfo
{
    class LocalPlayerService : PlayerService{

        private GameObserver gameObserver;
        public Player localPlayer { get; set; }

        public LocalPlayerService() {
            gameObserver = Container.get<GameObserver>();
        }

        public Player createPlayer(){
            return new Player(System.Guid.NewGuid().ToString(), new List<Card>(), null, PlayerType.LOCAL);
        }

        public void play(Player player){
            if(player != localPlayer){
                throw new InvalidOperationException("Not the local player. Only one local player is allowed.");
            }
            gameObserver.trigger(Events.LOCAL_PLAYERS_TURN, player);
        }
    }
}
