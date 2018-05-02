using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02_Eyal_321149296_Daniel_311250336
{
    public class Player
    {
        private const int k_MaxNameLength = 20;
        private int m_Score = 0;
        public int Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }
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
        private bool m_CapturedAPiece = false;
        public bool CapturedAPiece
        {
            get
            {
                return m_CapturedAPiece;
            }
            set
            {
                m_CapturedAPiece = value;
            }
        }
        private GameBoard m_BoardData;
        private readonly PieceSymbol r_PlayerSymbol;
        private readonly bool r_IsComputer = false;

        public bool IsComputer
        {
            get
            {
                return  r_IsComputer;
            }
        }
        List <GamePiece> m_AvailablePieces = null;
        public List<GamePiece> AvailablePieces
        {
            get
            {
                return m_AvailablePieces;
            }
        }
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
        public void ConnectThePiecesToThePlayer()
        {
            m_AvailablePieces.Clear();

            for (int i = 0; i < m_BoardData.GameBoardSize; i++)
            {
                for (int j = 0; j < m_BoardData.GameBoardSize; j++)
                {
                    if (m_BoardData.GetCellSymbol(i, j) == r_PlayerSymbol)
                    {
                        GamePiece newGamePiece = new GamePiece(j, i, r_PlayerSymbol.RegularSymbol, m_BoardData, this);
                        m_AvailablePieces.Add(newGamePiece);
                    }
                }
            }
        }
        public Player(string i_PlayerName, bool i_IsComputer,Game.eGameSymbols i_PlayerSymbol,GameBoard i_GameBoard)
        {
            m_Name = i_PlayerName;
            r_IsComputer = i_IsComputer;
            r_PlayerSymbol.RegularSymbol = (char)i_PlayerSymbol;
            m_BoardData = i_GameBoard;

            m_AvailablePieces = new List <GamePiece>((i_GameBoard.GameBoardSize / 2) * ((i_GameBoard.GameBoardSize / 2) - 1));

            ConnectThePiecesToThePlayer();
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
        public bool CheckMoveAvailabillityAndMove(BoardPosition i_CurrentPlace, BoardPosition i_NextPlace ,List<GamePiece> i_PlayerThatMustCapture)
        {
            bool isValid;

            if(m_CanCapture == true)//if there are game pieces that must capture, we must use only them
            {
                checkMoveAndMoveIfPossible(i_CurrentPlace, i_NextPlace, i_PlayerThatMustCapture, out isValid);
            }
            else //if there are no pieces that can capture, we move regularly
            {
                checkMoveAndMoveIfPossible(i_CurrentPlace, i_NextPlace, m_AvailablePieces, out isValid);
            }

            return isValid;
        }
        public List<GamePiece> PiecesThatMustCapture()
        {
            /*this method will check with the gameboard if there are player owned pieces
              that must capture, and will update the m_CanCapture field a well as return
              a list of those pieces.
            */
            List<GamePiece> mustCapturePieces = new List<GamePiece>();

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
        private void checkMoveAndMoveIfPossible(BoardPosition i_CurrentPlace, BoardPosition i_NextPlace, List<GamePiece> i_AvailablePieces, out bool o_moveValidity)
        {
            o_moveValidity = false;

            foreach (GamePiece piece in i_AvailablePieces)
            {
                if ((piece.Column == i_CurrentPlace.Column) && (piece.Row == i_CurrentPlace.Row))
                {//check if the left part of the input string is an available game piece
                    foreach (BoardPosition move in piece.MoveList)
                    {//check if the right part of the input is an available movement of the left part string
                        if ((move.Column == i_NextPlace.Column) && (move.Row == i_NextPlace.Row))
                        {
                            o_moveValidity = true;
                            piece.MovePiece(move);
                        }
                    }
                }
            }
        }
        public void UpdatePiecesMoves()
        {

            foreach(GamePiece piece in m_AvailablePieces)
            {
                piece.CanCapture = false;
                piece.MoveList.Clear();
                piece.UpdateAvailableMovement();
                piece.CheckIfCanCaptureAndUpdateMoveListAccrodingly();
            }
        }
        public void RemovePieces()
        {
            for(int i = m_AvailablePieces.Count - 1; i >= 0; i--)
            {
                char boardSymbol;

                boardSymbol = m_BoardData.GetCellSymbol(m_AvailablePieces[i].Row, m_AvailablePieces[i].Column);
                if(boardSymbol == ' ')
                {
                    m_AvailablePieces.RemoveAt(i);
                }
            }
        }
        public int CalculateScore()
        {
            int sumOfPieces = 0;

            foreach (GamePiece piece in AvailablePieces)
            {
                if (piece.isKing == true)
                {
                    sumOfPieces = sumOfPieces + 4;
                }
                else
                {
                    sumOfPieces++;
                }
            }

            return sumOfPieces;
        }
        public void CalculateAndSetScore()
        {
            int sumOfPieces = m_Score;
            foreach (GamePiece piece in AvailablePieces)
            {
                if (piece.isKing == true)
                {
                    sumOfPieces = sumOfPieces + 4;
                }
                else
                {
                    sumOfPieces++;
                }
            }

            m_Score = sumOfPieces;
        }
        public string ComputerPlayerMove(List<GamePiece> i_PiecesThatMustCapture)
        {
            Random randomPiece = new Random();
            Random randomMove = new Random();
            BoardPosition pieceCurrentBoardPosition  = new BoardPosition();
            BoardPosition pieceNextBoardPosition = new BoardPosition();
            int pieceToMove;
            int moveToDo;

            if (m_CanCapture == true)
            {
                pieceToMove = randomPiece.Next(i_PiecesThatMustCapture.Count) % i_PiecesThatMustCapture.Count;
                moveToDo = randomMove.Next(i_PiecesThatMustCapture[pieceToMove].MoveList.Count) % i_PiecesThatMustCapture.Count;
                pieceCurrentBoardPosition.Row = i_PiecesThatMustCapture[pieceToMove].Row;
                pieceCurrentBoardPosition.Column = i_PiecesThatMustCapture[pieceToMove].Column;
                i_PiecesThatMustCapture[pieceToMove].MovePiece(i_PiecesThatMustCapture[pieceToMove].MoveList[moveToDo]);
                pieceNextBoardPosition.Row = i_PiecesThatMustCapture[pieceToMove].Row;
                pieceNextBoardPosition.Column = i_PiecesThatMustCapture[pieceToMove].Column;
            }
            else
            {
                do
                {
                    pieceToMove = randomPiece.Next(m_AvailablePieces.Count) % m_AvailablePieces.Count;
                } while (m_AvailablePieces[pieceToMove].MoveList.Count == 0);

                moveToDo = randomMove.Next(m_AvailablePieces[pieceToMove].MoveList.Count) % m_AvailablePieces.Count;
                pieceCurrentBoardPosition.Row = m_AvailablePieces[pieceToMove].Row;
                pieceCurrentBoardPosition.Column = m_AvailablePieces[pieceToMove].Column;
                m_AvailablePieces[pieceToMove].MovePiece(m_AvailablePieces[pieceToMove].MoveList[moveToDo]);
                pieceNextBoardPosition.Row = m_AvailablePieces[pieceToMove].Row;
                pieceNextBoardPosition.Column = m_AvailablePieces[pieceToMove].Column;
            }



            return translateBoardPositionsToString(pieceCurrentBoardPosition, pieceNextBoardPosition);

        }

        private string translateBoardPositionsToString(BoardPosition i_CurrentPosition, BoardPosition i_NextPosition)
        {
            char[] move = new char[5];
            string moveMade;

            move[0] = (char) (i_CurrentPosition.Column + 'A');
            move[1] = (char)(i_CurrentPosition.Row + 'a');
            move[2] = '>';
            move[3] = (char)(i_NextPosition.Column + 'A');
            move[4] = (char)(i_NextPosition.Row + 'a');
            moveMade = new string(move);

            return moveMade;
        }
    }
}
