
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParserCode
{
    class Program
    {
        static void Main(string[] args)
        {
            InputCode f = new InputCode();
            f.ReadFile("Test.txt");
            string s = f.codeString;
            Console.WriteLine("-------Code--------");
            GetStringsCode g = new GetStringsCode();
            g.shareString(s);
            g.Show();
            Console.WriteLine("-------Description code--------");
            CodeDescription codeDescription = new CodeDescription(g.linesProgram);
            codeDescription.LineReading();
            codeDescription.ShowCode();
            OutputCodDescription outCode = new OutputCodDescription(codeDescription.parsedCode);
            outCode.WriteInFile();
        }
    }
}
