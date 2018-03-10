using Sudoku.Model;

namespace Sudoku
{
    public interface ISolver
    {

        Board Solve(Board board);

    }
}