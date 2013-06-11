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
            this.tabContainer.SuspendLayout();
            this.tabGeneralCommand.SuspendLayout();
            this.tabQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbRegex
            // 
            this.tbRegex.Location = new System.Drawing.Point(10, 6);
            this.tbRegex.Name = "tbRegex";
            this.tbRegex.Size = new System.Drawing.Size(259, 20);
            this.tbRegex.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(259, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(10, 32);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(260, 182);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 251);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(259, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabContainer
            // 
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
            this.tabGeneralCommand.Controls.Add(this.tbRegex);
            this.tabGeneralCommand.Controls.Add(this.button2);
            this.tabGeneralCommand.Controls.Add(this.richTextBox1);
            this.tabGeneralCommand.Controls.Add(this.button1);
            this.tabGeneralCommand.Location = new System.Drawing.Point(4, 22);
            this.tabGeneralCommand.Name = "tabGeneralCommand";
            this.tabGeneralCommand.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneralCommand.Size = new System.Drawing.Size(699, 594);
            this.tabGeneralCommand.TabIndex = 0;
            this.tabGeneralCommand.Text = "General info";
            this.tabGeneralCommand.UseVisualStyleBackColor = true;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 644);
            this.Controls.Add(this.tabContainer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabContainer.ResumeLayout(false);
            this.tabGeneralCommand.ResumeLayout(false);
            this.tabGeneralCommand.PerformLayout();
            this.tabQuery.ResumeLayout(false);
            this.tabQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
    }
}

