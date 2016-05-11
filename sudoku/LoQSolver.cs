using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class LoQSolver
    {
        public Board Solve(Board board)
        {
            List<Queue<BoardDFSQ>> queueList = new List<Queue<BoardDFSQ>>(65);
            for (int i = 0; i < 65; i++)
            {
                queueList.Add(new Queue<BoardDFSQ>());
            }
             









            return null;
        }
    }
}
