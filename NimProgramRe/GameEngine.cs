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

        int turnDeterminer = 0;
        public Board currentBoard;

        public IPlayer player1;
        public IPlayer player2;

        public void ResetBoard()
        {
            currentBoard.SetRowValue(0, 3);
            currentBoard.SetRowValue(1, 5);
            currentBoard.SetRowValue(2, 7);
        }

        public void Run()
        {
            bool playingGame = true;

            while (playingGame)
            {
                Console.WriteLine("How would you like to play Nim?");
                int gameSelection = CSC160_ConsoleMenu.CIO.PromptForMenuSelection(new List<String> { "Human VS Human", "Human VS Computer", "Computer VS Computer" }, true);
                int aiGamesToPlay = 0;

                switch (gameSelection)
                {
                    case 1:
                        {
                            player1 = new HumanPlayer();
                            player2 = new HumanPlayer();
                            break;
                        }
                    case 2:
                        {
                            player1 = new HumanPlayer();
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
            if (turnDeterminer % 2 == 0)
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

            EndGame();
            turnDeterminer++;
        }

        public void EndGame()
        {
            int stateCountIndex = 1;

            foreach (int[] indexed in currentBoard.GetAllStates())
            {
                float scoreThisMove = ((float)Math.Pow(-1, currentBoard.GetAllStates().Count - stateCountIndex) * stateCountIndex) / (currentBoard.GetAllStates().Count);

                bool alreadyExisting = false;

                foreach (int[] keyIndex in MapOfMoves.Keys)
                {
                    if (EqualArrays(keyIndex, indexed))
                    {
                        alreadyExisting = true;
                        float newValue = ((MapOfMoves[keyIndex] * OccurencesOfState[keyIndex]) + scoreThisMove) / (OccurencesOfState[keyIndex] + 1);

                        OccurencesOfState[keyIndex]++;
                        MapOfMoves[keyIndex] = newValue;
                        break;
                    }
                }

                if (!alreadyExisting) 
                {
                    MapOfMoves.Add(indexed, scoreThisMove);
                    OccurencesOfState.Add(indexed, 1);
                }
                stateCountIndex++;
            }
            ResetBoard();
        }

        public bool EqualArrays(int[] first, int[] second)
        {
            bool result = true;
            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i])
                    result = false;
            }
            return result;
        }

        public bool IsGameEnded(Board givenBoard)
        {
            bool result = true;
            foreach (int indexed in givenBoard.GetAllRows())
            {
                if (indexed > 0)
                    result = false;
            }
            return result;
        }

        public void PrintBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < currentBoard.GetRowValue(i); j++)
                {
                    Console.Write("X");
                }
                Console.WriteLine();
            }
            Console.WriteLine("------------");
        }
    }
}
