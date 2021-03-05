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

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                dataGridView2.Rows.Clear();

                string sql = "";
                int numColumn = 0;

                switch (comboBox3.SelectedIndex)
                {
                    case 0:
                        numColumn = 1;
                        sql = "SELECT Дата_основания FROM Учет_Кафедра AS T1 WHERE Дата_основания = (SELECT Год_основания FROM Издательство AS T2 WHERE T1.Издатели = T2.Название_издательства)";
                        break;
                    case 1:
                        numColumn = 1;
                        sql = "SELECT dbo.NonColiWher(0)";
                        break;
                    case 2:
                        numColumn = 1;
                        sql = "SELECT Номера_работ FROM (SELECT Учет_Кафедра.Номера_работ, Работы.Тема, Работы.Номер_Работы, Учет_Кафедра.ID_Преподавателя FROM Учет_Кафедра, Работы WHERE ID_Преподавателя = Номер_Работы) AS R1, Преподаватели AS R2 WHERE R1.ID_Преподавателя = R2.ID_Преподавателя";
                        break;
                    case 3:
                        numColumn = 4;
                        sql = "SELECT * FROM Издательство WHERE Название_издательства = '1C'";
                        break;
                    case 4:
                        numColumn = 2;
                        sql = "SELECT Название_издательства,(SELECT Издатели FROM Учет_Кафедра WHERE Z.Название_издательства = Издатели) AS A FROM Издательство AS Z";
                        break;
                    case 5:
                        numColumn = 1;
                        sql = "SELECT Издатели FROM Учет_Кафедра WHERE ID_Преподавателя = '2'";
                        break;
                    case 6:
                        numColumn = 2;
                        sql = "DECLARE @J NVARCHAR(30);SET @J = 100;SELECT DISTINCT COUNT(ID_Преподавателя), AVG(Номер_Работы) FROM Учет_Кафедра, Работы WHERE Номер_Работы > 0 GROUP BY ID_Преподавателя, Номер_Работы HAVING Номер_Работы < @J";
                        break;
                }

                SqlCommand cmd = new SqlCommand(sql, conn);
                richTextBox1.Text = sql;

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        List<string[]> data = new List<string[]>();

                        while (reader.Read())
                        {
                            data.Add(new string[numColumn]);

                            for (int i = 0; i < numColumn; i += 1)
                            {
                                data[data.Count - 1][i] = reader[i].ToString();
                            }
                        }

                        foreach (string[] s in data)
                        {
                            dataGridView2.Rows.Add(s);
                        }
                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 1:
                    comboBox3.Items.Add("Коррелированный WHERE");
                    comboBox3.Items.Add("Не коррелированный WHERE");
                    comboBox3.Items.Add("Коррелированный FROM");
                    comboBox3.Items.Add("Не коррелированный FROM");
                    comboBox3.Items.Add("Коррелированный SELECT");
                    comboBox3.Items.Add("Не коррелированный SELECT");
                    comboBox3.Items.Add("Выборка с HAVING");
                    break;
            }
        }
    }
}
