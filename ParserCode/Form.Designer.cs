namespace ParserCode
{
    partial class Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Button_OpenFile = new System.Windows.Forms.Button();
            this.Label_CodeProgram = new System.Windows.Forms.Label();
            this.Button_SaveFile = new System.Windows.Forms.Button();
            this.OpenFile = new System.Windows.Forms.OpenFileDialog();
            this.LB_CodeProgram = new System.Windows.Forms.ListBox();
            this.LB_ParsedCode = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Button_ParseCode = new System.Windows.Forms.Button();
            this.SaveFile = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // Button_OpenFile
            // 
            this.Button_OpenFile.Location = new System.Drawing.Point(8, 28);
            this.Button_OpenFile.Name = "Button_OpenFile";
            this.Button_OpenFile.Size = new System.Drawing.Size(111, 35);
            this.Button_OpenFile.TabIndex = 0;
            this.Button_OpenFile.Text = "Выбрать файл";
            this.Button_OpenFile.UseVisualStyleBackColor = true;
            this.Button_OpenFile.Click += new System.EventHandler(this.Button_OpenFile_Click);
            // 
            // Label_CodeProgram
            // 
            this.Label_CodeProgram.AutoSize = true;
            this.Label_CodeProgram.Location = new System.Drawing.Point(231, 12);
            this.Label_CodeProgram.Name = "Label_CodeProgram";
            this.Label_CodeProgram.Size = new System.Drawing.Size(88, 13);
            this.Label_CodeProgram.TabIndex = 2;
            this.Label_CodeProgram.Text = "Код программы";
            // 
            // Button_SaveFile
            // 
            this.Button_SaveFile.Location = new System.Drawing.Point(8, 142);
            this.Button_SaveFile.Name = "Button_SaveFile";
            this.Button_SaveFile.Size = new System.Drawing.Size(111, 35);
            this.Button_SaveFile.TabIndex = 5;
            this.Button_SaveFile.Text = "Сохранить разобранный код";
            this.Button_SaveFile.UseVisualStyleBackColor = true;
            this.Button_SaveFile.Click += new System.EventHandler(this.Button_SaveFile_Click);
            // 
            // OpenFile
            // 
            this.OpenFile.FileName = "openFileDialog1";
            // 
            // LB_CodeProgram
            // 
            this.LB_CodeProgram.FormattingEnabled = true;
            this.LB_CodeProgram.HorizontalScrollbar = true;
            this.LB_CodeProgram.Location = new System.Drawing.Point(125, 28);
            this.LB_CodeProgram.Name = "LB_CodeProgram";
            this.LB_CodeProgram.ScrollAlwaysVisible = true;
            this.LB_CodeProgram.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.LB_CodeProgram.Size = new System.Drawing.Size(304, 316);
            this.LB_CodeProgram.TabIndex = 6;
            // 
            // LB_ParsedCode
            // 
            this.LB_ParsedCode.FormattingEnabled = true;
            this.LB_ParsedCode.HorizontalScrollbar = true;
            this.LB_ParsedCode.Location = new System.Drawing.Point(435, 28);
            this.LB_ParsedCode.Name = "LB_ParsedCode";
            this.LB_ParsedCode.ScrollAlwaysVisible = true;
            this.LB_ParsedCode.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.LB_ParsedCode.Size = new System.Drawing.Size(304, 316);
            this.LB_ParsedCode.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(507, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Разобранный код программы";
            // 
            // Button_ParseCode
            // 
            this.Button_ParseCode.Location = new System.Drawing.Point(8, 85);
            this.Button_ParseCode.Name = "Button_ParseCode";
            this.Button_ParseCode.Size = new System.Drawing.Size(111, 35);
            this.Button_ParseCode.TabIndex = 9;
            this.Button_ParseCode.Text = "Разобрать код";
            this.Button_ParseCode.UseVisualStyleBackColor = true;
            this.Button_ParseCode.Click += new System.EventHandler(this.Button_ParseCode_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 349);
            this.Controls.Add(this.Button_ParseCode);
            this.Controls.Add(this.LB_ParsedCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LB_CodeProgram);
            this.Controls.Add(this.Button_SaveFile);
            this.Controls.Add(this.Label_CodeProgram);
            this.Controls.Add(this.Button_OpenFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form";
            this.Text = "Лексический разбор кода";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_OpenFile;
        private System.Windows.Forms.Label Label_CodeProgram;
        private System.Windows.Forms.Button Button_SaveFile;
        private System.Windows.Forms.OpenFileDialog OpenFile;
        private System.Windows.Forms.ListBox LB_CodeProgram;
        private System.Windows.Forms.ListBox LB_ParsedCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Button_ParseCode;
        private System.Windows.Forms.FolderBrowserDialog SaveFile;
    }
}