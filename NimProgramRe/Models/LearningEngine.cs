using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimProgramRe.Models
{
    class LearningEngine
    {
        private Dictionary<BoardState, StateValue> StateStats;

        public LearningEngine()
        {

        }

        private void LoadStats()
        {

        }

        private void SaveStats()
        {

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
