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
    public partial class Menu : Form
    {
        SqlConnection conn;
        public Menu(SqlConnection connection)
        {
            InitializeComponent();
            conn = connection;
        }

        //////////////////////////////////////////////////////////////////////
        /////////////////////Начало первой вкладки ///////////////////////////
        //////////////////////////////////////////////////////////////////////
        //1-ая вкладка://
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
            int bit = 0;
            if (publication.Checked) bit = 1; else bit = 0;
            string sql = $"INSERT INTO " +
                $"Преподаватели(ФИО, Контактные_данные, Публикации) " +
                $"VALUES('{FIO.Text}','{Konk.Text}',{bit});";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            FIO.Text = "";
            Konk.Text = "";
            publication.Checked = false;
        }









        //2-ая вкладка://
        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl3.SelectedIndex) {
                case 1:
                    string sql = $"SELECT ФИО FROM Преподаватели";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            comboBox1.Items.Clear();
                            while (reader.Read())
                            {
                                int nameIndex = reader.GetOrdinal("ФИО");
                                string name = reader.GetString(nameIndex);

                                comboBox1.Items.Add(name);
                            }
                        }
                    }
                    break;
                //+ к третьей вкладке//
                case 2:
                    sql = $"SELECT ФИО FROM Преподаватели";
                    cmd = new SqlCommand(sql, conn);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            comboBox2.Items.Clear();
                            while (reader.Read())
                            {
                                int nameIndex = reader.GetOrdinal("ФИО");
                                string name = reader.GetString(nameIndex);
                                comboBox2.Items.Add(name);
                            }
                        }
                    }
                    break;
                    //До сюда//
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = $"DELETE FROM Преподаватели WHERE ФИО = '{comboBox1.SelectedItem.ToString()}';";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            int currentIndex = comboBox1.SelectedIndex;
            comboBox1.Items.RemoveAt(currentIndex);
            comboBox1.SelectedIndex = -1;
        }




        //3-ья вкладка://
        private void button3_Click(object sender, EventArgs e)
        {
            string sql;
            SqlCommand cmd;
            if (comboBox2.SelectedItem != null && comboBox2.SelectedItem.ToString() != "")
            {
                int bit = 0;
                if (Public1.Checked) bit = 1; else bit = 0;
                sql = $"UPDATE Преподаватели SET ФИО = '{FIO1.Text}', Контактные_данные = '{Kont1.Text}', Публикации = {bit} WHERE ФИО = '{comboBox2.SelectedItem.ToString()}';";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }


            sql = $"SELECT ФИО FROM Преподаватели";
            cmd = new SqlCommand(sql, conn);
            int index=0;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    index = comboBox2.SelectedIndex;
                    comboBox2.Items.Clear();
                    while (reader.Read())
                    {
                        int nameIndex = reader.GetOrdinal("ФИО");
                        string name = reader.GetString(nameIndex);
                        comboBox2.Items.Add(name);
                    }
                }
            }
            comboBox2.SelectedIndex = index;

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FIO1.Text = comboBox2.SelectedItem.ToString();


            string sql = $"SELECT Контактные_данные, Публикации FROM Преподаватели WHERE ФИО = '{comboBox2.SelectedItem.ToString()}'";
            SqlCommand cmd = new SqlCommand(sql, conn);

            using (DbDataReader reader = cmd.ExecuteReader())
            {

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int contIndex = reader.GetOrdinal("Контактные_данные");
                        string cont = reader.GetString(contIndex);
                        int pubIndex = reader.GetOrdinal("Публикации");
                        bool pub = reader.GetBoolean(pubIndex);
                        Kont1.Text = cont;
                        Public1.Checked = pub;
                    }

                }
            }
        }


        //////////////////////////////////////////////////////////////////////
        ////////Конец первой первой вкладки -> Начало второй вкладки//////////
        //////////////////////////////////////////////////////////////////////

        //1-ая вкладка://

        private void button4_Click(object sender, EventArgs e)
        {
            string sql = $"INSERT INTO " +
                $"Издательство(Название_издательства, Сборники, Реквизиты, Год_основания) " +
                $"VALUES('{tx21Box1.Text}','{tx21Box2.Text}','{tx21Box3.Text}', '{dTP1.Value}');";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            tx21Box1.Text = "";
            tx21Box2.Text = "";
            tx21Box3.Text = "";
            dTP1.Value = DateTime.Now;
        }


        





        //2-ая вкладка://

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl2.SelectedIndex)
            {
                case 1:
                    string sql = $"SELECT Название_издательства FROM Издательство";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            comBox1.Items.Clear();
                            while (reader.Read())
                            {
                                int nameIndex = reader.GetOrdinal("Название_издательства");
                                string name = reader.GetString(nameIndex);

                                comBox1.Items.Add(name);
                            }
                        }
                    }
                    break;
                    //+ к третьей вкладке//
                case 2:
                    sql = $"SELECT Название_издательства FROM Издательство";
                    cmd = new SqlCommand(sql, conn);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            comBox2.Items.Clear();
                            while (reader.Read())
                            {
                                int nameIndex = reader.GetOrdinal("Название_издательства");
                                string name = reader.GetString(nameIndex);
                                comBox2.Items.Add(name);
                            }
                        }
                    }
                    break;
                    //До сюда//
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sql = $"DELETE FROM Издательство WHERE Название_издательства = '{comBox1.SelectedItem.ToString()}';";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            int currentIndex = comBox1.SelectedIndex;
            comBox1.Items.RemoveAt(currentIndex);
            comBox1.SelectedIndex = -1;
        }



        //3-ья вкладка://
        private void button6_Click(object sender, EventArgs e)
        {
            string sql;
            SqlCommand cmd;
            if (comBox2.SelectedItem != null && comBox2.SelectedItem.ToString() != "")
            {
                sql = $"UPDATE Издательство SET Название_издательства = '{tx23Box1.Text}', Сборники = '{tx23Box2.Text}', Реквизиты = '{tx23Box3.Text}', Год_основания = '{dTP2.Value}'  WHERE Название_издательства = '{comBox2.SelectedItem.ToString()}';";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }


            sql = $"SELECT Название_издательства FROM Издательство";
            cmd = new SqlCommand(sql, conn);
            int index = 0;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    index = comBox2.SelectedIndex;
                    comBox2.Items.Clear();
                    while (reader.Read())
                    {
                        int nameIndex = reader.GetOrdinal("Название_издательства");
                        string name = reader.GetString(nameIndex);
                        comBox2.Items.Add(name);
                    }
                }
            }
            comBox2.SelectedIndex = index;
        }

        private void comBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            tx23Box1.Text = comBox2.SelectedItem.ToString();


            string sql = $"SELECT Название_издательства, Сборники, Реквизиты, Год_основания  FROM Издательство WHERE Название_издательства = '{comBox2.SelectedItem.ToString()}'";
            SqlCommand cmd = new SqlCommand(sql, conn);

            using (DbDataReader reader = cmd.ExecuteReader())
            {

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int namedIndex = reader.GetOrdinal("Название_издательства");
                        string named = reader.GetString(namedIndex);
                        int manualsIndex = reader.GetOrdinal("Сборники");
                        string manuals = reader.GetString(manualsIndex);
                        int requisitesIndex = reader.GetOrdinal("Реквизиты");
                        string requisites = reader.GetString(requisitesIndex);
                        int dateIndex = reader.GetOrdinal("Год_основания");
                        DateTime date = reader.GetDateTime(dateIndex);
                        tx23Box1.Text = named;
                        tx23Box2.Text = manuals;
                        tx23Box3.Text = requisites;
                        dTP2.Value = date;
                    }

                }
            }
        }
    }
}
