namespace SimpleSQL.TestApplication
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
            this.tbRegex = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tabContainer = new System.Windows.Forms.TabControl();
            this.tabGeneralCommand = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.tbArquivos = new System.Windows.Forms.TextBox();
            this.tabQuery = new System.Windows.Forms.TabPage();
            this.tbQueryRegistros = new System.Windows.Forms.TextBox();
            this.tbQueryFim = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbQueryInicio = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tbQuery = new System.Windows.Forms.TextBox();
            this.btRunQuery = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btConectar = new System.Windows.Forms.Button();
            this.tbAWSDomain = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbAWSSecretKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbAWSAccessKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabContainer.SuspendLayout();
            this.tabGeneralCommand.SuspendLayout();
            this.tabQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbRegex
            // 
            this.tbRegex.Location = new System.Drawing.Point(192, 539);
            this.tbRegex.Name = "tbRegex";
            this.tbRegex.Size = new System.Drawing.Size(501, 20);
            this.tbRegex.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 219);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(687, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Realizar Carga";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(6, 248);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(687, 182);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(434, 565);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(259, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabContainer
            // 
            this.tabContainer.Controls.Add(this.tabPage1);
            this.tabContainer.Controls.Add(this.tabGeneralCommand);
            this.tabContainer.Controls.Add(this.tabQuery);
            this.tabContainer.Location = new System.Drawing.Point(12, 12);
            this.tabContainer.Name = "tabContainer";
            this.tabContainer.SelectedIndex = 0;
            this.tabContainer.Size = new System.Drawing.Size(707, 620);
            this.tabContainer.TabIndex = 5;
            // 
            // tabGeneralCommand
            // 
            this.tabGeneralCommand.Controls.Add(this.label7);
            this.tabGeneralCommand.Controls.Add(this.tbArquivos);
            this.tabGeneralCommand.Controls.Add(this.tbRegex);
            this.tabGeneralCommand.Controls.Add(this.button2);
            this.tabGeneralCommand.Controls.Add(this.richTextBox1);
            this.tabGeneralCommand.Controls.Add(this.button1);
            this.tabGeneralCommand.Location = new System.Drawing.Point(4, 22);
            this.tabGeneralCommand.Name = "tabGeneralCommand";
            this.tabGeneralCommand.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneralCommand.Size = new System.Drawing.Size(699, 594);
            this.tabGeneralCommand.TabIndex = 0;
            this.tabGeneralCommand.Text = "Carga";
            this.tabGeneralCommand.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Arquivos para carregar";
            // 
            // tbArquivos
            // 
            this.tbArquivos.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.tbArquivos.Location = new System.Drawing.Point(6, 16);
            this.tbArquivos.Multiline = true;
            this.tbArquivos.Name = "tbArquivos";
            this.tbArquivos.Size = new System.Drawing.Size(687, 194);
            this.tbArquivos.TabIndex = 5;
            // 
            // tabQuery
            // 
            this.tabQuery.Controls.Add(this.tbQueryRegistros);
            this.tabQuery.Controls.Add(this.tbQueryFim);
            this.tabQuery.Controls.Add(this.label3);
            this.tabQuery.Controls.Add(this.label2);
            this.tabQuery.Controls.Add(this.label1);
            this.tabQuery.Controls.Add(this.tbQueryInicio);
            this.tabQuery.Controls.Add(this.dataGridView1);
            this.tabQuery.Controls.Add(this.tbQuery);
            this.tabQuery.Controls.Add(this.btRunQuery);
            this.tabQuery.Location = new System.Drawing.Point(4, 22);
            this.tabQuery.Name = "tabQuery";
            this.tabQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuery.Size = new System.Drawing.Size(699, 594);
            this.tabQuery.TabIndex = 1;
            this.tabQuery.Text = "Query";
            this.tabQuery.UseVisualStyleBackColor = true;
            // 
            // tbQueryRegistros
            // 
            this.tbQueryRegistros.Location = new System.Drawing.Point(571, 87);
            this.tbQueryRegistros.Name = "tbQueryRegistros";
            this.tbQueryRegistros.ReadOnly = true;
            this.tbQueryRegistros.Size = new System.Drawing.Size(122, 20);
            this.tbQueryRegistros.TabIndex = 8;
            // 
            // tbQueryFim
            // 
            this.tbQueryFim.Location = new System.Drawing.Point(571, 61);
            this.tbQueryFim.Name = "tbQueryFim";
            this.tbQueryFim.ReadOnly = true;
            this.tbQueryFim.Size = new System.Drawing.Size(122, 20);
            this.tbQueryFim.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(517, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Registros";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(542, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Fim";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(531, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Início";
            // 
            // tbQueryInicio
            // 
            this.tbQueryInicio.Location = new System.Drawing.Point(571, 35);
            this.tbQueryInicio.Name = "tbQueryInicio";
            this.tbQueryInicio.ReadOnly = true;
            this.tbQueryInicio.Size = new System.Drawing.Size(122, 20);
            this.tbQueryInicio.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 140);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(687, 451);
            this.dataGridView1.TabIndex = 2;
            // 
            // tbQuery
            // 
            this.tbQuery.Location = new System.Drawing.Point(6, 6);
            this.tbQuery.Multiline = true;
            this.tbQuery.Name = "tbQuery";
            this.tbQuery.Size = new System.Drawing.Size(460, 128);
            this.tbQuery.TabIndex = 1;
            // 
            // btRunQuery
            // 
            this.btRunQuery.Location = new System.Drawing.Point(472, 6);
            this.btRunQuery.Name = "btRunQuery";
            this.btRunQuery.Size = new System.Drawing.Size(221, 23);
            this.btRunQuery.TabIndex = 0;
            this.btRunQuery.Text = "Fetch!";
            this.btRunQuery.UseVisualStyleBackColor = true;
            this.btRunQuery.Click += new System.EventHandler(this.btRunQuery_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.btConectar);
            this.tabPage1.Controls.Add(this.tbAWSDomain);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.tbAWSSecretKey);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.tbAWSAccessKey);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(699, 594);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Credenciais";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btConectar
            // 
            this.btConectar.Location = new System.Drawing.Point(6, 125);
            this.btConectar.Name = "btConectar";
            this.btConectar.Size = new System.Drawing.Size(355, 23);
            this.btConectar.TabIndex = 6;
            this.btConectar.Text = "Conectar";
            this.btConectar.UseVisualStyleBackColor = true;
            this.btConectar.Click += new System.EventHandler(this.btConectar_Click);
            // 
            // tbAWSDomain
            // 
            this.tbAWSDomain.Location = new System.Drawing.Point(6, 99);
            this.tbAWSDomain.Name = "tbAWSDomain";
            this.tbAWSDomain.Size = new System.Drawing.Size(355, 20);
            this.tbAWSDomain.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "AWS Domain";
            // 
            // tbAWSSecretKey
            // 
            this.tbAWSSecretKey.Location = new System.Drawing.Point(6, 58);
            this.tbAWSSecretKey.Name = "tbAWSSecretKey";
            this.tbAWSSecretKey.Size = new System.Drawing.Size(355, 20);
            this.tbAWSSecretKey.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "AWS Secret Key";
            // 
            // tbAWSAccessKey
            // 
            this.tbAWSAccessKey.Location = new System.Drawing.Point(6, 19);
            this.tbAWSAccessKey.Name = "tbAWSAccessKey";
            this.tbAWSAccessKey.Size = new System.Drawing.Size(355, 20);
            this.tbAWSAccessKey.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "AWS Access Key";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(367, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "label8";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 644);
            this.Controls.Add(this.tabContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "SimpleSQL - Scratchpad";
            this.tabContainer.ResumeLayout(false);
            this.tabGeneralCommand.ResumeLayout(false);
            this.tabGeneralCommand.PerformLayout();
            this.tabQuery.ResumeLayout(false);
            this.tabQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbRegex;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabContainer;
        private System.Windows.Forms.TabPage tabGeneralCommand;
        private System.Windows.Forms.TabPage tabQuery;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox tbQuery;
        private System.Windows.Forms.Button btRunQuery;
        private System.Windows.Forms.TextBox tbQueryRegistros;
        private System.Windows.Forms.TextBox tbQueryFim;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbQueryInicio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btConectar;
        private System.Windows.Forms.TextBox tbAWSDomain;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbAWSSecretKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbAWSAccessKey;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbArquivos;
        private System.Windows.Forms.Label label8;
    }
}

