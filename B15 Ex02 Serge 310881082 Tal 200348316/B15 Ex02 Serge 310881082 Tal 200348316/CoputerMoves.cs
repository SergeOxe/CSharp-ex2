using System;
using System.Collections.Generic;
using System.Text;

namespace B15_Ex02_Serge_310881082_Tal_200348316
{
    class CoputerMoves
    {
        public Move FindBestMove(List<Move> i_PossibleMoves,ePiece i_Piece, Board i_Board, int i_Depth, int player)
        {
            Move maxMove = null;
            foreach (Move move in i_PossibleMoves){
                Board newBoard = new Board(i_Board.Size); 
                newBoard = i_Board;
                Move currentMove = recursiveMove(move, i_Piece, newBoard, i_Depth, player);
                if (currentMove.Value > maxMove.Value)
                {
                    maxMove = currentMove;
                }
            }
            return maxMove;
        }


        private Move recursiveMove(Move i_Move, ePiece i_Piece, Board i_Board, int i_Depth, int player) 
        {
            Move valueToReturn;
            if (i_Depth == 0)
            {
                valueToReturn = i_Move;
            }
            else
            {
                GameLogic.ExcecuteMove(i_Board,i_Move,i_Piece);
                valueToReturn = FindBestMove(GameLogic.GetPossibleMoves(i_Board, GameLogic.GetOppositePiece(i_Piece)), GameLogic.GetOppositePiece(i_Piece), i_Board, i_Depth - 1, player * -1);
            }
            return valueToReturn;
        }

    }
}
