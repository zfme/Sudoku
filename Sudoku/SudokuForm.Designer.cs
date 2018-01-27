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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dosyaYukle = new System.Windows.Forms.Button();
            this.hemenCalistir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "fileDialog";
            // 
            // dosyaYukle
            // 
            this.dosyaYukle.Location = new System.Drawing.Point(300, 12);
            this.dosyaYukle.Name = "dosyaYukle";
            this.dosyaYukle.Size = new System.Drawing.Size(75, 23);
            this.dosyaYukle.TabIndex = 0;
            this.dosyaYukle.Text = "Yükle";
            this.dosyaYukle.UseVisualStyleBackColor = true;
            this.dosyaYukle.Click += new System.EventHandler(this.DosyaYukle_Click);
            // 
            // hemenCalistir
            // 
            this.hemenCalistir.Location = new System.Drawing.Point(290, 60);
            this.hemenCalistir.Name = "hemenCalistir";
            this.hemenCalistir.Size = new System.Drawing.Size(98, 23);
            this.hemenCalistir.TabIndex = 1;
            this.hemenCalistir.Text = "Hemen Çalıştır";
            this.hemenCalistir.UseVisualStyleBackColor = true;
            this.hemenCalistir.Click += new System.EventHandler(this.HemenCalistir_Click);
            // 
            // SudokuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 453);
            this.Controls.Add(this.hemenCalistir);
            this.Controls.Add(this.dosyaYukle);
            this.Name = "SudokuForm";
            this.Text = "Sudoku";
            this.Load += new System.EventHandler(this.SudokuForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button dosyaYukle;
        private System.Windows.Forms.Button hemenCalistir;
    }
}

