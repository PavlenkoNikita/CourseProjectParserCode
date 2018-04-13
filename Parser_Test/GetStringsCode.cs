using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParserCode
{
    class GetStringsCode
    {
        private List<string> _LinesProgram;
        private Regex stringEndSentence = new Regex(@"[_A-Za-z0-9{}]");
        private Regex stringDo = new Regex(@"^do$");
        private Regex stringNamespace = new Regex(@"^namespace [_A-Za-z0-9]+$");
        private Regex stringClass = new Regex(@"^([a-z ]+ )*class [_A-Za-z0-9]+$");
        private Regex stringStruct = new Regex(@"^([a-z ]+ )*struct [_A-Za-z0-9]+$");
        private Regex stringSwitch = new Regex(@"^switch([_A-Za-z0-9[] ]+)");
        private Regex stringCase = new Regex(@"^case [_A-Za-z0-9[]]*\s*:");
        private Regex stringDefault = new Regex(@"^default:");
        private Regex stringElse = new Regex(@"^else");

        public List<string> linesProgram => _LinesProgram;

        public GetStringsCode()
        {
            _LinesProgram = new List<string>();
        }

        public void shareString(ref string codeProgramString)
        {
            codeProgramString = Regex.Replace(codeProgramString, @"\s+", " ");
            int countBrackets = 0;
            int countQuotes = 0;
            string sentence = "";
            char symb;
            int ind = 0;

            do
            {
                symb = codeProgramString[ind];
                codeProgramString = codeProgramString.Remove(0, 1);
                if (stringSwitch.IsMatch(sentence) == true && countQuotes == 0)
                {
                    addString(ref sentence);
                }
                else if (stringCase.IsMatch(sentence) == true && countQuotes == 0)
                {
                    addString(ref sentence);
                }
                else if (stringDefault.IsMatch(sentence) == true && countQuotes == 0)
                {
                    addString(ref sentence);
                }
                else if (stringElse.IsMatch(sentence) == true && countQuotes == 0 && (symb == ' ' || symb == '{'))
                {
                    addString(ref sentence);
                }

                if (symb == '"')
                {
                    if (countQuotes == 0)
                    {
                        countQuotes++;
                        sentence += symb;
                        continue;
                    }
                    else if (countQuotes == 1)
                    {
                        sentence += symb;
                        countQuotes = 0;
                        continue;
                    }
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
                else if (symb == ';')
                {
                    if (sentence[sentence.Length - 1] == ' ')
                    {
                        sentence = sentence.Remove(sentence.Length - 1, 1);
                    }
                    if (codeProgramString[ind] == ' ')
                    {
                        codeProgramString = codeProgramString.Remove(0, 1);
                    }
                    if (countBrackets == 0 && countQuotes == 0)
                    {
                        addSymbAndWriteString(ref sentence, ref symb);
                    }
                    else
                    {
                        sentence += symb;
                    }
                }
                else if (symb == '(')
                {
                    if (sentence != "")
                    {
                        if (sentence[sentence.Length - 1] == ' ')
                        {
                            sentence = sentence.Remove(sentence.Length - 1, 1);
                        }
                    }
                    if (codeProgramString[ind] == ' ')
                    {
                        codeProgramString = codeProgramString.Remove(0, 1);
                    }
                    if (countQuotes == 0)
                    {
                        countBrackets++;
                    }
                    sentence += symb;
                }
                else if (symb == '{')
                {
                    if (sentence != "")
                    {
                        if (sentence[sentence.Length - 1] == ' ')
                        {
                            sentence = sentence.Remove(sentence.Length - 1, 1);
                        }
                    }
                    if (stringNamespace.IsMatch(sentence) == true && countQuotes == 0)
                    {
                        addString(ref sentence);
                    }
                    else if (stringClass.IsMatch(sentence) == true && countQuotes == 0)
                    {
                        addString(ref sentence);
                    }
                    else if (stringStruct.IsMatch(sentence) == true && countQuotes == 0)
                    {
                        addString(ref sentence);
                    }
                    else if (stringDo.IsMatch(sentence) == true && countQuotes == 0)
                    {
                        addString(ref sentence);
                    }

                    if (codeProgramString[ind] == ' ')
                    {
                        codeProgramString = codeProgramString.Remove(0, 1);
                    }
                    if (sentence == "" && stringEndSentence.IsMatch(codeProgramString[ind].ToString()) == true)
                    {
                        addSymbAndWriteString(ref sentence, ref symb);
                        continue;
                    }
                    if (countQuotes == 0)
                    {
                        countBrackets++;
                    }
                    else
                    {
                        sentence += symb;
                    }
                }
                else if (symb == ')')
                {
                    if (sentence != "")
                    {
                        if (sentence[sentence.Length - 1] == ' ')
                        {
                            sentence = sentence.Remove(sentence.Length - 1, 1);
                        }
                    }
                    if (countQuotes == 0)
                    {
                        countBrackets--;
                    }
                    if (codeProgramString[ind] == ' ')
                    {
                        codeProgramString = codeProgramString.Remove(0, 1);
                    }
                    if (countBrackets == 0 && stringEndSentence.IsMatch(codeProgramString[ind].ToString()) == true && countQuotes == 0)
                    {
                        addSymbAndWriteString(ref sentence, ref symb);
                        continue;
                    }
                    else
                    {
                        sentence += symb;
                    }
                }
                else if (symb == '}')
                {

                    if (countBrackets == 0 && sentence == "" && countQuotes == 0)
                    {
                        addSymbAndWriteString(ref sentence, ref symb);
                        continue;
                    }
                    if (countQuotes == 0)
                    {
                        countBrackets--;
                    }
                    else
                    {
                        sentence += symb;
                    }
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

        public void Show()
        {
            for (int i = 0; i < _LinesProgram.Count; i++)
            {
                Console.WriteLine(_LinesProgram[i]);
            }
        }

    }
}
