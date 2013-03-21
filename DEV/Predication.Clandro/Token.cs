using System;
using System.Collections.Generic;
using System.Text;

namespace Predication.Clandro
{
    /// <summary>
    /// 
    /// </summary>
    public class Token
    {
        public int ColumnNumber { get; private set; }
        public int LineNumber { get; private set; }
        public string Text { get; private set; }
        public string Type { get; private set; }
        public bool IsWhiteSpace { get; private set; }
    }
}
