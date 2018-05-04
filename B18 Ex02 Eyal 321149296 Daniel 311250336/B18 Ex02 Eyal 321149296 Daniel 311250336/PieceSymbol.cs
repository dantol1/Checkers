using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02_Eyal_321149296_Daniel_311250336
{
    public struct PieceSymbol
    {
        private char m_RegularSymbol;

        public char RegularSymbol
        {
            get
            {
                return m_RegularSymbol;
            }

            set
            {
                m_RegularSymbol = value;
                if(value == (char)Game.eGameSymbols.PlayerOneRegular)
                {
                    m_KingSymbol = (char)Game.eGameSymbols.PlayerOneKing;
                }
                else
                {
                    m_KingSymbol = (char)Game.eGameSymbols.PlayerTwoKing;
                }
            }
        }

        private char m_KingSymbol;

        public char KingSymbol
        {
            get
            {
                return m_KingSymbol;
            }

            set
            {
                m_KingSymbol = value;
            }
        }

        public static bool operator !=(PieceSymbol i_Symbol, char i_SymbolToEqual)
        {
            bool notEqual = true;

            if((i_Symbol.RegularSymbol == i_SymbolToEqual) || (i_Symbol.KingSymbol == i_SymbolToEqual))
            {
                notEqual = false;
            }

            return notEqual;
        }

        public static bool operator ==(PieceSymbol i_Symbol, char i_SymbolToEqual)
        {
            bool equal = false;

            if ((i_Symbol.RegularSymbol == i_SymbolToEqual) || (i_Symbol.KingSymbol == i_SymbolToEqual))
            {
                equal = true;
            }

            return equal;
        }

        public static bool operator ==(char i_SymbolToEqual, PieceSymbol i_Symbol)
        {
            bool equal = false;

            if ((i_Symbol.RegularSymbol == i_SymbolToEqual) || (i_Symbol.KingSymbol == i_SymbolToEqual))
            {
                equal = true;
            }

            return equal;
        }

        public static bool operator !=(char i_SymbolToEqual, PieceSymbol i_Symbol)
        {
            bool notEqual = true;

            if ((i_Symbol.RegularSymbol == i_SymbolToEqual) || (i_Symbol.KingSymbol == i_SymbolToEqual))
            {
                notEqual = false;
            }

            return notEqual;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
