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


namespace sudoku
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dosya_yukle_Click(object sender, EventArgs e)
        {
            Board board = new Board();

            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<TextBox> textBoxs = board.Oku(theDialog.FileName);
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
                    Board solvedBoard = new DFSSolver().SolveWithDFS(board);
                    stopwatch.Stop();
                    Console.WriteLine("Time elapsed: {0} ms", stopwatch.ElapsedMilliseconds);

                    Board sonuc = new LoQSolver().Solve(board);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }

            }
         

        }


    }
}

