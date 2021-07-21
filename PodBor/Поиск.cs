using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using MySql.Data.MySqlClient;

namespace PodBor
{
    public partial class Поиск : Form
    {
        Thread th;
        public Поиск(string namemedtext)
        {
            InitializeComponent();
            this.namemedtext = namemedtext;
        }
        string namemedtext;
        WaitFormFunc waitForm = new WaitFormFunc();

        
        private void Поиск_Load(object sender, EventArgs e)
        {
            try
            {
                BZ bz = new BZ();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                string zap = "SELECT `Препараты`.`Наименование` " +
                    "FROM `Препараты` WHERE `Препараты`.`Наименование` " +
                    "LIKE '" + namemedtext + "%' GROUP BY `Препараты`.`Наименование` " +
                    "ORDER BY `Препараты`.`Наименование`";
                MySqlCommand command = new MySqlCommand(zap, bz.getConnection());
                table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);
                label1.Text = "Найденные ЛП на запрос " + "'"+ namemedtext +"'";



                if (table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        Button btnmed = new Button();
                        flowLayoutPanel1.Controls.Add(btnmed);
                        btnmed.Margin = new System.Windows.Forms.Padding(10, 0, 0, 2);
                        btnmed.Location = new System.Drawing.Point(1, 1);

                        btnmed.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(124,170,122);
                        btnmed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(124, 170, 122);
                        btnmed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        btnmed.FlatAppearance.BorderSize = 2;
                        btnmed.ForeColor = System.Drawing.Color.FromArgb(63, 68, 43);
                        btnmed.ImeMode = System.Windows.Forms.ImeMode.NoControl;
                        btnmed.UseVisualStyleBackColor = true; 
                        btnmed.Font = new System.Drawing.Font("Arial", 14F, 
                        System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        btnmed.Size = new System.Drawing.Size(460, 50);
                        btnmed.Click += new System.EventHandler(this.button_Click);
                        btnmed.MouseEnter += new System.EventHandler(this.button_MouseEnter);
                        btnmed.MouseLeave += new System.EventHandler(this.button_MouseLeave);
                        btnmed.Cursor = System.Windows.Forms.Cursors.Hand;
                        btnmed.Text = table.Rows[i][0].ToString();
                    }
                }
                else
                    MessageBox.Show("К сожалению, такого лекарства нет в базе", "Не найдено!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка подключения  к базе данных", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button_MouseEnter(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.ForeColor = System.Drawing.Color.White;
        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.ForeColor = System.Drawing.Color.FromArgb(63, 68, 43);
        }

        private void button_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (button != null)
            {
                BZ bz = new BZ();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                string zap;

                zap = "SELECT Наименование FROM Препараты WHERE Наименование=(@namemed) GROUP BY Наименование";
                MySqlCommand command = new MySqlCommand(zap, bz.getConnection());
                command.Parameters.AddWithValue("namemed", button.Text);
                table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count != 0)
                {
                    namemedtext = button.Text;
                    this.Close();
                    th = new Thread(Info);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                }
                else
                    MessageBox.Show("К сожалению, такого лекарства нет в базе", "Не найдено!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void Info(object obj)
        {
            Application.Run(new InfoMed(namemedtext));
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            Close();
        }


        // -- Перемещение формы -- //
        private bool drag = false;
        private Point start_point = new Point(0, 0);
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.drag = true;
            this.start_point = new Point(e.X, e.Y);
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
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
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.drag = false;
            }
        }
    }
}
