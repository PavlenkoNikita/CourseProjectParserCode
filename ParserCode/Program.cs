
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
            //Считывание кода с файла в List + удаление комментариев
            f.ReadFile("Test.txt");
            Console.WriteLine("-------Code--------");
            GetStringsCode g = new GetStringsCode();
            //Деление одной строки кода на подстроки кода
            g.shareString(f.codeString);
            g.Show();
            Console.WriteLine("-------Description code--------");
            CodeDescription codeDescription = new CodeDescription(g.linesProgram);
            //Разбор кода
            codeDescription.LineReading();
            codeDescription.ShowCode();
           
            OutputCodDescription outCode = new OutputCodDescription(codeDescription.parsedCode);
            //Формирование файлов с названием методов + записывание их содержимого
            outCode.WriteInFile();
        }
    }
}
