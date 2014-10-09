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

        private LearningEngine Learn;

        public AIPlayer()
        {
            RecievedStates = new List<BoardState>();
            SentStates = new List<BoardState>();
            Learn = new LearningEngine();
        }

        public void ChooseMove(Board currentBoard)
        {
            RecievedStates.Add(currentBoard.GetState());

            BoardState DesiredBoardState = Learn.GetBestMove(GetValidMoves(currentBoard.GetState().GetStateEnumerator()));
            
            IEnumerator<int> itr = DesiredBoardState.GetStateEnumerator();

            for (int i = 0; itr.MoveNext(); i++)
            {
                currentBoard.SetRowValue(i, itr.Current);
            }

            SentStates.Add(DesiredBoardState);
        }

        public void Win()
        {
            Learn.UpdateStats(RecievedStates, SentStates);
        }

        public void Lose()
        {
            Learn.UpdateStats(SentStates, RecievedStates);
        }

        private IEnumerator<BoardState> GetValidMoves(IEnumerator<int> boardIterator)
        {
            List<BoardState> validMoves = new List<BoardState>();

            int numberOfRows = 0;
            while (boardIterator.MoveNext()) { numberOfRows++; }
            boardIterator.Reset();

            int[] stateArray = new int[numberOfRows];

            for (int i = 0; boardIterator.MoveNext(); i++)
            {
                stateArray[i] = boardIterator.Current;
            }

            for (int i = 0; i < stateArray.Length; i++)
            {
                for (int j = stateArray[i] - 1; j >= 0; j--)
                {
                    int[] newState = new int[stateArray.Length];
                    for (int k = 0; k < stateArray.Length; k++)
                    {
                        if (k == i)
                        {
                            newState[k] = j;
                        }
                        else
                        {
                            newState[k] = stateArray[i];
                        }
                    }
                    validMoves.Add(new BoardState(newState));
                }
            }

            return validMoves.GetEnumerator();
        }
    }
}
