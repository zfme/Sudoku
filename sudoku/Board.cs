using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class Board
    {
        public Cell[,] Table = new Cell[9, 9];
        public State State { get; set; }

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
                if (cell.value == 0)
                {
                    continue;
                }
                bool value = numbers[cell.value - 1];
                if (value)
                {
                    return false;
                }
                numbers[cell.value- 1] = true;
            }
            return true;
        }
    }
}
