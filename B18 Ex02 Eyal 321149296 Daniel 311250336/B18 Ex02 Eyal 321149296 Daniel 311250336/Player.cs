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
        private bool m_CanCapture = false;
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
        private GameBoard m_BoardData;
        private readonly PieceSymbol r_PlayerSymbol;
        private readonly bool r_IsComputer = false;
        List <GamePiece> m_AvailablePieces = null;
        public enum eMoveDirection
        {
            Down = 1,
            Up = -1
        }
        private eMoveDirection m_MoveDirection;
        public eMoveDirection MoveDirection
        {
            get
            {
                return m_MoveDirection;
            }
            set
            {
                m_MoveDirection = value;
            }
        }

        public Player(string i_PlayerName, bool i_IsComputer,Game.eGameSymbols i_PlayerSymbol,GameBoard i_GameBoard)
        {
            m_Name = i_PlayerName;
            r_IsComputer = i_IsComputer;
            r_PlayerSymbol.RegularSymbol = (char)i_PlayerSymbol;
            m_BoardData = i_GameBoard;

            m_AvailablePieces = new List <GamePiece>((i_GameBoard.GameBoardSize / 2) * ((i_GameBoard.GameBoardSize / 2) - 1));

            for (int i = 0; i < i_GameBoard.GameBoardSize; i++)
            {
                for (int j = 0; j < i_GameBoard.GameBoardSize; j++)
                {
                    if (i_GameBoard.GetCellSymbol(i,j) == r_PlayerSymbol)
                    {
                        GamePiece newGamePiece = new GamePiece(j, i, r_PlayerSymbol.RegularSymbol,m_BoardData, this);
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
        public bool CheckMoveAvailabillity(BoardPosition i_CurrentPlace, BoardPosition i_NextPlace ,List<GamePiece> i_PlayerThatMustCapture)
        {
            bool isValid = true;
            if(m_CanCapture == true)
            {
                foreach (GamePiece piece in i_PlayerThatMustCapture)
                {
                    if ((piece.Column == i_CurrentPlace.Column) && (piece.Row == i_CurrentPlace.Row))
                    {

                    }
                }
            }

            return isValid;
        }
        public List<GamePiece> PiecesThatMustCapture()
        {
            /*this method will check with the gameboard if there are player owned pieces
              that must capture, and will update the m_CanCapture field a well as return
              a list of those pieces.
            */
            List<GamePiece> mustCapturePieces = new List<GamePiece>;

            foreach(GamePiece piece in m_AvailablePieces)
            {
                if (piece.CanCapture == true)
                {
                    mustCapturePieces.Add(piece);
                    m_CanCapture = true;
                }
            }

            return mustCapturePieces;
        }

    }
}
