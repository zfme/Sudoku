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

        private void SudokuForm_Load(object sender, EventArgs e)
        {

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
                //bool valid = board.IsValid();
                ////if (!valid)
                ////{
                ////    MessageBox.Show("Board hatalı");
                ////    return;
                ////}
                board.FillPossibleValues();

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Board solvedWithDfs = new DFSSolver().SolveWithDFS(board);
                stopwatch.Stop();
                Console.WriteLine("Time elapsed DFS: {0} ms", stopwatch.ElapsedMilliseconds);
                stopwatch.Restart();
                Board solvedWithLoq = new LoQSolver().Solve(board);
                stopwatch.Stop();
                Console.WriteLine("Time elapsed LoQ: {0} ms", stopwatch.ElapsedMilliseconds);
                Console.WriteLine("sonuc kontrol: {0}", solvedWithDfs.Equals(solvedWithLoq));

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }
    }
}

