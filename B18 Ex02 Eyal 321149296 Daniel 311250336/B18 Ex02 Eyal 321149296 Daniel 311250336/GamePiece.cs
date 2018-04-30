using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02_Eyal_321149296_Daniel_311250336
{
    public class GamePiece
    {
        private bool m_IsKing = false;
        private readonly PieceSymbol m_Symbol;
        public char Symbol
        {
            get
            {
                if(m_IsKing == true)
                {
                    return m_Symbol.KingSymbol;
                }
                else
                {
                    return m_Symbol.RegularSymbol;
                }
            }
        }
        private GameBoard m_BoardData;
        private BoardPosition m_Position;
        List<BoardPosition> m_AvailableMovement;
        public List<BoardPosition> MoveList
        {
            get
            {
                return m_AvailableMovement;
            }
        }
        private readonly Player r_PiecePlayer;
        private bool m_CanCapture;
        public bool CanCapture
        {
            get
            {
                return m_CanCapture;
            }
            set
            {
                m_CanCapture = value;
            }
        }        
        public int Column
        {
            get
            {
                return m_Position.Column;
            }
        }
        public int Row
        {
            get
            {
                return m_Position.Row;
            }
        }

        public GamePiece(int i_Column, int i_Row, char i_Symbol, GameBoard i_GameBoard, Player i_PiecePlayer)
        {
            m_BoardData = i_GameBoard;
            m_Position = new BoardPosition();
            m_Position.Column = i_Column;
            m_Position.Row = i_Row;
            m_Symbol.RegularSymbol = i_Symbol;
            r_PiecePlayer = i_PiecePlayer;

            m_AvailableMovement = new List<BoardPosition>(2);
        }
        public void UpdateAvailableMovement()
        {
            if (m_IsKing == true)
            {

            }
            else
            {
                int moveDirection;
                if (m_Symbol == 'X')
                {
                    moveDirection = -1;
                }
                else
                {
                    moveDirection = 1;
                }
                BoardPosition newPositionLeft = new BoardPosition();
                BoardPosition newPositionRight = new BoardPosition();

                newPositionRight.Column = m_Position.Column + 1;
                newPositionLeft.Row = newPositionRight.Row = m_Position.Row + moveDirection;
                newPositionLeft.Column = m_Position.Column - 1;

                if (m_BoardData.CheckIfInMargins(newPositionLeft) == true)
                {
                    bool capturedAPiece;

                    checkAndAddMovment(ref newPositionLeft, out capturedAPiece);
                }
                if (m_BoardData.CheckIfInMargins(newPositionRight) == true)
                {
                    bool capturedAPiece;

                    checkAndAddMovment(ref newPositionRight, out capturedAPiece);
                }
            }

            if (m_CanCapture == true)
            {
                r_PiecePlayer.CanCapture = m_CanCapture;
            }
        }
        public void MovePiece(BoardPosition i_NextPosition)
        {
            m_BoardData.UpdateBoardCell(m_Position, ' ');
            m_BoardData.UpdateBoardCell(i_NextPosition, Symbol);
            m_Position = i_NextPosition;
        }
        private void checkAndAddMovment(ref BoardPosition io_NewPosition, out bool io_CapturedAPiece)
        {
            if (m_BoardData.CheckCellAvailability(ref io_NewPosition, m_Symbol, io_NewPosition - m_Position,
                            out io_CapturedAPiece) == true)
            {
                m_AvailableMovement.Add(io_NewPosition);
                m_CanCapture = io_CapturedAPiece;
            }
        }
    }
}





//public bool CanCaptureAPiece(Player.eMoveDirection i_MoveDirection)
//{
//    bool canCapture = false;

//    if(m_IsKing == true)
//    {

//    }
//    else
//    {
//        BoardPosition newPosition = new BoardPosition();
//        newPosition.Row = m_Position.Row + (int)i_MoveDirection;
//        newPosition.Column = m_Position.Column + 1;
//        if (m_BoardData.CheckIfInMargins(newPosition) == true)
//        {
//            if (m_Symbol != m_BoardData.GetCellSymbol(newPosition.Row, newPosition.Column))
//            {
//                newPosition.Row = m_Position.Row + (int)i_MoveDirection;
//                newPosition.Column = m_Position.Column + 1;
//                if (m_BoardData.CheckIfInMargins(newPosition) == true)
//                {
//                    if (m_BoardData.GetCellSymbol(newPosition.Row, newPosition.Column) == ' ')
//                    {
//                        canCapture = true;
//                    }
//                    newPosition.Row = m_Position.Row - (int)i_MoveDirection;
//                    newPosition.Column = m_Position.Column - 1;
//                }
//            }
//        }

//        newPosition.Column = m_Position.Column - 2;
//        if (m_BoardData.CheckIfInMargins(newPosition) == true)
//        {
//            if (m_Symbol != m_BoardData.GetCellSymbol(newPosition.Row, newPosition.Column))
//            {
//                newPosition.Row = m_Position.Row + (int)i_MoveDirection;
//                newPosition.Column = m_Position.Column - 1;
//                if (m_BoardData.CheckIfInMargins(newPosition) == true)
//                {
//                    if (m_BoardData.GetCellSymbol(newPosition.Row, newPosition.Column) == ' ')
//                    {
//                        canCapture = true;
//                    }
//                }
//            }
//        }
//    }
//    return canCapture;
//}
