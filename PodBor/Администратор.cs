using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;




namespace PodBor
{
    public partial class Администратор : Form
    {
        public Администратор()
        {
            InitializeComponent();
        }

        WaitFormFunc waitForm = new WaitFormFunc();
        private void Adminstr_Load(object sender, EventArgs e)
        {
            this.AcceptButton = Add;
            waitForm.Show(this);
            BZ bz = new BZ();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            string zap1 = "SELECT ID, Форма FROM Форма ORDER BY Форма ASC";
            string zap2 = "SELECT ID, Показание FROM Показания ORDER BY Показание ASC";
            string zap3 = "SELECT ID, Противопоказание FROM Противопоказания";
            string zap4 = "SELECT ID, Страна FROM Страна ORDER BY Страна ASC";
            MySqlCommand command1 = new MySqlCommand(zap1, bz.getConnection());
            table = new DataTable();
            adapter.SelectCommand = command1;
            adapter.Fill(table);
            Форма.DataSource = table;
            Форма.DisplayMember = "Форма";// столбец для отображения
            Форма.ValueMember = "ID";//столбец с id
            Форма.IntegralHeight = false;
            Форма.MaxDropDownItems = 5;
            Форма.DropDownStyle = ComboBoxStyle.DropDownList;

            MySqlCommand command2 = new MySqlCommand(zap2, bz.getConnection());
            table = new DataTable();
            adapter.SelectCommand = command2;
            adapter.Fill(table);
            Показания.DataSource = table;
            Показания.DisplayMember = "Показание";
            Показания.ValueMember = "ID";
            Показания.IntegralHeight = false;
            Показания.MaxDropDownItems = 5;
            Показания.DropDownStyle = ComboBoxStyle.DropDownList;

            MySqlCommand command3 = new MySqlCommand(zap3, bz.getConnection());
            table = new DataTable();
            adapter.SelectCommand = command3;
            adapter.Fill(table);
            Противопоказания.DataSource = table;
            Противопоказания.DisplayMember = "Противопоказание";// столбец для отображения
            Противопоказания.ValueMember = "ID";//столбец с id
            Противопоказания.IntegralHeight = false;
            Противопоказания.MaxDropDownItems = 5;
            Противопоказания.DropDownStyle = ComboBoxStyle.DropDownList;

            MySqlCommand command4 = new MySqlCommand(zap4, bz.getConnection());
            table = new DataTable();
            adapter.SelectCommand = command4;
            adapter.Fill(table);
            Страна.DataSource = table;
            Страна.DisplayMember = "Страна";// столбец для отображения
            Страна.ValueMember = "ID";//столбец с id
            Страна.IntegralHeight = false;
            Страна.MaxDropDownItems = 5;
            Страна.DropDownStyle = ComboBoxStyle.DropDownList;

            Наименование.Multiline = true;
            Вещество.Multiline = true;
            Объем.Multiline = true;
            Состав.Multiline = true;
            Фармакодинамика.Multiline = true;
            Фармакокинетика.Multiline = true;
            Побочные.Multiline = true;
            
            ////
            //Показания.BackColor = System.Drawing.Color("White");
            waitForm.Close();
        }

        private void button3_MouseDown(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Image Files(*.BMP,*.JPG,*.GIF,*.PNG)|*.BMP;*.JPG;" +
                "*.GIF;*.PNG";
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
            Text = openFileDialog1.FileName;
            if (openFileDialog1.FileName != "")
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(Text);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            }


        }

