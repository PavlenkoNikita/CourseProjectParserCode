using System;
using System.Collections.Generic;
using System.IO;
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
                if (DescriptCode[i].Block == "START METHOD")
                {
                    if (DescriptCode[i + 1].Block == "END METHOD")
                    {
                        i++;
                        continue;
                    }
                    using (StreamWriter sw = new StreamWriter(DescriptCode[i].TextBlock + ".txt"))
                    {
                        for (; i < DescriptCode.Count; i++)
                        {
                            if (DescriptCode[i].Block == "END METHOD")
                            {
                                if (i + 1 != DescriptCode.Count)
                                {
                                    if (DescriptCode[i+1].Block == "START METHOD")
                                    {
                                        sw.WriteLine(DescriptCode[i].Block + "| " + DescriptCode[i].TextBlock);
                                        break;
                                    }
                                }
                                else
                                {
                                    sw.WriteLine(DescriptCode[i].Block + "| " + DescriptCode[i].TextBlock);
                                    break;
                                }
                            }
                            else
                            {
                                sw.WriteLine(DescriptCode[i].Block + "| " + DescriptCode[i].TextBlock);
                            }
                        }
                    }
                }
            }
        }
    }
}
