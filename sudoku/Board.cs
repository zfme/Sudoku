using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudoku
{
    class Board
    {
        public Cell[,] Table = new Cell[9, 9];
        public State State { get; set; }

        public Board Copy()
        {
            var board = new Board {State = State};
            var newTable = new Cell[9,9];
            for (var index = 0; index < 9; index++)
            {
                for (var indexy = 0; indexy < 9; indexy++)
                {
                    newTable[index, indexy] = Table[index, indexy].Copy();
                }
            }
            board.Table = newTable;
            return board;
        }

        public void FillPossibleValues()
        {
            while (true)
            {
                bool fillAgain = false;
                for (var index = 0; index < 9; index++)
                {
                    for (var indexy = 0; indexy < 9; indexy++)
                    {
                        var cell = Table[index, indexy];
                        var cellValues = new List<byte>();
                        if (cell.Value == 0)
                        {
                            for (int i = 1; i <= 9; i++)
                            {
                                cell.Value = Convert.ToByte(i);
                                if (IsValid())
                                {
                                    cellValues.Add(Convert.ToByte(i));
                                }
                                cell.Value = 0;
                            }
                            cell.PossibleValues = cellValues;
                            if (cell.PossibleValues.Count == 1)
                            {
                                cell.Value = cell.PossibleValues[0];
                                fillAgain = true;
                                break;
                            }
                        }
                    }
                    if (fillAgain)
                    {
                        break;
                    }
                }
                if (fillAgain)
                {
                    continue;
                }
                break;
            }
        }

        public List<TextBox> Oku(string fileName)
        {
            List<TextBox> liste = new List<TextBox>();

            using (StreamReader sr = new StreamReader(fileName))
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

                        if (number == 0)
                        {
                            tb.Text = " ";
                            Table[row, i] = new Cell { Value = 0 };
                        }
                        else
                        {
                            tb.Text = number.ToString();
                            Table[row, i] = new Cell { Value = number };
                        }
                        Point p = new Point(50 + x, 50 + y);
                        tb.Location = p;

                        liste.Add(tb);
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
            return liste;
        }

        public bool IsValid()
        {
            Cell[] row = new Cell[9];

            // satır
            for (var rowIndex = 0; rowIndex < 9; rowIndex++)
            {
                for (var colIndex = 0; colIndex < 9; colIndex++)
                {
                    row[colIndex] = Table[rowIndex, colIndex];
                }
                if (!ArraySayiKontrol(row))
                {
                    return false;
                }
            }
            // sütün
            for (var colIndex = 0; colIndex < 9; colIndex++)
            {
                // Cell[] col = new Cell[9];
                for (var rowIndex = 0; rowIndex < 9; rowIndex++)
                {
                    row[rowIndex] = Table[rowIndex, colIndex];
                }
                if (!ArraySayiKontrol(row))
                {
                    return false;
                }
            }
            // kare
            for (var index = 0; index < 9; index = index + 3)
            {

                for (var y = 0; y < 9; y = y + 3)
                {
                    // Cell[] sqr = new Cell[9];
                    var rowIndex = 0;
                    for (var i = index; i < index + 3; i++)
                    {
                        for (var k = y; k < y + 3; k++)
                        {
                            row[rowIndex++] = Table[i, k];
                        }
                    }
                    if (!ArraySayiKontrol(row))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool ArraySayiKontrol(Cell[] cells)
        {
            bool[] numbers = new bool[9];
            //for (var index = 0; index < numbers.Length; index++)
            //{
            //    numbers[index] = false;
            //}
            foreach (Cell cell in cells)
            {
                if (cell.Value == 0)
                {
                    continue;
                }
                bool value = numbers[cell.Value - 1];
                if (value)
                {
                    return false;
                }
                numbers[cell.Value - 1] = true;
            }
            return true;
        }
    }
}
