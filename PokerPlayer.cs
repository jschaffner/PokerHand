namespace PokerHand {
    public class PokerPlayer {
        public string Name { get; private set; }
        public HandOfCards HandOfCards { get; private set; }

        public PokerPlayer(string name, string hand) {
            Name = name;
            HandOfCards = new HandOfCards(hand);
        }
    }
}
