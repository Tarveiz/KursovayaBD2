using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;

namespace KursovayaBD2
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static SqlConnection
        GetDBConnection(string username, string password)
        {
            string datasource = @"DESKTOP-55LFU6J\SQLEXPRESS";
            string database = "Kursovaya";

            // Login: Tarveiz   
            // Password: 1234

            //string connString = @"Data Source=" + datasource + ";Initial Catalog="
            //+ database + ";User ID=" + username + ";Password=" + password;
            string connString = @"Data Source=" + datasource + ";Initial Catalog=" + database + ";Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);

            return conn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (login.Text != "" && password.Text != "")
            {
                SqlConnection conn = GetDBConnection(login.Text, password.Text);

                Console.WriteLine("Getting Connection ...");
                try
                {
                    Console.WriteLine("Openning Connection ...");

                    conn.Open();

                    Console.WriteLine("Connection successful!");



                    Form2 form2 = new Form2();
                    form2.Show();
                    //if (login.Text.Equals("Tarveiz") && password.Text.Equals("1234"))
                    //{
                    //    Form1 form1 = new Form1(conn);
                    //    form1.Show();
                    //}
                    //else
                    //{
                    //    Form2 form2 = new Form2(conn);
                    //    form2.Show();
                    //}
                    this.Hide();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                Console.Read();
            }
            else
            {
                Console.WriteLine("Ошибка: Поля не заполнены.");
            }
        }
    }
}
