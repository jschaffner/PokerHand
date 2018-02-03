using System.Linq;

namespace PokerHand {
    public class GameEngine : IGameEngine {
        /// <summary>
        /// Compares the hands of two players and returns the winner or null if there is a tie.
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <returns></returns>
        public PokerPlayer ComparePlayerHands(PokerPlayer player1, PokerPlayer player2) {
            var player1Rank = player1.HandOfCards.PokerHandRank;
            var player2Rank = player2.HandOfCards.PokerHandRank;

            if (player1Rank > player2Rank) {
                return player1;
            } else if (player1Rank < player2Rank) {
                return player2;
            }

            // Both players have the same hand rank, need to check card values
            switch (player1Rank) {
                case PokerHandRank.HIGH_CARD:
                    return EvaluateHighCard(player1, player2);
                case PokerHandRank.ONE_PAIR:
                    return EvaluateOnePair(player1, player2);
                case PokerHandRank.TWO_PAIR:
                    return EvaluateTwoPair(player1, player2);
                case PokerHandRank.THREE_OF_A_KIND:
                    return EvaluateThreeOrFourOfAKind(player1, player2, 3);
                case PokerHandRank.STRAIGHT:
                    return EvaluateHighCard(player1, player2);
                case PokerHandRank.FLUSH:
                    return EvaluateHighCard(player1, player2);
                case PokerHandRank.FULL_HOUSE:
                    return EvaluateThreeOrFourOfAKind(player1, player2, 3);
                case PokerHandRank.FOUR_OF_A_KIND:
                    return EvaluateThreeOrFourOfAKind(player1, player2, 4);
                case PokerHandRank.STRAIGHT_FLUSH:
                    return EvaluateStraightFlush(player1, player2);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Checks which player has the best high card
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <returns></returns>
        private PokerPlayer EvaluateHighCard(PokerPlayer player1, PokerPlayer player2) {
            for (var i = 0; i < 5; i++) {
                if (player1.HandOfCards.Cards[i].CardValue > player2.HandOfCards.Cards[i].CardValue) {
                    return player1;
                } else if (player1.HandOfCards.Cards[i].CardValue < player2.HandOfCards.Cards[i].CardValue) {
                    return player2;
                }
            }
            return null;
        }

        /// <summary>
        /// Compare which hand with one pair is better
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <returns></returns>
        private PokerPlayer EvaluateOnePair(PokerPlayer player1, PokerPlayer player2) {
            var player1Value = player1.HandOfCards.Cards.GroupBy(o => o.CardValue).Where(group => group.Count() == 2).OrderByDescending(group => group.Count()).First().Key;
            var player2Value = player2.HandOfCards.Cards.GroupBy(o => o.CardValue).Where(group => group.Count() == 2).OrderByDescending(group => group.Count()).First().Key;

            // Check if one of the pairs is better
            if (player1Value > player2Value) {
                return player1;
            } else if (player1Value < player2Value) {
                return player2;
            }

            // Pairs were the same, have to check the high card
            return EvaluateHighCard(player1, player2);
        }

        /// <summary>
        /// Compare which four-of-a-kind has higher cards
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <returns></returns>
        private PokerPlayer EvaluateThreeOrFourOfAKind(PokerPlayer player1, PokerPlayer player2, int number) {
            var player1Value = player1.HandOfCards.Cards.GroupBy(o => o.CardValue).Where(group => group.Count() == number).OrderByDescending(group => group.Count()).First().Key;
            var player2Value = player2.HandOfCards.Cards.GroupBy(o => o.CardValue).Where(group => group.Count() == number).OrderByDescending(group => group.Count()).First().Key;

            if (player1Value > player2Value) {
                return player1;
            } else if (player1Value < player2Value) {
                return player2;
            }

            return null;
        }

        /// <summary>
        /// Compares which hand of two-pair is better
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <returns></returns>
        private PokerPlayer EvaluateTwoPair(PokerPlayer player1, PokerPlayer player2) {
            var player1CardsGrouped = player1.HandOfCards.Cards.GroupBy(o => o.CardValue).OrderByDescending(group => group.Count());
            var player1Pairs = player1CardsGrouped.Where(group => group.Count() == 2);
            var player1FirstPairValue = player1Pairs.First().Key;
            var player1SecondPairValue = player1Pairs.Last().Key;

            var player2CardsGrouped = player2.HandOfCards.Cards.GroupBy(o => o.CardValue).OrderByDescending(group => group.Count());
            var player2Pairs = player2CardsGrouped.Where(group => group.Count() == 2);
            var player2FirstPairValue = player2Pairs.First().Key;
            var player2SecondPairValue = player2Pairs.Last().Key;

            // Check if the high pairs are not equal
            if (player1FirstPairValue > player2FirstPairValue) {
                return player1;
            } else if (player1FirstPairValue < player2FirstPairValue) {
                return player2;
            }
            // The higher value pairs were equal, have to check the second pair
            if (player1SecondPairValue > player2SecondPairValue) {
                return player1;
            } else if (player1SecondPairValue < player2SecondPairValue) {
                return player2;
            }

            // The second pair were also equal, have to check the high card
            var player1HighCard = player1CardsGrouped.Where(group => group.Count() == 1).First().Key;
            var player2HighCard = player2CardsGrouped.Where(group => group.Count() == 1).First().Key;

            if (player1HighCard > player2HighCard) {
                return player1;
            } else if (player1HighCard < player2HighCard) {
                return player2;
            }

            // High card is the same so no winner
            return null;
        }

        /// <summary>
        /// Compare which straight flush has a higher card
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <returns></returns>
        private PokerPlayer EvaluateStraightFlush(PokerPlayer player1, PokerPlayer player2) {
            if (player1.HandOfCards.Cards.First().CardValue > player2.HandOfCards.Cards.First().CardValue) {
                return player1;
            } else if (player1.HandOfCards.Cards.First().CardValue < player2.HandOfCards.Cards.First().CardValue) {
                return player2;
            }
            return null;
        }
    }
}
