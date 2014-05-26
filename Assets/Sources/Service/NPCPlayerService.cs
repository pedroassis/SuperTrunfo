using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

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


            if (player.cards.Count > 0) {

                Card bestCard = player.cards[player.cards.Count - 1];

                Property selectedProperty = turnService.currentProperty == Property.NONE ? getBestProperty(bestCard) : turnService.currentProperty;

                turnService.selectProperty(selectedProperty, player);

                turnService.play(bestCard, player);
            }
            
        }

        private Property getBestProperty(Card card) {
            return properties.Aggregate((prop1, prop2) => {

                FieldInfo field1 = typeof(Card).GetField(prop1.ToString().ToLower());
                FieldInfo field2 = typeof(Card).GetField(prop2.ToString().ToLower());

                
                int card1Value = (int) field1.GetValue(card);
                int card2Value = (int) field2.GetValue(card);

                return card1Value > card2Value ? prop1 : prop2;
            });
        }


        private Property getRandomProperty() {
            return properties[random.Next(0, properties.Length)];
        }
    }
}
