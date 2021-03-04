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
    public partial class User : Form
    {
        SqlConnection conn;
        public User(SqlConnection connection)
        {
            InitializeComponent();
            conn = connection;

            string sql = "SELECT ФИО FROM Преподаватели";
            SqlCommand cmd = new SqlCommand(sql, conn);

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int fioIndex = reader.GetOrdinal("ФИО");
                        string fio = reader.GetString(fioIndex);
                        comboBox1.Items.Add(fio);
                    }
                }
            }
            comboBox2.Items.Add("Название издательства");
            comboBox2.Items.Add("Дата основания");
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string sql = $"SELECT CASE [Преподаватели].ФИО WHEN '{comboBox1.SelectedItem.ToString()}' THEN 'Преподаватель' ELSE ' ' END, U.*  FROM[Преподаватели], [Учет_Кафедра] as U WHERE[Преподаватели].ID_Преподавателя = [U].ID_Преподавателя"; 
            SqlCommand cmd = new SqlCommand(sql, conn);

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    List<string[]> data = new List<string[]>();

                    while (reader.Read())
                    {
                        data.Add(new string[6]);

                        data[data.Count - 1][0] = reader[0].ToString();
                        data[data.Count - 1][1] = reader[1].ToString();
                        data[data.Count - 1][2] = reader[2].ToString();
                        data[data.Count - 1][3] = reader[3].ToString();
                        data[data.Count - 1][4] = reader[4].ToString();
                        data[data.Count - 1][5] = reader[5].ToString();
                    }

                    foreach (string[] s in data)
                    {
                        dataGridView1.Rows.Add(s);
                    }
                }
            }
        }

     

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string sql;
            if (comboBox2.SelectedItem == null)
            {
                sql = $"SELECT * FROM [view]";
            }
            else
            {
                string sort = "";
                switch (comboBox2.SelectedItem.ToString())
                {
                    case "Название издательства":
                        sort = "Название_издательства";
                        break;
                    case "Дата основания":
                        sort = "Дата_основания";
                        break;
                }

                if (checkBox1.Checked == true)
                {
                    sort += " ASC";
                }
                else
                {
                    sort += " DESC";
                }
                sql = $"SELECT * FROM [view] ORDER BY {sort}";
            }
            SqlCommand cmd = new SqlCommand(sql, conn);

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    List<string[]> data = new List<string[]>();

                    while (reader.Read())
                    {
                        data.Add(new string[6]);

                        data[data.Count - 1][0] = reader[0].ToString();
                        data[data.Count - 1][1] = reader[1].ToString();
                        data[data.Count - 1][2] = reader[2].ToString();
                        data[data.Count - 1][3] = reader[3].ToString();
                        data[data.Count - 1][4] = reader[4].ToString();
                        data[data.Count - 1][5] = reader[5].ToString();
                    }

                    foreach (string[] s in data)
                    {
                        dataGridView1.Rows.Add(s);
                    }
                }
            }

        }
    }
}
