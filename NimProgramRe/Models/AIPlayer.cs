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
            RecievedStates.Add(currentBoard.GetState());

            BoardState DesiredBoardState = learn.GetBestMove(GetValidMoves(currentBoard));
            
            for(int i = 0; i < DesiredBoardState.GetIntArray().Length; i++)
            {
                currentBoard.SetRowValue(i, DesiredBoardState.GetIntArray()[i]);
            }

            SentStates.Add(DesiredBoardState);
        }

        public void Win()
        {
            learn.UpdateStats(RecievedStates, SentStates);
        }

        public void Lose()
        {
            learn.UpdateStats(SentStates, RecievedStates);
        }

        private List<BoardState> GetValidMoves(Board board)
        {
            List<BoardState> validMoves = new List<BoardState>();

            int row1 = board.GetRowValue(0);
            for (int i = row1 - 1; i >= 0; i--) { validMoves.Add(new BoardState(new int[] { i, board.GetRowValue(1), board.GetRowValue(2) })); }
            int row2 = board.GetRowValue(1);
            for (int i = row2 - 1; i >= 0; i--) { validMoves.Add(new BoardState(new int[] { board.GetRowValue(0), i, board.GetRowValue(2) })); }
            int row3 = board.GetRowValue(2);
            for (int i = row3 - 1; i >= 0; i--) { validMoves.Add(new BoardState(new int[] { board.GetRowValue(0), board.GetRowValue(1), i })); }
            return validMoves;
        }
    }
}
