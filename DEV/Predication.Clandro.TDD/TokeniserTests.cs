using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Predication.Clandro.TDD
{
    /// <summary>
    /// Tests for the Tokeniser
    /// </summary>
    [TestClass]
    public class TokeniserTests
    {
        [TestMethod]
        public void Tokeniser_constructor()
        {
            Tokeniser tokeniser = new Tokeniser();
            Assert.IsNotNull(tokeniser);
        }

        [TestMethod]
        public void Tokeniser_Tokenise_null_string()
        {
            Tokeniser tokeniser = new Tokeniser();
            IEnumerable<Token> tokens = tokeniser.Tokenise(null);
            Assert.IsNotNull(tokens);
            Assert.AreEqual<int>(0, tokens.Count());
        }

        [TestMethod]
        public void Tokeniser_Tokenise_null_empty_string()
        {
            Tokeniser tokeniser = new Tokeniser();
            IEnumerable<Token> tokens = tokeniser.Tokenise("");
            Assert.IsNotNull(tokens);
            Assert.AreEqual<int>(0, tokens.Count());
        }

        [TestMethod]
        public void Tokeniser_Tokenise_null_one_space()
        {
            Tokeniser tokeniser = new Tokeniser();
            IEnumerable<Token> tokens = tokeniser.Tokenise(" ");
            Assert.IsNotNull(tokens);
            Assert.AreEqual<int>(1, tokens.Count());
        }

    }
}
