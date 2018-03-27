namespace Sudoku
{
    partial class SudokuForm
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
            this.components = new System.ComponentModel.Container();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dosyaYukle = new System.Windows.Forms.Button();
            this.hemenCalistir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.theadSayisiTextBox = new System.Windows.Forms.TextBox();
            this.maxChildCountTextBox = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label8 = new System.Windows.Forms.Label();
            this.metrikComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.calismaSayisiTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "fileDialog";
            // 
            // dosyaYukle
            // 
            this.dosyaYukle.Location = new System.Drawing.Point(291, 12);
            this.dosyaYukle.Name = "dosyaYukle";
            this.dosyaYukle.Size = new System.Drawing.Size(75, 23);
            this.dosyaYukle.TabIndex = 0;
            this.dosyaYukle.Text = "Yükle";
            this.dosyaYukle.UseVisualStyleBackColor = true;
            this.dosyaYukle.Click += new System.EventHandler(this.DosyaYukle_Click);
            // 
            // hemenCalistir
            // 
            this.hemenCalistir.Location = new System.Drawing.Point(290, 52);
            this.hemenCalistir.Name = "hemenCalistir";
            this.hemenCalistir.Size = new System.Drawing.Size(98, 23);
            this.hemenCalistir.TabIndex = 1;
            this.hemenCalistir.Text = "Hemen Çalıştır";
            this.hemenCalistir.UseVisualStyleBackColor = true;
            this.hemenCalistir.Click += new System.EventHandler(this.HemenCalistir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(287, 352);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "DFS:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(364, 352);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "....";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(287, 397);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "PDSFQ:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(364, 397);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "....";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(364, 432);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "....";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(299, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Thread Sayısı:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(288, 164);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Max Child Count:";
            // 
            // theadSayisiTextBox
            // 
            this.theadSayisiTextBox.Location = new System.Drawing.Point(391, 130);
            this.theadSayisiTextBox.Name = "theadSayisiTextBox";
            this.theadSayisiTextBox.Size = new System.Drawing.Size(54, 20);
            this.theadSayisiTextBox.TabIndex = 10;
            this.theadSayisiTextBox.Text = "2";
            // 
            // maxChildCountTextBox
            // 
            this.maxChildCountTextBox.Location = new System.Drawing.Point(391, 159);
            this.maxChildCountTextBox.Name = "maxChildCountTextBox";
            this.maxChildCountTextBox.Size = new System.Drawing.Size(54, 20);
            this.maxChildCountTextBox.TabIndex = 11;
            this.maxChildCountTextBox.Text = "2";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(290, 290);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(155, 23);
            this.progressBar1.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(334, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Metrik:";
            // 
            // metrikComboBox
            // 
            this.metrikComboBox.FormattingEnabled = true;
            this.metrikComboBox.Items.AddRange(new object[] {
            "ms",
            "ticks"});
            this.metrikComboBox.Location = new System.Drawing.Point(391, 97);
            this.metrikComboBox.Name = "metrikComboBox";
            this.metrikComboBox.Size = new System.Drawing.Size(54, 21);
            this.metrikComboBox.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(288, 254);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Çalışma Sayısı";
            // 
            // calismaSayisiTextBox
            // 
            this.calismaSayisiTextBox.Location = new System.Drawing.Point(391, 251);
            this.calismaSayisiTextBox.Name = "calismaSayisiTextBox";
            this.calismaSayisiTextBox.Size = new System.Drawing.Size(54, 20);
            this.calismaSayisiTextBox.TabIndex = 16;
            this.calismaSayisiTextBox.Text = "1";
            // 
            // SudokuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(940, 533);
            this.Controls.Add(this.calismaSayisiTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.metrikComboBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.maxChildCountTextBox);
            this.Controls.Add(this.theadSayisiTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hemenCalistir);
            this.Controls.Add(this.dosyaYukle);
            this.Name = "SudokuForm";
            this.Text = "Sudoku";
            this.Load += new System.EventHandler(this.SudokuForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SudokuForm_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button dosyaYukle;
        private System.Windows.Forms.Button hemenCalistir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox theadSayisiTextBox;
        private System.Windows.Forms.TextBox maxChildCountTextBox;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox metrikComboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox calismaSayisiTextBox;
    }
}

