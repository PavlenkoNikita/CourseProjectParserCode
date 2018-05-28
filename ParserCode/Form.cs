using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParserCode
{
    public partial class Form : System.Windows.Forms.Form
    {
        InputCode input;
        GetStringsCode shareCode;
        CodeDescription parsingCode;
        OutputCodDescription output;
        bool isFileChosen = false;
        bool isParsedCode = false;

        public Form()
        {
            InitializeComponent();
        }

        private void Button_OpenFile_Click(object sender, EventArgs e)
        {
            OpenFile.FileName = "Program";
            OpenFile.Filter = "Text Files(*.cs) | *.cs";
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    input = new InputCode();
                    shareCode = new GetStringsCode();
                    LB_CodeProgram.Items.Clear();
                    LB_ParsedCode.Items.Clear();
                    input.ReadFile(OpenFile.FileName);
                    shareCode.shareString(input.codeString);
                    shareCode.Show(LB_CodeProgram);
                    isFileChosen = true;
                    isParsedCode = false;
                }
                catch (MyException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }
            }
        }

        private void Button_ParseCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (isFileChosen == true)
                {
                    LB_ParsedCode.Items.Clear();
                    parsingCode = new CodeDescription(shareCode.linesProgram);
                    parsingCode.LineReading();
                    parsingCode.ShowCode(LB_ParsedCode);
                    isParsedCode = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void Button_SaveFile_Click(object sender, EventArgs e)
        {
            if (isParsedCode == true)
            {
                try
                {
                    output = new OutputCodDescription(parsingCode.parsedCode);
                    if (SaveFile.ShowDialog() == DialogResult.OK)
                    {
                        output.WriteInFile(SaveFile.SelectedPath);
                    }
                }
                catch (MyException ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }
            }
        }
    }
}
