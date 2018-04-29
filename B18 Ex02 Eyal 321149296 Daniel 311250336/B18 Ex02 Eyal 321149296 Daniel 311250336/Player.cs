using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02_Eyal_321149296_Daniel_311250336
{
    public class Player
    {
        private const int k_MaxNameLength = 20;
        private string m_Name;
        public string Name
        {
            get
            {
                return m_Name;
            }
        }
        private readonly char r_PlayerSymbol;
        private readonly bool r_IsComputer = false;
        List <GamePiece> m_AvailablePieces = null;

        public Player(string i_PlayerName, bool i_IsComputer,char i_PlayerSymbol,GameBoard i_GameBoard)
        {
            m_Name = i_PlayerName;
            r_IsComputer = i_IsComputer;
            r_PlayerSymbol = i_PlayerSymbol;

            m_AvailablePieces.Capacity = (i_GameBoard.GameBoardSize / 2) * ((i_GameBoard.GameBoardSize / 2) - 1);

            for (int i = 0; i < i_GameBoard.GameBoardSize; i++)
            {
                for (int j = 0; j < i_GameBoard.GameBoardSize; j++)
                {
                    if (i_GameBoard.GetCellSymbol(i,j) == r_PlayerSymbol)
                    {
                        GamePiece newGamePiece = new GamePiece(j, i, r_PlayerSymbol);
                        m_AvailablePieces.Add(newGamePiece);
                    }
                }
            }


        }
        public static bool CheckNameValidity(string i_NamePlayerOne)
        {
            bool isValid = true;

            if (i_NamePlayerOne.Length > k_MaxNameLength)
            {
                isValid = false;
            }

            foreach (char c in i_NamePlayerOne)
            {
                if (c == ' ')
                {
                    isValid = false;
                }
            }

            if (isValid == false)
            {
                Console.WriteLine("Name not valid");
            }
            return isValid;
        }

    }
}
