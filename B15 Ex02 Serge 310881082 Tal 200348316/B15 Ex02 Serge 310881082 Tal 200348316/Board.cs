

namespace B15_Ex02_Serge_310881082_Tal_200348316
{
    
    class Board
    {
        private ePiece[,] m_GameMatrix;

        public Board(int i_Size)
        {
            m_GameMatrix = new ePiece[i_Size, i_Size];
            ClearBoard();
        }

        public void ClearBoard()
        {
            int middle = m_GameMatrix.Length / 2;
            for (int x = 0; x < m_GameMatrix.Length; x++)
            {
                for (int y = 0; y < m_GameMatrix.Length; y++)
                {
                    m_GameMatrix[x, y] = ePiece.None;
                }
            }

            // Change pieces at the middle
            m_GameMatrix[middle - 1, middle - 1] = ePiece.White;
            m_GameMatrix[middle, middle] = ePiece.White;
            m_GameMatrix[middle - 1, middle] = ePiece.Black;
            m_GameMatrix[middle, middle - 1] = ePiece.Black;
        }


    }
}
