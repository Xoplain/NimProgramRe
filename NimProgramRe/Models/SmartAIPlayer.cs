using NimProgramRe.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimProgramRe.Models
{
    public class SmartAIPlayer : Player
    {
        public Board currentBoard;
        public Dictionary<int[], float> MapOfMoves;
        public Dictionary<int[], int> OccurencesOfState;
        public Random generator;

        public SmartAIPlayer(Board currentBoard, Dictionary<int[], float> MapOfMoves, Dictionary<int[], int> OccurencesOfState)
        {
            this.currentBoard = currentBoard;
            this.MapOfMoves = MapOfMoves;
            this.OccurencesOfState = OccurencesOfState;
            generator = new Random();
        }

        public override void ChooseMove()
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
                while (currentBoard.GetRowNumber(RowToDelete) <= 0)
                {
                    RowToDelete = generator.Next(3);
                }

                currentBoard.MinusOnRow(RowToDelete, generator.Next(currentBoard.GetRowNumber(RowToDelete)) + 1);
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
                    if (currentBoard.GetRowNumber(i) > boardToUse[i])
                    {
                        currentBoard.MinusOnRow(i, currentBoard.GetRowNumber(i) - boardToUse[i]);
                        break;
                    }
                }
            }
        }
    }
}
