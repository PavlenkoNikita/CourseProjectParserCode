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
            string codeProgramString = "private void Method() { do{ if(a == false) int a = 0; Object.Object2.Method(); }while(File != b && k == 0); var endSentence = new Regex(@\"[_A-Za-z0-9{ }]\");      Console.WriteLine(programStrings[i]);   List<string> programStrings = new List<string>();int p = Math.Cosinus(); for (int i = 0; i < n; i++ ) { File.TXT.Method(file.count);} while(i < programStrings.Count) Console.WriteLine(\"Hello\"); int k = 0; } public void Show() {for (int i = 0; i < 10; i++) {Console.WriteLine(\"Line\" + i);}}";
            //string codeProgramString = "int a = 5; switch(a) { case 4: { Console.WriteLine(4); break; } case 5: { Console.WriteLine(5); break; } default:{ Console.WriteLine(7); break;}";
            //string codeProgramString = "public internal class A { struct C {}}";
            //string codeProgramString = "class A { int cube(int x) => x * x * x; }";
            //string codeProgramString = "class Example {     public static void Main()  {   int a;  Console.WriteLine(\"allo\"); Console.WriteLine(5); Console.ReadLine(a) Console.Read(\"switch\"); Object.Find(asdfas); }  public void Main(int a, int g)  {   int a = 6, b = 8;  Object.Find(asdfas); }}";
            //string codeProgramString = "Console.WriteLine($\"The sum of { values.Count } die is { DiceLibrary.DiceSum(values) }\");  } }";
            // string codeProgramString = "using (StreamReader sr = new StreamReader(readPath, System.Text.Encoding.Default))  {  text = sr.ReadToEnd();   } ";
            //string codeProgramString = "public double    Value () { int k = 8; if (true == true) for (int j = 0; i < 5; j++)  for (int k = 0; k < 5; k++) Console.WriteLine(\"\"); Object.Method.File(\"asdfasf\");}";
            Console.WriteLine("-------Code--------");
            GetStringsCode g = new GetStringsCode();
            g.shareString(ref codeProgramString);
            g.Show();
            Console.WriteLine("-------Description code--------");
            CodeDescription codeDescription = new CodeDescription(g.linesProgram);
            codeDescription.LineReading();
            codeDescription.showCode();
        }
    }
}
