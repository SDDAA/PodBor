using MetroFramework.Forms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PodBor
{
    public partial class Аналоги : Form
    {
        Thread th;
        public Аналоги(string актив, string название, string показание)
        {
            InitializeComponent();
            this.актив = актив;
            this.название = название;
            this.показание = показание;
        }
        WaitFormFunc waitForm = new WaitFormFunc();
        string актив;
        string название;
        string показание;
        string namemedtext;

        private void Аналоги_Load(object sender, EventArgs e)
        {
            
            waitForm.Show(this);
            BZ bz = new BZ();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            string zap;
            zap = "select distinct `Препараты`.`Наименование`, " +
                "`Препараты`.`Активное вещество`, `Форма`.`Форма`, " +
                "`Препараты`.`Объем`, `Показания`.`Показание`, " +
                "`Препараты`.`Цена` FROM `Препараты` " +
                "	INNER JOIN `Форма` ON `Препараты`.`Форма` = `Форма`.`ID` " +
                "	INNER JOIN `Показания` ON `Препараты`.`Показания` = `Показания`.`ID` " +
                "WHERE `Препараты`.`Наименование` <> (@name)" +
                "GROUP BY `Препараты`.`Наименование`";
            MySqlCommand command = new MySqlCommand(zap, bz.getConnection());
            command.Parameters.AddWithValue("indication", показание);
            command.Parameters.AddWithValue("name", название);
            table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            string word = актив, text;
            int k=0;
            // button1
            // 
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    text = table.Rows[i][1].ToString();
                    if (text.IndexOf(word, StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        Button btnmed = new Button();
                        flowLayoutPanel1.Controls.Add(btnmed);
                        btnmed.Margin = new System.Windows.Forms.Padding(25, 0, 0, 0);
                        btnmed.Location = new System.Drawing.Point(1, 1);
                        btnmed.Font = new System.Drawing.Font("Century Gothic", 14.25F, 
                        System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                        btnmed.Size = new System.Drawing.Size(450, 70);
                        btnmed.Click += new System.EventHandler(this.btnmed_Click);
                        btnmed.Name = table.Rows[i][0].ToString();
                        btnmed.Cursor = System.Windows.Forms.Cursors.Hand;
                        btnmed.Text = table.Rows[i][0].ToString() +
                        " [" + table.Rows[i][2].ToString() + " " +
                        table.Rows[i][3].ToString() + "]" + " - " +
                        table.Rows[i][5].ToString() + " ₽";
                        

                    }
                    
                    else
                        continue;
                    k++;
                }
                waitForm.Close();
                this.Activate();
            }
            if(table.Rows.Count == 0 || k == 0)
            {
                Label l = new Label();
                flowLayoutPanel1.Controls.Add(l);
                l.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                l.Size = new System.Drawing.Size(1, 23);
                l.AutoSize = true;
                l.TabIndex = 1;
                l.Margin = new System.Windows.Forms.Padding(150, 10, 0, 10);
                l.Text = "Дженерики не найдены";
                l.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            }
            // Вторая кнопка
            Label label2 = new Label();
            flowLayoutPanel1.Controls.Add(label2);
            label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(89)))));
            label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            label2.Location = new System.Drawing.Point(0, 50);
            label2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            label2.Size = new System.Drawing.Size(497, 23);
            label2.Text = "Аналоги по показаниям к применению";
            label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            label2.ForeColor = System.Drawing.Color.White;
            zap = "select `Препараты`.`Наименование`, `Форма`.`Форма`, `Препараты`.`Объем`, " +
                "`Показания`.`Показание`, `Противопоказания`.`Противопоказание`, `Препараты`.`Цена`" +
                "FROM `Препараты` " +
                "INNER JOIN `Показания` ON `Препараты`.`Показания` = `Показания`.`ID` " +
                "INNER JOIN `Противопоказания` ON `Препараты`.`Противопоказания` = `Противопоказания`.`ID`" +
                "INNER JOIN `Форма` ON `Препараты`.`Форма` = `Форма`.`ID` " +
                "WHERE `Показания`.`Показание` = (@indication) AND `Препараты`.`Наименование` <> (@name)" +
                "GROUP BY `Препараты`.`Наименование`";
            MySqlCommand command1 = new MySqlCommand(zap, bz.getConnection());
            command1.Parameters.AddWithValue("indication", показание);
            command1.Parameters.AddWithValue("name", название);
            table = new DataTable();
            adapter.SelectCommand = command1;
            adapter.Fill(table);
            
            
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Button btnmed = new Button();
                    flowLayoutPanel1.Controls.Add(btnmed);
                    btnmed.Location = new System.Drawing.Point(1, 1);
                    btnmed.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                    btnmed.Size = new System.Drawing.Size(450, 70);
                    btnmed.TabIndex = 0;
                    btnmed.Cursor = System.Windows.Forms.Cursors.Hand;
                    btnmed.Name = table.Rows[i][0].ToString();
                    btnmed.Click += new System.EventHandler(this.btnmed_Click);
                    btnmed.Margin = new System.Windows.Forms.Padding(25,0,0,0);
                    btnmed.Text = table.Rows[i][0].ToString() +
                       " [" + table.Rows[i][1].ToString() + " " +
                       table.Rows[i][2].ToString() + "]" + " - " +
                       table.Rows[i][5].ToString() + " ₽";
                }
            }
            else
            {
                Label l = new Label();
                flowLayoutPanel1.Controls.Add(l);
                l.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                l.Size = new System.Drawing.Size(490, 22);
                l.Text = "Аналоги не найдены";
                l.ForeColor = System.Drawing.Color.White;
                l.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
                l.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            }
            waitForm.Close();
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Close();
        }

        private void btnmed_Click(object sender, EventArgs e)
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
                command.Parameters.AddWithValue("namemed", button.Name);
                table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count != 0)
                {
                    namemedtext = button.Name;
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


        // -- Перемещение формы -- //
        private bool drag = false;
        private Point start_point = new Point(0, 0);

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.drag = true;
            this.start_point = new Point(e.X, e.Y);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.drag = false;
            }
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

        private void button3_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Close();
        }
    }
}
