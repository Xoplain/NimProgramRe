using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimProgramRe.Models
{
    public class BoardState
    {
        int[] rows;
        public BoardState(int[] rows)
        {
            this.rows = rows;
        }
        public override int GetHashCode()
        {
            Array.Sort(rows);
            return rows[0] * 100 + rows[1] * 10 + rows[2];
        }

        public override bool Equals(BoardState boardState)
        {
            bool isEqual = (this.GetHashCode() == boardState.GetHashCode()) ? true : false;
            
            return isEqual;
        }
    }
}
