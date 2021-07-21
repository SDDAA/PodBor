using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace PodBor
{
    public partial class InfoMed : MetroForm
    {
        Thread th;
        public InfoMed(string namemedtext)
        {
            InitializeComponent();
            this.namemedtext = namemedtext;
            
        }
        WaitFormFunc waitForm = new WaitFormFunc();
        string namemedtext;
        string zap;

        private void Form_Load(object sender, EventArgs e)
        {
            waitForm.Show(this);
            namemed.Font = new System.Drawing.Font("Open Sans", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            flowLayoutPanel1.AutoScroll = true;
            namemed.TabStop = false;
            Состав.TabStop = false;
            Фармакодинамика.TabStop = false;
            Фармакокинетика.TabStop = false;
            Показания.TabStop = false;
            Противопоказания.TabStop = false;
            BZ bz = new BZ();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            
            

            zap = "SELECT `Препараты`.`ID`, `Препараты`.`Фото`, `Препараты`.`Наименование`, " +
                "`Препараты`.`Активное вещество`, `Форма`.`Форма`, " +
                "`Препараты`.`Объем`, `Препараты`.`Состав`, `Препараты`.`Фармакодинамика`, " +
                "`Препараты`.`Фармакокинетика`, `Препараты`.`Фармакокинетика`, `Показания`.`Показание`, " +
                "`Противопоказания`.`Противопоказание`, `Препараты`.`Побочнные_действия`, " +
                "`Препараты`.`Производитель`, `Страна`.`Страна`, `Препараты`.`Рецепт`, `Препараты`.`Цена`" +
                "FROM `Препараты` " +
                "	INNER JOIN `Форма` ON `Препараты`.`Форма` = `Форма`.`ID` " +
                "	INNER JOIN `Показания` ON `Препараты`.`Показания` = `Показания`.`ID` " +
                "	INNER JOIN `Противопоказания` ON `Препараты`.`Противопоказания` = `Противопоказания`.`ID` " +
                "	INNER JOIN `Страна` ON `Препараты`.`Страна` = `Страна`.`ID`" +
                "WHERE `Препараты`.`Наименование` LIKE '" + namemedtext + "%'";
            MySqlCommand command = new MySqlCommand(zap, bz.getConnection());
            table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            

                if (table.Rows.Count != 0)
                {
                    // Заполнение
                    // /// Фото
                    byte[] imgData1 = (byte[])table.Rows[0][1];
                    MemoryStream ms1 = new MemoryStream(imgData1);
                    pic.Image = Image.FromStream(ms1);
                    // /// Наименование
                    namemed.Text = table.Rows[0][2].ToString();
                    // /// Код
                    Код.Text = "000" + table.Rows[0][0].ToString();
                    // /// Производитель
                    Производитель.Text = table.Rows[0][13].ToString();
                    // /// Страна
                    Страна.Text = table.Rows[0][14].ToString();
                    // /// Форма выпуска
                    Форма.Text = table.Rows[0][4].ToString() + " (" + table.Rows[0][5].ToString() + ")";
                    // /// Рецепт
                    if (Convert.ToInt32(table.Rows[0][15]) == 0)
                        Рецепт.Text = "Без рецепта";
                    else
                        Рецепт.Text = "По рецепту";
                    // /// Цена
                    Цена.Text = table.Rows[0][16].ToString() + " ₽";

                    // /// 	Активное вещество
                    Актив.Text = "\t- " + table.Rows[0][3].ToString();
                    // /// Состав
                    Состав.Text = "\t- " + table.Rows[0][6].ToString();
                    // /// Фармакодинамика
                    Фармакодинамика.Text = "\t- " + table.Rows[0][7].ToString();
                    // /// Фармакокинетика
                    Фармакокинетика.Text = "\t- " + table.Rows[0][8].ToString();
                    // /// Показания
                    
                    if (table.Rows.Count > 1)
                    {
                        string indication = "";
                        Показания.Text = "";
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            if (indication != table.Rows[i][10].ToString())
                                Показания.Text += "\t- " + table.Rows[i][10].ToString() + "\n";
                            else
                                continue; 
                            indication = table.Rows[i][10].ToString();
                        }
                    }
                    else
                        Показания.Text = "\t- " + table.Rows[0][10].ToString();
                    // /// Противопоказания
                    if (table.Rows.Count > 1)
                    {
                        string contrindication = "";
                        Противопоказания.Text = "";
                        for (int i = 0; i < table.Rows.Count - 1; i++)
                        {
                            if (contrindication != table.Rows[i][11].ToString())
                                Противопоказания.Text += "\t- " + table.Rows[i][11].ToString() + "\n";
                            else
                                continue;
                            contrindication = table.Rows[i][11].ToString();
                        }
                    }
                    else
                        Противопоказания.Text = "\t- " + table.Rows[0][11].ToString();
                    // /// Побочнные_действия
                    Побочные.Text = "\t- " + table.Rows[0][12].ToString();
                }
                else
                {
                    MessageBox.Show("К сожалению, такого лекарства нет в базе", "Не найдено!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            
           

            waitForm.Close();
            this.Activate();
        }

        private void k_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Close();
        }

        private void SearchAgain_Click(object sender, EventArgs e)
        {
            Form fc = Application.OpenForms["Главная"];

            if (fc != null)
                this.Close();
            else
            {
                   this.Close();
                   th = new Thread(form1);
                   th.SetApartmentState(ApartmentState.STA);
                   th.Start();
            }


        }

            // -- Запуск новой формы -- //
        private void form1(object obj)
        {
            Application.Run(new Главная());
        }

        private void SearchAgain_MouseHover(object sender, EventArgs e)
        {
            SearchAgain.Image = global::PodBor.Properties.Resources.Искать_1;
        }

        private void SearchAgain_MouseLeave(object sender, EventArgs e)
        {
            SearchAgain.Image = global::PodBor.Properties.Resources.Искать;
        }

        private void k_Exit_MouseHover(object sender, EventArgs e)
        {
            k_Exit.Image = global::PodBor.Properties.Resources.Выход_1;
        }

        private void k_Exit_MouseLeave(object sender, EventArgs e)
        {
            k_Exit.Image = global::PodBor.Properties.Resources.Выход;
        }


        // -- Перемещение формы -- //
        private bool drag = false;
        private Point start_point = new Point(0, 0);

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

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.drag = true;
            this.start_point = new Point(e.X, e.Y);

        }


        // -- Перемещение формы -- //
        private void button1_Click(object sender, EventArgs e)
        {
            BZ bz = new BZ();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(zap, bz.getConnection());
            command.Parameters.AddWithValue("namemed", namemedtext);
            table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            Аналоги a = new Аналоги(table.Rows[0][3].ToString(), table.Rows[0][2].ToString(), table.Rows[0][10].ToString());
            a.ShowDialog();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ForeColor = System.Drawing.Color.White;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = System.Drawing.Color.SeaGreen;
        }
    }
}
