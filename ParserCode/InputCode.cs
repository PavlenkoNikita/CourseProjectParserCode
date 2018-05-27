using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserCode
{
    class InputCode
    {
        public List<string> codeList { get; private set; }
        public string codeString { get; private set; }

        public InputCode()
        {
            codeList = new List<string>();
        }

        public void ReadFile(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    codeList.Add(line);
                }
            }
            if (codeList.Count == 0) throw new MyException("Файл пуст");
            DeleteCommentary();
        }

        private void DeleteCommentary()
        {
            bool IsString = false;
            for (int i = 0; i < codeList.Count; i++)
            {
                for (int j = 0; j < codeList[i].Length; j++)
                {
                    if ((codeList[i])[j] == '\'')
                    {
                        if (IsString == true)
                        {
                            IsString = false;
                        }
                        else
                        {
                            IsString = true;
                        }
                    }
                    else if (j + 1 != (codeList[i]).Length && IsString == false)
                    {
                        if ((codeList[i])[j] == '/' && (codeList[i])[j + 1] == '/')
                        {
                            codeList[i] = codeList[i].Remove(j);
                            if (codeList[i].Length == 0)
                            {
                                codeList.Remove(codeList[i]);
                                i--;
                            }
                            break;
                        }
                    }
                }
            }
            ConnectCodeList();
            bool IsCommentary = false;

            for (int i = 0; i < codeString.Length; i++)
            {
                if (IsCommentary == false)
                {
                    if (codeString[i] == '"')
                    {
                        if (IsString == true)
                        {
                            IsString = false;
                        }
                        else
                        {
                            IsString = true;
                        }
                    }
                }
                else if (IsCommentary == true)
                {
                    if (i + 1 != codeString.Length)
                    {
                        if (codeString[i] == '*' && codeString[i + 1] == '/')
                        {
                            IsCommentary = false;
                            codeString = codeString.Remove(i, 2);
                            if (i == 0)
                            {
                                i = -1;
                            }
                            else
                            {
                                i--;
                            }
                            continue;
                        }
                        codeString = codeString.Remove(i, 1);
                        i--;
                        continue;
                    }
                }
                if (i + 1 != codeString.Length && IsString == false)
                {
                    if (codeString[i] == '/' && codeString[i + 1] == '*')
                    {
                        IsCommentary = true;
                        codeString = codeString.Remove(i, 2);
                        if (i == 0)
                        {
                            i = -1;
                        }
                        else
                        {
                            i--;
                        }
                        continue;
                    }
                }
            }
        }

        private void ConnectCodeList()
        {
            foreach (string s in codeList)
            {
                codeString += s;
            }
        }
    }
}
