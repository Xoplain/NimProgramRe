using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NimProgramRe.Models
{
    class LearningEngine
    {
        private Dictionary<BoardState, StateValue> StateStats;
        private string filePath = @"C:\Users\Public\Documents\LearningAI";

        public LearningEngine()
        {

        }

        private void LoadStats()
        {

        }

        private void SaveStats()
        {
            System.IO.Directory.CreateDirectory(filePath);
            using (Stream stream = File.OpenWrite(filePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, StateStats);
            }
        }

        public BoardState GetBestMove(List<BoardState> possibleMoves)
        {
            throw new NotImplementedException();
        }

        public void UpdateStats(List<BoardState> winningList, List<BoardState> losingList)
        {

        }
    }
}
