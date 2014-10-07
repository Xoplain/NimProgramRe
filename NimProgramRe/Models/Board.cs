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

        public BoardState GetState()
        {
            return new BoardState((int[])rows.Clone());
        }

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
            //StringBuilder sb = new StringBuilder();
            //sb.Append("1) ");
            //for (int i = 0; i < rows[0]; i++) { sb.Append("X"); }
            //sb.Append("\n");
            //sb.Append("2) ");
            //for (int i = 0; i < rows[1]; i++) { sb.Append("X"); }
            //sb.Append("\n");
            //sb.Append("3) ");
            //for (int i = 0; i < rows[2]; i++) { sb.Append("X"); }
            //sb.Append("\n");

            //return sb.ToString();

            StringBuilder DesiredString = new StringBuilder("");
            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < rows[i]; j++)
                {
                    DesiredString.Append("X");
                }
                DesiredString.Append(Environment.NewLine);
            }

            return DesiredString.ToString();
        }
    }
}
