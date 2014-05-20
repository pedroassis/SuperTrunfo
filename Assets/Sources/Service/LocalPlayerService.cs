using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperTrunfo
{
    class LocalPlayerService : PlayerService{

        private GameObserver gameObserver;

        private WebSocketService socket;

        public Player _localPlayer;

        public Player localPlayer
        {
            get;
            set;
        }

        public LocalPlayerService() {
            gameObserver    = Container.get<GameObserver>();
            socket          = Container.get<WebSocketService>();
        }

        public Player createPlayer(){
            return new Player(System.Guid.NewGuid().ToString(), new List<Card>(), null, PlayerType.LOCAL);
        }

        public void play(Player player){
            if(player != localPlayer){
                throw new InvalidOperationException("Not the local player. Only one local player is allowed.");
            }

            socket.sendMessage<Player>(new Message<Player>(
                "OnPlay",
                player,
                player.GetType().FullName
            ));

            gameObserver.trigger(Events.LOCAL_PLAYERS_TURN, player);
        }
    }
}
