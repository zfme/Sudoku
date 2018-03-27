using Serilog;
using Serilog.Core;
using Serilog.Events;
using Sudoku.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Sudoku
{
    public partial class SudokuForm : Form
    {
         

        public SudokuForm()
        {
            InitializeComponent();
        }
        
        private void SudokuForm_Paint(object sender, PaintEventArgs e)
        {

            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            e.Graphics.DrawLine(pen, 250, 200, 550, 200);
        }

        private void SudokuForm_Load(object sender, EventArgs e)
        {
            var logSwitch = new LoggingLevelSwitch();
            logSwitch.MinimumLevel = LogEventLevel.Information;
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(logSwitch)
                .WriteTo.File("logs\\sudokulog.txt", rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.ffffff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
            metrikComboBox.SelectedItem = "ms";
        }

        private void DosyaYukle_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog
            {
                Title = "Open Text File",
                Filter = "TXT files|*.txt",
                InitialDirectory = @"C:\"
            };
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                dosyaKullanarakCoz(theDialog.FileName);
            }

        }

        private void HemenCalistir_Click(object sender, EventArgs e)
        {
            dosyaKullanarakCoz("C:\\sudokuxxhard.txt");
        }

        private void dosyaKullanarakCoz(string dosyaPath)
        {
            try
            {
                Board board = new Board();
                List<TextBox> textBoxs = board.Oku(dosyaPath);
                foreach (var textBox in textBoxs)
                {
                    Controls.Add(textBox);
                }
                bool valid = board.IsValid();
                if (!valid)
                {
                    MessageBox.Show("Board hatalı");
                    return;
                }
                board.FillPossibleValues();

                int threadCount = Convert.ToInt32(theadSayisiTextBox.Text);
                int maxChildCount = Convert.ToInt32(maxChildCountTextBox.Text);
                string metrik = metrikComboBox.SelectedItem.ToString();
                int denemeSayisi = Convert.ToInt32(calismaSayisiTextBox.Text);
                progressBar1.Value = 0;
                progressBar1.PerformStep();

                // DFS çözüm
                BoardCozum solvedWithDfs = CozVeOrtalamaDondur(board, new DFSSolver(), denemeSayisi, metrik, true);
                label2.Text = string.Format("{0:0.000} , toplam: {1}\r\nDetay: {2}", solvedWithDfs.Sure, solvedWithDfs.Total, solvedWithDfs.Detail);
                // LoQ çözüm
                BoardCozum solvedWithLoq = CozVeOrtalamaDondur(board, new LoQSolver(threadCount, maxChildCount), denemeSayisi, metrik, true);
                label4.Text = string.Format("{0:0.000} , toplam: {1}\r\nDetay: {2}", solvedWithLoq.Sure, solvedWithLoq.Total, solvedWithLoq.Detail);
                progressBar1.PerformStep();
                progressBar1.Value = 50;
                // Çözüm karşılaştırma
                label5.Text = string.Format("Sonuç kontrol: {0}", solvedWithDfs.CozumlerAyniMi(solvedWithLoq));
                progressBar1.PerformStep();
                progressBar1.Value = 100;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }

        private BoardCozum CozVeOrtalamaDondur(Board board, ISolver solver, int denemeSayisi, string metrik, bool ilkiniAl)
        {
            long total = 0;
            int deneme = 0;
            List<Board> SolutionBoards = new List<Board>();
            string detail = "";
            for (int i = 0; i < denemeSayisi; i++)
            {
                Board copy = board.Copy();
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Board aSolutionBoard = solver.Solve(copy);
                SolutionBoards.Add(aSolutionBoard);
                stopwatch.Stop();
                solver.Temizle();
                long gecen = metrik.Equals("ms") ? stopwatch.ElapsedMilliseconds : stopwatch.ElapsedTicks;
                detail += gecen + ",";
                if (!ilkiniAl && i == 0)
                {                   
                    continue;
                }
                deneme++;
                total += gecen;
            }
            double avg = ((double)total) / deneme;
            return new BoardCozum
            {
                Total = total,
                Detail = detail.Remove(detail.Length - 1),
                CozumBoards = SolutionBoards,
                Sure = avg
            };
        }

    }
}

