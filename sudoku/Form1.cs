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
                    List<TextBox> textBoxs = board.Oku(theDialog.FileName);
                    foreach (var textBox in textBoxs)
                    {
                        Controls.Add(textBox);
                    }
                    bool valid = board.IsValid();
                    if (!valid)
                    {
                        MessageBox.Show("Board hatalı");
                    }
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
