using Sudoku.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    /// <summary>
    /// DFS queue çözümü için bir tahtayı temsil eden sınıf. Diğer tahta sınıfına ek olarak kuyruk index değeri bilgisi de mevcuttur.
    /// </summary>
    public class BoardForDFSQueue
    {
        public string Id { get; set; }
        /// <summary>
        /// Sudoku tahtası nesnesi
        /// </summary>
        public Board Board { get; set; }
        /// <summary>
        /// İşlem yapılma durumu
        /// </summary>
        public BoardProcessState State { get; set; }
        /// <summary>
        /// Tahtanın bulunduğu kuyruk index numarası
        /// </summary>
        public int QueueIndex { get; set; }

        public BoardForDFSQueue Copy() {
            return new BoardForDFSQueue
            {
                Id = Guid.NewGuid().ToString(),
                Board = this.Board.Copy(),
                State = this.State,
                QueueIndex = this.QueueIndex
            };
        }

        public bool KilitlendiMi()
        {
            return Board.KilitlendiMi();
        }
    }
}
