using System;

namespace PokerHand {
    public class PlayingCard {
        public PlayingCardValue CardValue { get; private set; }
        public PlayingCardSuit CardSuit { get; private set; }

        public PlayingCard(char cardValue, char cardSuit) {
            CardValue = CreateCardValue(cardValue);
            CardSuit = CreateCardSuit(cardSuit);
        }

        /// <summary>
        /// Creates a PlayingCardValue out of a value character
        /// </summary>
        /// <param name="cardValue"></param>
        /// <returns></returns>
        private PlayingCardValue CreateCardValue(char cardValue) {
            PlayingCardValue playingCardValue;

            switch (cardValue) {
                case 'T':
                    playingCardValue = PlayingCardValue.TEN;
                    break;
                case 'J':
                    playingCardValue = PlayingCardValue.JACK;
                    break;
                case 'Q':
                    playingCardValue = PlayingCardValue.QUEEN;
                    break;
                case 'K':
                    playingCardValue = PlayingCardValue.KING;
                    break;
                case 'A':
                    playingCardValue = PlayingCardValue.ACE;
                    break;
                default:
                    try {
                        playingCardValue = EnumConverter.ToEnum<PlayingCardValue>((int)char.GetNumericValue(cardValue));
                    } catch (InvalidEnumConversionException) {
                        throw new InvalidCardException(string.Format(Text.Exception_InvalidCard_Value, $"{cardValue}"));
                    }
                    break;
            }

            return playingCardValue;
        }

        /// <summary>
        /// Creates a PlayingCardSuit out of a suit character
        /// </summary>
        /// <param name="cardSuit"></param>
        /// <returns></returns>
        private PlayingCardSuit CreateCardSuit(char cardSuit) {
            PlayingCardSuit playingCardSuit;

            try {
                playingCardSuit = EnumConverter.ToEnum<PlayingCardSuit>(cardSuit);
            } catch (InvalidEnumConversionException) {
                throw new InvalidCardException(string.Format(Text.Exception_InvalidCard_Suit, $"{cardSuit}"));
            }

            return playingCardSuit;
        }
    }
}
