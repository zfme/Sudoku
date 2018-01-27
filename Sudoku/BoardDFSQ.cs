using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class BoardDFSQ
    {
        public Board Board { get; set; }
        public State State { get; set; }
        public int QueueNdx { get; set; }
        public BoardDFSQ Copy() {
            BoardDFSQ b = new BoardDFSQ();
            b.Board = this.Board.Copy();
            b.State = this.State;
            b.QueueNdx = this.QueueNdx;
            return b;
        }
    }
}
