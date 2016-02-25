using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class Cell
    {
        public byte value { get; set; }
        public List<byte> possibleValues { get; set; }
    }
}
