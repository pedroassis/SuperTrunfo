using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SuperTrunfo
{
    class TurnService{

        private GameObserver            gameObserver = Container.get<GameObserver>();

        public  List<Player>            players;

        public  Player                  currentPlayer;

        public  Property                currentProperty;

        private int                     currentHand         = 1;

        public  GameState               gameState           = GameState.NOT_STARTED;

        public Boolean                  isMaster;

        private WebSocketService        socket;

        public  Dictionary<Card, Player>cardsOnTable        = new Dictionary<Card, Player>();

        private static readonly String  SUPER_TRUMPH        = "A1";

        private static readonly int     MAX_PLAYERS         = 4;

        private static readonly int     MIN_PLAYERS         = 2;

        private static readonly String[]HEADS              = new String[] {    "B1", "C1", "D1", "E1"    };

        public TurnService() {

            socket = Container.get<WebSocketService>();
        }

        public Player nextPlayer{
            get {
                return players.IndexOf(currentPlayer) == players.Count - 1 ? players[0] : players[players.IndexOf(currentPlayer)+1]; 
            }
        }

        public void addPlayer(Player player) { 
            if(players.Count == MAX_PLAYERS){
                throw new InvalidOperationException("Max players on turn already reached.\nWhat's That?");
            }
            if(players.Contains(player)){
                throw new InvalidOperationException("Player already playing.\nWhat's That?");
            }

            players.Add(player);

        }

        public void startGame() { 
            if(GameState.NOT_STARTED != gameState){
                throw new InvalidOperationException("Already started the game.\nWhat's That?");
            } 
            if(players.Count < MIN_PLAYERS){
                throw new InvalidOperationException("Not enough players on turn.\nWhat's That?");
            }

            gameState = GameState.PLAYING;

        }

        public void play(Play play){

            Player player = play.player;
            Card chosenCard = play.selectedCard;

            this.play(chosenCard, player);
        }

        public void play(Card chosenCard, Player player) {

            if (currentPlayer != player){
                throw new InvalidOperationException("That's not the current player.\nWhat's That?");
            }

            if (currentPlayer.cards.IndexOf(chosenCard) == -1){
                throw new InvalidOperationException("Current Player does not have that card.\nWhat's That?");
            }

            if (cardsOnTable.Values.Contains(currentPlayer)) {
                throw new InvalidOperationException("Current Player already chose his card for this turn.\nWhat's That?");
            }

            if (isTrumph(chosenCard)) {
                gameObserver.trigger(Events.TRUMPH_ON_GAME, player);
            }

            if (isMaster && player.playerType == PlayerType.NPC) {
                socket.sendMessage<Player>(new Message<Player>(
                    "Play",
                    player,
                    player.GetType().FullName
                ));
            }

            cardsOnTable[chosenCard] = currentPlayer;

            currentPlayer = nextPlayer;

            gameObserver.trigger(Events.NEXT_PLAYERS_TURN, currentPlayer);

            currentHand++;

            if(currentHand > 4){
                endTurn();
            }

        }

        public void endTurn() {

            List<Card> cards = cardsOnTable.Keys.ToList();

            Card winner = getBiggest(cards, currentProperty);

            Player turnWinner = cardsOnTable[winner];

            cards.Remove(winner);

            turnWinner.cards.AddRange(cards);

            cards.ForEach((card) => {
                cardsOnTable[card].cards.Remove(card);
            });

            currentPlayer = turnWinner;

            currentHand = 0;

            currentProperty = Property.NONE;

            gameObserver.trigger(Events.TURN_WINNER, turnWinner);

            gameObserver.trigger(Events.END_TURN   , this);
        }

        public void selectProperty(Property property, Player player) {
            if (player == currentPlayer && currentProperty == Property.NONE) {
                currentProperty = property;
            }
            else {
                throw new InvalidOperationException("Invalid player or property already assined or both.");
            }
        }

        public Card getBiggest(List<Card> cards, Property selectedProp) {
            FieldInfo field = typeof(Card).GetField(selectedProp.ToString().ToLower());

            Func<Card, Card, Card> testBigger = (card1, card2) => {
                return ((int) field.GetValue(card1)) > ((int) field.GetValue(card2)) ? card1 : card2;
            };

            return cards.Aggregate((card1, card2) => {

                if (isTrumph(card1) && isHead(card2) || isTrumph(card2) && isHead(card1)
                    || !isTrumph(card1) && !isTrumph(card2)) {
                        return testBigger(card1, card2);
                }

                return isTrumph(card1) ? card1 : card2;
            });
        }

        public Boolean isTrumph(Card card) {
            return card.id.Equals(SUPER_TRUMPH);
        }

        public Boolean isHead(Card card) {
            return Array.IndexOf(HEADS, card) != -1;
        }

    }

}
