using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParserCode
{
    class CodeDescription
    {
        private Stack<string> callsConstructions;
        private Stack<string> iterators;
        private Stack<bool> Brackets;
        private Stack<int> countStrAfterConstruction;
        private List<string> programStrings;
        private Regex stringMethod = new Regex(@"^(?<Accept>(static)*\s*(public|private|protected|internal|protected internal|private protected|)\s*(static)*)\s*(?<ReturnTypeMethod>[A-Za-z0-9\[\]_]+) (?<MethodName>[a-zA-Z0-9 _]+)\((?<Params>[A-Za-z0-9, _\[\]]*)\)$");
        //private Regex stringClass = new Regex(@"^([a-z ]+ )*class [_A-Za-z0-9]+$");
        private Regex stringInputOutput = new Regex(@"^Console.(Write|Read).*\((?<IOText>.*)\);$");
        private Regex stringCase = new Regex(@"^case (?<variant>.+):$");
        private Regex stringCreateVariable = new Regex(@"^(?<Type>[_0-9a-zA-Z\[,\]]+)?\s(?<Values>.+);$");
        private Regex stringChangeVariable = new Regex(@"^(?<LValue>.+)\s?=\s?(?<RValue>.+);$");
        private Regex stringFor = new Regex(@"^for\s?\((?<Iterators>.*);(?<ConditionFOR>.*);(?<IteratorChanger>.*)\)$");
        private Regex stringIf = new Regex(@"^if\s?\((?<ConditionIF>.*)\)$");
        private Regex stringSwitch = new Regex(@"^switch\s?\((?<variableSWITCH>.*)\)$");
        private Regex stringCallMethod = new Regex(@"^((?<Objects>.*)\.)?(?<NameMethod>.+)\((?<Arguments>.*)\);$");
        private Regex stringDo = new Regex(@"^do$");
        private Regex stringDWhile = new Regex(@"^while\s?\((?<ConditionDWHILE>.*)\);$");
        private Regex stringWhile = new Regex(@"^while\s?\((?<ConditionWHILE>.*)\)$");

        private List<string> parsedCode;

        public CodeDescription(List<string> linesCode)
        {
            programStrings = linesCode;
            callsConstructions = new Stack<string>();
            Brackets = new Stack<bool>();
            iterators = new Stack<string>();
            countStrAfterConstruction = new Stack<int>();
            parsedCode = new List<string>();
        }

        public void LineReading()
        {
            for (int i = 0; i < programStrings.Count; i++)
            {
                CheckAndCommentMethodConstruction(ref i);
                if (countStrAfterConstruction.Peek() == 1 && Brackets.Peek() == false)
                {
                    CheckEndBlockConstructionNoBracket();
                }
                //
                if (CheckAndCommentSimpleLine(programStrings[i]) == true)
                {
                    while (countStrAfterConstruction.Peek() == 1 && Brackets.Peek() == false)
                    {
                        CheckEndBlockConstructionNoBracket();
                    }
                    continue;
                }
                //Do
                if (stringDo.IsMatch(programStrings[i]))
                {
                    CheckAndCommentDWhileConstruction();
                    i++;
                    continue;
                }
                //If
                if (stringIf.IsMatch(programStrings[i]))
                {
                    CheckAndCommentIfConstruction(ref i);
                    continue;
                }
                //For
                if (stringFor.IsMatch(programStrings[i]))
                {
                    CheckAndCommentForConstruction(ref i);
                    continue;
                }
                //While
                if (stringWhile.IsMatch(programStrings[i]))
                {
                    CheckAndCommentWhileConstruction(ref i);
                    continue;
                }         

                if (programStrings[i] == "}" && callsConstructions.Count > 0)
                {
                    CheckEndBlockConstructionBracket(ref i);
                }
            }
        }

        private bool CheckAndCommentSimpleLine(string line)
        {
            bool checkBlock = false;
            if (stringInputOutput.IsMatch(line))
            {
                parsedCode.Add("IO BLOCK |" + stringInputOutput.Match(line).Groups["IOText"]);
                checkBlock = true;
            }
            else if (stringCreateVariable.IsMatch(line))
            {
                parsedCode.Add("VAR BLOCK |" + line);
                checkBlock = true;
            }
            else if (stringChangeVariable.IsMatch(line))
            {
                parsedCode.Add("VAR BLOCK |" + line);
                checkBlock = true;
            }
            else if (stringCallMethod.IsMatch(line))
            {
                parsedCode.Add("FUNC BLOCK |" + line);
                checkBlock = true;
            }
            if (checkBlock == true)
            {
                int countStr = countStrAfterConstruction.Pop();
                countStrAfterConstruction.Push(++countStr);
                return true;
            }
            return false;
        }

        private void CheckAndCommentMethodConstruction(ref int index)
        {
            if (stringMethod.IsMatch(programStrings[index]))
            {
                callsConstructions.Push("METHOD");
                Brackets.Push(true);
                countStrAfterConstruction.Push(0);
                parsedCode.Add("START METHOD |" +
                    stringMethod.Match(programStrings[index]).Groups["ReturnTypeMethod"] +
                    " " + stringMethod.Match(programStrings[index]).Groups["MethodName"] +
                    "(" + stringMethod.Match(programStrings[index]).Groups["Params"] + ")");
                index++;
            }
        }

        private void CheckAndCommentDWhileConstruction()
        {
            callsConstructions.Push("DWHILE");
            Brackets.Push(true);
            int countStr = countStrAfterConstruction.Pop();
            countStrAfterConstruction.Push(++countStr);
            countStrAfterConstruction.Push(0);
            parsedCode.Add("START DO |");
        }

        private void CheckAndCommentWhileConstruction(ref int index)
        {
            callsConstructions.Push("WHILE");
            int countStr = countStrAfterConstruction.Pop();
            countStrAfterConstruction.Push(++countStr);
            countStrAfterConstruction.Push(0);
            parsedCode.Add("START WHILE |" + stringWhile.Match(programStrings[index]).Groups["ConditionWHILE"]);
            if (programStrings[index + 1] == "{")
            {
                Brackets.Push(true);
                index++;
            }
            else
            {
                Brackets.Push(false);
            }
        }

        private void CheckAndCommentIfConstruction(ref int index)
        {
                callsConstructions.Push("IF");
                int countStr = countStrAfterConstruction.Pop();
                countStrAfterConstruction.Push(++countStr);
                countStrAfterConstruction.Push(0);
                parsedCode.Add("START IF |" +
                    stringIf.Match(programStrings[index]).Groups["ConditionIF"]);
                if (programStrings[index + 1] == "{")
                {
                    Brackets.Push(true);
                    index++;
                }
                else
                {
                    Brackets.Push(false);
                }
        }

        private void CheckAndCommentForConstruction(ref int index)
        {
                callsConstructions.Push("FOR");
                int countStr = countStrAfterConstruction.Pop();
                countStrAfterConstruction.Push(++countStr);
                countStrAfterConstruction.Push(0);
                parsedCode.Add("VAR BLOCK |" + stringFor.Match(programStrings[index]).Groups["Iterators"]);
                parsedCode.Add("START FOR |" + stringFor.Match(programStrings[index]).Groups["ConditionFOR"]);
                iterators.Push("" + stringFor.Match(programStrings[index]).Groups["IteratorChanger"]);
                if (programStrings[index + 1] == "{")
                {
                    Brackets.Push(true);
                    index++;
                }
                else
                {
                    Brackets.Push(false);
                }
        }

        private void CheckEndBlockConstructionBracket(ref int index)
        {
            bool checkEndConstruction = false;
            switch (callsConstructions.Peek())
            {
                case "METHOD":
                    {
                        checkEndConstruction = true;
                        break;
                    }
                case "FOR":
                    {
                        if (iterators.Peek() != "")
                        {
                            parsedCode.Add("VAR BLOCK |" + iterators.Pop());
                        }
                        checkEndConstruction = true;
                        break;
                    }
                case "IF":
                    {
                        checkEndConstruction = true;
                        break;
                    }
                case "WHILE":
                    {
                        checkEndConstruction = true;
                        break;
                    }
                case "DWHILE":
                    {
                        checkEndConstruction = true;
                        parsedCode.Add("END " + callsConstructions.Pop() + " |" + stringDWhile.Match(programStrings[++index]).Groups["ConditionDWHILE"]);
                        Brackets.Pop();
                        countStrAfterConstruction.Pop();
                        return;
                    }
            }
            if (checkEndConstruction == true)
            {
                parsedCode.Add("END " + callsConstructions.Pop() + " |");
                Brackets.Pop();
                countStrAfterConstruction.Pop();
            }
        }

        private void CheckEndBlockConstructionNoBracket()
        {
            bool checkEndConstruction = false;
            switch (callsConstructions.Peek())
            {
                case "FOR":
                    {
                        if (iterators.Peek() != "")
                        {
                            parsedCode.Add("VAR BLOCK |" + iterators.Pop());
                        }
                        checkEndConstruction = true;
                        break;
                    }
                case "IF":
                    {
                        checkEndConstruction = true;
                        break;
                    }
                case "WHILE":
                    {
                        checkEndConstruction = true;
                        break;
                    }
            }
            if (checkEndConstruction == true)
            {
                parsedCode.Add("END " + callsConstructions.Pop() + " |");
                Brackets.Pop();
                countStrAfterConstruction.Pop();
            }
        }

        public void showCode()
        {
            for (int i = 0; i < parsedCode.Count; i++)
            {
                Console.WriteLine(parsedCode[i]);
            }
        }
    }
}
