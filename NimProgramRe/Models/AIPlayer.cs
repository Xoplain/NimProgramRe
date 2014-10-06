using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NimProgramRe.Interface;

namespace NimProgramRe.Models
{
    class AIPlayer : IPlayer
    {
        private List<BoardState> RecievedStates;
        private List<BoardState> SentStates;

        private LearningEngine learn;

        public AIPlayer()
        {
            RecievedStates = new List<BoardState>();
            SentStates = new List<BoardState>();
            learn = new LearningEngine();
        }

        public void ChooseMove(Board currentBoard)
        {
            throw new NotImplementedException();
        }

        public void Win()
        {
            throw new NotImplementedException();
        }

        public void Lose()
        {
            throw new NotImplementedException();
        }

        private List<BoardState> GetValidMoves(Board board)
        {
            List<BoardState> validMoves = new List<BoardState>();
        } 
    }
}
