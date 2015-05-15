using System;
using System.Collections.Generic;
using System.Text;

namespace B15_Ex02_Serge_310881082_Tal_200348316
{
    class CoputerMoves
    {
        public Move FindBestMove(List<Move> i_possibleMoves,ePiece i_piece, Board i_board, int i_depth, int player)
        {
            Move maxMove = null;
            foreach (Move move in i_possibleMoves){
                Board newBoard = new Board(i_board.Size); 
                newBoard = i_board;
                Move currentMove = recursiveMove(move, i_piece,newBoard,i_depth,player)
                if (currentMove.value() > maxMove)
                {
                    maxMove = currentMove;
                }
            }
            return maxMove;
        }


        private Move recursiveMove(Move i_move, ePiece i_piece, Board i_board, int i_depth, int player) 
        {

            if (i_depth == 0)
            {
                return i_move;
            }
            else
            {
                
            }
        }





    }
}
