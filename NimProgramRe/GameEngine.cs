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
            generator = new Random();
            MapOfMoves = new Dictionary<int[], float>();
            OccurencesOfState = new Dictionary<int[], int>();
        }

        int turnDeterminer = 0;
        public Random generator;
        public Board currentBoard;
        public Dictionary<int[], float> MapOfMoves;
        public Dictionary<int[], int> OccurencesOfState;

        public Player player1;
        public Player player2;

        public static bool IsLegal(int[] currentState, int[] stateInQuestion)
        {
            int count = 0;

            for (int i = 0; i < currentState.Length; i++)
            {
                if (stateInQuestion[i] < currentState[i])
                    count++;
                if (stateInQuestion[i] > currentState[i])
                    return false;
            }
            return (count == 1);
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
                            player1 = new HumanPlayer(currentBoard);
                            player2 = new HumanPlayer(currentBoard);
                            break;
                        }
                    case 2:
                        {
                            player1 = new HumanPlayer(currentBoard);
                            player2 = new SmartAIPlayer(currentBoard, MapOfMoves, OccurencesOfState);
                            break;
                        }
                    case 3:
                        {
                            player1 = new SmartAIPlayer(currentBoard, MapOfMoves, OccurencesOfState);
                            player2 = new RandomAIPlayer(currentBoard);
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

            while (!IsGameEnded())
            {

                if (player1.GetType().Equals(typeof(HumanPlayer)))
                {
                    string whoseTurn = FirstPlayerTurn ? "1" : "2";
                    Console.WriteLine();
                    Console.WriteLine("========");
                    Console.WriteLine("PLAYER " + whoseTurn);
                    Console.WriteLine("========");
                    Console.WriteLine();

                    PrintBoard();
                }


                if (FirstPlayerTurn)
                    player1.ChooseMove();
                else
                    player2.ChooseMove();

                FirstPlayerTurn = !FirstPlayerTurn;
            }

            if (player1.GetType().Equals(typeof(HumanPlayer)))
            {
                PrintBoard();
                string winningPlayer = FirstPlayerTurn ? "1" : "2";
                Console.WriteLine();
                Console.WriteLine("Congratulations Player " + winningPlayer + "!");
                Console.WriteLine();
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

            currentBoard.ResetBoard();
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

        public bool IsGameEnded()
        {
            bool result = true;
            foreach (int indexed in currentBoard.GetAllRows())
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
                for (int j = 0; j < currentBoard.GetRowNumber(i); j++)
                {
                    Console.Write("X");
                }
                Console.WriteLine();
            }
            Console.WriteLine("------------");
        }

        public void RandomlyChooseMove()
        {
            int RowToDelete = generator.Next(3);
            while (currentBoard.GetRowNumber(RowToDelete) <= 0)
            {
                RowToDelete = generator.Next(3);
            }

            currentBoard.MinusOnRow(RowToDelete, generator.Next(currentBoard.GetRowNumber(RowToDelete)) + 1);
        }
    }
}
