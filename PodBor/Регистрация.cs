using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PodBor
{
    public partial class Регистрация : MetroForm
    {
        public Регистрация()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Close();
        }

        private void Войти_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Пока не доступно");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Пока не доступно");
        }
    }
}
