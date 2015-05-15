using System;
using System.Collections.Generic;
using System.Text;

namespace B15_Ex02_Serge_310881082_Tal_200348316
{
    class CoputerMoves
    {
        public static Move FindBestMove(List<Move> i_PossibleMoves,ePiece i_Piece, Board i_Board, int i_Depth)
        {
            //int? bestMoveValue = null;
            Move bestMove = null;
            foreach (Move move in i_PossibleMoves)
            {
                int currentMoveValue = recursiveMove(move, i_Piece, i_Board, i_Depth);
                if (bestMove == null || currentMoveValue > bestMove.Value)
                {
                    bestMove = move;
                    //bestMoveValue = currentMoveValue;
                }
            }
            return bestMove;
        }


        private static int recursiveMove(Move i_Move, ePiece i_Piece, Board i_Board, int i_Depth) 
        {
            int valueToReturn = 0;
            if (i_Depth != 0)
            {
                Board newBoard = i_Board.GetCopy();
                GameLogic.ExcecuteMove(newBoard, i_Move, i_Piece);
                ePiece oppositePiece = GameLogic.GetOppositePiece(i_Piece);
                List<Move> newPossibleMoves = GameLogic.GetPossibleMoves(newBoard, oppositePiece);
                Move bestMove = FindBestMove(newPossibleMoves, oppositePiece, newBoard, i_Depth - 1);
                if (bestMove != null)
                {
                    valueToReturn = bestMove.Value;
                }
                //valueToReturn =
                //    FindBestMove(GameLogic.GetPossibleMoves(newBoard, GameLogic.GetOppositePiece(i_Piece)),
                //        GameLogic.GetOppositePiece(i_Piece), newBoard, i_Depth - 1, player * -1).Value;
            }

            return i_Move.Value - valueToReturn;
        }

    }
}
