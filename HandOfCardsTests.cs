using NUnit.Framework;
using PokerHand;
using System.Linq;
using System.Collections.Generic;

namespace UnitTest.PokerHand {
    [TestFixture]
    public class HandOfCardsTests {
        [Test]
        public void HandOfCards_ConvertsStringToListOfPlayingCard() {
            var hand = "2C TD 3S 7H AC";
            var expected = new List<PlayingCard> {
                new PlayingCard('2', 'C'),
                new PlayingCard('3', 'S'),
                new PlayingCard('7', 'H'),
                new PlayingCard('T', 'D'),
                new PlayingCard('A', 'C')
            }.OrderByDescending(o => o.CardValue).ToList();

            var result = new HandOfCards(hand).Cards;

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(expected[0].CardValue, result[0].CardValue);
            Assert.AreEqual(expected[0].CardSuit, result[0].CardSuit);
            Assert.AreEqual(expected[1].CardValue, result[1].CardValue);
            Assert.AreEqual(expected[1].CardSuit, result[1].CardSuit);
            Assert.AreEqual(expected[2].CardValue, result[2].CardValue);
            Assert.AreEqual(expected[2].CardSuit, result[2].CardSuit);
            Assert.AreEqual(expected[3].CardValue, result[3].CardValue);
            Assert.AreEqual(expected[3].CardSuit, result[3].CardSuit);
            Assert.AreEqual(expected[4].CardValue, result[4].CardValue);
            Assert.AreEqual(expected[4].CardSuit, result[4].CardSuit);
        }

        [TestCase("2C TD 3S 7H", TestName = "HandOfCards_ThrowsInvalidHandExceptionWhenHandHasLessThan5Cards")]
        [TestCase("2C TD 3S 7H AC QH", TestName = "HandOfCards_ThrowsInvalidHandExceptionWhenHandHasMoreThan5Cards")]
        [TestCase(null, TestName = "HandOfCards_ThrowsInvalidHandExceptionWhenHandIsNull")]
        [TestCase("", TestName = "HandOfCards_ThrowsInvalidHandExceptionWhenHandIsEmpty")]
        public void HandOfCards_ThrowsInvalidHandException(string hand) {
            Assert.Throws<InvalidHandException>(() => new HandOfCards(hand));
        }

        [TestCase("2C TD 3 7H AC", TestName = "HandOfCards_ThrowsInvalidCardExceptionWhenACardIsLessThan2CharactersLong")]
        [TestCase("2C TD 13S 7H AC", TestName = "HandOfCards_ThrowsInvalidCardExceptionWhenACardIsMoreThan2CharactersLong")]
        public void HandOfCards_ThrowsInvalidCardException(string hand) {
            Assert.Throws<InvalidCardException>(() => new HandOfCards(hand));
        }

        [TestCase("9C QD 3C 2S 8C", PokerHandRank.HIGH_CARD, TestName = "HandOfCardsSetsPokerHandRankToHighCard")]
        [TestCase("9C 9D 3C 2S 8C", PokerHandRank.ONE_PAIR, TestName = "HandOfCardsSetsPokerHandRankToOnePair")]
        [TestCase("7C 7D 3H 3S 8C", PokerHandRank.TWO_PAIR, TestName = "HandOfCardsSetsPokerHandRankToTwoPair")]
        [TestCase("7C 7D 7H 2S 8C", PokerHandRank.THREE_OF_A_KIND, TestName = "HandOfCardsSetsPokerHandRankToThreeOfAKind")]
        [TestCase("2C 3C 4D 5C 6H", PokerHandRank.STRAIGHT, TestName = "HandOfCardsSetsPokerHandRankToStraight")]
        [TestCase("6C 8C TC QC KC", PokerHandRank.FLUSH, TestName = "HandOfCardsSetsPokerHandRankToFlush")]
        [TestCase("2C 2D 2H 5C 5D", PokerHandRank.FULL_HOUSE, TestName = "HandOfCardsSetsPokerHandRankToFullHouse")]
        [TestCase("7C 7D 7H 7S 8C", PokerHandRank.FOUR_OF_A_KIND, TestName = "HandOfCardsSetsPokerHandRankToFourOfAKind")]
        [TestCase("2C 3C 4C 5C 6C", PokerHandRank.STRAIGHT_FLUSH, TestName = "HandOfCardsSetsPokerHandRankToStraightFlush")]
        public void HandOfCards_SetsPokerHandRank(string hand, PokerHandRank expectedPokerHandRank) {
            var handOfCards = new HandOfCards(hand);

            Assert.AreEqual(expectedPokerHandRank, handOfCards.PokerHandRank);
        }
    }
}
