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
            this.textBox_Input = new System.Windows.Forms.TextBox();
            this.bt_Analize = new System.Windows.Forms.Button();
            this.textBox_Tokens = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.abrirArquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analisarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvarSaídaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_Browse
            // 
            this.bt_Browse.Location = new System.Drawing.Point(3, 3);
            this.bt_Browse.Name = "bt_Browse";
            this.bt_Browse.Size = new System.Drawing.Size(68, 23);
            this.bt_Browse.TabIndex = 0;
            this.bt_Browse.Text = "Browse";
            this.bt_Browse.UseVisualStyleBackColor = true;
            this.bt_Browse.Click += new System.EventHandler(this.bt_abrir_arquivo_Click);
            // 
            // textBox_Input
            // 
            this.textBox_Input.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_Input.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Input.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Input.Location = new System.Drawing.Point(23, 3);
            this.textBox_Input.Multiline = true;
            this.textBox_Input.Name = "textBox_Input";
            this.textBox_Input.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Input.Size = new System.Drawing.Size(402, 415);
            this.textBox_Input.TabIndex = 1;
            this.textBox_Input.Text = "Insira seu código aqui ou abra o de um arquivo .txt.";
            // 
            // bt_Analize
            // 
            this.bt_Analize.Location = new System.Drawing.Point(3, 53);
            this.bt_Analize.Name = "bt_Analize";
            this.bt_Analize.Size = new System.Drawing.Size(68, 23);
            this.bt_Analize.TabIndex = 2;
            this.bt_Analize.Text = "Analize";
            this.bt_Analize.UseVisualStyleBackColor = true;
            this.bt_Analize.Click += new System.EventHandler(this.bt_Analize_Click);
            // 
            // textBox_Tokens
            // 
            this.textBox_Tokens.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_Tokens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Tokens.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Tokens.Location = new System.Drawing.Point(451, 3);
            this.textBox_Tokens.Multiline = true;
            this.textBox_Tokens.Name = "textBox_Tokens";
            this.textBox_Tokens.ReadOnly = true;
            this.textBox_Tokens.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Tokens.Size = new System.Drawing.Size(402, 415);
            this.textBox_Tokens.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel1.Controls.Add(this.textBox_Input, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox_Tokens, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(877, 421);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.bt_Browse, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.bt_Analize, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(865, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(74, 100);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirArquivoToolStripMenuItem,
            this.analisarToolStripMenuItem,
            this.salvarSaídaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(877, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // abrirArquivoToolStripMenuItem
            // 
            this.abrirArquivoToolStripMenuItem.Name = "abrirArquivoToolStripMenuItem";
            this.abrirArquivoToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.abrirArquivoToolStripMenuItem.Text = "Abrir Arquivo";
            this.abrirArquivoToolStripMenuItem.Click += new System.EventHandler(this.bt_abrir_arquivo_Click);
            // 
            // analisarToolStripMenuItem
            // 
            this.analisarToolStripMenuItem.Name = "analisarToolStripMenuItem";
            this.analisarToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.analisarToolStripMenuItem.Text = "Analisar";
            this.analisarToolStripMenuItem.Click += new System.EventHandler(this.bt_Analize_Click);
            // 
            // salvarSaídaToolStripMenuItem
            // 
            this.salvarSaídaToolStripMenuItem.Name = "salvarSaídaToolStripMenuItem";
            this.salvarSaídaToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.salvarSaídaToolStripMenuItem.Text = "Salvar Saída";
            this.salvarSaídaToolStripMenuItem.Click += new System.EventHandler(this.salvarSaídaToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 445);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Compiladores: Analisador Léxico";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_Browse;
        private System.Windows.Forms.TextBox textBox_Input;
        private System.Windows.Forms.Button bt_Analize;
        private System.Windows.Forms.TextBox textBox_Tokens;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem abrirArquivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analisarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salvarSaídaToolStripMenuItem;
    }
}

