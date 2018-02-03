using System;

namespace PokerHand {
    public class Program {
        public static void Main(string[] args) {
            IGameEngine gameEngine = new GameEngine();
            var player1Name = GetPlayerName(1);
            var player2Name = GetPlayerName(2);

            Console.WriteLine();
            Console.WriteLine(Text.HandFormat);

            var player1Hand = GetPlayerHand(player1Name);
            var player2Hand = GetPlayerHand(player2Name);

            try {
                var player1 = new PokerPlayer(player1Name, player1Hand.ToUpper());
                var player2 = new PokerPlayer(player2Name, player2Hand.ToUpper());
                var result = gameEngine.ComparePlayerHands(player1, player2);
                if (result == null) {
                    Console.WriteLine(Text.Result_Tie);
                } else {
                    Console.WriteLine(string.Format(Text.Result_Winner, result.Name));
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }

        /// <summary>
        /// Prompts the user for the player's name
        /// </summary>
        /// <param name="playerNumber"></param>
        /// <returns></returns>
        private static string GetPlayerName(int playerNumber) {
            Console.Write(string.Format(Text.NamePrompt, playerNumber));
            var input = Console.ReadLine();

            return input;
        }

        /// <summary>
        /// Prompts the user for the player's hand
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns></returns>
        private static string GetPlayerHand(string playerName) {
            Console.Write(string.Format(Text.HandPrompt, playerName));
            var input = Console.ReadLine();

            return input;
        }
    }
}
