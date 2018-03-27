using Sudoku.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    /// <summary>
    /// DFS Sudoku çözücüsü
    /// </summary>
    public class DFSSolver : ISolver
    {
        /// <summary>
        /// Tahtaların bulunduğu yığıt
        /// </summary>
        private Stack<Board> stack = new Stack<Board>();

        /// <summary>
        /// Verilen tahtayı DFS ile çözer ve sonuç olarak tüm hücrelerinin değeri dolu ve geçerli bir tahat nesnesi döndürür
        /// </summary>
        /// <param name="board">Sudoku problemi tahtası</param>
        /// <returns>Çözüm tahtası</returns>
        public Board Solve(Board board)
        {
            stack.Push(board);
            while (stack.Count > 0)
            {
                Board current = stack.Pop();
                current.FillPossibleValues();
                if (current.IsSolved())
                {
                    // found solution
                    return current;
                }
                List<Board> possibleBoards = current.GetBoardsWithFirstPossibleValues();
                possibleBoards.ForEach(b => stack.Push(b));
            }
            return null;
        }

        public void Temizle()
        {
            stack = new Stack<Board>();
        }
    }
}
