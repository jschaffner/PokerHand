using NUnit.Framework;
using PokerHand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.PokerHand {
    [TestFixture]
    public class GameEngineTests {
        [TestCase("2C 3C 4C 5C 6C", "7C 7D 7H 7S 8C", "p1", TestName = "ComparePlayerHands_Player1StraightFlushBeatsFourOfAKind")]
        [TestCase("7C 7D 7H 7S 8C", "2C 2D 2H 5C 5D", "p1", TestName = "ComparePlayerHands_Player1FourOfAKindBeatsFullHouse")]
        [TestCase("2C 2D 2H 5C 5D", "6C 8C TC QC KC", "p1", TestName = "ComparePlayerHands_Player1FullHouseBeatsFlush")]
        [TestCase("6C 8C TC QC KC", "2C 3C 4D 5C 6H", "p1", TestName = "ComparePlayerHands_Player1FlushBeatsStraight")]
        [TestCase("2C 3C 4D 5C 6H", "7C 7D 7H 2S 8C", "p1", TestName = "ComparePlayerHands_Player1StraightBeatsThreeOfAKind")]
        [TestCase("2C 2D 2S 5C 6H", "7C 7D 3H 3S 8C", "p1", TestName = "ComparePlayerHands_Player1ThreeOfAKindBeatsTwoPair")]
        [TestCase("7C 7D 3H 3S 8C", "9C 9D 3C 2S 8C", "p1", TestName = "ComparePlayerHands_Player1TwoPairBeatsOnePair")]
        [TestCase("7C 7D 4H 3S 8C", "9C QD 3C 2S 8C", "p1", TestName = "ComparePlayerHands_Player1OnePairBeatsHighCard")]
        [TestCase("7C 7D 7H 7S 8C", "2C 3C 4C 5C 6C", "p2", TestName = "ComparePlayerHands_Player2StraightFlushBeatsFourOfAKind")]
        [TestCase("2C 2D 2H 5C 5D", "7C 7D 7H 7S 8C", "p2", TestName = "ComparePlayerHands_Player2FourOfAKindBeatsFullHouse")]
        [TestCase("6C 8C TC QC KC", "2C 2D 2H 5C 5D", "p2", TestName = "ComparePlayerHands_Player2FullHouseBeatsFlush")]
        [TestCase("2C 3C 4D 5C 6H", "6C 8C TC QC KC", "p2", TestName = "ComparePlayerHands_Player2FlushBeatsStraight")]
        [TestCase("7C 7D 7H 2S 8C", "2C 3C 4D 5C 6H", "p2", TestName = "ComparePlayerHands_Player2StraightBeatsThreeOfAKind")]
        [TestCase("7C 7D 3H 3S 8C", "2C 2D 2S 5C 6H", "p2", TestName = "ComparePlayerHands_Player2ThreeOfAKindBeatsTwoPair")]
        [TestCase("9C 9D 3C 2S 8C", "7C 7D 3H 3S 8C", "p2", TestName = "ComparePlayerHands_Player2TwoPairBeatsOnePair")]
        [TestCase("9C QD 3C 2S 8C", "7C 7D 4H 3S 8C", "p2", TestName = "ComparePlayerHands_Player2OnePairBeatsHighCard")]
        public void ComparePlayerHands_PlayerWinsWithHigherRankHand(string player1Hand, string player2Hand, string expectedWinnerName) {
            var player1 = new PokerPlayer("p1", player1Hand);
            var player2 = new PokerPlayer("p2", player2Hand);

            var result = new GameEngine().ComparePlayerHands(player1, player2);

            Assert.AreEqual(expectedWinnerName, result.Name);
        }

        [TestCase("7C 8C 9C TC JC", "6S 7S 8S 9S TS", "p1", TestName = "ComparePlayerHands_Player1HasBetterStraightFlush")]
        [TestCase("7C 7D 7H 7S JC", "6S 6C 6H 6D TS", "p1", TestName = "ComparePlayerHands_Player1HasBetterFourOfAKind")]
        [TestCase("7C 7D 7H 2S 2C", "6S 6C 6H 3D 3H", "p1", TestName = "ComparePlayerHands_Player1HasBetterFullHouse")]
        [TestCase("7C 2C 9C 8C JC", "6S 7S 8S 9S 2S", "p1", TestName = "ComparePlayerHands_Player1HasBetterFlush")]
        [TestCase("7C 8D 9C TH JC", "6S 7H 8S 9C TS", "p1", TestName = "ComparePlayerHands_Player1HasBetterStraight")]
        [TestCase("7C 7D 7H 4S JC", "6S 6C 6H 3D TS", "p1", TestName = "ComparePlayerHands_Player1HasBetterThreeOfAKind")]
        [TestCase("7C 7D 5H 5S JC", "6S 6C 4H 4D TS", "p1", TestName = "ComparePlayerHands_Player1HasBetterTwoPair")]
        [TestCase("7C 7D 2H 5S JC", "6S 6C 4H 8D TS", "p1", TestName = "ComparePlayerHands_Player1HasBetterOnePair")]
        [TestCase("7C 4D 2H 5S JC", "6S 9C 4H 8D TS", "p1", TestName = "ComparePlayerHands_Player1HasBetterHighCard")]
        [TestCase("6S 7S 8S 9S TS", "7C 8C 9C TC JC", "p2", TestName = "ComparePlayerHands_Player2HasBetterStraightFlush")]
        [TestCase("6S 6C 6H 6D TS", "7C 7D 7H 7S JC", "p2", TestName = "ComparePlayerHands_Player2HasBetterFourOfAKind")]
        [TestCase("6S 6C 6H 3D 3H", "7C 7D 7H 2S 2C", "p2", TestName = "ComparePlayerHands_Player2HasBetterFullHouse")]
        [TestCase("6S 7S 8S 9S 2S", "7C 2C 9C 8C JC", "p2", TestName = "ComparePlayerHands_Player2HasBetterFlush")]
        [TestCase("6S 7H 8S 9C TS", "7C 8D 9C TH JC", "p2", TestName = "ComparePlayerHands_Player2HasBetterStraight")]
        [TestCase("6S 6C 6H 3D TS", "7C 7D 7H 4S JC", "p2", TestName = "ComparePlayerHands_Player2HasBetterThreeOfAKind")]
        [TestCase("6S 6C 4H 4D TS", "7C 7D 5H 5S JC", "p2", TestName = "ComparePlayerHands_Player2HasBetterTwoPair")]
        [TestCase("6S 6C 4H 8D TS", "7C 7D 2H 5S JC", "p2", TestName = "ComparePlayerHands_Player2HasBetterOnePair")]
        [TestCase("6S 9C 4H 8D TS", "7C 4D 2H 5S JC", "p2", TestName = "ComparePlayerHands_Player2HasBetterHighCard")]
        public void ComparePlayerHands_PlayerWinsWithHigherValueCardInSameRankHand(string player1Hand, string player2Hand, string expectedWinnerName) {
            var player1 = new PokerPlayer("p1", player1Hand);
            var player2 = new PokerPlayer("p2", player2Hand);

            var result = new GameEngine().ComparePlayerHands(player1, player2);

            Assert.AreEqual(expectedWinnerName, result.Name);
        }

        [TestCase("JC TD 8H 5S 4C", "TS 8C 5H 4D 3S", "p1", TestName = "ComparePlayerHands_Player1HasBetterFirstHighCard")]
        [TestCase("JC TD 8H 5S 4C", "JS 8C 5H 4D 3S", "p1", TestName = "ComparePlayerHands_Player1HasBetterSecondHighCard")]
        [TestCase("JC TD 8H 5S 4C", "JS TC 5H 4D 3S", "p1", TestName = "ComparePlayerHands_Player1HasBetterThirdHighCard")]
        [TestCase("JC TD 8H 5S 4C", "JS TC 8C 4D 3S", "p1", TestName = "ComparePlayerHands_Player1HasBetterFourthHighCard")]
        [TestCase("JC TD 8H 5S 4C", "JS TC 8C 5D 3S", "p1", TestName = "ComparePlayerHands_Player1HasBetterFifthHighCard")]
        [TestCase("TS 8C 5H 4D 3S", "JC TD 8H 5S 4C", "p2", TestName = "ComparePlayerHands_Player2HasBetterFirstHighCard")]
        [TestCase("JS 8C 5H 4D 3S", "JC TD 8H 5S 4C", "p2", TestName = "ComparePlayerHands_Player2HasBetterSecondHighCard")]
        [TestCase("JS TC 5H 4D 3S", "JC TD 8H 5S 4C", "p2", TestName = "ComparePlayerHands_Player2HasBetterThirdHighCard")]
        [TestCase("JS TC 8C 4D 3S", "JC TD 8H 5S 4C", "p2", TestName = "ComparePlayerHands_Player2HasBetterFourthHighCard")]
        [TestCase("JS TC 8C 5D 3S", "JC TD 8H 5S 4C", "p2", TestName = "ComparePlayerHands_Player2HasBetterFifthHighCard")]
        public void ComparePlayerHands_CheckHighCards(string player1Hand, string player2Hand, string expectedWinnerName) {
            var player1 = new PokerPlayer("p1", player1Hand);
            var player2 = new PokerPlayer("p2", player2Hand);

            var result = new GameEngine().ComparePlayerHands(player1, player2);

            Assert.AreEqual(expectedWinnerName, result.Name);
        }

        [TestCase("2S 3S 4S 5S 6S", "2C 3C 4C 5C 6C", TestName = "ComparePlayerHands_EqualStraightFlushReturnsNoWinner")]
        [TestCase("2S 3S 4S 5S 7S", "2C 3C 4C 5C 7C", TestName = "ComparePlayerHands_EqualFlushReturnsNoWinner")]
        [TestCase("2S 3S 4S 5D 6S", "2C 3C 4C 5H 6C", TestName = "ComparePlayerHands_EqualStraightReturnsNoWinner")]
        [TestCase("2S 2C 4S 4D 6S", "2D 2H 4C 4H 6C", TestName = "ComparePlayerHands_EqualTwoPairReturnsNoWinner")]
        [TestCase("2S 2C TS 4D 6S", "2D 2H TC 4H 6C", TestName = "ComparePlayerHands_EqualOnePairReturnsNoWinner")]
        [TestCase("2S QC TS 4D 6S", "2D QH TC 4H 6C", TestName = "ComparePlayerHands_EqualHighCardReturnsNoWinner")]
        public void ComparePlayerHands_EqualHandsReturnNoWinner(string player1Hand, string player2Hand) {
            var player1 = new PokerPlayer("p1", player1Hand);
            var player2 = new PokerPlayer("p2", player2Hand);

            var result = new GameEngine().ComparePlayerHands(player1, player2);

            Assert.IsNull(result);
        }
    }
}
