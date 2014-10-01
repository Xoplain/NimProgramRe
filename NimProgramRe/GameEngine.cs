using NimProgramRe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NimProgramRe
{
    public class GameEngine
    {
        public GameEngine()
        {
            currentBoard = new Board();
            generator = new Random();
            MapOfMoves = new Dictionary<int[], float>();
            OccurencesOfState = new Dictionary<int[],int>();
        }

        public Board currentBoard;
        public Random generator;
        public Dictionary<int[], float> MapOfMoves;
        public Dictionary<int[], int> OccurencesOfState;

        int asdfasdf = 0;

        public void ChooseMoveForAI()
        {
            Dictionary<int[], float> LegalMoves = new Dictionary<int[],float>();
            foreach(int[] stateInQuestion in MapOfMoves.Keys)
            {
                if(IsLegal(currentBoard.GetAllRows(), stateInQuestion) && MapOfMoves[stateInQuestion] <= 0)
                {
                    LegalMoves.Add(stateInQuestion, MapOfMoves[stateInQuestion]);
                }
            }

            if(LegalMoves.Count == 0)
            {
                RandomlyChooseMove();
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

        public bool IsLegal(int[] currentState, int[] stateInQuestion)
        {
            int count = 0;

            for(int i = 0; i < currentState.Length; i++)
            {
                if (stateInQuestion[i] < currentState[i])
                    count++;
                if (stateInQuestion[i] > currentState[i])
                    return false;
                
            }

            return (count == 1);
        }

        bool smartTurn = true;

        int smartPlayerWins = 0;
        int randomPlayerWins = 0;

        int turnDeterminer = 0;

        public void Run()
        {

            for (int i = 0; i < 10000; i++)
            {
                if (turnDeterminer % 2 == 0)
                    smartTurn = true;
                else
                    smartTurn = false;

                while (!IsGameEnded())
                {
                    //PrintBoard();
                    if (smartTurn)
                        ChooseMoveForAI();
                    else
                        RandomlyChooseMove();

                    smartTurn = !smartTurn;
                }
                EndGame();
                turnDeterminer++;
            }
        }

        

        public void EndGame()
        {
            int stateCountIndex = 1;

            if (smartTurn)
                smartPlayerWins++;
            else
                randomPlayerWins++;

            foreach(int[] indexed in currentBoard.GetAllStates())
            {
                float scoreThisMove = ((float)Math.Pow(-1, currentBoard.GetAllStates().Count - stateCountIndex) * stateCountIndex) / (currentBoard.GetAllStates().Count);
                
                bool alreadyExisting = false;

                foreach(int[] keyIndex in MapOfMoves.Keys)
                {
                    if(EqualArrays(keyIndex, indexed))
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
            for(int i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i])
                    result = false;
            }
            return result;
        }

        public bool IsGameEnded()
        {
            bool result = true;
            foreach(int indexed in currentBoard.GetAllRows())
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
