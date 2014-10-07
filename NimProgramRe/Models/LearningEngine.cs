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
        private string FilePath = @"C:\Users\Public\Documents\LearningAI";

        public LearningEngine()
        {
            LoadStats();
        }

        private void LoadStats()
        {
            if (File.Exists(FilePath + @"\stats.lrn"))
            {
                using (Stream stream = File.OpenRead(FilePath + @"\stats.lrn"))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    StateStats = (Dictionary<BoardState, StateValue>)bf.Deserialize(stream);
                }
            }
            else
            {
                StateStats = new Dictionary<BoardState, StateValue>();
            }
        }

        public void SaveStats()
        {
            System.IO.Directory.CreateDirectory(FilePath);
            using (Stream stream = File.OpenWrite(FilePath + @"\stats.lrn"))
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
