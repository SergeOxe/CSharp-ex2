using System.Collections.Generic;

namespace B15_Ex02_Serge_310881082_Tal_200348316.Othello
{
    internal class GameAI
    {
        public static Move FindBestMove(List<Move> i_PossibleMoves, ePiece i_Piece, Board i_Board, int i_Depth)
        {
            int? bestMoveValue = null;
            Move bestMove = null;
            foreach (Move move in i_PossibleMoves)
            {
                int currentMoveValue = recursiveMove(move, i_Piece, i_Board, i_Depth);
                if (bestMoveValue == null || currentMoveValue > bestMoveValue)
                {
                    bestMove = move;
                    bestMoveValue = currentMoveValue;
                }
            }
            
            return bestMove;
        }

        private static int recursiveMove(Move i_Move, ePiece i_Piece, Board i_Board, int i_Depth) 
        {
            Board newBoard = i_Board.GetCopy();
            GameLogic.ExcecuteMove(newBoard, i_Move, i_Piece);
            ePiece oppositePiece = GameLogic.GetOppositePiece(i_Piece);

            if (i_Depth != 0)
            {
                List<Move> newPossibleMoves = GameLogic.GetPossibleMoves(newBoard, oppositePiece);
                Move bestMove = FindBestMove(newPossibleMoves, oppositePiece, newBoard, i_Depth - 1);
                if (bestMove != null)
                {
                    GameLogic.ExcecuteMove(newBoard, bestMove, oppositePiece);
                }
            }

            return myPoints(newBoard, i_Piece) - myPoints(newBoard, oppositePiece);
        }

        private static int myPoints(Board i_Board, ePiece i_Piece)
        {
            int points = 0;

            switch (i_Piece)
            {
                case ePiece.Black:
                    points = i_Board.BlackPoints;
                    break;
                case ePiece.White:
                    points = i_Board.WhitePoints;
                    break;
            }

            return points;
        }
    }
}
