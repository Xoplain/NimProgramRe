using NimProgramRe.Models;
using System;
using CSC160_ConsoleMenu;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NimProgramRe.Interface;

namespace NimProgramRe
{
    public class GameEngine
    {
        public GameEngine()
        {
            currentBoard = new Board();
        }

        int firstTurnDeterminer = 0;
        public Board currentBoard;

        public IPlayer player1;
        public IPlayer player2;

        public void Run()
        {
            bool playingGame = true;

            while (playingGame)
            {
                Console.WriteLine("How would you like to play Nim?");
                int gameSelection = CIO.PromptForMenuSelection(new List<String> { "Human VS Human", "Human VS Computer", "Computer VS Computer" }, true);
                int aiGamesToPlay = 0;

                switch (gameSelection)
                {
                    case 1:
                        {
                            player1 = new HumanPlayer("Player 1");
                            player2 = new HumanPlayer("Player 2");
                            break;
                        }
                    case 2:
                        {
                            player1 = new HumanPlayer("Player 1");
                            player2 = new SmartAIPlayer();
                            break;
                        }
                    case 3:
                        {
                            player1 = new SmartAIPlayer();
                            player2 = new RandomAIPlayer();
                            aiGamesToPlay = CSC160_ConsoleMenu.CIO.PromptForInt("How many games would you like the AI to play?", 1, int.MaxValue);

                            for (int i = 0; i < aiGamesToPlay; i++)
                            {
                                PlayAGame();
                            }

                            Console.WriteLine("Done!");
                            aiGamesToPlay = 0;

                            break;
                        }
                    case 0:
                        {
                            playingGame = false;
                            break;
                        }
                }

                if (playingGame)
                {
                    PlayAGame();
                }
                else
                {
                    Console.WriteLine("Goodbye!");
                }

            }
        }

        public void PlayAGame()
        {
            bool FirstPlayerTurn = false;
            if (firstTurnDeterminer % 2 == 0)
            {
                FirstPlayerTurn = true;
            }

            while (!IsGameEnded(currentBoard))
            {
                if (FirstPlayerTurn)
                    player1.ChooseMove(currentBoard);
                else
                    player2.ChooseMove(currentBoard);

                FirstPlayerTurn = !FirstPlayerTurn;
            }

            if(FirstPlayerTurn)
            {
                player1.Win();
                player2.Lose();
            }
            else
            {
                player2.Win();
                player1.Lose();
            }

            firstTurnDeterminer++;
        }

        public void ResetBoard()
        {
            currentBoard.SetRowValue(0, 3);
            currentBoard.SetRowValue(1, 5);
            currentBoard.SetRowValue(2, 7);
        }
  
        public bool IsGameEnded(Board givenBoard)
        {
            bool result = false;
            /*
             * 
             * GET BOARDSTATE. SEE IF BOARD STATE IS 0 0 0
             * 
             *
             */ 
            if(givenBoard.GetState().GetHashCode() == 0)
            {
                result = true;
            }


            return result;
        }
    }
}
