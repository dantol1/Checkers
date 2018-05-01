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
        private bool m_CapturedAPiece = false;
        public bool CapturedAPiece
        {
            get
            {
                return m_CapturedAPiece;
            }
        }
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
                BoardPosition newPositionLeftUp = new BoardPosition();
                BoardPosition newPositionRightUp = new BoardPosition();
                BoardPosition newPositionLeftDown = new BoardPosition();
                BoardPosition newPositionRightDown = new BoardPosition();

                newPositionRightUp.Row = newPositionLeftUp.Row = m_Position.Row - 1;
                newPositionRightDown.Row = newPositionLeftDown.Row = m_Position.Row + 1;
                newPositionRightDown.Column = newPositionRightUp.Column = m_Position.Column + 1;
                newPositionLeftDown.Column = newPositionLeftUp.Column = m_Position.Column - 1;

                if (m_BoardData.CheckIfInMargins(newPositionRightUp) == true)
                {
                    checkAndAddMovment(ref newPositionRightUp);
                }

                if (m_BoardData.CheckIfInMargins(newPositionRightDown) == true)
                {
                    checkAndAddMovment(ref newPositionRightDown);
                }

                if (m_BoardData.CheckIfInMargins(newPositionLeftUp) == true)
                {
                    checkAndAddMovment(ref newPositionLeftUp);
                }

                if (m_BoardData.CheckIfInMargins(newPositionLeftDown) == true)
                {
                    checkAndAddMovment(ref newPositionLeftDown);
                }

                if (m_CanCapture == true)
                {
                    r_PiecePlayer.CanCapture = m_CanCapture;
                }

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
                    checkAndAddMovment(ref newPositionLeft);
                }

                if (m_BoardData.CheckIfInMargins(newPositionRight) == true)
                {
                    checkAndAddMovment(ref newPositionRight);
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
            m_CapturedAPiece = checkMoveForCapturePossibility(i_NextPosition, m_Position);
            r_PiecePlayer.CapturedAPiece = m_CapturedAPiece;
            if(m_CapturedAPiece == true)
            {
                removeCapturedPieceFromBoard(i_NextPosition, m_Position);
                m_CanCapture = false;
            }
            m_Position = i_NextPosition;
            if (m_Symbol == 'X')
            {
                if(m_Position.Row == 0)
                {
                    m_IsKing = true;
                }
            }
            else
            {
                if (m_Position.Row == m_BoardData.GameBoardSize - 1)
                {
                    m_IsKing = true;
                }
            }
        }

        private void removeCapturedPieceFromBoard(BoardPosition i_NextPosition, BoardPosition i_CurrPosition)
        {
            BoardPosition capturedPiece;

            capturedPiece = i_NextPosition - i_CurrPosition;
            capturedPiece.Column = capturedPiece.Column / 2;
            capturedPiece.Row = capturedPiece.Row / 2;
            capturedPiece = i_CurrPosition + capturedPiece;
            m_BoardData.UpdateBoardCell(capturedPiece, ' ');
        }

        private void checkAndAddMovment(ref BoardPosition io_NewPosition)
        {
            bool moveCaptureAPiece = false;

            if (m_BoardData.CheckCellAvailabilityAndUpdateCaptureLocation(ref io_NewPosition, m_Symbol, io_NewPosition - m_Position,
                            out moveCaptureAPiece) == true)
            {
                if (m_CanCapture == false)
                {
                    m_CanCapture = moveCaptureAPiece;
                }
                if (m_CanCapture == true)
                {
                    if (moveCaptureAPiece == true)
                    {
                        m_AvailableMovement.Add(io_NewPosition);
                    }
                }
                else
                {
                    m_AvailableMovement.Add(io_NewPosition);
                }
            }
        }

        public void CheckIfCanCaptureAndUpdateMoveListAccrodingly()
        {
            if (m_CanCapture == true)
            {
                for(int i = m_AvailableMovement.Count - 1 ; i >= 0; i--)
                {
                    if (checkMoveForCapturePossibility(m_AvailableMovement[i], m_Position) == false)
                    {
                        m_AvailableMovement.Remove(m_AvailableMovement[i]);
                    }
                }
            }
        }

        private bool checkMoveForCapturePossibility(BoardPosition i_Move, BoardPosition i_CurrentPosition)
        {
            bool capturePossible = false;
            BoardPosition jump = i_Move - i_CurrentPosition;

            if(Math.Abs(jump.Row) > 1)
            {
                capturePossible = true;
            }

            return capturePossible;
        } 
    }
}
