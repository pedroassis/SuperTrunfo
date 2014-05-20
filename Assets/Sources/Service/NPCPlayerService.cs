using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperTrunfo
{
    class NPCPlayerService : PlayerService {

        private TurnService turnService;

        private GameObserver gameObserver;

        private static readonly Random random = new Random();

        private static readonly Property[] properties = new Property[] { 
                                                                            Property.EQUIPMENT,
                                                                            Property.HABILITY,
                                                                            Property.INTELIGENCE,
                                                                            Property.POWER,
                                                                            Property.VELOCITY
                                                                        };

        public NPCPlayerService() {
            turnService  = Container.get<TurnService>();
            gameObserver = Container.get<GameObserver>();
        }

        public Player createPlayer(){
            return new Player(System.Guid.NewGuid().ToString(), new List<Card>(), null, PlayerType.NPC);
        }

        public void play(Player player){
            gameObserver.trigger(Events.NPC_TURN, player);

            Property selectedProperty = turnService.currentProperty == Property.NONE ? getRandomProperty() : turnService.currentProperty;

            Card bestCard = turnService.getBiggest(player.cards, selectedProperty);

            turnService.selectProperty(selectedProperty, player);

            turnService.play(bestCard, player);
            
        }


        private Property getRandomProperty() {
            return properties[random.Next(0, properties.Length)];
        }
    }
}
