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
                Move currentMove = recursiveMove(move, i_piece, newBoard, i_depth, player);
                if (currentMove.Value > maxMove.Value)
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
                GameLogic.ExcecuteMove(i_board,i_move,i_piece);
                return FindBestMove(GameLogic.GetPossibleMoves(i_board, GameLogic.GetOppositePiece(i_piece)), GameLogic.GetOppositePiece(i_piece), i_board, i_depth - 1, player * -1);
            }
        }

    }
}
