using NimProgramRe.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimProgramRe.Models
{
    public class HumanPlayer : Player
    {
        //MENU HERE sure.
        public Board currentBoard;
        
        public HumanPlayer (Board currentBoard)
        {
            this.currentBoard = currentBoard;
        }
        public override void ChooseMove()
        {
            Console.WriteLine("Choose what row to remove from");
            int rowToRemove = CSC160_ConsoleMenu.CIO.PromptForMenuSelection(new List<String>{"First row", "Second row", "Third row"}, false) - 1;
            if (currentBoard.GetRowNumber(rowToRemove) != 0)
            {
                int removal = CSC160_ConsoleMenu.CIO.PromptForInt("How many would you like to take?", 1, currentBoard.GetRowNumber(rowToRemove));
                currentBoard.MinusOnRow(rowToRemove, removal);
            }
            else
            {
                Console.WriteLine("There's no more to remove from that row.");
                ChooseMove();
                bool win = true;
                string winright = (win) ? "you win" : "you lost";
            }
        }

        /*
        public class HumanPlayer : Player
        {
            public override void DoMove(Board board)
            {
                Console.WriteLine("Choose what row to remove from");
                int rowToRemove = CSC160_ConsoleMenu.CIO.PromptForMenuSelection(new List<String> { "First row", "Second row", "Third row" }, false) - 1;
                bool flag = true;
                while (flag)
                    if (board.GetRowNumber(rowToRemove) != 0)
                    {
                        int removal = CSC160_ConsoleMenu.CIO.PromptForInt("How many would you like to take?", 1, board.getRowVal(rowToRemove));
                        board.setRowValue(rowToRemove, removal);
                        flag = false;
                    }
                    else
                    {
                        Console.WriteLine("There's no more to remove from that row.");
                    }
            }

            public void win()
        {
            System.out.println("You win");
        }
            public void lost()
        {
            System.out.println("You lost");
        }
        }
         */
    }
}
