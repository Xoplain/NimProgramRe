using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimProgramRe.Models
{
    [Serializable()]
    public class BoardState
    {
        int[] rows;
        
        public BoardState(int[] rows)
        {
            this.rows = rows;
        }

        public int[] GetIntArray()
        {
            int[] tempArray = new int[rows.Length];
            Array.Copy(rows, tempArray, rows.Length);
            return tempArray;
        }

        public override int GetHashCode()
        {
            int[] x = (int[])rows.Clone();
            Array.Sort(x);
            return x[0] * 100 + x[1] * 10 + x[2];
        }

        public override bool Equals(object boardState)
        {
            bool isEqual = false;

            if(boardState.GetType().Equals(typeof(BoardState)))
            {
                isEqual = (this.GetHashCode() == boardState.GetHashCode()) ? true : false;
            }

            return isEqual;
        }
    }
}
