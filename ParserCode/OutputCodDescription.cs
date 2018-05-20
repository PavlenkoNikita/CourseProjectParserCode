using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserCode
{
    class OutputCodDescription
    {
        public List<ParsedStr> DescriptCode { private set; get; }

        public OutputCodDescription(List<ParsedStr> Dcode)
        {
            this.DescriptCode = Dcode;
        }

        public void WriteInFile()
        {
            for (int i = 0; i < DescriptCode.Count; i++)
            {

            }
        }
    }
}
