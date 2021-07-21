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
using MySql.Data.MySqlClient;
using MetroFramework.Forms;

namespace PodBor
{
    public partial class ListMed : MetroForm
    {
        public ListMed(int teg, bool st)
        {
            InitializeComponent();
            this.teg = teg;
            this.st = st;

        }
        string namemed;
        int teg;
        bool st;
        Thread th;

        WaitFormFunc waitForm = new WaitFormFunc();

        void Form_Load(object sender, EventArgs e)
        {
            BZ bz = new BZ();
            string zap;
            connect.Enabled = false;
            connect.Visible = false;
            waitForm.Show(this);
            try
            {
                bz.openConnection();


                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                zap = "SELECT `Препараты`.`ID`, `Препараты`.`Фото`, `Препараты`.`Наименование`, " +
                        "`Препараты`.`Активное вещество`, `Форма`.`Форма`, `Препараты`.`Объем`, " +
                        "`Препараты`.`Производитель`, `Страна`.`Страна`, `Препараты`.`Рецепт`, " +
                        "`Препараты`.`Цена` FROM `Препараты` INNER JOIN `Форма` ON `Препараты`.`Форма` = `Форма`.`ID` " +
                        "	INNER JOIN `Страна` ON `Препараты`.`Страна` = `Страна`.`ID` WHERE `Препараты`.Показания =" + teg +
                        " GROUP BY `Препараты`.`Наименование`";
                if (st == true)
                    zap += " Order by `Цена` ASC";

                MySqlCommand command = new MySqlCommand(zap, bz.getConnection());

                table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);
                table.DefaultView.Sort = "ID DESC";
                // flowLayoutPanel1
                // 
                FlowLayoutPanel flowLayoutPanel5 = new FlowLayoutPanel();
                this.Controls.Add(flowLayoutPanel5);
                flowLayoutPanel5.BackColor = System.Drawing.Color.White; ;
                flowLayoutPanel5.AutoScroll = true;
                flowLayoutPanel5.Location = new System.Drawing.Point(2, 100);
                flowLayoutPanel5.BringToFront();
                flowLayoutPanel5.Size = new System.Drawing.Size(796, 466);
                flowLayoutPanel5.TabIndex = 0;

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    GroupBox groupdim = new GroupBox();
                    this.Controls.Add(groupdim);
                    flowLayoutPanel5.Controls.Add(groupdim);
                    groupdim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    groupdim.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
                    groupdim.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
                    groupdim.Location = new System.Drawing.Point(3, 3);
                    groupdim.Name = "";
                    groupdim.RightToLeft = System.Windows.Forms.RightToLeft.No;
                    if (table.Rows.Count >= 2)
                        groupdim.Size = new System.Drawing.Size(773, 230);
                    else
                        groupdim.Size = new System.Drawing.Size(790, 230);
                    groupdim.TabIndex = 0;
                    groupdim.TabStop = false;

                    ////////////   Название лекарства    /////////////////
                    Label lblname = new Label();
                    this.Controls.Add(lblname);
                    groupdim.Controls.Add(lblname);

                    lblname.Click += new System.EventHandler(this.lblname_Click);
                    lblname.MouseLeave += new System.EventHandler(this.lblname_Leave);
                    lblname.MouseEnter += new System.EventHandler(this.lblname_Hover);
                    lblname.Font = new System.Drawing.Font("Open Sans", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblname.ForeColor = System.Drawing.SystemColors.WindowText;
                    lblname.Location = new System.Drawing.Point(277, 10);
                    lblname.Size = new System.Drawing.Size(480, 30);
                    lblname.AutoSize = true;
                    lblname.TabIndex = 23;
                    lblname.Text = table.Rows[i][2].ToString();
                    lblname.TextAlign = System.Drawing.ContentAlignment.TopLeft;



                    // Картинка
                    // 
                    PictureBox picdim = new PictureBox();
                    this.Controls.Add(picdim);
                    groupdim.Controls.Add(picdim);
                    picdim.Location = new System.Drawing.Point(60, 10);
                    picdim.Size = new System.Drawing.Size(210, 210);
                    picdim.TabIndex = 1;
                    picdim.TabStop = false;
                    picdim.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    byte[] imgData1 = (byte[])table.Rows[i][1];
                    MemoryStream ms1 = new MemoryStream(imgData1);
                    picdim.Image = Image.FromStream(ms1);
                    /////////////////////////////

                    // Цена
                    // 
                    GroupBox groupCost = new GroupBox();
                    flowLayoutPanel5.Controls.Add(groupCost);
                    groupdim.Controls.Add(groupCost);

                    groupCost.Font = new System.Drawing.Font("Roboto Condensed", 11.25F, System.Drawing.FontStyle.Bold);
                    groupCost.ForeColor = System.Drawing.Color.SeaGreen;
                    groupCost.Location = new System.Drawing.Point(580, 80);
                    groupCost.Name = "groupBox1";
                    groupCost.Size = new System.Drawing.Size(171, 78);
                    groupCost.TabIndex = 32;
                    groupCost.TabStop = false;
                    groupCost.Text = "Цена:";
                    groupCost.ForeColor = System.Drawing.Color.SeaGreen;
                    groupCost.TabStop = false;

                    ////////////   Цена лекарства    /////////////////
                    Label lblcost = new Label();
                    Controls.Add(lblcost);
                    groupCost.Controls.Add(lblcost);

                    lblcost.Font = new System.Drawing.Font("Arial", 30F);
                    lblcost.ForeColor = System.Drawing.Color.Black;
                    lblcost.ImeMode = System.Windows.Forms.ImeMode.NoControl;
                    lblcost.Location = new System.Drawing.Point(3, 21);
                    lblcost.Margin = new System.Windows.Forms.Padding(3, 0, 50, 0);
                    lblcost.Size = new System.Drawing.Size(165, 45);
                    lblcost.Text = table.Rows[i][9].ToString() + " ₽";
                    lblcost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    ////////////   Краткая информация    /////////////////
                    Label lblinfo1 = new Label();
                    this.Controls.Add(lblinfo1);
                    groupdim.Controls.Add(lblinfo1);
                    lblinfo1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblinfo1.ForeColor = System.Drawing.Color.SeaGreen;
                    lblinfo1.Location = new System.Drawing.Point(280, 55);
                    lblinfo1.Size = new System.Drawing.Size(210, 20);
                    lblinfo1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
                    lblinfo1.Text = "Краткая информация:";

                    Label lblinfo2 = new Label();
                    this.Controls.Add(lblinfo2);
                    groupdim.Controls.Add(lblinfo2);
                    lblinfo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblinfo2.ForeColor = System.Drawing.Color.DimGray;
                    lblinfo2.Location = new System.Drawing.Point(280, 80);
                    lblinfo2.Size = new System.Drawing.Size(103, 18);
                    lblinfo2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
                    lblinfo2.Text = "Код товара:";

                    Label lblinfo3 = new Label();
                    this.Controls.Add(lblinfo3);
                    groupdim.Controls.Add(lblinfo3);
                    lblinfo3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblinfo3.ForeColor = System.Drawing.Color.DimGray;
                    lblinfo3.Location = new System.Drawing.Point(280, 110);
                    lblinfo3.Size = new System.Drawing.Size(136, 18);
                    lblinfo3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
                    lblinfo3.Text = "Производитель:";

                    Label lblinfo4 = new Label();
                    this.Controls.Add(lblinfo4);
                    groupdim.Controls.Add(lblinfo4);
                    lblinfo4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblinfo4.ForeColor = System.Drawing.Color.DimGray;
                    lblinfo4.Location = new System.Drawing.Point(280, 140);
                    lblinfo4.Size = new System.Drawing.Size(69, 18);
                    lblinfo4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
                    lblinfo4.Text = "Страна:";

                    Label lblinfo5 = new Label();
                    this.Controls.Add(lblinfo5);
                    groupdim.Controls.Add(lblinfo5);
                    lblinfo5.ForeColor = System.Drawing.Color.DimGray;
                    lblinfo5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblinfo5.Location = new System.Drawing.Point(280, 170);
                    lblinfo5.Size = new System.Drawing.Size(137, 18);
                    lblinfo5.TextAlign = System.Drawing.ContentAlignment.TopLeft;
                    lblinfo5.Text = "Форма выпуска:";

                    Label lblinfo6 = new Label();
                    this.Controls.Add(lblinfo6);
                    groupdim.Controls.Add(lblinfo6);
                    lblinfo6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblinfo6.ForeColor = System.Drawing.Color.DimGray;
                    lblinfo6.Location = new System.Drawing.Point(280, 200);
                    lblinfo6.Size = new System.Drawing.Size(113, 18);
                    lblinfo6.TextAlign = System.Drawing.ContentAlignment.TopLeft;
                    lblinfo6.Text = "Отпускается:";

                    // Информация-данные
                    // код
                    Label lblinfotext1 = new Label();
                    this.Controls.Add(lblinfotext1);
                    groupdim.Controls.Add(lblinfotext1);
                    lblinfotext1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblinfotext1.ForeColor = System.Drawing.Color.Black;
                    lblinfotext1.Location = new System.Drawing.Point(384, 82);
                    lblinfotext1.Size = new System.Drawing.Size(120, 20);
                    lblinfotext1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
                    lblinfotext1.Text = "000" + table.Rows[i][0].ToString();
                    // Производитель
                    Label lblinfotext2 = new Label();
                    this.Controls.Add(lblinfotext2);
                    groupdim.Controls.Add(lblinfotext2);
                    lblinfotext2.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblinfotext2.ForeColor = System.Drawing.Color.Black;
                    lblinfotext2.Location = new System.Drawing.Point(417, 112);
                    lblinfotext2.Size = new System.Drawing.Size(180, 20);
                    lblinfotext2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
                    lblinfotext2.Text = table.Rows[i][6].ToString();
                    // Страна
                    Label lblinfotext3 = new Label();
                    this.Controls.Add(lblinfotext3);
                    groupdim.Controls.Add(lblinfotext3);
                    lblinfotext3.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblinfotext3.ForeColor = System.Drawing.Color.Black;
                    lblinfotext3.Location = new System.Drawing.Point(350, 142);
                    lblinfotext3.Size = new System.Drawing.Size(180, 20);
                    lblinfotext3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
                    lblinfotext3.Text = table.Rows[i][7].ToString();
                    ////////////   Форма и объем лекарства    /////////////////
                    Label lblform = new Label();
                    this.Controls.Add(lblform);
                    groupdim.Controls.Add(lblform);
                    lblform.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblform.ForeColor = System.Drawing.Color.Black;
                    lblform.Location = new System.Drawing.Point(418, 172);
                    lblform.Size = new System.Drawing.Size(180, 20);
                    lblform.Text = table.Rows[i][4].ToString() + " (" + table.Rows[i][5].ToString() + ")";
                    lblform.TextAlign = System.Drawing.ContentAlignment.TopLeft;
                    // Рецепт
                    Label lblinfotext4 = new Label();
                    this.Controls.Add(lblinfotext4);
                    groupdim.Controls.Add(lblinfotext4);
                    lblinfotext4.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblinfotext4.ForeColor = System.Drawing.Color.Black;
                    lblinfotext4.Location = new System.Drawing.Point(394, 202);
                    lblinfotext4.Size = new System.Drawing.Size(134, 20);
                    lblinfotext4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
                    if (Convert.ToInt32(table.Rows[i][8]) != 0)
                        lblinfotext4.Text = "По рецепту";
                    else
                        lblinfotext4.Text = "Без рецепта";


                }
                waitForm.Close();
                this.Activate();
            }
            catch
            {
                MessageBox.Show("Ошибка подключения  к базе данных", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                waitForm.Close();
                this.Activate();
                connect.Enabled = true;
                connect.Visible = true;
            }
           

        }

