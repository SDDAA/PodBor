using MetroFramework.Forms;
using MetroFramework.Animation;
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
using MySql.Data.MySqlClient;

namespace PodBor
{
    public partial class Загрузка : Form
    {
        public Загрузка()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        public Загрузка(Form parent)
        {
            InitializeComponent();
            if (parent != null)
            {
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                
            }
            else
                this.StartPosition = FormStartPosition.CenterScreen;
            BZ bz = new BZ();
            try
            {
                bz.getConnection();
            }
            catch
            {
                MessageBox.Show("Системная ошибка", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void CloseWaitForm()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }
        }

    }
}
