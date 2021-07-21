using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Controls;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;

namespace PodBor
{
    public partial class Противопоказания : MetroForm
    {
        public Противопоказания(string логин)
        {
            InitializeComponent();
            this.логин = логин;

        }
        string логин;


        private void button1_Click(object sender, EventArgs e)
        {
            BZ bz = new BZ();
            bz.openConnection();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            //MySqlCommand pr = new MySqlCommand("SELECT `Противопоказания`.`Противопоказание` " +
            //    "FROM `Пользователь_Противопоказания` " +
            //    "LEFT JOIN `Противопоказания` " +
            //    "ON `Противопоказания`.`ID`=`Пользователь_Противопоказания`.`Противопоказание` " +
            //    "WHERE `Пользователь_Противопоказания`.`пользователь` = " +
            //    "(Select `Пользователи`.`ID` from `Пользователи` WHERE `Пользователи`.`логин`= @login)", bz.getConnection());
            MySqlCommand pr = new MySqlCommand("SELECT * FROM `Пользователи` " +
                "WHERE `Пользователи`.`логин`= @login", bz.getConnection());
            pr.Parameters.AddWithValue("login", логин);
            table = new DataTable();
            adapter.SelectCommand = pr;
            adapter.Fill(table);
            try
            {
                if (table.Rows.Count > 0)
                {
                    if (Беременность.Checked == true)
                    {
                        string zap = "INSERT INTO `Пользователь_Противопоказания` " +
                            "(`Пользователь`, `Противопоказание`) " +
                        "VALUES ((SELECT ID FROM `Пользователи` " +
                    "WHERE `Логин`= @login), @противопоказание)";
                        MySqlCommand command = new MySqlCommand(zap, bz.getConnection());
                        command.Parameters.AddWithValue("@login", логин);
                        command.Parameters.AddWithValue("@противопоказание", 1);
                        command.ExecuteNonQuery();
                    }
                    else
                    {

                    }
                }
            }
            catch
            {
                MessageBox.Show("Пока не доступно"); 
            }
        }

        private void Противопоказания_Load(object sender, EventArgs e)
        {
            login.Text += " " + логин; 
            BZ bz = new BZ();
            bz.openConnection();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand sql = new MySqlCommand("SELECT `Имя` FROM `Пользователи`" +
                "WHERE `Пользователи`.`логин`= @login", bz.getConnection());
            sql.Parameters.AddWithValue("login", логин);
            table = new DataTable();
            adapter.SelectCommand = sql;
            adapter.Fill(table);
            Имя.Text = table.Rows[0][0].ToString();

            MySqlCommand pr = new MySqlCommand("SELECT `Противопоказания`.`Противопоказание` " +
                "FROM `Пользователь_Противопоказания` " +
                "LEFT JOIN `Противопоказания` " +
                "ON `Противопоказания`.`ID`=`Пользователь_Противопоказания`.`Противопоказание` " +
                "WHERE `Пользователь_Противопоказания`.`пользователь` = " +
                "(Select `Пользователи`.`ID` from `Пользователи` WHERE `Пользователи`.`логин`= @login)", bz.getConnection());
            pr.Parameters.AddWithValue("login", логин);
            table = new DataTable();
            adapter.SelectCommand = pr;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                foreach (Control cnt in tableLayoutPanel2.Controls)
                {
                    MetroRadioButton rb = cnt as MetroRadioButton;
                    CheckBox tb = cnt as CheckBox;

                    if (rb != null)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            if (rb.Text == table.Rows[i][0].ToString())
                                rb.Checked = true;
                        }
                    }

                    if (tb != null)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            if (tb.Text == table.Rows[i][0].ToString())
                                tb.Checked = true;
                        }
                    }
                    
                }
            }

        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Close();
        }


        // -- Перемещение формы -- //
        private bool drag = false;
        private Point start_point = new Point(0, 0);

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            this.drag = true;
            this.start_point = new Point(e.X, e.Y);
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.drag)
            {
                Point p1 = new Point(e.X, e.Y);
                Point p2 = this.PointToScreen(p1);
                Point p3 = new Point(p2.X - this.start_point.X,
                                     p2.Y - this.start_point.Y);
                this.Location = p3;
            }
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.drag = false;
            }
        }

    }
}
