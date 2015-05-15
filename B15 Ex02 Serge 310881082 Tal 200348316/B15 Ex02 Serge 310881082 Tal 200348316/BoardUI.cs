using System.Text;
using System;
namespace B15_Ex02_Serge_310881082_Tal_200348316
{
    
    class BoardUI
    {

        public static void DrawBoard(Board i_Board)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            drawLetters(i_Board.Size);
            drawLineSeparator(i_Board.Size);
            drawCells(i_Board);
            System.Console.WriteLine();
        }


        private static void drawLetters(int i_BoardSize)
        {
            StringBuilder lineToPrint = new StringBuilder();
            lineToPrint.Append("  ");
            for (int i = 0; i < i_BoardSize; i++)
            {
                char letter = (char)('A' + i);
                lineToPrint.Append("  " + letter + " ");
            }
            System.Console.WriteLine(lineToPrint);
        }

        private static void drawLineSeparator(int i_BoardSize)
        {
            StringBuilder lineToPrint = new StringBuilder();
            lineToPrint.Append("  =");
            for (int i = 0; i < i_BoardSize; i++)
            {
                lineToPrint.Append("====");
            }
            System.Console.WriteLine(lineToPrint);
        }

        private static void drawCells(Board i_Board)
        {
            StringBuilder lineToPrint = new StringBuilder();
            char piece;
            for (int i = 0; i < i_Board.Size; i++)
            {
                lineToPrint.Append((i + 1) + " |");
                for (int j = 0; j < i_Board.Size; j++)
                {
                    piece = ' ';
                    switch (i_Board[i, j])
                    {

                        case ePiece.Black:
                            piece = 'X';
                            break;

                        case ePiece.White:
                            piece = 'O';
                            break;
                    }

                    
                    lineToPrint.Append(" "+piece+" |");
                }
                System.Console.WriteLine(lineToPrint);
                drawLineSeparator(i_Board.Size);
                lineToPrint.Length = 0;

            }
        }

    }

}
