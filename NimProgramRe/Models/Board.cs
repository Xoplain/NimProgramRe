using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimProgramRe.Models
{
    public class Board
    {
        int[] rows = new int[3];
        
        public Board()
        {
            rows[0] = 3;
            rows[1] = 5;
            rows[2] = 7;
        }

        public void SetRowValue(int rowNum, int howMany)
        {
            rows[rowNum] = howMany;
        }

        public int GetRowValue(int rowNum)
        {
            return rows[rowNum];
        }

        public override string ToString()
        {
            StringBuilder DesiredString = new StringBuilder("");
            for(int i = 0; i < rows.Length; i++)
            {
                for( int j = 0; j < rows[i]; j++)
                {
                    DesiredString.Append("X");
                }
                DesiredString.Append(Environment.NewLine);
            }

            return DesiredString.ToString();
        }
    }
}
