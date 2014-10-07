using NimProgramRe.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimProgramRe.Models
{
    public class RandomAIPlayer : IPlayer
    {
        public Random generator;

        public RandomAIPlayer()
        {
            generator = new Random();
        }

        public void ChooseMove(Board currentBoard)
        {
            //DUPLICATED IN SMART
            int RowToDelete = generator.Next(3);
            while (currentBoard.GetRowValue(RowToDelete) <= 0)
            {
                RowToDelete = generator.Next(3);
            }

            //currentBoard.MinusOnRow(RowToDelete, generator.Next(currentBoard.GetRowValue(RowToDelete)) + 1);
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
