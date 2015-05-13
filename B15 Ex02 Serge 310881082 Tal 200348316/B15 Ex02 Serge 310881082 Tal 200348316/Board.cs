using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B15_Ex02_Serge_310881082_Tal_200348316
{
    public enum Piece {Blank,Black,White};
    class BoardUI
    {

        public static void DrawBoard(Piece[,] i_Board)
        {
            int boardSize = i_Board.Length;
            drawLetters(boardSize);
            drawLineSeparator(boardSize);
            drawCells(i_Board);
            System.Console.WriteLine();
        }


        private static void drawLetters(int i_BoardSize)
        {
            System.Console.Write("  ");
            for (int i = 0; i < i_BoardSize; i++)
            {
                char letter = (char)('A' + i);
                System.Console.Write("  " + letter + " ");
            }
            System.Console.WriteLine();
        }

        private static void drawLineSeparator(int i_BoardSize)
        {
            System.Console.Write("  =");
            for (int i = 0; i < i_BoardSize; i++)
            {
                System.Console.Write("====");
            }
            System.Console.WriteLine();
        }

        private static void drawCells(Piece[,] i_board)
        {
            int boardSize = i_board.Length;
            char piece;
            for (int i = 0; i < boardSize; i++)
            {
                System.Console.Write((i+1) + " |");
                for (int j = 0; j < boardSize; j++)
                {
                    piece = ' ';
                    switch (i_board[i, j])
                    {

                        case Piece.Black:
                            piece = 'X';
                            break;

                        case Piece.White:
                            piece = 'O';
                            break;
                    }

                    
                    System.Console.Write(" "+piece+" |");
                }
                System.Console.WriteLine("");
                drawLineSeparator(boardSize);
            }
            
        }
       
       



    }

}
