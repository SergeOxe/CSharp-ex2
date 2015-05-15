

namespace B15_Ex02_Serge_310881082_Tal_200348316
{
    
    class Board
    {
        private ePiece[,] m_GameMatrix;
        private int m_WhitePoints;
        private int m_BlackPoints;

        public int Size
        {
            get { return m_GameMatrix.GetLength(0); }
        }

        public int WhitePoints { get { return m_WhitePoints; } }
        public int BlackPoints { get { return m_BlackPoints; } }

        public ePiece this[int i_X, int i_Y]
        {
            get { return getPiece(i_X, i_Y); }
            set { setPiece(i_X, i_Y, value); }
        }

        public Board(int i_Size)
        {
            m_GameMatrix = new ePiece[i_Size, i_Size];
        }

        public Board GetCopy()
        {
            Board copyBoard = new Board(Size);
            copyBoard.m_BlackPoints = BlackPoints;
            copyBoard.m_WhitePoints = WhitePoints;
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    copyBoard.m_GameMatrix[x, y] = m_GameMatrix[x, y];
                }
            }

            return copyBoard;
        }

        private void setPiece(int i_X, int i_Y, ePiece i_Piece)
        {
            switch (i_Piece)
            {
                case ePiece.Black:
                    if (m_GameMatrix[i_X, i_Y] == ePiece.White)
                    {
                        m_WhitePoints--;
                    }
                    
                    m_BlackPoints++;
                    break;
                case ePiece.White:
                    if (m_GameMatrix[i_X, i_Y] == ePiece.Black)
                    {
                        m_BlackPoints--;
                    }
                    
                    m_WhitePoints++;
                    break;
            }
            m_GameMatrix[i_X, i_Y] = i_Piece;
        }

        private ePiece getPiece(int i_X, int i_Y)
        {
            return m_GameMatrix[i_X, i_Y];
        }

        public void InitForNewGame()
        {
            int middle = Size / 2;
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    m_GameMatrix[x, y] = ePiece.None;
                }
            }

            m_WhitePoints = 0;
            m_BlackPoints = 0;

            // Change pieces at the middle
            setPiece(middle - 1, middle - 1, ePiece.White);
            setPiece(middle, middle, ePiece.White);
            setPiece(middle - 1, middle, ePiece.Black);
            setPiece(middle, middle - 1, ePiece.Black);
        }

        public bool TryParse(string i_Pos, out int o_X, out int o_Y)
        {
            bool isParsed = false;
            o_X = -1;
            o_Y = -1;
            if (!string.IsNullOrEmpty(i_Pos) && i_Pos.Length == 2 && char.IsLetter(i_Pos[0]) && char.IsDigit(i_Pos[1]))
            {
                int y = convertToNumber(i_Pos[0]);
                int x = int.Parse(i_Pos[1].ToString()) - 1;
                if (IsInBounds(x, y))
                {
                    o_X = x;
                    o_Y = y;
                    isParsed = true;
                }
            }

            return isParsed;
        }

        private int convertToNumber(char i_Letter)
        {
            char upperCaseLetter = char.ToUpper(i_Letter);

            return upperCaseLetter - 'A';
        }

        //public void ExecuteMove(Move i_Move, ePiece i_PlayerColor)
        //{
        //    m_GameMatrix[i_Move.X, i_Move.Y] = i_PlayerColor;

        //    turnPieces(i_Move.X, i_Move.Y, -1, -1, i_PlayerColor);
        //    turnPieces(i_Move.X, i_Move.Y, -1, 0, i_PlayerColor);
        //    turnPieces(i_Move.X, i_Move.Y, -1, 1, i_PlayerColor);
        //    turnPieces(i_Move.X, i_Move.Y, 0, 1, i_PlayerColor);
        //    turnPieces(i_Move.X, i_Move.Y, 1, 1, i_PlayerColor);
        //    turnPieces(i_Move.X, i_Move.Y, 0, 1, i_PlayerColor);
        //    turnPieces(i_Move.X, i_Move.Y, -1, 1, i_PlayerColor);
        //    turnPieces(i_Move.X, i_Move.Y, 0, -1, i_PlayerColor);
        //}

        //private void turnPieces(int i_X, int i_Y, int i_XDirection, int i_YDirection, ePiece i_PlayerColor)
        //{
        //    i_X += i_XDirection;
        //    i_Y += i_YDirection;
        //    if (isOppositeColor(i_X, i_Y, i_PlayerColor) &&
        //        endWithMyColor(i_X + i_XDirection, i_Y + i_YDirection, i_XDirection, i_YDirection, i_PlayerColor))
        //    {
        //        do
        //        {
        //            m_GameMatrix[i_X, i_Y] = i_PlayerColor;
        //            i_X += i_XDirection;
        //            i_Y += i_YDirection;
        //        } while (isOppositeColor(i_X, i_Y, i_PlayerColor));
        //    }
        //}

        //private bool endWithMyColor(int i_X, int i_Y, int i_XDirection, int i_YDirection, ePiece i_PlayerColor)
        //{
        //    bool hasEncounteredMyColor = false;

        //    do
        //    {
        //        if (isOppositeColor(i_X, i_Y, i_PlayerColor))
        //        {
        //            i_X += i_XDirection;
        //            i_Y += i_YDirection;
        //        }
        //        else if (isEmpty(i_X, i_Y))
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            hasEncounteredMyColor = true;
        //            break;
        //        }
                
        //    } while (isInBounds(i_X, i_Y));

        //    return hasEncounteredMyColor;
        //}

        private bool isEmpty(int i_X, int i_Y)
        {
            return m_GameMatrix[i_X, i_Y] == ePiece.None;
        }

        public bool IsInBounds(int i_X, int i_Y)
        {
            return i_X >= 0 && i_Y >= 0 && i_X < Size && i_Y < Size;
        }

        private bool isOppositeColor(int i_X, int i_Y, ePiece i_PlayerColor)
        {
            bool isOpposite = true;

            if (m_GameMatrix[i_X, i_Y] == ePiece.None)
            {
                isOpposite = false;
            }
            else if (m_GameMatrix[i_X, i_Y] == i_PlayerColor)
            {
                isOpposite = false;
            }

            return isOpposite;
        }
    }
}
