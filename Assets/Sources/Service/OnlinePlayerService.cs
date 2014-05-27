using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperTrunfo
{
    class OnlinePlayerService : PlayerService{
        
        private TurnService  turnService;

        private GameObserver gameObserver;

        private WebSocketService socket;
        
        public OnlinePlayerService() {
            turnService     = Container.get<TurnService>();
            gameObserver    = Container.get<GameObserver>();
            socket          = Container.get<WebSocketService>();

            gameObserver.addListener("OnPlay", (message) => {
                Play play = ((Message<Play>) message).message;

                turnService.play(play);
            });
            
            gameObserver.addListener(Events.CARD_TO_TABLE, (message) => {
                Play play = (Play) message;
                
                if(play.player.playerType == PlayerType.LOCAL){
                    socket.sendMessage<Play>(new Message<Play>(
                        "Play",
                        play,
                        play.GetType().FullName
                    ));
                }
                
            });
            
            gameObserver.addListener("giveCards", (message) => {
                Room room = ((Message<Room>) message).message;

                turnService.currentRoom.players.ForEach((player) => {
                    var onPlayer = room.players.Find((play) => {
                        return play.id == player.id;
                    });

                    player.cards = onPlayer.cards;
                });
            });
            
            gameObserver.addListener("cardsToPlayers", (message) => {
                Room room = (Room) message;

                if (turnService.isMaster) {
                    socket.sendMessage<Room>(new Message<Room>("giveCards", room, room.GetType().FullName));
                }
            });
            
            

        }

        public Player createPlayer(){
            return new Player(System.Guid.NewGuid().ToString(), new List<Card>(), null, PlayerType.REMOTE);
        }

        public void play(Player player){
            //socket.sendMessage<Player>(new Message<Player>(
            //    "ShouldPlay",
            //    player,
            //    player.GetType().FullName
            //));
        }
    }
}
