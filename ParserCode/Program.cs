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
            //string codeProgramString = "private void Method() { do{ if(a == false) int a = 0; Object.Object2.Method(); }while(File != b && k == 0); var endSentence = new Regex(@\"[_A-Za-z0-9{ }]\");      Console.WriteLine(programStrings[i]);   List<string> programStrings = new List<string>();int p = Math.Cosinus(); for (int i = 0; i < n; i++ ) { File.TXT.Method(file.count); for (int j = 0; j < 3; j++) int b = 7;} while(i < programStrings.Count) Console.WriteLine(\"Hello\"); int k = 0; } public void Show() {for (int i = 0; i < 10; i++) {Console.WriteLine(\"Line\" + i);}}";
            //string codeProgramString = "public void Main(string[] args) { switch(a) { case 5:  Console.WriteLine(5); break; int a; a =6; case 4: { Console.WriteLine(4); break;} default:{ Console.WriteLine(7); break;} } }";
            //string codeProgramString = "public void Main(string[] args) { do { return; if (a == true) return false; else if (b == false) Console.WriteLine(\"\"); else { Object.Max();} }while(max =< 9);}";
            //string codeProgramString = "class A { int cube(int x) => x * x * x; private void Method() { do{ if(a == false) int a = 0; Object.Object2.Method(); }while(File != b && k == 0); var endSentence = new Regex(@\"[_A-Za-z0-9{ }]\");      Console.WriteLine(programStrings[i]);   List<string> programStrings = new List<string>();int p = Math.Cosinus(); for (int i = 0; i < n; i++ ) { File.TXT.Method(file.count); for (int j = 0; j < 3; j++) int b = 7;} while(i < programStrings.Count) Console.WriteLine(\"Hello\"); int k = 0; } public void Show() {for (int i = 0; i < 10; i++) {Console.WriteLine(\"Line\" + i);}}}";
            //string codeProgramString = "class Example {     public static void Main()  {   int a;  Console.WriteLine(\"allo\"); Console.WriteLine(5); Console.ReadLine(a) Console.Read(\"switch\"); Object.Find(asdfas); }  public void Main(int a, int g)  {   int a = 6, b = 8;  Object.Find(asdfas); }}";
            //string codeProgramString = "Console.WriteLine($\"The sum of { values.Count } die is { DiceLibrary.DiceSum(values) }\");  } }";
            //string codeProgramString = "using (StreamReader sr = new StreamReader(readPath, System.Text.Encoding.Default))  {  text = sr.ReadToEnd();   } ";
            //string codeProgramString = "public double    Value () { int k = 8; if (true == true) for (int j = 0; i < 5; j++)  for (int k = 0; k < 5; k++) Console.WriteLine(\"\"); Object.Method.File(\"asdfasf\");}";
            //string codeProgramString = "private void CheckAndCommentMethodConstruction(ref int index) {  callsConstructions.Push(\"METHOD\");  Brackets.Push(true);  countStrAfterConstruction.Push(0); parsedCode.Add(\"START METHOD |\" + stringMethod.Match(programStrings[index]).Groups[\"ReturnTypeMethod\"] +\" \" + stringMethod.Match(programStrings[index]).Groups[\"MethodName\"] + \"(\" + stringMethod.Match(programStrings[index]).Groups[\"Params\"] + \")\"); index++;}  private void CheckAndCommentDWhileConstruction() {  callsConstructions.Push(\"DWHILE\");  Brackets.Push(true);int countStr = countStrAfterConstruction.Pop();  countStrAfterConstruction.Push(++countStr);countStrAfterConstruction.Push(0);  callCycle.Push(\"DWHILE\"); parsedCode.Add(\"START DO |\");  }";

            //switch(5)
            //{
            //    case 1:
            //        {
            //            break;
            //        }
            //    default:
            //        {
            //            goto L1;
            //            break;
            //            L1:
            //            break;
            //        }
            //}

            //Regex stringForeach = new Regex(@"^foreach\s?\(\s?(?<TypeVariable>[_A-Za-z0-9]+)\s(?<NameVariable>[_A-Za-z0-9]+) in (?<Collection>[_A-Za-z0-9\[\]]+)\s?\)$");
            //Console.WriteLine(stringForeach.IsMatch("foreach(int a in massive)"));

            InputCode f = new InputCode("Test.txt");
            f.ReadFile();
            string s = f.codeString;
            Console.WriteLine("-------Code--------");
            GetStringsCode g = new GetStringsCode();
            g.shareString(s);
            g.Show();
            Console.WriteLine("-------Description code--------");
            CodeDescription codeDescription = new CodeDescription(g.linesProgram);
            codeDescription.LineReading();
            codeDescription.ShowCode();
        }
    }
}
