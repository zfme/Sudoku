using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Model
{
    /// <summary>
    /// Sudoku tahasındaki bir hücreyi temsil eden sınıftır.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Hücrenin değeri, eğer hesaplanmamış ise '0' olur
        /// </summary>
        public byte Value { get; set; }
        /// <summary>
        /// Hücrenin alabileceği muhtemel değerler listesidir. Hesaplandığı an bu listeye eklenir.
        /// </summary>
        public List<byte> PossibleValues { get; set; }

        /// <summary>
        /// Hücrenin bir kopyasını alan fonksiyondur. Değer ve muhtemel değerler de olduğu gibi kopyalanır.
        /// Sudoku tahtası kopyalanırken kullanılır.
        /// </summary>
        /// <returns>Kopylanan yeni hücre nesnesi</returns>
        public Cell Copy()
        {
            Cell copy = new Cell {Value = Value, PossibleValues = new List<byte>()};
            copy.PossibleValues.AddRange(this.PossibleValues);
            return copy;
        }

        public override string ToString()
        {
            return $"Value: {Value}";
        }
    }
}
