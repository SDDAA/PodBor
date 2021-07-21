namespace PodBor
{
    partial class ListMed
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListMed));
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.k_Exit = new System.Windows.Forms.PictureBox();
            this.SearchAgain = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.connect = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.k_Exit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SearchAgain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.SeaGreen;
            this.label1.Location = new System.Drawing.Point(147, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(503, 37);
            this.label1.TabIndex = 15;
            this.label1.Text = "*Настояльтельно рекомендуем Вам перед покупкой любого препарата \r\nпроконсультиров" +
    "аться со специалистом.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.button3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button3.Location = new System.Drawing.Point(768, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(31, 31);
            this.button3.TabIndex = 72;
            this.button3.Text = "x";
            this.button3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button3_MouseDown);
            // 
            // k_Exit
            // 
            this.k_Exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.k_Exit.Image = global::PodBor.Properties.Resources.Выход;
            this.k_Exit.Location = new System.Drawing.Point(440, 579);
            this.k_Exit.Name = "k_Exit";
            this.k_Exit.Size = new System.Drawing.Size(210, 70);
            this.k_Exit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.k_Exit.TabIndex = 13;
            this.k_Exit.TabStop = false;
            this.k_Exit.Click += new System.EventHandler(this.k_Exit_Click);
            this.k_Exit.MouseEnter += new System.EventHandler(this.k_Exit_MouseHover);
            this.k_Exit.MouseLeave += new System.EventHandler(this.k_Exit_MouseLeave);
            // 
            // SearchAgain
            // 
            this.SearchAgain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SearchAgain.Image = global::PodBor.Properties.Resources.Искать;
            this.SearchAgain.Location = new System.Drawing.Point(150, 579);
            this.SearchAgain.Name = "SearchAgain";
            this.SearchAgain.Size = new System.Drawing.Size(210, 70);
            this.SearchAgain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SearchAgain.TabIndex = 12;
            this.SearchAgain.TabStop = false;
            this.SearchAgain.Click += new System.EventHandler(this.SearchAgain_Click);
            this.SearchAgain.MouseEnter += new System.EventHandler(this.SearchAgain_MouseHover);
            this.SearchAgain.MouseLeave += new System.EventHandler(this.SearchAgain_MouseLeave);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::PodBor.Properties.Resources.Logo;
            this.pictureBox3.Location = new System.Drawing.Point(2, 6);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(795, 63);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 14;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox3_MouseDown);
            this.pictureBox3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox3_MouseMove);
            this.pictureBox3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox3_MouseUp);
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Green;
            // 
            // connect
            // 
            this.connect.AutoSize = true;
            this.connect.Font = new System.Drawing.Font("Century Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connect.ForeColor = System.Drawing.Color.IndianRed;
            this.connect.Location = new System.Drawing.Point(96, 300);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(610, 36);
            this.connect.TabIndex = 73;
            this.connect.Text = "Отсутствует подключение к Интернету";
            // 
            // ListMed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(800, 660);
            this.Controls.Add(this.connect);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.k_Exit);
            this.Controls.Add(this.SearchAgain);
            this.Controls.Add(this.pictureBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListMed";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.k_Exit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SearchAgain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox SearchAgain;
        private System.Windows.Forms.PictureBox k_Exit;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private System.Windows.Forms.Label connect;
    }
}