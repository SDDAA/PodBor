using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using MetroFramework.Components;
using MetroFramework.Forms;
using System.Runtime.InteropServices;


namespace PodBor
{
    public partial class Главная : MetroForm
    {
        Thread th;
        int y = 0;
        int teg=0;
        bool st;
        string namemed;
        public Главная()
        {
            InitializeComponent();
        }
        

        


        WaitFormFunc waitForm = new WaitFormFunc();

        private void Form1_Load(object sender, EventArgs e)
        {
            if (bSearch.Enabled == true && bSearch.Visible == true)
                this.AcceptButton = bSearch;
            pictureBox1.Visible = true;
            pictureBox1.Enabled = true;
            label2.Visible = true;
            label2.Enabled = true;
            Start.Enabled = true;
            Start.Visible = true;
            k_Exit.Enabled = true;
            k_Exit.Visible = true;
            bSearch.Visible = false;
            bSearch.Enabled = false;
            Search.Visible = false;
            Search.Enabled = false;
            label2.TabStop = false;
            Questions.Visible = false;
            pic_q.Visible = false;
            SearchAgain.Visible = false;
            SearchAgain.Enabled = false;
            k_No.Visible = false;
            k_Yes.Visible = false;
            k_sort.Visible = false;
            k_sort.Enabled = false;
            k_nosort.Visible = false;
            k_nosort.Enabled = false;
            Back.Enabled = false;
            Back.Visible = false;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Back.Enabled = true;
            Back.Visible = true;

            label2.Visible = false;
            label2.Enabled = false;
            Questions.Visible = true;
            Questions.Text = "У Вас имеется рецепт?";
            pic_q.Visible = true;
            pic_q.Image = Image.FromFile("img/Заболевания/Recept.png");
            Start.Enabled = false;
            Start.Visible = false;
            k_Exit.Enabled = false;
            k_Exit.Visible = false;
            k_No.Visible = true;
            k_Yes.Visible = true;
            k_No.Enabled = true;
            k_Yes.Enabled = true;
            pictureBox1.Visible = false;
        }

        private void k_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void NotFound()
        {
            Questions.Text = "Ничего не найдено";
            pic_q.Image = Image.FromFile("img/Заболевания/Вопрос2N.png");
            k_No.Visible = false;
            k_No.Enabled = false;
            k_Yes.Visible = false;
            k_Yes.Enabled = false;
            SearchAgain.Visible = true;
            SearchAgain.Enabled = true;
            k_Exit.Visible = true;
            k_Exit.Enabled = true;
            Back.Visible = false;
            Back.Enabled = false;
        }

        public void Sort()
        {
            Questions.Text = "Хотите выполнить сортировку по цене?";
            pic_q.Image = Image.FromFile("img/Заболевания/Сортировка.png");
        }

        public void print_sort()
        {
            Questions.Visible = false;
            pic_q.Visible = false;
            k_No.Visible = false;
            k_No.Enabled = false;
            k_Yes.Visible = false;
            k_Yes.Enabled = false;
            SearchAgain.Visible = false;
            SearchAgain.Enabled = false;
            k_Exit.Visible = false;
            k_Exit.Enabled = false;
        }
        private void SearchAgain_Click(object sender, EventArgs e)
        {
            label2.TabStop = false;
            Questions.Visible = false;
            pic_q.Visible = false;
            SearchAgain.Visible = false;
            SearchAgain.Enabled = false;
            k_No.Visible = false;
            k_Yes.Visible = false;
            pic_q.Visible = false;
            Questions.Visible = false;
            Start.Visible = true;
            Start.Enabled = true;
            label2.Visible = true;
            label2.Enabled = true;
            pictureBox1.Visible = true;
        }

