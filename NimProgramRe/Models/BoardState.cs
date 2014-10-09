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
        
        //Test comment v2
        public BoardState(int[] rows)
        {
            this.rows = rows;
        }

        public IEnumerator<int> GetStateEnumerator()
        {
            int[] tempArray = (int[])rows.Clone();
            return ((IEnumerable<int>)tempArray).GetEnumerator();
        }

        public override int GetHashCode()
        {
            int[] arrayCopy = (int[])rows.Clone();
            Array.Sort(arrayCopy);
            return arrayCopy[0] * 100 + arrayCopy[1] * 10 + arrayCopy[2];
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
