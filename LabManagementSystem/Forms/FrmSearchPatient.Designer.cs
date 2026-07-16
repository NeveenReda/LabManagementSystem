namespace LabManagementSystem.Forms
{
    partial class FrmSearchPatient
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
            pnlTitle = new Panel();
            label1 = new Label();
            panel1 = new Panel();
            label2 = new Label();
            textBox1 = new TextBox();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            pnlTitle.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pnlTitle
            // 
            pnlTitle.BackColor = Color.Gainsboro;
            pnlTitle.Controls.Add(label1);
            pnlTitle.Dock = DockStyle.Top;
            pnlTitle.Location = new Point(0, 0);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Size = new Size(1841, 60);
            pnlTitle.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.RoyalBlue;
            label1.Location = new Point(656, 9);
            label1.Name = "label1";
            label1.Size = new Size(286, 38);
            label1.TabIndex = 1;
            label1.Text = "شاشة البحث عن مريض";
            label1.Click += label1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(label2);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 60);
            panel1.Name = "panel1";
            panel1.Size = new Size(1841, 70);
            panel1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(55, 71, 79);
            label2.Location = new Point(859, 5);
            label2.Name = "label2";
            label2.Size = new Size(239, 28);
            label2.TabIndex = 2;
            label2.Text = "بحث بالاسم او كود المريض";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(527, 5);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(270, 27);
            textBox1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.searchIcon1;
            pictureBox1.Location = new Point(462, -6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(42, 36);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.RoyalBlue;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(58, 5);
            button1.Name = "button1";
            button1.Size = new Size(154, 42);
            button1.TabIndex = 4;
            button1.Text = "إضافة مريض ➕  ";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // FrmSearchPatient
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1841, 591);
            Controls.Add(panel1);
            Controls.Add(pnlTitle);
            Name = "FrmSearchPatient";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            Text = "FrmSearchPatient";
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTitle;
        private Label label1;
        private Panel panel1;
        private TextBox textBox1;
        private Button button1;
        private PictureBox pictureBox1;
        private Label label2;
    }
}