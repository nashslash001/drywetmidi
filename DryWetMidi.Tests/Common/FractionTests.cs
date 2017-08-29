﻿using Melanchall.DryWetMidi.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Melanchall.DryWetMidi.Tests.Common
{
    [TestClass]
    public class FractionTests
    {
        #region Test methods

        [TestMethod]
        [Description("Try parse valid fraction represented by numerator and denominator.")]
        public void TryParse_NumeratorAndDenominator()
        {
            TestTryParse("3/4", new Fraction(3, 4));
        }

        [TestMethod]
        [Description("Try parse valid fraction represented by numerator and divider.")]
        public void TryParse_NumeratorAndSlash()
        {
            TestTryParse("3/", new Fraction(3, 1));
        }

        [TestMethod]
        [Description("Try parse valid fraction represented by numerator only.")]
        public void TryParse_Numerator()
        {
            TestTryParse("3", new Fraction(3, 1));
        }

        [TestMethod]
        [Description("Try parse valid fraction represented by divider and denominator.")]
        public void TryParse_SlashAndDenominator()
        {
            TestTryParse("/4", new Fraction(1, 4));
        }

        [TestMethod]
        [Description("Parse valid fraction represented by numerator and denominator.")]
        public void Parse_NumeratorAndDenominator()
        {
            Assert.AreEqual(new Fraction(3, 4), Fraction.Parse("3/4"));
        }

        [TestMethod]
        [Description("Parse valid fraction represented by numerator and divider.")]
        public void Parse_NumeratorAndSlash()
        {
            Assert.AreEqual(new Fraction(3, 1), Fraction.Parse("3/"));
        }

        [TestMethod]
        [Description("Parse valid fraction represented by numerator only.")]
        public void Parse_Numerator()
        {
            Assert.AreEqual(new Fraction(3, 1), Fraction.Parse("3"));
        }

        [TestMethod]
        [Description("Parse valid fraction represented by divider and denominator.")]
        public void Parse_SlashAndDenominator()
        {
            Assert.AreEqual(new Fraction(1, 4), Fraction.Parse("/4"));
        }

        [TestMethod]
        [Description("Parse string representation of a fraction.")]
        public void Parse_ToString()
        {
            var expectedFraction = new Fraction(5, 17);
            Assert.AreEqual(expectedFraction, Fraction.Parse(expectedFraction.ToString()));
        }

        [TestMethod]
        [Description("Parse null input string.")]
        public void Parse_Invalid_NullString()
        {
            Assert.ThrowsException<ArgumentException>(() => Fraction.Parse(null));
        }

        [TestMethod]
        [Description("Parse input string with numerator is out of range.")]
        public void Parse_Invalid_NumeratorIsOutOfRange()
        {
            Assert.ThrowsException<FormatException>(() => Fraction.Parse("9223372036854775808/2"));
        }

        [TestMethod]
        [Description("Parse input string with denominator is out of range.")]
        public void Parse_Invalid_DenominatorIsOutOfRange()
        {
            Assert.ThrowsException<FormatException>(() => Fraction.Parse("1/9223372036854775808"));
        }

        #endregion

        #region Private methods

        private static void TestTryParse(string input, Fraction expectedFraction)
        {
            Fraction.TryParse(input, out var actualFraction);
            Assert.AreEqual(expectedFraction, actualFraction);
        }

        #endregion
    }
}