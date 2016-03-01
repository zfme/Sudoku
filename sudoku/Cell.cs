using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class Cell
    {
        public byte Value { get; set; }
        public List<byte> PossibleValues { get; set; }
    }
}
