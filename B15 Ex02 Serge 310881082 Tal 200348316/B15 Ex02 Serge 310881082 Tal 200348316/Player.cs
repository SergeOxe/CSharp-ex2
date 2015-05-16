namespace B15_Ex02_Serge_310881082_Tal_200348316.Othello
{
    internal class Player
    {
        private readonly string r_Name;
        private readonly ePiece r_Piece;
        private readonly bool r_IsHuman;

        public ePiece Piece
        {
            get { return r_Piece; }
        }

        public string Name
        {
            get { return r_Name; }
        }

        public bool IsHuman
        {
            get { return r_IsHuman; }
        }

        public Player(string i_Name, ePiece i_Piece, bool i_IsHuman)
        {
            r_Name = i_Name;
            r_Piece = i_Piece;
            r_IsHuman = i_IsHuman;
        }

        public Player(string i_Name, ePiece i_Piece) : this(i_Name, i_Piece, true)
        {
            // Empty in purpose
        }
    }
}
