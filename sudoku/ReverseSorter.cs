using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    public class ReverseSorter : IComparer<byte>
    {
        public int Compare(byte x, byte y)
        {
            return x > y ? -1 : (x < y ? 1 : 0);
        }
    }
}
