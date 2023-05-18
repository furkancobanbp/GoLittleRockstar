namespace GoLittleRockstar
{
    partial class Main
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.form11 = new GoLittleRockstar.Form1();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.controlGercekZamanliUretim1 = new GoLittleRockstar.controlGercekZamanliUretim();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.evrakNumaraVerme1 = new GoLittleRockstar.evrakNumaraVerme();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1241, 610);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.form11);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1233, 582);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Veri Çekme-Listeleme";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // form11
            // 
            this.form11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.form11.Location = new System.Drawing.Point(3, 3);
            this.form11.Name = "form11";
            this.form11.Size = new System.Drawing.Size(1227, 576);
            this.form11.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.controlGercekZamanliUretim1);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1233, 582);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Gerçek Zamanlı Üretim Listeleme";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // controlGercekZamanliUretim1
            // 
            this.controlGercekZamanliUretim1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlGercekZamanliUretim1.Location = new System.Drawing.Point(3, 3);
            this.controlGercekZamanliUretim1.Name = "controlGercekZamanliUretim1";
            this.controlGercekZamanliUretim1.Size = new System.Drawing.Size(1227, 576);
            this.controlGercekZamanliUretim1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.evrakNumaraVerme1);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1233, 582);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Evrak Numarası Verme-Listeleme";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // evrakNumaraVerme1
            // 
            this.evrakNumaraVerme1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.evrakNumaraVerme1.Location = new System.Drawing.Point(0, 0);
            this.evrakNumaraVerme1.Name = "evrakNumaraVerme1";
            this.evrakNumaraVerme1.Size = new System.Drawing.Size(1233, 582);
            this.evrakNumaraVerme1.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1241, 610);
            this.Controls.Add(this.tabControl1);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage2;
        private TabPage tabPage1;
        private Form1 form11;
        private controlGercekZamanliUretim controlGercekZamanliUretim1;
        private TabPage tabPage3;
        private evrakNumaraVerme evrakNumaraVerme1;
    }
}