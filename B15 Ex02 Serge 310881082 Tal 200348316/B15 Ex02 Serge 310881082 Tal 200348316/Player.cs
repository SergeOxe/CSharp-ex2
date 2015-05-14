

using System;
using System.Collections.Generic;

namespace B15_Ex02_Serge_310881082_Tal_200348316
{
    class Player
    {
        private readonly string r_Name;
        private readonly ePiece r_Piece;
        private readonly bool r_IsHuman;
        private Board m_Board;

        public ePiece Piece { get { return r_Piece;} }
        public string Name { get { return r_Name; } }


        public Player(Board i_Board, string i_Name, ePiece i_Piece, bool i_IsHuman)
        {
            m_Board = i_Board;
            r_Name = i_Name;
            r_Piece = i_Piece;
            r_IsHuman = i_IsHuman;
        }

        public Player(Board i_Board, string i_Name, ePiece i_Piece) : this(i_Board, i_Name, i_Piece, true)
        {
            // Empty in purpose
        }

        public Move GetMove(List<Move> i_PossibleMoves )
        {
            bool hasLegalMove = false;
            int x;
            int y;
            Move move = null;

            while (!hasLegalMove)
            {
                string moveStr = Console.ReadLine();
                if (string.IsNullOrEmpty(moveStr) || !m_Board.TryParse(moveStr, out x, out y))
                {
                    Console.WriteLine("The given input is invalid. Try again.");
                }
                else if(!isContainedInPossibleMoves(x, y, i_PossibleMoves, out move))
                {
                    Console.WriteLine("The given move ({0}) is not a legal move. Try again.", moveStr);
                }
                else
                {
                    hasLegalMove = true;
                }                
            }

            return move;
        }

        private bool isContainedInPossibleMoves(int i_X, int i_Y, List<Move> i_PossibleMoves, out Move o_Move)
        {
            o_Move = null;

            foreach (Move move in i_PossibleMoves)
            {
                if (move.X == i_X && move.Y == i_Y)
                {
                    o_Move = move;
                    break;
                }
            }

            return o_Move != null;
        }
    }
}
