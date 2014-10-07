using NimProgramRe.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimProgramRe.Models
{
    public class SmartAIPlayer : IPlayer
    {
        public Dictionary<int[], float> MapOfMoves;
        public Dictionary<int[], int> OccurencesOfState;
        public Random generator;

        public SmartAIPlayer()
        {
            generator = new Random();
        }

        public override void ChooseMove(Board currentBoard)
        {
            Dictionary<int[], float> LegalMoves = new Dictionary<int[], float>();
            foreach (int[] stateInQuestion in MapOfMoves.Keys)
            {
                if (GameEngine.IsLegal(currentBoard.GetAllRows(), stateInQuestion) && MapOfMoves[stateInQuestion] <= 0)
                {
                    LegalMoves.Add(stateInQuestion, MapOfMoves[stateInQuestion]);
                }
            }

            if (LegalMoves.Count == 0)
            {
                //DUPLICATED IN RANDOM
                int RowToDelete = generator.Next(3);
                while (currentBoard.GetRowValue(RowToDelete) <= 0)
                {
                    RowToDelete = generator.Next(3);
                }

                currentBoard.MinusOnRow(RowToDelete, generator.Next(currentBoard.GetRowValue(RowToDelete)) + 1);
            }
            else
            {
                int[] boardToUse = null;

                foreach (int[] BigIndex in LegalMoves.Keys)
                {
                    if (boardToUse == null)
                    {
                        boardToUse = BigIndex;
                    }

                    if (LegalMoves[BigIndex] < LegalMoves[boardToUse])
                    {
                        boardToUse = BigIndex;
                    }
                }

                for (int i = 0; i < currentBoard.GetAllRows().Length; i++)
                {
                    if (currentBoard.GetRowValue(i) > boardToUse[i])
                    {
                        currentBoard.MinusOnRow(i, currentBoard.GetRowValue(i) - boardToUse[i]);
                        break;
                    }
                }
            }
        }


        public void Win()
        {
            throw new NotImplementedException();
        }

        public void Lose()
        {
            throw new NotImplementedException();
        }
    }
}
