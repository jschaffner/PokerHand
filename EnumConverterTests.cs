using System;
using PokerHand;
using NUnit.Framework;

namespace UnitTest.PokerHand {
    [TestFixture]
    public class EnumConverterTests {
        public enum TestIntEnum {
            TRUE = 1,
            FALSE = 0
        }

        public enum TestCharEnum {
            TRUE = 'T',
            FALSE = 'F'
        }

        [Test]
        public void ToEnum_ConvertsInt1ToTrue() {
            var result = EnumConverter.ToEnum<TestIntEnum>(1);

            Assert.AreEqual(TestIntEnum.TRUE, result);
        }

        [Test]
        public void ToEnum_ConvertsInt0ToFalse() {
            var result = EnumConverter.ToEnum<TestIntEnum>(0);

            Assert.AreEqual(TestIntEnum.FALSE, result);
        }

        [Test]
        public void ToEnum_ThrowsExceptionWhenIntIsNotValidEnum() {
            Assert.Throws<InvalidEnumConversionException>(() => EnumConverter.ToEnum<TestIntEnum>(3));
        }

        [Test]
        public void ToEnum_ConvertsCharTToTrue() {
            var result = EnumConverter.ToEnum<TestCharEnum>('T');

            Assert.AreEqual(TestCharEnum.TRUE, result);
        }

        [Test]
        public void ToEnum_ConvertsCharFToFalse() {
            var result = EnumConverter.ToEnum<TestCharEnum>('F');

            Assert.AreEqual(TestCharEnum.FALSE, result);
        }

        [Test]
        public void ToEnum_ThrowsExceptionWhenCharIsNotValidEnum() {
            Assert.Throws<InvalidEnumConversionException>(() => EnumConverter.ToEnum<TestCharEnum>('G'));
        }
    }
}
