

using System;

namespace B15_Ex02_Serge_310881082_Tal_200348316
{

    class Board
    {
        
    }

    public class OtheloGame
    {

        private Board m_Board;
        private Player m_WhitePlayer;
        private Player m_BlackPlayer;
        private Player m_CurrentPlayer;

        public void StartGame()
        {
            string secondPlayerName = "John from Microsoft";
            // Get game settings from player
            printWelcomeMsg();
            string firstPlayerName = getPlayerName(1);
            bool isTwoPlayers = getIsTwoPlayers();

            if (isTwoPlayers)
            {
                secondPlayerName = getPlayerName(2);
            }

            int boardSize = getBoardSize();

            initGame(firstPlayerName, secondPlayerName, isTwoPlayers, boardSize);

            while (!isGameOver())
            {
                BoardUI.DrawBoard(m_Board);
                printNextMoveMsg();
                Move nextMove = getNextMove();
                m_Board.ExcectureMove(nextMove);
                switchTurn();
            }

            printGameOverMsg();
            bool anotherGame = getAnotherGame();
            // TODO: Refactor this function so if anotherGame == true we start again inside while loop

        }

        private bool isGameOver()
        {
            return !m_Board.HasMove(Piece.Black) && !m_Board.HasMove(Piece.White);
        }

        private void initGame(string i_FirstPlayerName, string i_SecondPlayerName, bool i_IsTwoPlayers, int i_BoardSize)
        {
            m_WhitePlayer = new Player(i_FirstPlayerName, Piece.White);
            m_BlackPlayer = new Player(i_SecondPlayerName, Piece.Black);
            m_BlackPlayer.IsComputer = !i_IsTwoPlayers;
            m_Board = new Board(i_BoardSize);
            m_CurrentPlayer = m_WhitePlayer;
        }

        private int getBoardSize()
        {
            int size;

            Console.Write("Please enter board size (6 or 8): ");
            string sizeStr = Console.ReadLine();
            while (string.IsNullOrEmpty(sizeStr) || !int.TryParse(sizeStr, out size) || (size != 6 && size != 8))
            {
                Console.WriteLine("Please enter 6 or 8 only for size. Try again.");
                sizeStr = Console.ReadLine();
            }

            return size;
        }

        private bool getIsTwoPlayers()
        {
            Console.WriteLine("Is there another player?{0} Y - for yes{0}N - for no", Environment.NewLine);
            string yesOrNoStr = Console.ReadLine();
            while (string.IsNullOrEmpty(yesOrNoStr) || (yesOrNoStr.ToUpper() != "Y" && yesOrNoStr.ToUpper() != "N"))
            {
                Console.WriteLine("Please enter Y or N. Try again.");
                yesOrNoStr = Console.ReadLine();
            }

            return yesOrNoStr.ToUpper() == "Y";
        }

        private string getPlayerName(int i_PlayerNum)
        {
            Console.Write("Please enter {0} player name: ", MyStringUtils.AddOrdinal(i_PlayerNum));
            string name = Console.ReadLine();
            while (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Please don't enter an empty string. Try again.");
                name = Console.ReadLine();
            }

            return name;
        }
    }
}
