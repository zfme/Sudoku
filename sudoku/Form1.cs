using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                    
                        using (StreamReader sr = new StreamReader(theDialog.FileName))
                        {
                            int y = 1;
                            int separatrixY = 1;
                            string numbers;
                            int row = 0;
                            while ((numbers = sr.ReadLine()) != null)
                            {
                                int x = 1;
                                int separatrixX = 0;
                                for (int i = 0; i < numbers.Length; i++)
                                {
                                    byte number = byte.Parse(numbers[i].ToString());
                                    if (separatrixX % 3 == 0)
                                    {
                                        x = x + 5;
                                    }

                                    TextBox tb = new TextBox();
                                    tb.Size = new System.Drawing.Size(15, 15);
                                    tb.TextAlign = contentAlign;
                                    if (number == 0)
                                    {
                                        tb.Text = " ";
                                        board.Table[row, i] = new Cell { value = 0 };
                                    }
                                    else
                                    {
                                        tb.Text = number.ToString();
                                        board.Table[row, i] = new Cell { value = number };
                                    }
                                    Point p = new Point(50 + x, 50 + y);
                                    tb.Location = p;
                                    this.Controls.Add(tb);
                                    separatrixX++;
                                    x = x + 15;
                                }

                                if (separatrixY % 3 == 0)
                                {
                                    y = y + 25;
                                }
                                else
                                {
                                    y = y + 20;
                                }

                                separatrixY++;
                                row++;
                            }
                        }
                    //board kontrol
                    bool valid = board.IsValid();
                    Console.WriteLine(valid);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }


        public HorizontalAlignment contentAlign { get; set; }

        public string lines { get; set; }
    }
}
////listBox1.Items.Clear();

//            // Dosyamızı okuyacak.
//            StreamReader oku;

//            // Belirtmiş olduğum yoldaki dosyayı açacak. 
//            /* NOT: @ bu işareti koymamın nedeni \\ 2 defa bundan 
//            yapmamak içindir. */
//            oku = File.OpenText(@"C:\Documents and Settings\YIGIT
//            \Belgelerim\visual studio 2010
//            \Projects\TxtVeriOkuma\TxtVeriOkuma\bin\Debug\C#.txt");

//            string yazi;

//            // Satır boş olana kadar okumaya devam eder.
//            while ((yazi = oku.ReadLine()) != null)
//            {
//                // Listbox'ı .txt içeriği ile doldur.
//                lstOku.Items.Add(yazi.ToString());
//            }
