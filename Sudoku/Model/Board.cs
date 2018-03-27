using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku.Model
{
    /// <summary>
    /// Sudoku tahtasını temsil eden sınıftır.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Sudoku tahasındaki 81 tane hücreyi temsil eden iki boyutlu dizidir.
        /// </summary>
        public Cell[,] Table = new Cell[9, 9];
        /// <summary>
        /// Sudoku tahtasının çözülme işlem durumudur. Tahta için eğer çözülme işlemi başlamış ise durum burada değiştirilir.
        /// </summary>
        public BoardProcessState State { get; set; }

        /// <summary>
        /// Tahta kopyalama işini yapar. evcut durumdaki tüm hücrelerini de kopyalayıp yeni bir tahta döndürür.
        /// </summary>
        /// <returns>Kopyalanmış tahta</returns>
        public Board Copy()
        {
            var board = new Board { State = State };
            var newTable = new Cell[9, 9];
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

        public bool KilitlendiMi()
        {
            for (var index = 0; index < 9; index++)
            {
                for (var indexy = 0; indexy < 9; indexy++)
                {
                    var cell = Table[index, indexy];
                    if (cell.Value == 0 && cell.PossibleValues.Count == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Tahtanın hücrelerinin alabileceği muhtemel değerleri doldurur.
        /// </summary>
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
                        if (cell.Value == 0)
                        {
                            var cellValues = new List<byte>();
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

        /// <summary>
        /// Verilen dizindeki sudoku txt formatındaki dosyasını okur ve görüntülemek için TextBox listesi döndürür.
        /// </summary>
        /// <param name="fileName">Sudoku dosyası</param>
        /// <returns></returns>
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

                        TextBox tb = new TextBox
                        {
                            Size = new Size(15, 15)
                        };

                        if (number == 0)
                        {
                            tb.Text = " ";
                            Table[row, i] = new Cell { Value = 0, PossibleValues = new List<byte>() };
                        }
                        else
                        {
                            tb.Text = number.ToString();
                            Table[row, i] = new Cell { Value = number, PossibleValues = new List<byte>() };
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

        /// <summary>
        /// Sudoku tahtasının hücre değerlerinin kurallara ugun olup olmadığını kontrol eder, eğer uygun ise true döndürür.
        /// </summary>
        /// <returns>true, eğer tahta kurallara uygun ise, aksi halde false döndürür.</returns>
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

        public bool IsValidDfsq()
        {
            //foreach (Cell cell in this.Table)
            //{
            //    if (cell.PossibleValues.Count == 0)
            //    {
            //        return false;
            //    }
            //}
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

        public bool IsSolved()
        {
            for (var index = 0; index < 9; index++)
            {
                for (var indexy = 0; indexy < 9; indexy++)
                {
                    var cellValue = Table[index, indexy].Value;
                    if (cellValue == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// DFS çözümü içide kullanılan bir fonksiyondur. Değeri bilinmeyen ilk hücre için muhtemel değerlerini içeren tahta kopyalarının listesini döndürür.
        /// </summary>
        public List<Board> GetBoardsWithFirstPossibleValues()
        {
            for (var index = 0; index < 9; index++)
            {
                for (var indexy = 0; indexy < 9; indexy++)
                {
                    var cell = Table[index, indexy];
                    var cellValue = cell.Value;
                    if (cellValue == 0)
                    {
                        List<Board> possibleBoards = new List<Board>();
                        for (int i = cell.PossibleValues.Count - 1; i >= 0; i--)
                        {
                            Board copyBoard = this.Copy();
                            copyBoard.Table[index, indexy].Value = cell.PossibleValues[i];
                            possibleBoards.Add(copyBoard);
                        }
                        return possibleBoards;
                    }
                }
            }
            return new List<Board>(0);
        }


        public List<Board> FirstEmptyCellPossibleBoards()
        {
            var boards = new List<Board>();
            for (var index = 0; index < 9; index++)
            {
                for (var indexy = 0; indexy < 9; indexy++)
                {
                    if (Table[index, indexy].Value == 0)
                    {
                        foreach (var possibleValue in Table[index, indexy].PossibleValues)
                        {
                            Board board = this.Copy();
                            board.Table[index, indexy].Value = possibleValue;
                            boards.Add(board);
                        }
                        return boards;
                    }
                }
            }
            return boards;
        }

        protected bool Equals(Board other)
        {
            for (var index = 0; index < 9; index++)
            {
                for (var indexy = 0; indexy < 9; indexy++)
                {
                    if (other.Table[index, indexy].Value != Table[index, indexy].Value)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Board)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Table != null ? Table.GetHashCode() : 0) * 397) ^ (int)State;
            }
        }
    }
}
