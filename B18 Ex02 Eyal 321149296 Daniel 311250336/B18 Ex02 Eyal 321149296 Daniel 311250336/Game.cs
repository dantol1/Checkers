using System;
using System.Collections.Generic;
using System.Text;
using Ex02.ConsoleUtils;

namespace B18_Ex02_Eyal_321149296_Daniel_311250336
{
    //UI Class
    public class Game
    {
        private enum eGameType
        {
            PlayerVsPlayer = 1,
            PlayerVsComputer = 2,
        }
        private const bool v_ComputerPlayer = true;
        private eGameType TypeOfTheGame;
        private GameBoard m_BoardData;
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private Player m_CurrentPlayer;
        private Game(string i_NamePlayerOne, string i_NamePlayerTwo, int i_BoardSize, eGameType i_GameType)
        {
            TypeOfTheGame = i_GameType;

            m_BoardData = new GameBoard(i_BoardSize);
            m_PlayerOne = new Player(i_NamePlayerOne,!v_ComputerPlayer,'X', m_BoardData);

            if (i_GameType == eGameType.PlayerVsPlayer)
            {
                m_PlayerTwo = new Player(i_NamePlayerTwo, !v_ComputerPlayer,'O', m_BoardData);
            }
            else
            {
                m_PlayerTwo = new Player(i_NamePlayerTwo, v_ComputerPlayer,'O', m_BoardData);
            }

        }
        public static Game Initialize()
        {
            string namePlayerOne;
            string namePlayerTwo;
            int boardSize;
            eGameType gameTypeOption;


            namePlayerOne = getName();

            do
            {
                Console.WriteLine("Please enter a valid size of the board (6/8/10): ");
                boardSize = int.Parse(Console.ReadLine());
            } while (GameBoard.CheckBoardSizeValidity(boardSize) == false);

            do
            {
                Console.WriteLine("For Player vs. Player Press (1) {0}For Player vs. Computer Press (2)", Environment.NewLine);
                gameTypeOption = (eGameType)int.Parse(Console.ReadLine());
            } while (checkGameTypeOption(gameTypeOption) == false);

            if (gameTypeOption == eGameType.PlayerVsPlayer)
            {
                namePlayerTwo = getName();
            }
            else
            {
                namePlayerTwo = "Computer";
            }

            Game newGame = new Game(namePlayerOne, namePlayerTwo, boardSize, gameTypeOption);
            return newGame;
        }
        private static string getName()
        {
            string name;
            do
            {
                Console.WriteLine("Please enter a valid name: ");
                name = Console.ReadLine();
            } while (Player.CheckNameValidity(name) == false);

            return name;
        }
        private static bool checkGameTypeOption(eGameType i_GameTypeOption)
        {
            bool isValid = true;

            if (Enum.IsDefined(typeof(eGameType), i_GameTypeOption) == false)
            {
                isValid = false;
                Console.WriteLine("Wrong option, please select a valid option");
            }

            return isValid;
        }
        public void play()
        {
            string nextMove;
            Screen.Clear();
            printGameBoard();
            bool gameOver = false;

            while (gameOver == false)
            {
                m_CurrentPlayer = m_PlayerOne; //TODO - add a swap method to exchange curr player and next player.
                Console.Write("{0}'s turn: ", m_CurrentPlayer.Name);

                do
                {
                    nextMove = Console.ReadLine();
                    /*
                     use a method to analyze the move like so:
                     nextMove[0] = pieceCol
                     nextMove[1] = pieceRow
                     check if such piece exists (nextMove[2] supposed to be '>')
                     nextMove[3] = pieceNextPlaceCol
                     nextMove[4] = pieceNextPlaceRow
                     check if the place is available - have the game board check if the place is empty and if not
                                                        check if it is an opponent piece that can be eaten.
                     to calculate the column from the letter it is assigned we use nextMove[0 or 3] - 'A'
                     to calculate the row from the letter it is assigned we use nextMove[1 or 4] - 'a'
                     */
                    int i = 5;
                }
            }


        }
        private void printGameBoard()
        {

            for (int i = 0; i < m_BoardData.GameBoardSize; i++)
            {
                Console.Write("   ");
                Console.Write((char)('A' + i));
            }
            Console.WriteLine("   ");
            printSeperator();

            for (int i = 0; i < m_BoardData.GameBoardSize; i++)
            {
                Console.Write((char)('a' + i));
                Console.Write('|');
                for (int j = 0; j < m_BoardData.GameBoardSize; j++)
                {
                    Console.Write(" {0} |", m_BoardData.GetCellSymbol(i, j));
                }
                Console.WriteLine(' ');
                printSeperator();
            }
        }

        private void printSeperator()
        {
            Console.WriteLine(" ================================= ");
        }
    }
}
