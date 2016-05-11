using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    public class DFSSolver
    {
        private Stack<Board> stack = new Stack<Board>();
        public Board SolveWithDFS(Board board)
        {
            stack.Push(board);
            while (stack.Count > 0)
            {
                Board current = stack.Pop();
                current.FillPossibleValues();
                if (current.IsSolved())
                {
                    return current;
                }
                current.FillStackWithFirstPossibleValues(stack);
            }
            return null;
        }
    }
}
