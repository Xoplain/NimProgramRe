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
            CheckForNewStates(possibleMoves);

            List<BoardState> bestMoves = new List<BoardState>();
            double bestMoveValue = 1.0d;

            foreach (BoardState b in possibleMoves)
            {
                double moveValue = StateStats[b].GetAverage();
                if (moveValue < bestMoveValue)
                {
                    if (bestMoves.Any())
                    {
                        bestMoves.Clear();
                        bestMoves.Add(b);
                    }
                    else
                    {
                        bestMoves.Add(b);
                    }
                    bestMoveValue = moveValue;
                }
                else if(bestMoveValue == moveValue)
                {
                    bestMoves.Add(b);
                }
            }
            return bestMoves[new Random().Next(bestMoves.Count)];
        }

        public void UpdateStats(List<BoardState> winningList, List<BoardState> losingList)
        {
            for(int i = 1; i <= winningList.Count; i++)
            {
                StateStats[winningList[i - 1]].UpdateValue(i / winningList.Count);
            }

            for (int i = 1; i <= losingList.Count; i++)
            {
                StateStats[losingList[i - 1]].UpdateValue(-i / losingList.Count);
            }
            SaveStats();
        }

        private void CheckForNewStates(List<BoardState> states)
        {
            foreach (BoardState b in states)
            {
                if (!StateStats.ContainsKey(b))
                {
                    StateStats.Add(b, new StateValue());
                }
            }
        }
    }
}
