using Sudoku.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Model
{
    public class BoardCozum
    {
        public List<Board> CozumBoards { get; set; }
        public double Sure { get; set; }
        public long Total { get; internal set; }
        public string Detail { get; internal set; }



        public bool CozumlerAyniMi(BoardCozum cozum)
        {
            //if (CozumBoards.Count != cozum.CozumBoards.Count)
            //{
            //    return false;
            //}
            //foreach (var item in CozumBoards)
            //{
            //    if (!cozum.CozumBoards.Contains(item))
            //    {
            //        return false;
            //    }
            //}
            //return true;
            for (int i = 0; i < CozumBoards.Count; i++)
            {
                if (!CozumBoards.ElementAt(i).Equals(cozum.CozumBoards.ElementAt(i)))
                {
                    return false;
                }
            }
            return true;
        }


    }

}
