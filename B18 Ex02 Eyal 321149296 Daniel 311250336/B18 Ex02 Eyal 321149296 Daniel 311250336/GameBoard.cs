using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02_Eyal_321149296_Daniel_311250336
{
    //Logic Class
    public class GameBoard
    {
        private enum eGameBoardSize
        {
            Small = 6,
            Medium = 8,
            Large = 10,
        }
        private eGameBoardSize m_Size;
        private char[,] m_Board = null;
        public int GameBoardSize
        {
            get
            {
                return (int)m_Size;
            }
        }
        public GameBoard(int i_BoardSize)
        {
            m_Size = (eGameBoardSize)i_BoardSize;

            m_Board = new char[i_BoardSize, i_BoardSize];

            //Putting all the Game Pieces on board
            for (int i = 0; i < i_BoardSize/2-1; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < i_BoardSize; j += 2)
                    {
                        m_Board[i, j] = ' ';
                        m_Board[i, j + 1] = 'O';
                    }
                }
                else
                {
                    for (int j = 0; j < i_BoardSize; j += 2)
                    {
                        m_Board[i, j] = 'O';
                        m_Board[i, j + 1] = ' ';
                    }
                }
            }
            //Putting two space lines between players
            for (int i = i_BoardSize/2-1; i< i_BoardSize / 2 + 1; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    m_Board[i, j] = ' ';
                }
            }
            //Putting the opposite player game piece on board
            for (int i = i_BoardSize / 2 + 1; i < i_BoardSize; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < i_BoardSize; j += 2)
                    {
                        m_Board[i, j] = ' ';
                        m_Board[i, j + 1] = 'X';
                    }
                }
                else
                {
                    for (int j = 0; j < i_BoardSize; j += 2)
                    {
                        m_Board[i, j] = 'X';
                        m_Board[i, j + 1] = ' ';
                    }
                }
            }

        }
        public static bool CheckBoardSizeValidity(int i_BoardSize)
        {
            bool isValid = true;

            if (Enum.IsDefined(typeof(eGameBoardSize), i_BoardSize) == false)
            {
                isValid = false;
            }

            
            return isValid;
        }
        public char GetCellSymbol(int i_Row, int i_Col)
        {
            return m_Board[i_Row, i_Col];
        }

        public bool CheckCellAvailability(ref BoardPosition io_GamePieceNewBoardPosition, PieceSymbol i_GamePieceSymbol, BoardPosition i_MoveDirection, out bool o_PieceCaptured)
        {
            bool isAvailable = false;
            o_PieceCaptured = false;

            if (m_Board[io_GamePieceNewBoardPosition.Row, io_GamePieceNewBoardPosition.Column] == ' ')
            { //the cell the piece wants to move to is empty
                isAvailable = true;
            }
            else if (m_Board[io_GamePieceNewBoardPosition.Row, io_GamePieceNewBoardPosition.Column] != i_GamePieceSymbol)
            { //the cell the piece wants to move to has an opponent piece
                io_GamePieceNewBoardPosition = io_GamePieceNewBoardPosition + i_MoveDirection;
                if (CheckIfInMargins(io_GamePieceNewBoardPosition) == true)
                {
                    if (m_Board[io_GamePieceNewBoardPosition.Row, io_GamePieceNewBoardPosition.Column] == ' ')
                    { //the piece can capture the opponent piece.
                        isAvailable = true;
                        o_PieceCaptured = true;
                    }
                }

            }

            return isAvailable;
        }
        public void UpdateBoardCell(BoardPosition i_CellToChange, char i_SymbolToChangeTo)
        {
            m_Board[i_CellToChange.Row, i_CellToChange.Column] = i_SymbolToChangeTo;
        }
        public bool CheckIfInMargins(BoardPosition i_PositionToCheck)
        {
            bool isValid = true;

            if(i_PositionToCheck.Row < 0)
            {
                isValid = false;
            }
            else if(i_PositionToCheck.Row >= (int)m_Size)
            {
                isValid = false;
            }
            else if (i_PositionToCheck.Column < 0)
            {
                isValid = false;
            }
            else if (i_PositionToCheck.Column >= (int)m_Size)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
