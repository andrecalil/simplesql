using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using SimpleSQL.Domain;
using System.IO;

namespace SimpleSQL.TestApplication
{
    public partial class Form1 : Form
    {
        private SimpleSQL.ClientInterface.Connection aConnection;

        public Form1()
        {
            InitializeComponent();

            this.aConnection = new ClientInterface.Connection("yourcredentials", "forAWS", "your-domain");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.aConnection.GetCommand<Insert>(this.tbRegex.Text);
            List<FileStream> mFilesToInsert = new List<FileStream>
            {
                //File.OpenRead(@"D:\Dropbox\TCC\Produção\Experimentos\queries\candidato_parte_1.sql")
                //File.OpenRead(@"D:\Dropbox\TCC\Produção\Experimentos\queries\candidato.sql"),
                //File.OpenRead(@"D:\Dropbox\TCC\Produção\Experimentos\queries\estabelecimentoEnsino.sql"),
                //File.OpenRead(@"D:\Dropbox\TCC\Produção\Experimentos\queries\candidatoClassificado.sql"),
                File.OpenRead(@"D:\Dropbox\TCC\Produção\Experimentos\carga\opcaoCandidato_simplesql_parte_1.txt"),
                File.OpenRead(@"D:\Dropbox\TCC\Produção\Experimentos\carga\candidato_simplesql_restante.txt")
                //File.OpenRead(@"D:\Dropbox\TCC\Produção\Experimentos\queries\opcaoCandidato.sql")
            };

            StringBuilder mResult = new StringBuilder();
            int mTotalInsert = 0;
            StreamReader mReader;

            foreach (FileStream mCurrentFile in mFilesToInsert)
            {
                mResult.AppendLine(string.Format("Início: {0}", DateTime.Now.ToString()));

                mReader = new StreamReader(mCurrentFile);

                while (!mReader.EndOfStream)
                {
                    this.aConnection.ExecuteNonQuery(mReader.ReadLine());

                    mTotalInsert++;
                }

                mResult.AppendLine(string.Format("{0}: {1}", mCurrentFile.Name, mTotalInsert));

                mResult.AppendLine(string.Format("Fim: {0}", DateTime.Now.ToString()));

                mTotalInsert = 0;

                mReader.Close();
                mReader.Dispose();

                this.richTextBox1.Text = mResult.ToString();
                Application.DoEvents();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Regex expressao = new Regex("^(.*(?<teste>(aaa))+.*)+$", RegexOptions.Compiled);
            if (expressao.IsMatch(this.tbRegex.Text))
            {
                MatchCollection matches = expressao.Matches(this.tbRegex.Text);
                string b = matches[0].Groups["teste"].Captures[0].Value;
            }
        }

        private void btRunQuery_Click(object sender, EventArgs e)
        {
            //StringBuilder mClock = new StringBuilder(this.richTextBox1.Text);
            this.tbQueryInicio.Text = DateTime.Now.ToString();
            //mClock.AppendLine(string.Format("Início: {0}", DateTime.Now.ToString()));
            this.dataGridView1.DataSource = this.aConnection.ExecuteQuery(this.tbQuery.Text.Replace("\r\n",""));
            //mClock.AppendLine(string.Format("{0} lines", this.dataGridView1.Rows.Count));
            //mClock.AppendLine(string.Format("Fim: {0}", DateTime.Now.ToString()));
            this.tbQueryRegistros.Text = this.dataGridView1.Rows.Count.ToString();
            this.tbQueryFim.Text = DateTime.Now.ToString();
            //this.richTextBox1.Text = mClock.ToString();
        }
    }
}