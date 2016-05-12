using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    public class BoardDFSQ
    {
        public Board Board { get; set; }
        public State State { get; set; }
        public int QueueNdx { get; set; }
    }
}
