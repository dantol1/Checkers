using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02_Eyal_321149296_Daniel_311250336
{
    class GamePiece
    {
        private bool m_IsKing = false;
        private char m_Symbol;
        private GameBoard m_BoardData;
        private BoardPosition m_Position;
        List<BoardPosition> m_AvailableMovement;
        
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

        public GamePiece(int i_Column, int i_Row, char i_Symbol, GameBoard i_GameBoard)
        {
            m_BoardData = i_GameBoard;
            m_Position.Column = i_Column;
            m_Position.Row = i_Row;
            m_Symbol = i_Symbol;

            m_AvailableMovement = new List<BoardPosition>(2);
        }
        public void updateAvailableMovement()
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
                newPositionRight.Row = m_Position.Row + moveDirection;

                newPositionLeft.Column = m_Position.Column - 1;
                newPositionLeft.Row = m_Position.Row + moveDirection;

                if ((newPositionLeft.Column > 0) && (newPositionLeft.Column < m_BoardData.GameBoardSize))
                {

                }
            }
        }
    }
}
