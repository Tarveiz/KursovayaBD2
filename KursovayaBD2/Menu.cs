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
        ////////////Конец первой вкладки -> Начало второй вкладки/////////////
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


        //////////////////////////////////////////////////////////////////////
        //////////Конец второй вкладки -> Начало третьей вкладки//////////////
        //////////////////////////////////////////////////////////////////////
        

        //1-ая вкладка//
        private void button7_Click(object sender, EventArgs e)
        {
            string sql = $"INSERT INTO " +
                $"Работы(Тема, Дата_публикации, Автор) " +
                $"VALUES('{tx31Box1.Text}','{dTP3.Value}','{tx31Box2.Text}');";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            tx31Box1.Text = "";
            tx31Box2.Text = "";
            dTP3.Value = DateTime.Now;
        }


        //2-ая вкладка//

        private void button8_Click(object sender, EventArgs e)
        {
            string sql = $"DELETE FROM Работы WHERE Автор = '{comBox4.SelectedItem.ToString()}';";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            int currentIndex = comBox4.SelectedIndex;
            comBox4.Items.RemoveAt(currentIndex);
            comBox4.SelectedIndex = -1;
        }

        private void tabControl4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl4.SelectedIndex)
            {
                case 1:
                    string sql = $"SELECT Автор FROM Работы";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            comBox4.Items.Clear();
                            while (reader.Read())
                            {
                                int nameIndex = reader.GetOrdinal("Автор");
                                string name = reader.GetString(nameIndex);

                                comBox4.Items.Add(name);
                            }
                        }
                    }
                    break;
                //+ к третьей вкладке//
                case 2:
                    sql = $"SELECT Автор FROM Работы";
                    cmd = new SqlCommand(sql, conn);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            comBox5.Items.Clear();
                            while (reader.Read())
                            {
                                int nameIndex = reader.GetOrdinal("Автор");
                                string name = reader.GetString(nameIndex);
                                comBox5.Items.Add(name);
                            }
                        }
                    }
                    break;
                    //До сюда//
            }
        }


        //3-ья вкладка//

        private void button9_Click(object sender, EventArgs e)
        {
            string sql;
            SqlCommand cmd;
            if (comBox5.SelectedItem != null && comBox5.SelectedItem.ToString() != "")
            {
                sql = $"UPDATE Работы SET Автор = '{tx33Box2.Text}', Тема = '{tx33Box1.Text}', Дата_публикации = '{dTP4.Value}'  WHERE Автор = '{comBox5.SelectedItem.ToString()}';";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }


            sql = $"SELECT Автор FROM Работы";
            cmd = new SqlCommand(sql, conn);
            int index = 0;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    index = comBox5.SelectedIndex;
                    comBox5.Items.Clear();
                    while (reader.Read())
                    {
                        int nameIndex = reader.GetOrdinal("Автор");
                        string name = reader.GetString(nameIndex);
                        comBox5.Items.Add(name);
                    }
                }
            }
            comBox5.SelectedIndex = index;
        }

        private void comBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            tx33Box2.Text = comBox5.SelectedItem.ToString();


            string sql = $"SELECT Тема, Дата_публикации, Автор FROM Работы WHERE Автор = '{comBox5.SelectedItem.ToString()}'";
            SqlCommand cmd = new SqlCommand(sql, conn);

            using (DbDataReader reader = cmd.ExecuteReader())
            {

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int chapterIndex = reader.GetOrdinal("Тема");
                        string chapter = reader.GetString(chapterIndex);

                        int date2Index = reader.GetOrdinal("Дата_публикации");
                        DateTime date2 = reader.GetDateTime(date2Index);

                        int authorIndex = reader.GetOrdinal("Автор");
                        string author = reader.GetString(authorIndex);


                        tx33Box1.Text = chapter;
                        tx33Box2.Text = author;
                        dTP4.Value = date2;
                    }

                }
            }
        }



        //////////////////////////////////////////////////////////////////////
        //////////Конец третьей вкладки -> Начало четвертой вкладки///////////
        //////////////////////////////////////////////////////////////////////
        
        //1-ая вкладка//

        private void button10_Click(object sender, EventArgs e)
        {
            string sql = $"INSERT INTO " +
                $"Учет_Кафедра(Название_кафедры, Издатели, Номера_работ, Дата_основания, ID_Преподавателя) " +
                $"VALUES('{tx41Box1.Text}','{cmBox1.SelectedItem}','{cmBox2.SelectedItem}', '{dTP41.Value}','{cmBox3.SelectedItem}');";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            tx41Box1.Text = "";
            dTP41.Value = DateTime.Now;
            cmBox1.SelectedIndex = -1;
            cmBox2.SelectedIndex = -1;
            cmBox3.SelectedIndex = -1;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 3:
                    //Издатели
                    string sql = $"SELECT Название_издательства FROM Издательство";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            cmBox1.Items.Clear();
                            while (reader.Read())
                            {
                                int nameIndex = reader.GetOrdinal("Название_издательства");
                                string name = reader.GetString(nameIndex);

                                cmBox1.Items.Add(name);
                            }
                        }
                    }

                    //Номера_работ
                    sql = $"SELECT Номер_Работы FROM Работы GROUP BY Номер_Работы";
                    cmd = new SqlCommand(sql, conn);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            cmBox2.Items.Clear();
                            while (reader.Read())
                            {
                                int nameIndex = reader.GetOrdinal("Номер_Работы");
                                int name = reader.GetInt32(nameIndex);

                                cmBox2.Items.Add(name);
                            }
                        }
                    }

                    //ID_Преподавателя
                    sql = $"SELECT ID_Преподавателя FROM Преподаватели GROUP BY ID_Преподавателя";
                    cmd = new SqlCommand(sql, conn);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            cmBox3.Items.Clear();
                            while (reader.Read())
                            {
                                int nameIndex = reader.GetOrdinal("ID_Преподавателя");
                                int name = reader.GetInt32(nameIndex);

                                cmBox3.Items.Add(name);
                                //int[] mass;
                                //mass.
                            }
                        }
                        else
                        {
                            cmBox3.Items.Add("");
                            cmBox3.SelectedIndex = -1;
                            cmBox3.Items.Clear();
                        }
                    }
                    break;
            }
        }


        //2-ая вкладка//


        private void button11_Click(object sender, EventArgs e)
        {
            string sql = $"DELETE FROM Учет_Кафедра WHERE Название_кафедры = '{cmBox4.SelectedItem.ToString()}';";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            int currentIndex = cmBox4.SelectedIndex;
            cmBox4.Items.RemoveAt(currentIndex);
            cmBox4.SelectedIndex = -1;
        }

        private void tabControl5_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl5.SelectedIndex)
            {
                case 1:
                    string sql = $"SELECT Название_кафедры FROM Учет_Кафедра";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            cmBox4.Items.Clear();
                            while (reader.Read())
                            {
                                int nameIndex = reader.GetOrdinal("Название_кафедры");
                                string name = reader.GetString(nameIndex);

                                cmBox4.Items.Add(name);
                            }
                        }
                    }
                    break;
                //+ к третьей вкладке//
                case 2:
                    sql = $"SELECT Название_кафедры FROM Учет_Кафедра";
                    cmd = new SqlCommand(sql, conn);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            cmBox5.Items.Clear();
                            while (reader.Read())
                            {
                                int nameIndex = reader.GetOrdinal("Название_кафедры");
                                string name = reader.GetString(nameIndex);
                                cmBox5.Items.Add(name);
                            }
                        }
                    }
                    //Издатели
                    sql = $"SELECT Название_издательства FROM Издательство";
                    cmd = new SqlCommand(sql, conn);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            cmBox7.Items.Clear();
                            while (reader.Read())
                            {
                                int nameIndex = reader.GetOrdinal("Название_издательства");
                                string name = reader.GetString(nameIndex);

                                cmBox7.Items.Add(name);
                            }
                        }
                    }

                    //Номера_работ
                    sql = $"SELECT Номер_Работы FROM Работы GROUP BY Номер_Работы";
                    cmd = new SqlCommand(sql, conn);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            cmBox8.Items.Clear();
                            while (reader.Read())
                            {
                                int nameIndex = reader.GetOrdinal("Номер_Работы");
                                int name = reader.GetInt32(nameIndex);

                                cmBox8.Items.Add(name);
                            }
                        }
                    }

                    //ID_Преподавателя
                    sql = $"SELECT ID_Преподавателя FROM Преподаватели GROUP BY ID_Преподавателя";
                    cmd = new SqlCommand(sql, conn);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            cmBox6.Items.Clear();
                            while (reader.Read())
                            {
                                int nameIndex = reader.GetOrdinal("ID_Преподавателя");
                                int name = reader.GetInt32(nameIndex);

                                cmBox6.Items.Add(name);
                            }
                        }
                        else
                        {
                            cmBox6.Items.Add("");
                            cmBox6.SelectedIndex = -1;
                            cmBox6.Items.Clear();
                        }
                    }
                    break;
                    //До сюда//
            }
        }

        //3-ья вкладка//


        private void button12_Click(object sender, EventArgs e)
        {
            string sql;
            SqlCommand cmd;
            if (cmBox5.SelectedItem != null && cmBox5.SelectedItem.ToString() != "")
            {
                sql = $"UPDATE Учет_Кафедра SET Название_кафедры = '{tx43Box1.Text}', Издатели = '{cmBox7.SelectedItem}', Номера_работ = '{cmBox8.SelectedItem}', Дата_основания = '{dTP42.Value}', ID_Преподавателя = '{cmBox6.SelectedItem}'  WHERE Название_кафедры = '{cmBox5.SelectedItem.ToString()}';";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }


            sql = $"SELECT Название_кафедры FROM Учет_Кафедра";
            cmd = new SqlCommand(sql, conn);
            int index = 0;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    index = cmBox5.SelectedIndex;
                    cmBox5.Items.Clear();
                    while (reader.Read())
                    {
                        int nameIndex = reader.GetOrdinal("Название_кафедры");
                        string name = reader.GetString(nameIndex);
                        cmBox5.Items.Add(name);
                    }
                }
            }
            cmBox5.SelectedIndex = index;
        }

        private void cmBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            tx43Box1.Text = cmBox5.SelectedItem.ToString();


            string sql = $"SELECT Название_кафедры, Издатели, Номера_работ, Дата_основания, ID_Преподавателя FROM Учет_Кафедра WHERE Название_кафедры = '{cmBox5.SelectedItem.ToString()}'";
            SqlCommand cmd = new SqlCommand(sql, conn);

            using (DbDataReader reader = cmd.ExecuteReader())
            {

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int kafedraIndex = reader.GetOrdinal("Название_кафедры");
                        string kafedra = reader.GetString(kafedraIndex);

                        int date3Index = reader.GetOrdinal("Дата_основания");
                        DateTime date3 = reader.GetDateTime(date3Index);

                        int izdateliIndex = reader.GetOrdinal("Издатели");
                        string izdateli = reader.GetString(izdateliIndex);

                        int numberIndex = reader.GetOrdinal("Номера_работ");
                        int number = reader.GetInt32(numberIndex);

                        int teacherIndex = reader.GetOrdinal("ID_Преподавателя");
                        int teacher = reader.GetInt32(teacherIndex);


                        tx43Box1.Text = kafedra;
                        dTP42.Value = date3;
                        cmBox6.SelectedItem = teacher;
                        cmBox7.SelectedItem = izdateli;
                        cmBox8.SelectedItem = number;
                    }

                }
            }
        }
    }
}
