using System;
using System.Collections.Generic;
using System.Text;

namespace Predication.Clandro
{
    /// <summary>
    /// Processes a string which contains the expression to be evaluated and returns a sequence of tokens.
    /// </summary>
    public class Tokeniser
    {
        public IEnumerable<Token> Tokenise(string expression)
        {
            string expressionToTokenise = PreProcessExpression(expression);
            if (string.IsNullOrEmpty(expressionToTokenise))
                expressionToTokenise = expression;
            IEnumerable<Token> tokens = CoreTokeniseMethod(expressionToTokenise);
            IEnumerable<Token> postProcessedTokens = PostProcessTokens(expressionToTokenise, tokens);
            return postProcessedTokens == null ? tokens : postProcessedTokens;
        }

        #region private Methods

        private IEnumerable<Token> CoreTokeniseMethod(string expression)
        {
            List<Token> tokens = new List<Token>();
            if (!string.IsNullOrEmpty(expression))
            {
                /*
                what are the things we will detect as core tokens?
                WHITESPACE spaces, tabs, ...
                STRING e.g "hello Mr. O'Neil", "Who is the ""Special"" One?", "I am \"Delighted\"!"
                BRA e.g. (
                KET e.g. ) - with apologies to Professor Dirac!
                VARIABLE ab23, _apologies, 
                OPERATOR + - * / ^ % == != > < <= >= || && !  
                ?? DO WE ALLOW ++ -- = etc...
                FUNCTION Sin Cos Avg - which would be followed by (
                COMMA ,
                INTEGER -2, 345
                DECIMAL 0.000345 -345.034
                NEWLINE \n 
                UNKNOWN (anything else!) (unclosed strings, TAB, other crap)
                */
                int lineCount = 1;
                int linePosition = 1;
                foreach (char c in expression)
                {
                    if (c == ' ')
                    {
                        tokens.Add(new Token(lineCount, linePosition, " ", "WHITESPACE", true));
                        linePosition++;
                    }
                }
            }
            return tokens;
        }

        #endregion

        #region virtual Methods

        protected virtual string PreProcessExpression(string expression)
        {
            return null;
        }

        protected virtual IEnumerable<Token> PostProcessTokens(string expression, IEnumerable<Token> tokens)
        {
            return null;
        }

        #endregion
    }
}
