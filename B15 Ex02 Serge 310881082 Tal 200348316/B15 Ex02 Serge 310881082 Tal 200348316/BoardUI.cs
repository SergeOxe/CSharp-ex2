
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

        private static void drawCells(Board i_Board)
        {
            char piece;
            for (int i = 0; i < i_Board.Size; i++)
            {
                System.Console.Write((i+1) + " |");
                for (int j = 0; j < i_Board.Size; j++)
                {
                    piece = ' ';
                    switch (i_Board.GetPiece(i, j))
                    {

                        case ePiece.Black:
                            piece = 'X';
                            break;

                        case ePiece.White:
                            piece = 'O';
                            break;
                    }

                    
                    System.Console.Write(" "+piece+" |");
                }
                System.Console.WriteLine("");
                drawLineSeparator(i_Board.Size);
            }
            
        }
       
       



    }

}