        private void lblname_Hover(object sender, EventArgs e)
        {
            var label = (Label)sender;
            metroToolTip1.SetToolTip(label, "Нажмите, чтобы узнать подробнее");
            label.Font = new System.Drawing.Font("Open Sans", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label.ForeColor = System.Drawing.Color.SeaGreen;
        }

        private void lblname_Leave(object sender, EventArgs e)
        {
            var label = (Label)sender;
            
            label.Font = new System.Drawing.Font("Open Sans", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label.ForeColor = System.Drawing.Color.Black;
        }

        private void lblname_Click(object sender, EventArgs e)
        { 
            var label = (Label)sender;
            if (label != null)
            {
                BZ bz = new BZ();
                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                string zap;

                zap = "SELECT Наименование FROM Препараты WHERE Наименование=(@namemed) GROUP BY Наименование";
                MySqlCommand command = new MySqlCommand(zap, bz.getConnection());
                command.Parameters.AddWithValue("namemed", label.Text);
                table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count != 0)
                {
                    namemed = label.Text;
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
            Application.Run(new InfoMed(namemed));
        }

        private void SearchAgain_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(form1);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        private void form1(object obj)
        {
            Application.Run(new Главная());
        }

        private void k_Exit_Click(object sender, EventArgs e)
        {
            Close();
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
