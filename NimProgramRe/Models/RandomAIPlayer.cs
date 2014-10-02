using NimProgramRe.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimProgramRe.Models
{
    public class RandomAIPlayer : Player
    {
        public Board currentBoard;
        public Random generator;

        public RandomAIPlayer(Board currentBoard)
        {
            this.currentBoard = currentBoard;
            generator = new Random();
        }

        public override void ChooseMove()
        {
            //DUPLICATED IN SMART
            int RowToDelete = generator.Next(3);
            while (currentBoard.GetRowNumber(RowToDelete) <= 0)
            {
                RowToDelete = generator.Next(3);
            }

            currentBoard.MinusOnRow(RowToDelete, generator.Next(currentBoard.GetRowNumber(RowToDelete)) + 1);
        }
    }
}
