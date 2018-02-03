using NUnit.Framework;
using PokerHand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.PokerHand {
    [TestFixture]
    public class PlayingCardTests {
        
        [TestCase('0', 'S', TestName = "PlayingCard_ThrowsInvalidCardExceptionWhenACardValueIsNotValid")]
        [TestCase('3', 'Z', TestName = "PlayingCard_ThrowsInvalidCardExceptionWhenACardSuitIsNotValid")]
        public void PlayingCard_ThrowsInvalidCardException(char cardValue, char cardSuit) {
            Assert.Throws<InvalidCardException>(() => new PlayingCard(cardValue, cardSuit));
        }

        [TestCase('2', PlayingCardValue.TWO, TestName = "PlayingCard_Converts2ToTwo")]
        [TestCase('3', PlayingCardValue.THREE, TestName = "PlayingCard_Converts3ToThree")]
        [TestCase('4', PlayingCardValue.FOUR, TestName = "PlayingCard_Converts4ToFour")]
        [TestCase('5', PlayingCardValue.FIVE, TestName = "PlayingCard_Converts5ToFive")]
        [TestCase('6', PlayingCardValue.SIX, TestName = "PlayingCard_Converts6ToSix")]
        [TestCase('7', PlayingCardValue.SEVEN, TestName = "PlayingCard_Converts7ToSeven")]
        [TestCase('8', PlayingCardValue.EIGHT, TestName = "PlayingCard_Converts8ToEight")]
        [TestCase('9', PlayingCardValue.NINE, TestName = "PlayingCard_Converts9ToNine")]
        [TestCase('T', PlayingCardValue.TEN, TestName = "PlayingCard_ConvertsTToTen")]
        [TestCase('J', PlayingCardValue.JACK, TestName = "PlayingCard_ConvertsJToJack")]
        [TestCase('Q', PlayingCardValue.QUEEN, TestName = "PlayingCard_ConvertsQToQueen")]
        [TestCase('K', PlayingCardValue.KING, TestName = "PlayingCard_ConvertsKToKing")]
        [TestCase('A', PlayingCardValue.ACE, TestName = "PlayingCard_ConvertsAToAce")]
        public void PlayingCard_ConvertsValueToEnum(char cardValue, PlayingCardValue expectedValue) {
            var playingCard = new PlayingCard(cardValue, 'C');

            Assert.AreEqual(expectedValue, playingCard.CardValue);
        }

        [TestCase('C', PlayingCardSuit.CLUBS, TestName = "PlayingCard_ConvertsCToClubs")]
        [TestCase('D', PlayingCardSuit.DIAMONDS, TestName = "PlayingCard_ConvertsDToDiamonds")]
        [TestCase('H', PlayingCardSuit.HEARTS, TestName = "PlayingCard_ConvertsHToHearts")]
        [TestCase('S', PlayingCardSuit.SPADES, TestName = "PlayingCard_ConvertsSToSpades")]
        public void PlayingCard_ConvertsSuitToEnum(char cardSuit, PlayingCardSuit expectedSuit) {
            var playingCard = new PlayingCard('2', cardSuit);

            Assert.AreEqual(expectedSuit, playingCard.CardSuit);
        }
    }
}