        private void NextQuestion()
        {
            // Вопросы
            switch (Questions.Text)
            {
                case "У Вас имеется рецепт?":
                    if (y>0)
                    {
                        Search.Focus();
                        Questions.Text = "Введите название лекарственного препарата";
                        pic_q.Image = Image.FromFile("img/Оформление Форм/Поиск.png");
                        k_No.Visible = false;
                        k_No.Enabled = false;
                        k_Yes.Visible = false;
                        k_Yes.Enabled = false;
                        k_Exit.Visible = true;
                        k_Exit.Enabled = true;
                        SearchAgain.Visible = false;
                        SearchAgain.Enabled = false;
                        bSearch.Visible = true;
                        bSearch.Enabled = true;
                        Search.Visible = true;
                        Search.Enabled = true;
                        Search.Focus();
                    }
                    else
                    {
                        Questions.Text = "У Вас плохое самочувствие?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Самочувствие.png");
                    }
                    y = 0;
                    break;
                case "У Вас плохое самочувствие?":
                    if (y > 0)
                    {
                        Questions.Text = "Вы ощущаете признаки простуды?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Простуда.png");
                    }
                    else
                    {
                        Questions.Text = "Вам нужны средства индивидуальной защиты?";
                        pic_q.Image = Image.FromFile("img/Заболевания/СИЗ.png");
                    }
                    y = 0;
                    break;
                case "Вам нужны средства индивидуальной защиты?":
                if (y > 0)
                    {
                        teg = 9;
                        Sort();
                    }
                    else
                    {
                        NotFound();
                    }
                    y = 0;
                    break;
                case "Вы ощущаете признаки простуды?":
                    if (y > 0)
                    {
                        Questions.Text = "Вы чувствуете боли в горле?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Горло.png");
                        teg = 1;
                    }
                    else
                    {
                        Questions.Text = "У Вас есть проблемы с пищеварением?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Пищеварение.png");
                    }
                    y = 0;
                    break;
                case "Вы чувствуете боли в горле?":
                    if (y > 0)
                    {
                        teg = 1;
                        Sort();
                    }
                    else
                    {
                        Questions.Text = "Вас беспокоит насморк?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Насморк.png");
                    }
                    y = 0;
                    break;
                case "Вас беспокоит насморк?":
                    if (y > 0)
                    {
                        teg = 2;
                        Sort();
                    }
                    else
                    {
                        Questions.Text = "Вас беспокоит повышенная температура?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Температура.png");
                    }
                    y = 0;
                    break;
                case "Вас беспокоит повышенная температура?":
                    if (y > 0)
                    {
                        Questions.Text = "Есть ли у вас сыпь?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Ветрянка.png");
                    }
                    else
                    {
                        Questions.Text = "Вы испытываете головную боль?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Голова.png");
                    }
                    y = 0;
                    break;
                case "Есть ли у вас сыпь?":
                    if (y > 0)
                    {
                        teg = 13;
                        Sort();
                    }
                    else
                    {
                        teg = 3;
                        Sort();
                    }
                    y = 0;
                    break;

                case "Вы испытываете головную боль?":
                    if (y > 0)
                    {
                        teg = 4;
                        Sort();
                    }
                    else
                    {
                        Questions.Text = "У Вас есть кашель?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Кашель.png");
                    }
                    y = 0;
                    break;
                case "У Вас есть кашель?":
                    if (y > 0)
                    {
                        teg = 11;
                        Sort();
                    }
                    else
                    {
                        NotFound();
                    }
                    y = 0;
                    break;
                case "У Вас есть проблемы с пищеварением?":
                    if (y > 0)
                    {
                        Questions.Text = "У вас изжога?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Изжога.png");
                    }
                    else
                    {
                        Questions.Text = "Есть ли у вас признаки аллергии?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Аллергия.png");
                    }
                    y = 0;
                    break;
                case "У вас изжога?":
                    if (y > 0)
                    {
                        teg = 6;
                        Sort();
                    }
                    else
                    {
                        NotFound();
                    }
                    y = 0;
                    break;
                case "Есть ли у вас признаки аллергии?":
                    if (y > 0)
                    {
                        teg = 5;
                        Sort();
                    }
                    else
                    {
                        Questions.Text = "Вы испытываете боли в области сердца?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Сердце.png");
                    }
                    y = 0;
                    break;
                case "Вы испытываете боли в области сердца?":
                    if (y > 0)
                    {
                        teg = 7;
                        Sort();
                    }
                    else
                    {
                        Questions.Text = "У Вас есть нарушения сна?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Сон.png");
                    }
                    y = 0;
                    break;
                case "У Вас есть нарушения сна?":
                    if (y > 0)
                    {
                        teg = 8;
                        Sort();
                    }
                    else
                    {
                        Questions.Text = "Имеются ли у Вас проблемы с кожей?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Кожа.png");
                    }
                    y = 0;
                    break;
                case "Имеются ли у Вас проблемы с кожей?":
                    if (y > 0)
                    {
                        Questions.Text = "Вас беспокоят заболевания акне?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Акне.png");
                    }
                    else
                    {
                        NotFound();
                    }
                    y = 0;
                    break;
                case "Вас беспокоят заболевания акне?":
                    if (y > 0)
                    {
                        teg = 12;
                        Sort();
                    }
                    else
                    {
                        NotFound();
                    }
                    y = 0;
                    break;
                // Вывод
                case "Хотите выполнить сортировку по цене?":
                    this.Close();
                    th = new Thread(form2);
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    if (y > 0)
                        st = true;
                    ListMed f1 = new ListMed(teg, st);
                    break;
            }


        }
        private void k_Yes_Click(object sender, EventArgs e)
        {
            y++;
            NextQuestion();
        }
        private void k_No_Click(object sender, EventArgs e)
        {
            NextQuestion();
        }

        private void Back_click(object sender, EventArgs e)
        {
            switch (Questions.Text)
            {
                case "У Вас имеется рецепт?":
                    Form1_Load(sender, e);
                    y = 0;
                    break;
                case "Введите название лекарственного препарата":
                    Start_Click(sender, e);
                    bSearch.Enabled = false;
                    bSearch.Visible = false;
                    Search.Enabled = false;
                    Search.Visible = false;
                    break;
                case "У Вас плохое самочувствие?":
                    Start_Click(sender, e);
                    bSearch.Enabled = false;
                    bSearch.Visible = false;
                    Search.Enabled = false;
                    Search.Visible = false;
                    break;
                case "Вы ощущаете признаки простуды?":
                    Questions.Text = "У Вас плохое самочувствие?";
                    pic_q.Image = Image.FromFile("img/Заболевания/Самочувствие.png");
                    break;
                case "Вам нужны средства индивидуальной защиты?":
                    Questions.Text = "У Вас плохое самочувствие?";
                    pic_q.Image = Image.FromFile("img/Заболевания/Самочувствие.png");
                    break;
                case "Вы чувствуете боли в горле?":
                    Questions.Text = "Вы ощущаете признаки простуды?";
                    pic_q.Image = Image.FromFile("img/Заболевания/Простуда.png");
                    break;
                case "Вас беспокоит насморк?":
                    Questions.Text = "Вы чувствуете боли в горле?";
                    pic_q.Image = Image.FromFile("img/Заболевания/Горло.png");
                    break;
                case "Вас беспокоит повышенная температура?":
                    Questions.Text = "Вас беспокоит насморк?";
                    pic_q.Image = Image.FromFile("img/Заболевания/Насморк.png");
                    break;
                case "Есть ли у вас сыпь?":
                    Questions.Text = "Вас беспокоит повышенная температура?";
                    pic_q.Image = Image.FromFile("img/Заболевания/Температура.png");
                    break;

                case "Вы испытываете головную боль?":
                    Questions.Text = "Вас беспокоит повышенная температура?";
                    pic_q.Image = Image.FromFile("img/Заболевания/Температура.png");
                    break;
                case "У Вас есть кашель?":
                    Questions.Text = "Вы испытываете головную боль?";
                    pic_q.Image = Image.FromFile("img/Заболевания/Голова.png");
                    break;
                case "У Вас есть проблемы с пищеварением?":
                    Questions.Text = "Вы ощущаете признаки простуды?";
                    pic_q.Image = Image.FromFile("img/Заболевания/Простуда.png");
                    break;
                case "У вас изжога?":
                    Questions.Text = "У Вас есть проблемы с пищеварением?";
                    pic_q.Image = Image.FromFile("img/Заболевания/Пищеварение.png");
                    break;
                case "Есть ли у вас признаки аллергии?":
                    Questions.Text = "У Вас есть проблемы с пищеварением?";
                    pic_q.Image = Image.FromFile("img/Заболевания/Пищеварение.png");
                    break;
                case "Вы испытываете боли в области сердца?":
                    Questions.Text = "Есть ли у вас признаки аллергии?";
                    pic_q.Image = Image.FromFile("img/Заболевания/Аллергия.png");
                    break;
                case "У Вас есть нарушения сна?":
                    Questions.Text = "Вы испытываете боли в области сердца?";
                    pic_q.Image = Image.FromFile("img/Заболевания/Сердце.png");
                    break;
                case "Имеются ли у Вас проблемы с кожей?":
                    Questions.Text = "У Вас есть нарушения сна?";
                    pic_q.Image = Image.FromFile("img/Заболевания/Сон.png");
                    break;
                case "Вас беспокоят заболевания акне?":
                    Questions.Text = "Имеются ли у Вас проблемы с кожей?";
                    pic_q.Image = Image.FromFile("img/Заболевания/Кожа.png");
                    break;
                // Вывод
                case "Хотите выполнить сортировку по цене?":
                    if (teg == 1)
                    {
                        Questions.Text = "Вы чувствуете боли в горле?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Горло.png");
                    }
                    else if (teg == 2)
                    {
                        Questions.Text = "Вас беспокоит насморк?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Насморк.png");
                    }
                    else if (teg == 3)
                    {
                        Questions.Text = "Вас беспокоит повышенная температура?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Температура.png");
                    }
                    else if (teg == 4)
                    {
                        Questions.Text = "Вы испытываете головную боль?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Голова.png");
                    }
                    else if (teg == 5)
                    {
                        Questions.Text = "Есть ли у вас признаки аллергии?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Аллергия.png");
                    }
                    else if (teg == 6)
                    {
                        Questions.Text = "Вы испытываете боли в области сердца?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Сердце.png");
                    }
                    else if (teg == 7)
                    {
                        Questions.Text = "Вы испытываете головную боль?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Голова.png");
                    }
                    else if (teg == 8)
                    {
                        Questions.Text = "У Вас есть нарушения сна?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Сон.png");
                    }
                    else if (teg == 9)
                    {
                        Questions.Text = "Вам необходимы средства индивидуальной защиты?";
                        pic_q.Image = Image.FromFile("img/Заболевания/СИЗ.png");
                    }
                    else if (teg == 11)
                    {
                        Questions.Text = "У Вас есть кашель?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Кашель.png");
                    }
                    else if (teg == 12)
                    {
                        Questions.Text = "Имеются ли у Вас проблемы с кожей?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Кожа.png");
                    }
                    else if (teg == 13)
                    {
                        Questions.Text = "Есть ли у вас сыпь?";
                        pic_q.Image = Image.FromFile("img/Заболевания/Ветрянка.png");
                    }
                    break;
            }
        }


        private void bSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Search.Text))
                {
                    BZ bz = new BZ();
                    DataTable table = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();

                    string zap;
                    zap = "SELECT `Наименование` FROM `Препараты` WHERE `Наименование` LIKE '" 
                    + Search.Text + "%' " +                      "GROUP BY Наименование";

                    MySqlCommand command = new MySqlCommand(zap, bz.getConnection());
                    
                    table = new DataTable();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    namemed = Search.Text;

                    if (table.Rows.Count >= 1)
                    {
                        if (table.Rows.Count == 1)
                        {
                            this.Close();
                            th = new Thread(form3);
                            th.SetApartmentState(ApartmentState.STA);
                            th.Start();
                        }
                        else
                        {
                            Поиск p = new Поиск(namemed);
                            p.ShowDialog();
                        }

                    }
                    else
                        MessageBox.Show("К сожалению, такого лекарства нет в базе", "Не найдено!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Вы не ввели название", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
               MessageBox.Show("Ошибка подключения  к базе данных", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void form2(object obj)
        {

            Application.Run(new ListMed(teg, st));

        }

        private void form3(object obj)
        {
            Application.Run(new InfoMed(namemed));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Авторизация f4 = new Авторизация();
            f4.ShowDialog();
        }

        // -- Наведение кнопок -- //
        private void SearchAgain_MouseHover(object sender, EventArgs e)
        {
            SearchAgain.Image = global::PodBor.Properties.Resources.Искать_1;
        }

        private void SearchAgain_MouseLeave(object sender, EventArgs e)
        {
            SearchAgain.Image = global::PodBor.Properties.Resources.Искать;
        }

        private void Start_MouseHover(object sender, EventArgs e)
        {
            Start.Image = global::PodBor.Properties.Resources.Поиск_лекарств_1;
        }

        private void Start_MouseLeave(object sender, EventArgs e)
        {
            Start.Image = global::PodBor.Properties.Resources.Поиск_лекарств;
        }

        private void k_Exit_MouseHover(object sender, EventArgs e)
        {
            k_Exit.Image = global::PodBor.Properties.Resources.Выход_1;
        }

        private void k_Exit_MouseLeave(object sender, EventArgs e)
        {
            k_Exit.Image = global::PodBor.Properties.Resources.Выход;
        }

        private void k_Yes_MouseHover(object sender, EventArgs e)
        {
            k_Yes.Image = global::PodBor.Properties.Resources.Да_1;
        }

        private void k_Yes_MouseLeave(object sender, EventArgs e)
        {
            k_Yes.Image = global::PodBor.Properties.Resources.Да;
        }

        private void k_No_MouseHover(object sender, EventArgs e)
        {
            k_No.Image = global::PodBor.Properties.Resources.нет_1;
        }

        private void k_No_MouseLeave(object sender, EventArgs e)
        {
            k_No.Image = global::PodBor.Properties.Resources.нет;
        }


        //private void bSearch_MouseLeave(object sender, EventArgs e)
        //{
        //    
        //}

        //private void bSearch_MouseEnter(object sender, EventArgs e)
        //{
        //    
        //}

        private void bSearch_MouseEnter(object sender, EventArgs e)
        {
            bSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            bSearch.BackgroundImage = global::PodBor.Properties.Resources.Поиск_лекарств_1;
        }

        private void bSearch_MouseLeave(object sender, EventArgs e)
        {
            bSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            bSearch.BackgroundImage = global::PodBor.Properties.Resources.Поиск_лекарств;
        }

        private void Back_MouseEnter(object sender, EventArgs e)
        {
            Back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            Back.BackgroundImage = global::PodBor.Properties.Resources.back_2;
        }

        private void Back_MouseLeave(object sender, EventArgs e)
        {
            Back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            Back.BackgroundImage = global::PodBor.Properties.Resources.back_1;
        }

        private void Find_Click(object sender, EventArgs e)
        {
            // отключение объектов
            label2.Visible = false;
            label2.Enabled = false;

            Start.Enabled = false;
            Start.Visible = false;

            pictureBox1.Visible = false;
            pictureBox1.Enabled = false;

            k_No.Visible = false;
            k_No.Enabled = false;

            k_Yes.Visible = false;
            k_Yes.Enabled = false;

            SearchAgain.Visible = false;
            SearchAgain.Enabled = false;
            // включение объектов
            k_Exit.Visible = true;
            k_Exit.Enabled = true;

            Questions.Visible = true;
            Questions.Text = "Введите название лекарственного препарата";

            pic_q.Visible = true;
            pic_q.Enabled = true;
            pic_q.Image = Image.FromFile("img/Оформление Форм/Поиск.png");

            bSearch.Visible = true;
            bSearch.Enabled = true;

            Search.Visible = true;
            Search.Enabled = true;
            Search.Focus();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }

        private void Main_Enter(object sender, EventArgs e)
        {
            bSearch_Click(sender, e);
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
