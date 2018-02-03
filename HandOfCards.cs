using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHand {
    public class HandOfCards {
        public List<PlayingCard> Cards { get; private set; }
        public PokerHandRank PokerHandRank { get; private set; }
        private bool IsStraight => Cards.First().CardValue - Cards.Last().CardValue == 4;
        private bool IsFlush => Cards.GroupBy(o => o.CardSuit).Count() == 1;
        private bool HasThreeOfAKind => Cards.GroupBy(card => card.CardValue).Any(group => group.Count() == 3);
        private bool HasPair => Cards.GroupBy(card => card.CardValue).Any(group => group.Count() == 2);
        private bool HasTwoPair => Cards.GroupBy(card => card.CardValue).Count(group => group.Count() == 2) == 2;
        private bool HasFourOfAKind => Cards.GroupBy(card => card.CardValue).Any(group => group.Count() == 4);

        public HandOfCards(string hand) {
            if (string.IsNullOrWhiteSpace(hand)) {
                throw new InvalidHandException(Text.Exception_InvalidHand_IncorrectNumberOfCards);
            }

            var cardList = hand.Split();
            Cards = new List<PlayingCard>();

            foreach (var card in cardList) {
                if (card.Length != 2) {
                    throw new InvalidCardException(string.Format(Text.Exception_InvalidCard, card));
                }
                Cards.Add(new PlayingCard(card[0], card[1]));
            }
            
            if (Cards.Count != 5) {
                throw new InvalidHandException(Text.Exception_InvalidHand_IncorrectNumberOfCards);
            }
            Cards = Cards.OrderByDescending(o => o.CardValue).ToList();
            PokerHandRank = SetPokerHandRank();
        }

        /// <summary>
        /// Determines the rank of the poker hand
        /// </summary>
        /// <param name="pokerHand"></param>
        /// <returns></returns>
        private PokerHandRank SetPokerHandRank() {
            if (IsFlush && IsStraight) {
                return PokerHandRank.STRAIGHT_FLUSH;
            }
            if (HasFourOfAKind) {
                return PokerHandRank.FOUR_OF_A_KIND;
            }
            if (HasPair && HasThreeOfAKind) {
                return PokerHandRank.FULL_HOUSE;
            }
            if (IsFlush) {
                return PokerHandRank.FLUSH;
            }
            if (IsStraight) {
                return PokerHandRank.STRAIGHT;
            }
            if (HasThreeOfAKind && !HasPair) {
                return PokerHandRank.THREE_OF_A_KIND;
            }
            if (HasTwoPair) {
                return PokerHandRank.TWO_PAIR;
            }
            if (HasPair && !HasThreeOfAKind) {
                return PokerHandRank.ONE_PAIR;
            }

            return PokerHandRank.HIGH_CARD;
        }
    }
}
