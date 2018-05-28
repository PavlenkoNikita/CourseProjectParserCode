using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParserCode
{
    class OutputCodDescription
    {
        public List<ParsedStr> DescriptCode { private set; get; }

        public OutputCodDescription(List<ParsedStr> Dcode)
        {
            this.DescriptCode = Dcode;
        }

        public void WriteInFile(string path)
        {
            int countEmptyMethod = 0;
            for (int i = 0; i < DescriptCode.Count; i++)
            {
                if (DescriptCode[i].Block == "START METHOD")
                {
                    if (DescriptCode[i + 1].Block == "END METHOD")
                    {
                        countEmptyMethod++;
                        i++;
                        continue;
                    }
                    using (StreamWriter sw = new StreamWriter(path + "\\" + DescriptCode[i].TextBlock + ".txt"))
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
            if (countEmptyMethod == DescriptCode.Count/2)
            {
                throw new MyException("Файлы созданы не будут, т.к. все методы в файле пусты.");
            }
        }
    }
}
