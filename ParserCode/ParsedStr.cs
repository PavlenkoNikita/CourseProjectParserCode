using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserCode
{
    class ParsedStr
    {
        public string TextBlock { get; private set; }
        public string Block { get; private set; }

        public ParsedStr(string Block, string TextBlock)
        {
            this.TextBlock = TextBlock;
            this.Block = Block;
        }

        public void Show()
        {
            Console.WriteLine(Block + "| " + TextBlock);
        }
    }
}
