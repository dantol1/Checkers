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
        public enum eGameSymbols
        {
            PlayerOneRegular = 'X',
            PlayerOneKing = 'K',
            PlayerTwoRegular = 'O',
            PlayerTwoKing = 'U',
        }
        private const bool v_ComputerPlayer = true;
        private eGameType TypeOfTheGame;
        private GameBoard m_BoardData;
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private Player m_CurrentPlayer;
        private Player m_NextPlayer;
        private Game(string i_NamePlayerOne, string i_NamePlayerTwo, int i_BoardSize, eGameType i_GameType)
        {
            TypeOfTheGame = i_GameType;

            m_BoardData = new GameBoard(i_BoardSize);
            m_PlayerOne = new Player(i_NamePlayerOne,!v_ComputerPlayer,eGameSymbols.PlayerOneRegular, m_BoardData);
            m_PlayerOne.MoveDirection = Player.eMoveDirection.Up;

            if (i_GameType == eGameType.PlayerVsPlayer)
            {
                m_PlayerTwo = new Player(i_NamePlayerTwo, !v_ComputerPlayer, eGameSymbols.PlayerTwoRegular, m_BoardData);
            }
            else
            {
                m_PlayerTwo = new Player(i_NamePlayerTwo, v_ComputerPlayer, eGameSymbols.PlayerTwoRegular, m_BoardData);
            }

            m_CurrentPlayer = m_PlayerTwo;
            m_NextPlayer = m_PlayerOne;
            m_PlayerTwo.MoveDirection = Player.eMoveDirection.Down;
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
                do
                {
                  Console.WriteLine("Please enter a valid size of the board (6/8/10): ");
                } while (int.TryParse(Console.ReadLine(), out boardSize) == false);
            } while (GameBoard.CheckBoardSizeValidity(boardSize) == false);

            int userChoice;
            do
            {
                Console.WriteLine("For Player vs. Player Press (1) {0}For Player vs. Computer Press (2)", Environment.NewLine);
            } while ((int.TryParse(Console.ReadLine(), out userChoice) == false) && (checkGameTypeOption((eGameType)userChoice) == false));

            gameTypeOption = (eGameType) userChoice;
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
        private void convertStringToBoardPositions(string i_NextMove, out BoardPosition o_CurrentPosition, out BoardPosition o_NextPosition)
        {
            o_CurrentPosition = new BoardPosition();
            o_NextPosition = new BoardPosition();
            o_CurrentPosition.Column = i_NextMove[0] - 'A';
            o_CurrentPosition.Row = i_NextMove[1] - 'a';
            o_NextPosition.Column = i_NextMove[3] - 'A';
            o_NextPosition.Row = i_NextMove[4] - 'a';
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
            bool quitGame = false;
            string nextMove;
            Screen.Clear();
            printGameBoard();
            bool gameOver = false;
            while (quitGame == false)
            {
                while (gameOver == false)
                {
                    if (m_CurrentPlayer.CanCapture == false)
                    {
                        swapActivePlayer(ref m_CurrentPlayer, ref m_NextPlayer);
                        m_CurrentPlayer.UpdatePiecesMoves();
                    }

                    List<GamePiece> PiecesThatMustCapture;
                    PiecesThatMustCapture = m_CurrentPlayer.PiecesThatMustCapture(); //first
                                                                                     //we check if there are pieces that must capture
                    if (m_CurrentPlayer.IsComputer == true)
                    {
                        nextMove = m_CurrentPlayer.ComputerPlayerMove(PiecesThatMustCapture);
                    }
                    else
                    {
                        BoardPosition CurrentPlace;
                        BoardPosition NextPlace;
                        do
                        {
                            do
                            {
                                Console.Write("{0}'s turn: ", m_CurrentPlayer.Name);
                                nextMove = Console.ReadLine();
                            } while (checkMoveInputValidity(nextMove) == false);
                            convertStringToBoardPositions(nextMove, out CurrentPlace, out NextPlace);
                        } while (m_CurrentPlayer.CheckMoveAvailabillityAndMove(CurrentPlace, NextPlace, PiecesThatMustCapture) == false);
                    }

                    Screen.Clear();
                    printGameBoard();
                    Console.WriteLine("{0}'s move was: {1}", m_CurrentPlayer.Name, nextMove);
                    m_CurrentPlayer.CanCapture = false;
                    if (m_CurrentPlayer.CapturedAPiece == true)
                    {
                        m_NextPlayer.RemovePieces();
                        m_CurrentPlayer.UpdatePiecesMoves();
                        m_CurrentPlayer.CapturedAPiece = false;
                    }
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
            Console.Write(' ');
            for (int i = 0; i < (m_BoardData.GameBoardSize*4) + 2; i++)
            {
                Console.Write('=');
            }
            Console.WriteLine(' ');
        }
        private bool checkMoveInputValidity(string i_NextMove)
        {
            bool isValid = true;

            if (i_NextMove.Length != 5)
            {
                isValid = false;
            }
            else
            {
                if (i_NextMove[0]<'A' || i_NextMove[0]>'A'+m_BoardData.GameBoardSize)
                {
                    isValid = false;
                }
                else if (i_NextMove[1] < 'a' || i_NextMove[1] > 'a' + m_BoardData.GameBoardSize)
                {
                    isValid = false;
                }
                else if (i_NextMove[2] != '>')
                {
                    isValid = false;
                }
                else if (i_NextMove[3] < 'A' || i_NextMove[3] > 'A' + m_BoardData.GameBoardSize)
                {
                    isValid = false;
                }
                else if (i_NextMove[4] < 'a' || i_NextMove[4] > 'a' + m_BoardData.GameBoardSize)
                {
                    isValid = false;
                }
            }

            return isValid;
        }
        private void swapActivePlayer(ref Player i_ActivePlayer, ref Player i_NextPlayer)
        {
            Player temp;
            temp = i_ActivePlayer;
            i_ActivePlayer = i_NextPlayer;
            i_NextPlayer = temp;
        }
        
    }
}
