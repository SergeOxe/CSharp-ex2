using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B15_Ex02_Serge_310881082_Tal_200348316
{
    public enum Piece {Blank,Black,White};
    class Board
    {
        private Piece[,] board;
       
        private int m_boardSize;

        public Board (int i_boardSize)
        {
            m_boardSize = i_boardSize;

        }


        public void DrawBoard(Piece[,] i_board)
        {
            drawLetters();
            drawLineSeparator();
            drawCells(i_board);

            System.Console.WriteLine();



            System.Console.Read();
        }

        private void drawLetters()
        {
            System.Console.Write("  ");
            for (int i = 0; i < m_boardSize; i++)
            {
                char letter = (char)('A' + i);
                System.Console.Write("  " + letter + " ");
            }
            System.Console.WriteLine();
        }

        private void drawLineSeparator()
        {
            System.Console.Write("  =");
            for (int i = 0; i < m_boardSize; i++)
            {
                System.Console.Write("====");
            }
            System.Console.WriteLine();
        }

        private void drawCells(Piece[,] i_board)
        {
            char piece;
            for (int i = 0; i < m_boardSize; i++)
            {
                System.Console.Write((i+1) + " |");
                for (int j = 0; j < m_boardSize; j++)
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
                drawLineSeparator();
            }
            
        }
       
       



    }

    class Stam{
        public static void Main()
        {
            Board board = new Board(6);
            board.DrawBoard();
        } 

    }
}
