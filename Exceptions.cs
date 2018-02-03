using System;

namespace PokerHand {
    public class InvalidCardException : Exception {
        public InvalidCardException() { }
        public InvalidCardException(string message) : base(message) { }
    }

    public class InvalidHandException : Exception {
        public InvalidHandException() { }
        public InvalidHandException(string message) : base(message) { }
    }

    public class InvalidEnumConversionException : Exception {
        public InvalidEnumConversionException() { }
        public InvalidEnumConversionException(string message) : base(message) { }
    }
}
