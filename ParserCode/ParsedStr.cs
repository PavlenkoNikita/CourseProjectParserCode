using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public void Show(ListBox lb)
        {
            lb.Items.Add(Block + "| " + TextBlock);
        }
    }
}
