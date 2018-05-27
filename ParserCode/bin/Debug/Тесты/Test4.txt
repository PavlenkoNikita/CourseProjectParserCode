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
                    if (codeProgramString.Length != 0)
                    {
                        while (codeProgramString[0] == ' ')
                        {
                            codeProgramString = codeProgramString.Remove(0, 1);
                            if (codeProgramString.Length == 0)
                            {
                                break;
                            }
                        }
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

                    if (codeProgramString.Length != 0)
                    {
                        while (codeProgramString[0] == ' ')
                        {
                            codeProgramString = codeProgramString.Remove(0, 1);
                            if (codeProgramString.Length == 0)
                            {
                                break;
                            }
                        }
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

                    if (codeProgramString.Length != 0)
                    {
                        while (codeProgramString[0] == ' ')
                        {
                            codeProgramString = codeProgramString.Remove(0, 1);
                            if (codeProgramString.Length == 0)
                            {
                                break;
                            }
                        }
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

                    if (codeProgramString.Length != 0)
                    {
                        while (codeProgramString[0] == ' ')
                        {
                            codeProgramString = codeProgramString.Remove(0, 1);
                            if (codeProgramString.Length == 0)
                            {
                                break;
                            }
                        }
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