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
        public void Tokeniser_Tokenise_empty_string()
        {
            Tokeniser tokeniser = new Tokeniser();
            IEnumerable<Token> tokens = tokeniser.Tokenise("");
            Assert.IsNotNull(tokens);
            Assert.AreEqual<int>(0, tokens.Count());
        }

        [TestMethod]
        public void Tokeniser_Tokenise_one_space()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = " ";
            List<Token> expected = new List<Token>
            {
                new Token("Whitespace", " ")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.AreEqual<int>(1, tokens.Count());
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }

        [TestMethod]
        public void Tokeniser_Tokenise_simple_addition_with_whitespace()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = " 3 + 4 ";
            List<Token> expected = new List<Token>
            {
                new Token("Whitespace", " ")
                , new Token("Integer", "3")
                , new Token("Whitespace", " ")
                , new Token("Operator", "+")
                , new Token("Whitespace", " ")
                , new Token("Integer", "4")
                , new Token("Whitespace", " ")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }

        [TestMethod]
        public void Tokeniser_Tokenise_three()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = "3";
            List<Token> expected = new List<Token>
            {
                new Token("Integer", "3")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }

        [TestMethod]
        public void Tokeniser_Tokenise_three_space_four()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = "3 4";
            List<Token> expected = new List<Token>
            {
                new Token("Integer", "3")
                , new Token("Whitespace", " ")
                , new Token("Integer", "4")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }
            
        [TestMethod]
        public void Tokeniser_Tokenise_hello_string()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = " \"hello\" "; ;
            List<Token> expected = new List<Token>
            {
                new Token("Whitespace", " ")
                , new Token("String", "hello")
                , new Token("Whitespace", " ")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }

        [TestMethod]
        public void Tokeniser_Tokenise_hello_World()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = " \"hello\" + \" World!\" ";
            List<Token> expected = new List<Token>
            {
                new Token("Whitespace", " ")
                , new Token("String", "hello")
                , new Token("Whitespace", " ")
                , new Token("Operator", "+")
                , new Token("Whitespace", " ")
                , new Token("String", " World!")
                , new Token("Whitespace", " ")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }

        [TestMethod]
        public void Tokeniser_Tokenise_3dp004_4()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = " 3.004 4 ";
            List<Token> expected = new List<Token>
            {
                new Token("Whitespace", " ")
                , new Token("Number", "3.004")
                , new Token("Whitespace", " ")
                , new Token("Integer", "4")
                , new Token("Whitespace", " ")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }


        [TestMethod]
        public void Tokeniser_Tokenise_345dp000dp34565()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = "345.000.34565";
            List<Token> expected = new List<Token>
            {
                new Token("Number", "345.000")
                , new Token("Number", ".34565")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }

        [TestMethod]
        public void Tokeniser_Tokenise_two_integers()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = " 33090 04023 ";
            List<Token> expected = new List<Token>
            {
                new Token("Whitespace", " ")
                , new Token("Integer", "33090")
                , new Token("Whitespace", " ")
                , new Token("Integer", "04023")
                , new Token("Whitespace", " ")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }

        [TestMethod]
        public void Tokeniser_Tokenise_33090()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = "33090";
            List<Token> expected = new List<Token>
            {
                new Token("Integer", "33090")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }

        [TestMethod]
        public void Tokeniser_Tokenise_space_33090()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = " 33090";
            List<Token> expected = new List<Token>
            {
                new Token("Whitespace", " ")
                , new Token("Integer", "33090")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }

        [TestMethod]
        public void Tokeniser_Tokenise_bra_33090_ket()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = "(33090)";
            List<Token> expected = new List<Token>
            {
                new Token("Bracket", "(")
                , new Token("Integer", "33090")
                , new Token("Bracket", ")")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }

        [TestMethod]
        public void Tokeniser_Tokenise_space_330_comma_90_comma_12()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = " 330,90,12 ";
            List<Token> expected = new List<Token>
            {
                new Token("Whitespace", " ")
                , new Token("Integer", "330")
                , new Token("Comma", ",")
                , new Token("Integer", "90")
                , new Token("Comma", ",")
                , new Token("Integer", "12")
                , new Token("Whitespace", " ")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }

        [TestMethod]
        public void Tokeniser_Tokenise_equation_001()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = "(3^4)/(2*5.5)";
            List<Token> expected = new List<Token>
            {
                new Token("Bracket", "(")
                , new Token("Integer", "3")
                , new Token("Operator", "^")
                , new Token("Integer", "4")
                , new Token("Bracket", ")")
                , new Token("Operator", "/")
                , new Token("Bracket", "(")
                , new Token("Integer", "2")
                , new Token("Operator", "*")
                , new Token("Decimal", "5.5")
                , new Token("Bracket", ")")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }

        [TestMethod]
        public void Tokeniser_Tokenise_equation_002()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = "a+bsd_234/_thisAPI2";
            List<Token> expected = new List<Token>
            {
                new Token("Identifier", "a")
                , new Token("Operator", "+")
                , new Token("Identifier", "bsd_234")
                , new Token("operator", "/")
                , new Token("Identifier", "_thisAPI2")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }

        [TestMethod]
        public void Tokeniser_Tokenise_equation_003()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = "a+b";
            List<Token> expected = new List<Token>
            {
                new Token("Identifier", "a")
                , new Token("Operator", "+")
                , new Token("Identifier", "b")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }

        [TestMethod]
        public void Tokeniser_Tokenise_equation_004()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = "(a+b)";
            List<Token> expected = new List<Token>
            {               
                new Token("Bracket", "(")
                , new Token("Identifier", "a")
                , new Token("Operator", "+")
                , new Token("Identifier", "b")
                , new Token("Bracket", ")")
                
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }



        [TestMethod]
        public void Tokeniser_Tokenise_identifiers_001()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = "ase sd345 _this0ne";
            List<Token> expected = new List<Token>
            {
                new Token("Identifier", "ase")
                , new Token("Whitespace", " ")
                , new Token("Identifier", "sd345a")
                , new Token("Whitespace", " ")
                , new Token("Identifier", "_this0ne")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }

        [TestMethod]
        public void Tokeniser_Tokenise_identifiers_002()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = "as123 this345 true0986";
            List<Token> expected = new List<Token>
            {
                new Token("Identifier", "as123")
                , new Token("Whitespace", " ")
                , new Token("Identifier", "this345")
                , new Token("Whitespace", " ")
                , new Token("Identifier", "true0987")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }

        [TestMethod]
        public void Tokeniser_Tokenise_simple_addition_without_whitespace()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = "3+4";
            List<Token> expected = new List<Token>
            {
                new Token("Integer", "3")
                , new Token("Operator", "+")
                , new Token("Integer", "4")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }

        [TestMethod]
        public void Tokeniser_Tokenise_simple_two_spaces()
        {
            Tokeniser tokeniser = new Tokeniser();
            string expr = "  ";
            List<Token> expected = new List<Token>
            {
                new Token("Whitespace", " ")
                , new Token("Whitespace", " ")
            };
            IEnumerable<Token> tokens = tokeniser.Tokenise(expr);
            Assert.IsNotNull(tokens);
            Assert.IsTrue(AreTheSame(expr, tokens, expected));
        }


        #region protected Methods

        protected void Show(string expression, IEnumerable<Token> tokens)
        {
            Console.WriteLine("expr: {0}", expression);
            Console.WriteLine("Tokens.Count = : {0}", tokens.Count());
            int count = 1;
            foreach(Token t in tokens)
                Console.WriteLine(string.Format("{0:000} {1} <{2}>", count++, t.Type, t.Text));
        }

        protected bool AreTheSame(string expression, IEnumerable<Token> tokens, List<Token> expectedTokens)
        {
            bool areTheSame = true;
            Console.WriteLine("expr: <[{0}]>", expression);
            int lenTokens = tokens.Count();
            int lenExpectedTokens = expectedTokens.Count();
            if (lenTokens != lenExpectedTokens)
                areTheSame = false;
            // we will attempt to show them side by side
            int maxLength = lenExpectedTokens < lenTokens ? lenTokens : lenExpectedTokens;
            List<Token> listTokens = tokens.ToList();
            for (int i = 0; i < maxLength; i++)
            {
                Console.WriteLine("{0:000} [", i);
                if (lenTokens > i)
                    Console.WriteLine(string.Format("  act: {0} <{1}>", listTokens[i].Type, listTokens[i].Text));
                else 
                    Console.WriteLine("  act: --- <--->");
                if (lenExpectedTokens > i)
                    Console.WriteLine(string.Format("  exp: {0} <{1}>", expectedTokens[i].Type, expectedTokens[i].Text));
                else
                    Console.WriteLine("  exp: --- <--->");
                if (lenTokens < i && lenExpectedTokens < i && areTheSame)
                {
                    areTheSame = listTokens[i].Text == expectedTokens[i].Text
                        && listTokens[i].Type == expectedTokens[i].Type;
                }
                Console.WriteLine("]", i);
            }
            return areTheSame;
        }



        #endregion

    }
}
