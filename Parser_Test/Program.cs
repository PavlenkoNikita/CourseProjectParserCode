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

            //string s = "class A";
            //var stringClass = new Regex(@"^([a-z ]+ )*class [_A-Za-z0-9]+$");
            //Console.WriteLine("Class: " + stringClass.Match(s));

            var stringEndSentence = new Regex(@"[_A-Za-z0-9{}]");
            var stringDo = new Regex(@"^do$");
            var stringNamespace = new Regex(@"^namespace [_A-Za-z0-9]+$");
            var stringClass = new Regex(@"^([a-z ]+ )*class [_A-Za-z0-9]+$");
            var stringStruct = new Regex(@"^([a-z ]+ )*struct [_A-Za-z0-9]+$");
            var stringMethod = new Regex(@"^(static)*\s*(public|private|protected|internal|protected internal|private protected|)\s*(static)*\s[A-Za-z0-9\[\]_]+ [a-zA-Z0-9 _]+\([A-Za-z0-9, _\[\]]*\)$");
            var stringSwitch = new Regex(@"^switch([_A-Za-z0-9[] ]+)");
            var stringCase = new Regex(@"^case [_A-Za-z0-9[]]*\s*:");
            var stringDefault = new Regex(@"^default:");
            var stringElse = new Regex(@"^else");

            codeProgramString = Regex.Replace(codeProgramString, @"\s+", " ");
            List<string> programStrings = new List<string>();
            int countBrackets = 0;
            int countQuotes = 0;
            string sentence = "";
            char symb;
            int ind = 0;

            //do
            //{
            //    symb = codeProgramString[ind];
            //    codeProgramString = codeProgramString.Remove(0, 1);
            //    if (stringSwitch.IsMatch(sentence) == true && countQuotes == 0)
            //    {
            //        programStrings.Add(sentence);
            //        sentence = "";
            //    }
            //    else if (stringCase.IsMatch(sentence) == true && countQuotes == 0)
            //    {
            //        programStrings.Add(sentence);
            //        sentence = "";
            //    }
            //    else if (stringDefault.IsMatch(sentence) == true && countQuotes == 0)
            //    {
            //        programStrings.Add(sentence);
            //        sentence = "";
            //    }
            //    else if (stringElse.IsMatch(sentence) == true && countQuotes == 0 && (symb == ' ' || symb == '{'))
            //    {
            //        programStrings.Add(sentence);
            //        sentence = "";
            //    }

            //    if (symb == '"')
            //    {
            //        if (countQuotes == 0)
            //        {
            //            countQuotes++;
            //            sentence += symb;
            //            continue;
            //        }
            //        else if (countQuotes == 1)
            //        {
            //            sentence += symb;
            //            countQuotes = 0;
            //            continue;
            //        }
            //    }
            //    else if (symb == ' ')
            //    {
            //        if (sentence == "")
            //        {
            //            continue;
            //        }
            //        sentence += symb;
            //        continue;
            //    }
            //    else if (symb == ';')
            //    {
            //        if (sentence[sentence.Length - 1] == ' ')
            //        {
            //            sentence = sentence.Remove(sentence.Length - 1, 1);
            //        }
            //        if (codeProgramString[ind] == ' ')
            //        {
            //            codeProgramString = codeProgramString.Remove(0, 1);
            //        }
            //        if (countBrackets == 0 && countQuotes == 0)
            //        {
            //            addSymb(ref sentence, ref symb, ref programStrings);
            //        }
            //        else
            //        {
            //            sentence += symb;
            //        }
            //    }
            //    else if (symb == '(')
            //    {
            //        if (sentence != "")
            //        {
            //            if (sentence[sentence.Length - 1] == ' ')
            //            {
            //                sentence = sentence.Remove(sentence.Length - 1, 1);
            //            }
            //        }
            //        if (codeProgramString[ind] == ' ')
            //        {
            //            codeProgramString = codeProgramString.Remove(0, 1);
            //        }
            //        if (countQuotes == 0)
            //        {
            //            countBrackets++;
            //        }
            //        sentence += symb;
            //    }
            //    else if (symb == '{')
            //    {
            //        if (sentence != "")
            //        {
            //            if (sentence[sentence.Length - 1] == ' ')
            //            {
            //                sentence = sentence.Remove(sentence.Length - 1, 1);
            //            }
            //        }

            //        if (stringNamespace.IsMatch(sentence) == true && countQuotes == 0)
            //        {
            //            programStrings.Add(sentence);
            //            sentence = "";
            //        }
            //        else if (stringClass.IsMatch(sentence) == true && countQuotes == 0)
            //        {
            //            programStrings.Add(sentence);
            //            sentence = "";
            //        }
            //        else if (stringStruct.IsMatch(sentence) == true && countQuotes == 0)
            //        {
            //            programStrings.Add(sentence);
            //            sentence = "";
            //        }
            //        else if (stringDo.IsMatch(sentence) == true && countQuotes == 0)
            //        {
            //            programStrings.Add(sentence);
            //            sentence = "";
            //        }

            //        if (codeProgramString[ind] == ' ')
            //        {
            //            codeProgramString = codeProgramString.Remove(0, 1);
            //        }
            //        if (sentence == "" && stringEndSentence.IsMatch(codeProgramString[ind].ToString()) == true)
            //        {
            //            addSymb(ref sentence, ref symb, ref programStrings);
            //            continue;
            //        }
            //        if (countQuotes == 0)
            //        {
            //            countBrackets++;
            //        }
            //        else
            //        {
            //            sentence += symb;
            //        }
            //    }
            //    else if (symb == ')')
            //    {
            //        if (sentence != "")
            //        {
            //            if (sentence[sentence.Length - 1] == ' ')
            //            {
            //                sentence = sentence.Remove(sentence.Length - 1, 1);
            //            }
            //        }
            //        if (countQuotes == 0)
            //        {
            //            countBrackets--;
            //        }
            //        if (codeProgramString[ind] == ' ')
            //        {
            //            codeProgramString = codeProgramString.Remove(0, 1);
            //        }
            //        if (countBrackets == 0 && stringEndSentence.IsMatch(codeProgramString[ind].ToString()) == true && countQuotes == 0)
            //        {
            //            addSymb(ref sentence, ref symb, ref programStrings);
            //            continue;
            //        }
            //        else
            //        {
            //            sentence += symb;
            //        }
            //    }
            //    else if (symb == '}')
            //    {
            //        if (countBrackets == 0 && sentence == "" && countQuotes == 0)
            //        {
            //            addSymb(ref sentence, ref symb, ref programStrings);
            //            continue;
            //        }
            //        if (countQuotes == 0)
            //        {
            //            countBrackets--;
            //        }
            //        else
            //        {
            //            sentence += symb;
            //        }
            //    }
            //    else
            //    {
            //        sentence += symb;
            //    }

            //} while (codeProgramString != "");

            //for (int i = 0; i < programStrings.Count; i++)
            //{
            //    Console.WriteLine(programStrings[i]);
            //}

            GetStringsCode g = new GetStringsCode();
            g.shareString(ref codeProgramString);
            g.Show();
            CodeDescription codeDescription = new CodeDescription(g.linesProgram);
            codeDescription.LineReading();
            codeDescription.showCode();



            ////Input-Output
            //string testIOString = "Console.WriteLine(programStrings[i]);";
            //var searchIO = new Regex(@"^Console.(Write|Read).*\((?<IOText>.*)\);$");
            //Console.WriteLine("Input-Output: " + searchIO.Match(testIOString).Groups["IOText"]);

            ////Case
            //string testCaseString = "case main[0]:";
            //var searchCase = new Regex(@"case (?<variant>.+):$");
            //Console.WriteLine("Case: " + searchCase.Match(testCaseString).Groups["variant"]);

            ////Create Variable
            //string testCreateVariableString = "int a, b;";
            //var searchCreateVariable = new Regex(@"^(?<Type>[_0-9a-zA-Z\[,\]]+)?\s(?<Values>.+);$");
            //Console.WriteLine(testCreateVariableString);
            //Console.WriteLine("Type: " + searchCreateVariable.Match(testCreateVariableString).Groups["Type"]);
            //Console.WriteLine("Values: " + searchCreateVariable.Match(testCreateVariableString).Groups["Values"] + '\n');

            ////Change Variable
            //string testVariableString = "m=Method(ref a, out b);";
            //var searchVariable = new Regex(@"^(?<LValue>.+)\s?=\s?(?<RValue>.+);$");
            //Console.WriteLine(testVariableString);
            //Console.WriteLine("LValue: " + searchVariable.Match(testVariableString).Groups["LValue"]);
            //Console.WriteLine("RValue: " + searchVariable.Match(testVariableString).Groups["RValue"]);


            //var searchType = new Regex(@"^[_A-z0-9]+ ");
            //Console.WriteLine(searchType.Match(codeProgramString));

            ////Цикл for
            //Console.WriteLine("For");
            //string testForString = "for (int i = 0, int l = 9; i < programStrings.Count; i++)";
            //var searchFor = new Regex(@"^for\s?\((?<Iterators>.*);(?<ConditionFOR>.*);(?<IteratorChanger>.*)\)$");
            //Console.WriteLine("Iterarors: " + searchFor.Match(testForString).Groups["Iterators"]);
            //Console.WriteLine("Condition: " + searchFor.Match(testForString).Groups["ConditionFOR"]);
            //Console.WriteLine("IteratorChange: " + searchFor.Match(testForString).Groups["IteratorChanger"]);


            ////Условие If
            //Console.WriteLine("If");
            //string testIfString = "if (a == true && b == false)";
            //var searchIf = new Regex(@"^if\s?\((?<ConditionIF>.*)\)$");
            //Console.WriteLine("ConditionIF: " + searchIf.Match(testIfString).Groups["ConditionIF"]);

            ////Условие DWhile
            //Console.WriteLine("while");
            //string testDWhileString = "while(t == true)";
            //var searchWhile = new Regex(@"^while\s?\((?<ConditionWHILE>.*)\)$");
            //Console.WriteLine("ConditionDWHILE: " + searchWhile.Match(testDWhileString).Groups["ConditionWHILE"]);

            ////Условие switch
            //Console.WriteLine("switch");
            //string testSwitchString = "switch (b)";
            //var searchSwitch = new Regex(@"^switch\s?\((?<variableSWITCH>.*)\)$");
            //Console.WriteLine("ConditionIF: " + searchSwitch.Match(testSwitchString).Groups["variableSWITCH"]);


            ////Call method
            //string testCallMethodString = "Console.ReadKey(\"switch\");";
            //var searchCallMethod = new Regex(@"^((?<Objects>.*)\.)?(?<NameMethod>.+)\((?<Arguments>.*)\);$");
            //Console.WriteLine("Objects: " + searchCallMethod.Match(testCallMethodString).Groups["Objects"]);
            //Console.WriteLine("NameMethod: " + searchCallMethod.Match(testCallMethodString).Groups["NameMethod"]);
            //Console.WriteLine("Arguments: " + searchCallMethod.Match(testCallMethodString).Groups["Arguments"]);

            //метод
            //    Console.WriteLine("Method");
            //    string testMethodString = "public static void Main(string[] args, int a)";
            //    var searchMethod = new Regex(@"^(?<Accept>(static)*\s*(public|private|protected|internal|protected internal|private protected|)\s*(static)*)\s*(?<ReturnTypeMethod>[A-Za-z0-9\[\]_]+) (?<MethodName>[a-zA-Z0-9 _]+)\((?<Params>[A-Za-z0-9, _\[\]]*)\)$");
            //    Console.WriteLine("String: " + testMethodString);
            //    Console.WriteLine("Accept: " + searchMethod.Match(testMethodString).Groups["Accept"]);
            //    Console.WriteLine("ReturnTypeMethod: " + searchMethod.Match(testMethodString).Groups["ReturnTypeMethod"]);
            //    Console.WriteLine("MethodName: " + searchMethod.Match(testMethodString).Groups["MethodName"]);
            //    Console.WriteLine("Params: " + searchMethod.Match(testMethodString).Groups["Params"]);
        }

        static void addSymb(ref string sentence, ref char symb, ref List<string> lineProgram)
        {
            sentence += symb;
            lineProgram.Add(sentence);
            sentence = "";
            symb = default(char);
        }
    }
}