        private void Add_Click(object sender, EventArgs e)
        {
            BZ bz = new BZ();
            bz.openConnection();
            if ((!string.IsNullOrEmpty(Наименование.Text)) && (!string.IsNullOrEmpty(Вещество.Text)) &&
                (!string.IsNullOrEmpty(Объем.Text)) && (!string.IsNullOrEmpty(Состав.Text)) &&
                (!string.IsNullOrEmpty(Фармакодинамика.Text)) && (!string.IsNullOrEmpty(Фармакокинетика.Text)) &&
                (!string.IsNullOrEmpty(Побочные.Text)) && (!string.IsNullOrEmpty(Производитель.Text)) &&
                (!string.IsNullOrEmpty(Цена.Text)))
            {
                try
                {
                    // конвертация изображения в байты
                    MemoryStream stream = new MemoryStream();
                    if (pictureBox1.Image != null)
                        pictureBox1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    else
                    {
                        MessageBox.Show("Вы не выбрали фото для препарата!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    byte[] pic = stream.ToArray();
                    MySqlCommand command = new MySqlCommand("INSERT INTO `Препараты` (`Фото`, `Наименование`, " +
                        "`Активное вещество`, `Форма`, `Объем`, `Состав`, `Фармакодинамика`, `Фармакокинетика`, " +
                        "`Показания`, `Противопоказания`, `Побочнные_действия`, `Производитель`, " +
                        "`Страна`, `Рецепт`, `Цена`) VALUES(@Фото, @Наименование, @Вещество, @Форма, " +
                        "@Объем, @Состав, @Фармакодинамика, @Фармакокинетика," +
                        "@Показания, @Противопоказания, @Побочнные_действия, @Производитель, " +
                        "@Страна, @Рецепт, @Цена)", bz.getConnection());

                    command.Parameters.AddWithValue("@Фото", pic); // записываем само изображение
                    command.Parameters.AddWithValue("@Наименование", Наименование.Text);
                    command.Parameters.AddWithValue("@Вещество", Вещество.Text);
                    command.Parameters.AddWithValue("@Форма", Форма.SelectedValue);
                    command.Parameters.AddWithValue("@Объем", Объем.Text);
                    command.Parameters.AddWithValue("@Состав", Состав.Text);
                    command.Parameters.AddWithValue("@Фармакодинамика", Фармакодинамика.Text);
                    command.Parameters.AddWithValue("@Фармакокинетика", Фармакокинетика.Text);
                    command.Parameters.AddWithValue("@Показания", Показания.SelectedValue);
                    command.Parameters.AddWithValue("@Противопоказания", Противопоказания.SelectedValue);
                    command.Parameters.AddWithValue("@Побочнные_действия", Побочные.Text);
                    command.Parameters.AddWithValue("@Производитель", Производитель.Text);
                    command.Parameters.AddWithValue("@Страна", Страна.SelectedValue);
                    command.Parameters.AddWithValue("@Рецепт", Рецепт.Checked);
                    command.Parameters.AddWithValue("@Цена", Цена.Text);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Информация добавлена");
                }
                catch
                {
                    MessageBox.Show("Ошибка при вводе данных!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Не все поля были заполнены!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Наименование.Text = "";
            Вещество.Text = "";
            Форма.Text = "";
            Объем.Text = "";
            Состав.Text = "";
            Фармакодинамика.Text = "";
            Фармакокинетика.Text = "";
            Побочные.Text = "";
            Показания.Text = "";
            Производитель.Text = "";
            Противопоказания.Text = "";
            Страна.Text = "";
            Цена.Text = "";
            Рецепт.Checked = false;
        }


        // -- Перемещение формы -- //
        private bool drag = false;
        private Point start_point = new Point(0, 0);

        private void Admin_MouseMove(object sender, MouseEventArgs e)
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

        private void Admin_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.drag = false;
            }
        }

        private void Admin_MouseDown(object sender, MouseEventArgs e)
        {
            this.drag = true;
            this.start_point = new Point(e.X, e.Y);
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            this.drag = true;
            this.start_point = new Point(e.X, e.Y);
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
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

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.drag = false;
            }
        }

        // -- Перемещение формы -- //

        private void Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            BZ bz = new BZ();
            bz.closeConnection();
            Главная m = new Главная();
            m.Focus();
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Close();
        }
    }
}
