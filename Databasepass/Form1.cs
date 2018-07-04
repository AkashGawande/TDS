using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Databasepass
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string Con = "";
        OleDbConnection cn;
        OleDbCommand cmd;
        DataTable dt = new DataTable();
        OleDbDataAdapter od;
        private void Form1_Load(object sender, EventArgs e)
        {
            Con = GetConnection();
            cn = new OleDbConnection(Con);
            showdata("");
        }


        public void showdata(string Empid)
        {
            string sql = "";
            DataSet ds = new DataSet();

            if (Empid == "")
            {
                sql = "select * from Test";
            }
            else
            {
                sql = "select * from Test where ID=" + Empid + "";
            }
            cmd = new OleDbCommand(sql, cn);
            od = new OleDbDataAdapter(cmd);
            od.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }
        }
        public void Cleartext()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        public string GetConnection()
        {
            string path = Environment.CurrentDirectory.ToString();

            if (path.Contains("\\bin\\Debug"))
            {
                path = System.IO.Directory.GetCurrentDirectory().Replace("\\bin\\Debug", "");
            }
            else if (path.Contains("\\bin\\x86\\Debug"))
            {
                path = System.IO.Directory.GetCurrentDirectory().Replace("\\bin\\x86\\Debug", "");

            }
            if (path.Contains("\\bin\\Release"))
            {
                path = System.IO.Directory.GetCurrentDirectory().Replace("\\bin\\Release", "");
            }
            else if (path.Contains("\\bin\\x86\\Release"))
            {
                path = System.IO.Directory.GetCurrentDirectory().Replace("\\bin\\x86\\Release", "");

            }
            //   path = System.IO.Directory.GetCurrentDirectory().Replace("\\bin\\Debug", "");
           // return @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + "\\DB\\New.accdb;Jet OLEDB:Database Password=123";
            return @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\\DESKTOP-3IQE237\\Akash\\DB\\New.accdb;Jet OLEDB:Database Password=123";
        }



        private void add(string name, string Salry)
        {
            //SQL STMT
            string sql = "INSERT INTO Test(Name,LName) VALUES(@Name,@LName)";
            cmd = new OleDbCommand(sql, cn);

            //ADD PARAMS
            //cmd.Parameters.AddWithValue("@NAME", name);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@LName", Salry);

            //OPEN CON AND EXEC INSERT
            try
            {
                cn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show(@"Successfully Inserted");
                    showdata("");
                    Cleartext();
                }
                cn.Close();
                //retrieve();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                add(textBox1.Text, textBox2.Text);
            }
            catch (Exception ex)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showdata(textBox3.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showdata("");
        }
    }
}
