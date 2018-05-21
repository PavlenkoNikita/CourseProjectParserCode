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
        private Stack<bool> Brackets;
        private Stack<int> countStrAfterConstruction;
        private Stack<string> callCycle;
        private Stack<Stack<string>> callSwitchConstructions;
        private Stack<string> callSwitch;
        private List<string> programStrings;
        public List<ParsedStr> parsedCode;
        private Regex stringMethod = new Regex(@"^(?<Accept>(static)*\s*(public|private|protected|internal|protected internal|private protected|)\s*(static)*)\s*(?<ReturnTypeMethod>[A-Za-z0-9\[\]_]+) (?<MethodName>[a-zA-Z0-9 _]+)\((?<Params>.*)\)$");
        private Regex stringInputOutput = new Regex(@"^Console.(Write|Read).*\((?<IOText>.*)\);$");
        private Regex stringSwitch = new Regex(@"^switch\s?\((?<variableSWITCH>.+)\)$");
        private Regex stringCase = new Regex(@"^case (?<variant>.+):$");
        private Regex stringDefault = new Regex(@"^default:");
        private Regex stringCreateVariable = new Regex(@"^(?<Type>[_0-9a-zA-Z\[,\]]+)\s(?<Variable>[_0-9a-zA-Z]+)\s?=?\s?(?<Value>.+)?;$");
        private Regex stringChangeVariable = new Regex(@"^(?<LValue>.+)\s?=\s?(?<RValue>.+);$");
        private Regex stringFor = new Regex(@"^for\s?\((?<Iterators>.*);(?<ConditionFOR>.*);(?<IteratorChanger>.*)\)$");
        private Regex stringForeach = new Regex(@"^foreach\s?\(\s?(?<TypeVariable>[_A-Za-z0-9]+)\s(?<NameVariable>[_A-Za-z0-9]+) in (?<Collection>[_A-Za-z0-9\[\]]+)\s?\)$");
        private Regex stringIf = new Regex(@"^if\s?\((?<ConditionIF>.*)\)$");
        private Regex stringCallMethod = new Regex(@"^((?<Objects>.*)\.)?(?<NameMethod>.+)\((?<Arguments>.*)\);$");
        private Regex stringDo = new Regex(@"^do$");
        private Regex stringDWhile = new Regex(@"^while\s?\((?<ConditionDWHILE>.*)\);$");
        private Regex stringWhile = new Regex(@"^while\s?\((?<ConditionWHILE>.*)\)$");
        private Regex stringBreak = new Regex(@"^break;$");
        private Regex stringContinue = new Regex(@"^continue;$");
        private Regex stringElse = new Regex(@"^else");
        private Regex stringStartGoto = new Regex(@"^goto (?<NamePlaceTransition>.*);$");
        private Regex stringEndGoto = new Regex(@"^(?<NamePlaceTransition>.*):$");
        private Regex stringReturn = new Regex(@"^return\s?(?<ReturnData>.*)?;$");
        private Regex stringIncDec = new Regex(@"^(((\+\+)|(--))(?<Varivable>[_0-9a-zA-Z\[,\]]+))|((?<Varivable>[_0-9a-zA-Z\[,\]]+)((\+\+)|(--)));$");


        public CodeDescription(List<string> linesCode)
        {
            programStrings = linesCode;
            callsConstructions = new Stack<string>();
            Brackets = new Stack<bool>();
            callSwitchConstructions = new Stack<Stack<string>>();
            callSwitch = new Stack<string>();
            countStrAfterConstruction = new Stack<int>();
            callCycle = new Stack<string>();
            parsedCode = new List<ParsedStr>();
        }

        public void LineReading()
        {
            for (int i = 0; i < programStrings.Count; i++)
            {
                if (stringMethod.IsMatch(programStrings[i]))
                {
                    CheckAndCommentMethodConstruction(ref i);
                    continue;
                }
                if (callsConstructions.Count >= 1)
                {
                    if (programStrings[i] == "{")
                    {
                        callsConstructions.Push("SIMPLE");
                        continue;
                    }
                    if (countStrAfterConstruction.Count != 0 && Brackets.Count != 0)
                    {
                        if ((countStrAfterConstruction.Peek() == 1 && Brackets.Peek() == false) && (callsConstructions.Peek() != "CASE" && callsConstructions.Peek() != "DEFAULT"))
                        {
                            CheckEndBlockConstructionNoBracket();
                        }
                    }
                    //
                    if (CheckAndCommentSimpleLine(ref i) == true)
                    {
                        while (countStrAfterConstruction.Peek() == 1 && Brackets.Peek() == false && (callsConstructions.Peek() != "CASE" && callsConstructions.Peek() != "DEFAULT"))
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
                    //Foreach
                    if (stringForeach.IsMatch(programStrings[i]))
                    {
                        CheckAndCommentForeachConstruction(ref i);
                        continue;
                    }
                    //While
                    if (stringWhile.IsMatch(programStrings[i]))
                    {
                        CheckAndCommentWhileConstruction(ref i);
                        continue;
                    }
                    //Else(if)
                    if (stringElse.IsMatch(programStrings[i]))
                    {
                        CheckAndCommentElseConstruction(ref i);
                        continue;
                    }
                    //Switch
                    if (stringSwitch.IsMatch(programStrings[i]))
                    {
                        CheckAndCommentSwitchConstruction(ref i);
                        continue;
                    }
                    //Case
                    if (stringCase.IsMatch(programStrings[i]))
                    {
                        CheckAndCommentCase(ref i);
                        continue;
                    }
                    //Default
                    if (stringDefault.IsMatch(programStrings[i]))
                    {
                        CheckAndCommentDefault(ref i);
                        continue;
                    }
                    //End Block
                    if (programStrings[i] == "}" && callsConstructions.Count > 0)
                    {
                        CheckEndBlockConstructionBracket(ref i);
                    }
                }
            }
        }

        private bool CheckAndCommentSimpleLine(ref int index)
        {
            bool checkBlock = false;
            if (stringInputOutput.IsMatch(programStrings[index]))
            {
                parsedCode.Add(new ParsedStr("IO BLOCK", "" + stringInputOutput.Match(programStrings[index]).Groups["IOText"]));
                checkBlock = true;
            }
            else if (stringStartGoto.IsMatch(programStrings[index]))
            {
                parsedCode.Add(new ParsedStr("START LINK", programStrings[index]));
                if (callSwitch.Count > 0)
                {
                    if (Brackets.Peek() == true)
                    {
                        if (programStrings[index + 1] == "}" && callsConstructions.Peek() == "SIMPLE")
                        {
                            index++;
                            CheckEndBlockConstructionBracket(ref index);
                        }
                        int tempCountBrackets = 0;
                        do
                        {
                            if (programStrings[index + 1] == "}" && tempCountBrackets == 0 && callsConstructions.Peek() == "SIMPLE")
                            {
                                index++;
                                CheckEndBlockConstructionBracket(ref index);
                            }
                            index++;
                            if (programStrings[index] == "{")
                            {
                                tempCountBrackets++;
                            }
                            else if (programStrings[index] == "}")
                            {
                                tempCountBrackets--;
                            }

                        } while (programStrings[index] != "}" && tempCountBrackets == 0);
                    }
                    else
                    {
                        if (programStrings[index + 1] == "}" && callsConstructions.Peek() == "SIMPLE")
                        {
                            index++;
                            CheckEndBlockConstructionBracket(ref index);
                        }
                        int tempCountBrackets = 0;
                        while (programStrings[index + 1] != "}" && stringDefault.IsMatch(programStrings[index + 1]) != true && stringCase.IsMatch(programStrings[index + 1]) != true && tempCountBrackets == 0)
                        {
                            if (programStrings[index + 1] == "}" && tempCountBrackets == 0 && callsConstructions.Peek() == "SIMPLE")
                            {
                                index++;
                                CheckEndBlockConstructionBracket(ref index);
                            }
                            index++;
                            if (programStrings[index] == "{")
                            {
                                tempCountBrackets++;
                            }

                            else if (programStrings[index] == "}")
                            {
                                tempCountBrackets--;
                            }
                        }
                        CheckEndBlockConstructionNoBracket();
                    }
                    Brackets.Pop();
                }
                else
                {
                    if (Brackets.Peek() == true && programStrings[index + 1] != "}")
                    {
                        int tempCountBrackets = 0;
                        while (true)
                        {
                            if (programStrings[index + 1] == "}" && tempCountBrackets == 0 && callsConstructions.Peek() == "SIMPLE")
                            {
                                index++;
                                CheckEndBlockConstructionBracket(ref index);
                            }
                            if (programStrings[index + 1] == "}" && tempCountBrackets == 0)
                            {
                                break;
                            }
                            index++;
                            if (programStrings[index] == "{")
                            {
                                tempCountBrackets++;
                            }
                            else if (programStrings[index] == "}")
                            {
                                tempCountBrackets--;
                            }

                        }
                    }
                }
                checkBlock = true;
            }
            else if (stringEndGoto.IsMatch(programStrings[index]) && stringCase.IsMatch(programStrings[index]) == false && stringDefault.IsMatch(programStrings[index]) == false)
            {
                parsedCode.Add(new ParsedStr("END LINK", programStrings[index]));
                checkBlock = true;
            }
            else if (stringReturn.IsMatch(programStrings[index]))
            {
                parsedCode.Add(new ParsedStr("RETURN", "" + stringReturn.Match(programStrings[index]).Groups["ReturnData"]));
                if (callSwitch.Count > 0)
                {
                    parsedCode.Add(new ParsedStr("END METHOD", ""));
                    if (Brackets.Peek() == true)
                    {
                        if (programStrings[index + 1] == "}" && callsConstructions.Peek() == "SIMPLE")
                        {
                            index++;
                            CheckEndBlockConstructionBracket(ref index);
                        }
                        int tempCountBrackets = 0;
                        do
                        {
                            if (programStrings[index + 1] == "}" && tempCountBrackets == 0 && callsConstructions.Peek() == "SIMPLE")
                            {
                                index++;
                                CheckEndBlockConstructionBracket(ref index);
                            }
                            index++;
                            if (programStrings[index] == "{")
                            {
                                tempCountBrackets++;
                            }
                            else if (programStrings[index] == "}")
                            {
                                tempCountBrackets--;
                            }
                        } while (programStrings[index] != "}" && tempCountBrackets == 0);
                    }
                    else
                    {
                        if (programStrings[index + 1] == "}" && callsConstructions.Peek() == "SIMPLE")
                        {
                            index++;
                            CheckEndBlockConstructionBracket(ref index);
                        }
                        int tempCountBrackets = 0;
                        while (true)
                        {
                            if (programStrings[index + 1] == "}" && tempCountBrackets == 0 && callsConstructions.Peek() == "SIMPLE")
                            {
                                index++;
                                CheckEndBlockConstructionBracket(ref index);
                            }
                            if ((programStrings[index + 1] == "}" || stringDefault.IsMatch(programStrings[index + 1]) == true || stringCase.IsMatch(programStrings[index + 1]) == true) && tempCountBrackets == 0)
                            {
                                break;
                            }
                            index++;
                            if (programStrings[index] == "{")
                            {
                                tempCountBrackets++;
                            }
                            else if (programStrings[index] == "}")
                            {
                                tempCountBrackets--;
                            }
                        }
                        CheckEndBlockConstructionNoBracket();
                    }
                    Brackets.Pop();
                }
                else
                {
                    if (programStrings[index + 1] == "}" && callsConstructions.Peek() == "METHOD")
                    {

                    }
                    else
                    {
                        parsedCode.Add(new ParsedStr("END METHOD", ""));
                    }

                    if (Brackets.Peek() == true && programStrings[index + 1] != "}")
                    {
                        if (programStrings[index + 1] == "}" && callsConstructions.Peek() == "SIMPLE")
                        {
                            index++;
                            CheckEndBlockConstructionBracket(ref index);
                        }
                        int tempCountBrackets = 0;
                        while (true)
                        {
                            if (programStrings[index + 1] == "}" && tempCountBrackets == 0 && callsConstructions.Peek() == "SIMPLE")
                            {
                                index++;
                                CheckEndBlockConstructionBracket(ref index);
                            }
                            if (programStrings[index + 1] == "}" && tempCountBrackets == 0)
                            {
                                break;
                            }
                            index++;
                            if (programStrings[index] == "{")
                            {
                                tempCountBrackets++;
                            }
                            else if (programStrings[index] == "}")
                            {
                                tempCountBrackets--;
                            }

                        }
                    }
                }
                checkBlock = true;
            }
            else if (stringCreateVariable.IsMatch(programStrings[index]) || stringChangeVariable.IsMatch(programStrings[index]) || stringIncDec.IsMatch(programStrings[index]))
            {
                parsedCode.Add(new ParsedStr("VAR BLOCK", programStrings[index]));
                checkBlock = true;
            }
            else if (stringCallMethod.IsMatch(programStrings[index]))
            {
                parsedCode.Add(new ParsedStr("FUNC BLOCK", programStrings[index]));
                checkBlock = true;
            }
            else if (stringBreak.IsMatch(programStrings[index]))
            {
                if (callSwitch.Count > 0)
                {

                    parsedCode.Add(new ParsedStr("END " + callSwitch.Pop(), ""));
                    if (Brackets.Peek() == true)
                    {
                        if (programStrings[index + 1] == "}" && callsConstructions.Peek() == "SIMPLE")
                        {
                            index++;
                            CheckEndBlockConstructionBracket(ref index);
                        }
                        int tempCountBrackets = 0;
                        do
                        {
                            if (programStrings[index + 1] == "}" && tempCountBrackets == 0 && callsConstructions.Peek() == "SIMPLE")
                            {
                                index++;
                                CheckEndBlockConstructionBracket(ref index);
                            }
                            index++;
                            if (programStrings[index] == "{")
                            {
                                tempCountBrackets++;
                            }
                            else if (programStrings[index] == "}")
                            {
                                tempCountBrackets--;
                            }
                        } while (programStrings[index] != "}" && tempCountBrackets == 0);
                    }
                    else
                    {
                        if (programStrings[index + 1] == "}" && callsConstructions.Peek() == "SIMPLE")
                        {
                            index++;
                            CheckEndBlockConstructionBracket(ref index);
                        }
                        int tempCountBrackets = 0;
                        while (true)
                        {
                            if (programStrings[index + 1] == "}" && tempCountBrackets == 0 && callsConstructions.Peek() == "SIMPLE")
                            {
                                index++;
                                CheckEndBlockConstructionBracket(ref index);
                            }
                            if ((programStrings[index + 1] == "}" || stringDefault.IsMatch(programStrings[index + 1]) == true || stringCase.IsMatch(programStrings[index + 1]) == true) && tempCountBrackets == 0)
                            {
                                break;
                            }
                            index++;
                            if (programStrings[index] == "{")
                            {
                                tempCountBrackets++;
                            }
                            else if (programStrings[index] == "}")
                            {
                                tempCountBrackets--;
                            }
                        }
                        CheckEndBlockConstructionNoBracket();
                    }
                    Brackets.Pop();

                }
                else
                {
                    parsedCode.Add(new ParsedStr("END " + callCycle.Peek(), programStrings[index]));
                    if (programStrings[index + 1] == "}" && callsConstructions.Peek() == "SIMPLE")
                    {
                        index++;
                        CheckEndBlockConstructionBracket(ref index);
                    }
                    if (Brackets.Peek() == true && programStrings[index + 1] != "}")
                    {
                        int tempCountBrackets = 0;
                        while (true)
                        {
                            if (programStrings[index + 1] == "}" && tempCountBrackets == 0 && callsConstructions.Peek() == "SIMPLE")
                            {
                                index++;
                                CheckEndBlockConstructionBracket(ref index);
                            }
                            if (programStrings[index + 1] == "}" && tempCountBrackets == 0)
                            {
                                break;
                            }
                            index++;
                            if (programStrings[index] == "{")
                            {
                                tempCountBrackets++;
                            }
                            else if (programStrings[index] == "}")
                            {
                                tempCountBrackets--;
                            }

                        }
                    }
                }
                checkBlock = true;
            }
            else if (stringContinue.IsMatch(programStrings[index]))
            {
                parsedCode.Add(new ParsedStr("CONTINUE " + callCycle.Peek(), ""));
                if (programStrings[index + 1] == "}" && callsConstructions.Peek() == "SIMPLE")
                {
                    index++;
                    CheckEndBlockConstructionBracket(ref index);
                }
                if (Brackets.Peek() == true && programStrings[index + 1] != "}")
                {
                    int tempCountBrackets = 0;
                    while (true)
                    {
                        if (programStrings[index + 1] == "}" && tempCountBrackets == 0 && callsConstructions.Peek() == "SIMPLE")
                        {
                            index++;
                            CheckEndBlockConstructionBracket(ref index);
                        }
                        if (programStrings[index + 1] == "}" && tempCountBrackets == 0)
                        {
                            break;
                        }
                        index++;
                        if (programStrings[index] == "{")
                        {
                            tempCountBrackets++;
                        }
                        else if (programStrings[index] == "}")
                        {
                            tempCountBrackets--;
                        }
                    }
                }
                checkBlock = true;
            }

            if (checkBlock == true && countStrAfterConstruction.Count != 0)
            {
                int countStr = countStrAfterConstruction.Pop();
                countStrAfterConstruction.Push(++countStr);
                return true;
            }
            return false;
        }

        private void CheckAndCommentMethodConstruction(ref int index)
        {
            callsConstructions.Push("METHOD");
            Brackets.Push(true);
            countStrAfterConstruction.Push(0);
            parsedCode.Add(new ParsedStr("START METHOD",
                //stringMethod.Match(programStrings[index]).Groups["ReturnTypeMethod"] +
                "" + stringMethod.Match(programStrings[index]).Groups["MethodName"]
                /*+"(" + stringMethod.Match(programStrings[index]).Groups["Params"] + ")"*/));
            index++;
        }

        private void CheckAndCommentDWhileConstruction()
        {
            callsConstructions.Push("DWHILE");
            Brackets.Push(true);
            int countStr = countStrAfterConstruction.Pop();
            countStrAfterConstruction.Push(++countStr);
            countStrAfterConstruction.Push(0);
            callCycle.Push("DWHILE");
            parsedCode.Add(new ParsedStr("START DO", ""));
        }

        private void CheckAndCommentWhileConstruction(ref int index)
        {
            callsConstructions.Push("WHILE");
            int countStr = countStrAfterConstruction.Pop();
            countStrAfterConstruction.Push(++countStr);
            countStrAfterConstruction.Push(0);
            parsedCode.Add(new ParsedStr("START WHILE", "" + stringWhile.Match(programStrings[index]).Groups["ConditionWHILE"]));
            callCycle.Push("WHILE");
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

        private void CheckAndCommentDefault(ref int index)
        {
            //callsConstructions.Push("CASE");
            callSwitch.Push("CASE");
            int countStr = countStrAfterConstruction.Pop();
            countStrAfterConstruction.Push(++countStr);
            countStrAfterConstruction.Push(0);
            parsedCode.Add(new ParsedStr("START CASE", ""));
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

        private void CheckAndCommentCase(ref int index)
        {
            //callsConstructions.Push("CASE");
            callSwitch.Push("CASE");
            int countStr = countStrAfterConstruction.Pop();
            countStrAfterConstruction.Push(++countStr);
            countStrAfterConstruction.Push(0);
            parsedCode.Add(new ParsedStr("START CASE", "" +
                stringCase.Match(programStrings[index]).Groups["variant"]));

            if (stringCase.IsMatch(programStrings[index + 1]))
            {
                index++;
                callsConstructions.Pop();
                countStrAfterConstruction.Pop();
                CheckAndCommentCase(ref index);
            }
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

        private void CheckAndCommentSwitchConstruction(ref int index)
        {
            callsConstructions.Push("SWITCH");
            if (callSwitch.Count != 0)
            {
                callSwitchConstructions.Push(callSwitch);
            }
            callSwitch = new Stack<string>();
            int countStr = countStrAfterConstruction.Pop();
            countStrAfterConstruction.Push(++countStr);
            countStrAfterConstruction.Push(0);
            parsedCode.Add(new ParsedStr("SWITCH", "" +
                stringSwitch.Match(programStrings[index]).Groups["variableSWITCH"]));
            Brackets.Push(true);
            index++;
        }

        private void CheckAndCommentIfConstruction(ref int index)
        {
            callsConstructions.Push("IF");
            int countStr = countStrAfterConstruction.Pop();
            countStrAfterConstruction.Push(++countStr);
            countStrAfterConstruction.Push(0);
            parsedCode.Add(new ParsedStr("START IF", "" +
                stringIf.Match(programStrings[index]).Groups["ConditionIF"]));
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

        private void CheckAndCommentElseConstruction(ref int index)
        {
            if (stringIf.IsMatch(programStrings[index + 1]) == true)
            {
                callsConstructions.Push("IF");
                int countStr = countStrAfterConstruction.Pop();
                countStrAfterConstruction.Push(++countStr);
                countStrAfterConstruction.Push(0);
                parsedCode.Add(new ParsedStr("START IF", "" +
                    stringIf.Match(programStrings[index + 1]).Groups["ConditionIF"]));
                index++;
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
            else
            {
                callsConstructions.Push("ELSE");
                int countStr = countStrAfterConstruction.Pop();
                countStrAfterConstruction.Push(++countStr);
                countStrAfterConstruction.Push(0);
                parsedCode.Add(new ParsedStr("START ELSE", ""));
                if (programStrings[index + 1] == "{")
                {
                    index++;
                    Brackets.Push(true);
                }
                else
                {
                    Brackets.Push(false);
                }
            }
        }

        private void CheckAndCommentForConstruction(ref int index)
        {
            callsConstructions.Push("FOR");
            int countStr = countStrAfterConstruction.Pop();
            countStrAfterConstruction.Push(++countStr);
            countStrAfterConstruction.Push(0);
            if ("" + stringFor.Match(programStrings[index]).Groups["ConditionFOR"] == "")
            {
                parsedCode.Add(new ParsedStr("START FOR", stringFor.Match(programStrings[index]).Groups["Iterators"] +
                ";true;" +
                stringFor.Match(programStrings[index]).Groups["IteratorChanger"]));
            }
            else
            {
                parsedCode.Add(new ParsedStr("START FOR", stringFor.Match(programStrings[index]).Groups["Iterators"] +
                ";" + stringFor.Match(programStrings[index]).Groups["ConditionFOR"] + ";" +
                stringFor.Match(programStrings[index]).Groups["IteratorChanger"]));
            }
            callCycle.Push("FOR");
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

        private void CheckAndCommentForeachConstruction(ref int index)
        {
            callsConstructions.Push("FOR");
            int countStr = countStrAfterConstruction.Pop();
            countStrAfterConstruction.Push(++countStr);
            countStrAfterConstruction.Push(0);

            parsedCode.Add(new ParsedStr("START FOR", stringForeach.Match(programStrings[index]).Groups["TypeVariable"] + " " +
                stringForeach.Match(programStrings[index]).Groups["NameVariable"] + " = " +
                stringForeach.Match(programStrings[index]).Groups["Collection"] + "[0];" +
                stringForeach.Match(programStrings[index]).Groups["NameVariable"] + " != " +
                stringForeach.Match(programStrings[index]).Groups["Collection"] + "[" +
                stringForeach.Match(programStrings[index]).Groups["Collection"] + ".Length - 1];" + stringForeach.Match(programStrings[index]).Groups["NameVariable"] + "++"));
            callCycle.Push("FOR");
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
                        checkEndConstruction = true;
                        callCycle.Pop();
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
                        callCycle.Pop();
                        break;
                    }
                case "DWHILE":
                    {
                        checkEndConstruction = true;
                        parsedCode.Add(new ParsedStr("END " + callsConstructions.Pop(), "" + stringDWhile.Match(programStrings[++index]).Groups["ConditionDWHILE"]));
                        Brackets.Pop();
                        countStrAfterConstruction.Pop();
                        callCycle.Pop();
                        return;
                    }
                case "SWITCH":
                    {
                        if (callSwitchConstructions.Count != 0)
                        {
                            callSwitch = callSwitchConstructions.Pop();
                        }
                        else
                        {
                            callSwitch.Clear();
                        }

                        checkEndConstruction = true;
                        break;
                    }
                case "ELSE":
                    {
                        checkEndConstruction = true;
                        break;
                    }
                case "SIMPLE":
                    {
                        callsConstructions.Pop();
                        break;
                    }
            }
            if (checkEndConstruction == true)
            {
                parsedCode.Add(new ParsedStr("END " + callsConstructions.Pop(), ""));

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
                        checkEndConstruction = true;
                        callCycle.Pop();
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
                        callCycle.Pop();
                        break;
                    }
                case "ELSE":
                    {
                        checkEndConstruction = true;
                        break;
                    }
            }
            if (checkEndConstruction == true)
            {
                parsedCode.Add(new ParsedStr("END " + callsConstructions.Pop(), ""));
                Brackets.Pop();
                countStrAfterConstruction.Pop();
            }
        }

        public void ShowCode()
        {
            foreach (ParsedStr s in parsedCode)
            {
                s.Show();
            }
        }
    }
}
