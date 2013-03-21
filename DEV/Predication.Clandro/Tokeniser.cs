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
