using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace B18_Ex02_Eyal_321149296_Daniel_311250336
{
    public struct BoardPosition
    {
        private int m_Column;
        private int m_Row;
        public int Column
        {
            get
            {
                return m_Column;
            }
            set
            {
                m_Column = value;
            }
        }
        public int Row
        {
            get
            {
                return m_Row;
            }
            set
            {
                m_Row = value;
            }
        }

        public static BoardPosition operator+(BoardPosition i_OldBoardPosition, BoardPosition i_BoardPositionToAdd)
        {
            BoardPosition newBoardPosition = new BoardPosition();

            newBoardPosition.Row = i_OldBoardPosition.Row + i_BoardPositionToAdd.Row;
            newBoardPosition.Column = i_OldBoardPosition.Column + i_BoardPositionToAdd.Column;

            return newBoardPosition;
        }
        public static BoardPosition operator-(BoardPosition i_OldBoardPosition, BoardPosition i_BoardPositionToAdd)
        {
            BoardPosition newBoardPosition = new BoardPosition();

            newBoardPosition.Row = i_OldBoardPosition.Row - i_BoardPositionToAdd.Row;
            newBoardPosition.Column = i_OldBoardPosition.Column - i_BoardPositionToAdd.Column;

            return newBoardPosition;
        }

    }
}
