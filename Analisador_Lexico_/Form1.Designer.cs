namespace Analisador_Lexico_
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bt_Browse = new System.Windows.Forms.Button();
            this.textBox_File = new System.Windows.Forms.TextBox();
            this.bt_Analize = new System.Windows.Forms.Button();
            this.richTextBox_File = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // bt_Browse
            // 
            this.bt_Browse.Location = new System.Drawing.Point(713, 13);
            this.bt_Browse.Name = "bt_Browse";
            this.bt_Browse.Size = new System.Drawing.Size(75, 23);
            this.bt_Browse.TabIndex = 0;
            this.bt_Browse.Text = "Browse";
            this.bt_Browse.UseVisualStyleBackColor = true;
            this.bt_Browse.Click += new System.EventHandler(this.bt_abrir_arquivo_Click);
            // 
            // textBox_File
            // 
            this.textBox_File.Location = new System.Drawing.Point(12, 417);
            this.textBox_File.Multiline = true;
            this.textBox_File.Name = "textBox_File";
            this.textBox_File.Size = new System.Drawing.Size(695, 21);
            this.textBox_File.TabIndex = 1;
            this.textBox_File.Visible = false;
            // 
            // bt_Analize
            // 
            this.bt_Analize.Location = new System.Drawing.Point(713, 42);
            this.bt_Analize.Name = "bt_Analize";
            this.bt_Analize.Size = new System.Drawing.Size(75, 23);
            this.bt_Analize.TabIndex = 2;
            this.bt_Analize.Text = "Analize";
            this.bt_Analize.UseVisualStyleBackColor = true;
            // 
            // richTextBox_File
            // 
            this.richTextBox_File.Location = new System.Drawing.Point(12, 12);
            this.richTextBox_File.Name = "richTextBox_File";
            this.richTextBox_File.Size = new System.Drawing.Size(695, 399);
            this.richTextBox_File.TabIndex = 3;
            this.richTextBox_File.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.richTextBox_File);
            this.Controls.Add(this.bt_Analize);
            this.Controls.Add(this.textBox_File);
            this.Controls.Add(this.bt_Browse);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Compiladores: Analisador Léxico";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_Browse;
        private System.Windows.Forms.TextBox textBox_File;
        private System.Windows.Forms.Button bt_Analize;
        private System.Windows.Forms.RichTextBox richTextBox_File;
    }
}

