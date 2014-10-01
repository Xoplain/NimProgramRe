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
        List<int[]> SavedGameStates = new List<int[]>();

        public Board()
        {
            rows[0] = 3;
            rows[1] = 5;
            rows[2] = 7;
        }

        public void ResetBoard()
        {
            rows[0] = 3;
            rows[1] = 5;
            rows[2] = 7;
            SavedGameStates.Clear();
        }

        public int[] GetAllRows()
        {
            return rows;
        }

        public List<int[]> GetAllStates()
        {
            return SavedGameStates;
        }

        public void MinusOnRow(int rowNum, int minus)
        {
            rows[rowNum] -= minus;
            AddToSaves();
        }

        public int GetRowNumber(int rowNum)
        {
            return rows[rowNum];
        }

        public void AddToSaves()
        {
            SavedGameStates.Add(new int[rows.Length]);
            Array.Copy(rows, SavedGameStates[SavedGameStates.Count - 1], 3);
        }
    }
}
