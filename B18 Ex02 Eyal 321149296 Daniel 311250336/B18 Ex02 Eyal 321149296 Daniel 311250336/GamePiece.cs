using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02_Eyal_321149296_Daniel_311250336
{
    class GamePiece
    {
        private bool m_IsKing = false;
        private char m_Symbol;
        private int m_Column;
        private int m_Row;

        public GamePiece(int i_Column, int i_Row, char i_Symbol)
        {
            m_Column = i_Column;
            m_Row = i_Row;
            m_Symbol = i_Symbol;
        }
    }
}
