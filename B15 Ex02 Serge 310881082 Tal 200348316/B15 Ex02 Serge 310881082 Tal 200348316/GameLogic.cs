using System;
using System.Collections.Generic;
using System.Text;

namespace B15_Ex02_Serge_310881082_Tal_200348316
{
    class GameLogic
    {
        public static bool IsGameOver(Board i_Board)
        {
            bool isGameOver = true;

            for (int x = 0; x < i_Board.Size && isGameOver; x++)
            {
                for (int y = 0; y < i_Board.Size; y++)
                {
                    if (i_Board[x, y] != ePiece.None)
                    {
                        continue;
                    }

                    if (getPossibleMoveAtPos(i_Board, x, y, ePiece.Black) != null)
                    {
                        isGameOver = false;
                        break;
                    }
                    if (getPossibleMoveAtPos(i_Board, x, y, ePiece.White) != null)
                    {
                        isGameOver = false;
                        break;
                    }
                }
            }

            return isGameOver;
        }

        public static List<Move> GetPossibleMoves(Board i_Board, ePiece i_Piece)
        {
            List<Move> possibleMoves = new List<Move>();

            for (int x = 0; x < i_Board.Size; x++)
            {
                for (int y = 0; y < i_Board.Size; y++)
                {
                    Move move = getPossibleMoveAtPos(i_Board, x, y, i_Piece);
                    if (move != null)
                    {
                        possibleMoves.Add(move);
                    }
                }
            }

            return possibleMoves;
        }

        public static void ExcecuteMove(Board i_Board, Move i_NextMove, ePiece i_CurrentPlayerPiece)
        {
            i_Board[i_NextMove.X, i_NextMove.Y] = i_CurrentPlayerPiece;
            foreach (Direction direction in i_NextMove.Directions)
            {
                turnPieces(i_Board, i_NextMove.X + direction.X, i_NextMove.Y + direction.Y, direction.X, direction.Y, i_CurrentPlayerPiece);
            }
        }

        public static ePiece GetOppositePiece(ePiece i_Piece)
        {
            if (i_Piece == ePiece.Black)
            {
                return ePiece.White;
            }
            else if (i_Piece == ePiece.White)
            {
                return ePiece.Black;
            }

            return ePiece.None;
        }

        private static void turnPieces(Board i_Board, int i_X, int i_Y, int i_XDirection, int i_YDirection, ePiece i_CurrentPlayerPiece)
        {
            while (i_Board[i_X, i_Y] != i_CurrentPlayerPiece)
            {
                i_Board[i_X, i_Y] = i_CurrentPlayerPiece;
                i_X += i_XDirection;
                i_Y += i_YDirection;
            }

        }

        private static Move getPossibleMoveAtPos(Board i_Board, int i_X, int i_Y, ePiece i_Piece)
        {
            Move move = null;
            List<Direction> directions = new List<Direction>();
            int moveValue = 1;

            if (i_Board[i_X, i_Y] == ePiece.None)
            {
                for (int xDirection = -1; xDirection <= 1; xDirection++)
                {
                    for (int yDirection = -1; yDirection <= 1; yDirection++)
                    {
                        if (isOppositePiece(i_Board, i_X + xDirection, i_Y + yDirection, i_Piece) &&
                            endWithMyPiece(i_Board, i_X + xDirection, i_Y + yDirection, xDirection, yDirection, i_Piece, ref moveValue))
                        {
                            directions.Add(new Direction(xDirection, yDirection));
                        }
                    }
                }
            }

            if (directions.Count > 0)
            {
                move = new Move(i_X, i_Y, directions, moveValue);
            }

            return move;
        }

        private static bool isOppositePiece(Board i_Board, int i_X, int i_Y, ePiece i_Piece)
        {
            bool isOpposite = true;

            if (!i_Board.IsInBounds(i_X, i_Y))
            {
                isOpposite = false;
            }
            else if (i_Board[i_X, i_Y] == ePiece.None)
            {
                isOpposite = false;
            }
            else if (i_Board[i_X, i_Y] == i_Piece)
            {
                isOpposite = false;
            }

            return isOpposite;
        }

        private static bool endWithMyPiece(Board i_Board, int i_X, int i_Y, int i_XDirection, int i_YDirection, ePiece i_Piece, ref int o_Value)
        {
            bool hasEncounteredMyPiece = false;
            int flippedPieces = 0;

            while (i_Board.IsInBounds(i_X, i_Y))
            {
                if (i_Board[i_X, i_Y] == ePiece.None)
                {
                    break;
                }
                if (i_Board[i_X, i_Y] == i_Piece)
                {
                    hasEncounteredMyPiece = true;
                    o_Value += flippedPieces;
                    break;
                }

                i_X += i_XDirection;
                i_Y += i_YDirection;
                flippedPieces++;
            }

            return hasEncounteredMyPiece;
        }
    }
}
