using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework.Components;
using MySql.Data.MySqlClient;
using System.Threading;

namespace PodBor
{
    public partial class Авторизация : Form
    {
        public Авторизация()
        {
            InitializeComponent();
        }
        Thread th;
        string login;

        private void Form4_Load(object sender, EventArgs e)
        {
            this.AcceptButton = Войти;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Логин.Text) && !string.IsNullOrEmpty(Пароль.Text))
            {
                BZ bz = new BZ();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                string zap;

                zap = "SELECT Логин, Пароль, Admin " +
                    "FROM Пользователи WHERE Логин=(@log)";

                MySqlCommand command = new MySqlCommand(zap, bz.getConnection());
                command.Parameters.AddWithValue("log", Логин.Text);

                table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                if (dataGridView1.Rows.Count > 1)
                {
                    if (dataGridView1.Rows[0].Cells[1].Value.ToString() == Пароль.Text)
                    {
                        if (Convert.ToInt32(dataGridView1.Rows[0].Cells[2].Value) == 1)
                        {
                            this.Close();
                            th = new Thread(Admin);
                            th.SetApartmentState(ApartmentState.STA);
                            th.Start();
                        }
                        else
                        {
                            login = Логин.Text;
                            this.Close();
                            th = new Thread(Protiv);
                            th.SetApartmentState(ApartmentState.STA);
                            th.Start();
                            
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль");
                    }
                }
                else
                {
                    MessageBox.Show("Пользователя не существует");
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Admin()
        {
            Application.Run(new Администратор());
        }
        private void Protiv()
        {
            Application.Run(new Противопоказания(login));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            th = new Thread(Reg);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void Reg()
        {
            Application.Run(new Регистрация());
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Close();
        }
        private void Войти_MouseEnter(object sender, EventArgs e)
        {
            Войти.ForeColor = System.Drawing.Color.White;
        }

        private void Войти_MouseLeave(object sender, EventArgs e)
        {
            Войти.ForeColor = System.Drawing.Color.SeaGreen;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Login: admin \n Password: 123");
        }
    }
}
