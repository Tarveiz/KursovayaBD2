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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //string sql = $"SELECT Name FROM [User]";
            //SqlCommand cmd = new SqlCommand(sql, conn);

            //using (DbDataReader reader = cmd.ExecuteReader())
            //{
            //    if (reader.HasRows)
            //    {
            //        while (reader.Read())
            //        {
            //            int nameIndex = reader.GetOrdinal("Name");
            //            string name = reader.GetString(nameIndex);

            //            //userSelection.Items.Add(name);
            //        }
            //    }
            //}


            //string sql = $"DELETE FROM Director WHERE Forename = '{item}';";
            //SqlCommand cmd = new SqlCommand(sql, conn);
            //cmd.ExecuteNonQuery();
            int a;
            if (int.TryParse(textBox1.Text, out a)) {
                Console.WriteLine("Педик");
            }
            else
            {
                Console.WriteLine("Не Педик");
            }
           
        }
    }
}
