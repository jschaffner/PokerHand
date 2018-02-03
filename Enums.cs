using System;

namespace PokerHand {
    public enum PlayingCardValue {
        TWO = 2,
        THREE = 3,
        FOUR = 4,
        FIVE = 5,
        SIX = 6,
        SEVEN = 7,
        EIGHT = 8,
        NINE = 9,
        TEN = 10,
        JACK = 11,
        QUEEN = 12,
        KING = 13,
        ACE = 14
    }

    public enum PlayingCardSuit {
        CLUBS = 'C',
        DIAMONDS = 'D',
        HEARTS = 'H',
        SPADES = 'S'
    }

    public enum PokerHandRank {
        HIGH_CARD = 1,
        ONE_PAIR = 2,
        TWO_PAIR = 3,
        THREE_OF_A_KIND = 4,
        STRAIGHT = 5,
        FLUSH = 6,
        FULL_HOUSE = 7,
        FOUR_OF_A_KIND = 8,
        STRAIGHT_FLUSH = 9
    }

    public static class EnumConverter {
        /// <summary>
        /// Converts an int to an enum or throws an InvalidEnumConversion exception if it can't convert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="intToConvert"></param>
        /// <returns></returns>
        /// <exception cref="InvalidEnumConversionException"></exception>
        public static T ToEnum<T>(int intToConvert) {
            if (Enum.IsDefined(typeof(T), intToConvert)) {
                return (T)Enum.ToObject(typeof(T), intToConvert);
            }

            throw new InvalidEnumConversionException();
        }

        /// <summary>
        /// Converts a char to an enum or throws an InvalidEnumConversion exception if it can't convert
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="charToConvert"></param>
        /// <returns></returns>
        /// <exception cref="InvalidEnumConversionException"></exception>
        public static T ToEnum<T>(char charToConvert) {
            int intValue = Convert.ToInt32(charToConvert);
            return ToEnum<T>(intValue);
        }
    }
}
