using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParserCode
{
    class GetStringsCode
    {
        private List<string> _LinesProgram;
        private Regex stringEndSentence = new Regex(@"[_A-Za-z0-9{}]");
        private Regex stringMethod = new Regex(@"^(?<Accept>(static)*\s*(public|private|protected|internal|protected internal|private protected|)\s*(static)*)\s*(?<ReturnTypeMethod>[A-Za-z0-9\[\]_]+) (?<MethodName>[a-zA-Z0-9 _]+)\((?<Params>[A-Za-z0-9, _\[\]]*)\)$");
        private Regex stringDo = new Regex(@"^do$");
        private Regex stringNamespace = new Regex(@"^namespace [_A-Za-z0-9]+$");
        private Regex stringClass = new Regex(@"^([a-z ]+ )*class [_A-Za-z0-9]+$");
        private Regex stringStruct = new Regex(@"^([a-z ]+ )*struct [_A-Za-z0-9]+$");
        private Regex stringSwitch = new Regex(@"^switch([_A-Za-z0-9[] ]+)");
        private Regex stringCase = new Regex(@"^case [_A-Za-z0-9[]]*\s*:");
        private Regex stringDefault = new Regex(@"^default:");
        private Regex stringElse = new Regex(@"^else");
        private Regex stringStartGoto = new Regex(@"^goto .*;$");
        private Regex stringEndGoto = new Regex(@"^.*:$");

        public List<string> linesProgram => _LinesProgram;

        public GetStringsCode()
        {
            _LinesProgram = new List<string>();
        }

        public void shareString(string codeProgramString)
        {
            codeProgramString = Regex.Replace(codeProgramString, @"\s+", " ");
            int countBrackets = 0;
            bool singleQuotes = false;
            bool doubleQuotes = false;
            string sentence = "";
            char symb;
            int ind = 0;
            do
            {
                symb = codeProgramString[ind];
                codeProgramString = codeProgramString.Remove(0, 1);

                if (singleQuotes == true && symb == '\\')
                {
                    if (codeProgramString[0] == '\'')
                    {
                        sentence += symb;
                        sentence = sentence.Remove(sentence.Length - 1, 1);
                        sentence += '\'';
                    }
                    if (codeProgramString[0] == '\"')
                    {
                        sentence += symb;
                        sentence = sentence.Remove(sentence.Length - 1, 1);
                        sentence += '\"';
                    }

                    codeProgramString = codeProgramString.Remove(0, 1);
                    continue;
                }
                if (doubleQuotes == true && symb == '\\' && codeProgramString[0] == '\"')
                {
                    sentence += symb;
                    sentence = sentence.Remove(sentence.Length - 1, 1);
                    sentence += '\"';
                    codeProgramString = codeProgramString.Remove(0, 1);
                    continue;
                }
                if (stringSwitch.IsMatch(sentence) == true && doubleQuotes == false && singleQuotes == false)
                {
                    addString(ref sentence);
                }
                else if (stringCase.IsMatch(sentence) == true && doubleQuotes == false && singleQuotes == false)
                {
                    addString(ref sentence);
                }
                else if (stringStartGoto.IsMatch(sentence) == true && doubleQuotes == false && singleQuotes == false)
                {
                    addString(ref sentence);
                }
                else if (stringEndGoto.IsMatch(sentence) == true && doubleQuotes == false && singleQuotes == false)
                {
                    addString(ref sentence);
                }
                else if (stringDefault.IsMatch(sentence) == true && doubleQuotes == false && singleQuotes == false)
                {
                    addString(ref sentence);
                }
                else if (stringElse.IsMatch(sentence) == true && doubleQuotes == false && singleQuotes == false && (symb == ' ' || symb == '{'))
                {
                    addString(ref sentence);
                }
                if (symb == '\'' && doubleQuotes == false)
                {
                    if (singleQuotes == false)
                    {
                        singleQuotes = true;
                    }
                    else if (singleQuotes == true)
                    {
                        singleQuotes = false;
                    }
                    sentence += symb;
                    continue;
                }
                if (symb == '"' && singleQuotes == false)
                {
                    if (doubleQuotes == false)
                    {
                        doubleQuotes = true;
                    }
                    else if (doubleQuotes == true)
                    {
                        doubleQuotes = false;
                    }
                    sentence += symb;
                    continue;
                }
                else if (symb == ' ')
                {
                    if (sentence == "")
                    {
                        continue;
                    }
                    sentence += symb;
                    continue;
                }
                else if (symb == ';' && doubleQuotes == false && singleQuotes == false)
                {
                    if (sentence[sentence.Length - 1] == ' ')
                    {
                        sentence = sentence.Remove(sentence.Length - 1, 1);
                    }
                    if (countBrackets == 0)
                    {
                        addSymbAndWriteString(ref sentence, ref symb);
                    }
                    else
                    {
                        sentence += symb;
                    }
                    if (codeProgramString[0] == ' ')
                    {
                        codeProgramString = codeProgramString.Remove(0, 1);
                    }
                }
                else if (symb == '(')
                {
                    if (sentence != "" && doubleQuotes == false && singleQuotes == false)
                    {
                        if (sentence[sentence.Length - 1] == ' ')
                        {
                            sentence = sentence.Remove(sentence.Length - 1, 1);
                        }
                        countBrackets++;
                    }
                    sentence += symb;

                    if (codeProgramString[0] == ' ')
                    {
                        codeProgramString = codeProgramString.Remove(0, 1);
                    }
                }
                else if (symb == '{' && singleQuotes == false && doubleQuotes == false)
                {
                    if (sentence != "")
                    {
                        if (sentence[sentence.Length - 1] == ' ')
                        {
                            sentence = sentence.Remove(sentence.Length - 1, 1);
                        }
                    }
                    if (stringNamespace.IsMatch(sentence) == true)
                    {
                        addString(ref sentence);
                        addSymbAndWriteString(ref sentence, ref symb);
                        continue;
                    }
                    else if (stringClass.IsMatch(sentence) == true)
                    {
                        addString(ref sentence);
                        addSymbAndWriteString(ref sentence, ref symb);
                        continue;
                    }
                    else if (stringStruct.IsMatch(sentence) == true)
                    {
                        addString(ref sentence);
                        addSymbAndWriteString(ref sentence, ref symb);
                        continue;
                    }
                    else if (stringDo.IsMatch(sentence) == true)
                    {
                        addString(ref sentence);
                        addSymbAndWriteString(ref sentence, ref symb);
                        continue;
                    }
                    else if (stringMethod.IsMatch(sentence) == true)
                    {
                        addString(ref sentence);
                        addSymbAndWriteString(ref sentence, ref symb);
                    }

                    if (codeProgramString[0] == ' ')
                    {
                        codeProgramString = codeProgramString.Remove(0, 1);
                    }
                    if (sentence == "" && stringEndSentence.IsMatch(codeProgramString[ind].ToString()) == true)
                    {
                        addSymbAndWriteString(ref sentence, ref symb);
                        continue;
                    }
                    countBrackets++;
                    sentence += symb;
                }
                else if (symb == ')' && singleQuotes == false && doubleQuotes == false)
                {
                    if (sentence != "")
                    {
                        if (sentence[sentence.Length - 1] == ' ')
                        {
                            sentence = sentence.Remove(sentence.Length - 1, 1);
                        }
                    }
                    countBrackets--;

                    if (codeProgramString[0] == ' ')
                    {
                        codeProgramString = codeProgramString.Remove(0, 1);
                    }
                    if (countBrackets == 0 && stringEndSentence.IsMatch(codeProgramString[ind].ToString()) == true)
                    {

                        addSymbAndWriteString(ref sentence, ref symb);
                        continue;
                    }
                    else
                    {
                        sentence += symb;
                    }
                }
                else if (symb == '}' && singleQuotes == false && doubleQuotes == false)
                {
                    if (countBrackets == 0 && sentence == "")
                    {
                        addSymbAndWriteString(ref sentence, ref symb);
                        continue;
                    }
                    if (doubleQuotes == false)
                    {
                        countBrackets--;
                    }
                    sentence += symb;
                }
                else
                {
                    sentence += symb;
                }
            } while (codeProgramString != "");
        }

        private void addSymbAndWriteString(ref string sentence, ref char symb)
        {
            sentence += symb;
            _LinesProgram.Add(sentence);
            sentence = "";
            symb = default(char);
        }

        private void addString(ref string sentence)
        {
            _LinesProgram.Add(sentence);
            sentence = "";
        }

        public void Show(ListBox lb)
        {
            foreach(string str in _LinesProgram)
            {
                lb.Items.Add(str);
            }
        }
    }
}