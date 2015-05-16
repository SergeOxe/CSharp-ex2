using System;
using System.Collections.Generic;

namespace B15_Ex02_Serge_310881082_Tal_200348316.Othello
{
    internal enum ePiece : byte
    {
        None,
        Black, 
        White
    }

    public class OthelloGame
    {
        private Board m_Board;
        private Player m_WhitePlayer;
        private Player m_BlackPlayer;
        private Player m_CurrentPlayer;

        public void StartGame()
        {
            bool isSignledQuit = false;
            string secondPlayerName = "John from Microsoft";

            // Get game settings from player
            printWelcomeMsg();
            string firstPlayerName = getPlayerName(1);
            bool isTwoPlayers = getIsTwoPlayers();

            if (isTwoPlayers)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                secondPlayerName = getPlayerName(2);
            }

            int boardSize = getBoardSize();

            initGame(firstPlayerName, secondPlayerName, isTwoPlayers, boardSize);

            while (!isSignledQuit)
            {
                m_Board.InitForNewGame();

                while (!GameLogic.IsGameOver(m_Board))
                {
                    Move nextMove = null;

                    BoardDrawer.DrawBoard(m_Board);
                    List<Move> possibleMoves = GameLogic.GetPossibleMoves(m_Board, m_CurrentPlayer.Piece);
                    if (possibleMoves.Count == 0)
                    {
                        printNoMovesMsg();
                        switchTurn();
                        Console.ReadLine();
                        continue;
                    }

                    printNextMoveMsg();
                    if (m_CurrentPlayer.IsHuman)
                    {
                        nextMove = getMoveFromPlayer(possibleMoves);
                    }
                    else
                    {
                        nextMove = GameAI.FindBestMove(possibleMoves, m_CurrentPlayer.Piece, m_Board, 6);
                    }

                    if (nextMove == null)
                    {
                        isSignledQuit = true;
                        break;
                    }

                    GameLogic.ExcecuteMove(m_Board, nextMove, m_CurrentPlayer.Piece);
                    switchTurn();
                }

                if (!isSignledQuit)
                {
                    BoardDrawer.DrawBoard(m_Board);
                    printGameOverMsg();
                    isSignledQuit = !getAnotherGame();
                }
            }

            printGoodbyeMsg();
            Console.ReadKey();
        }

        private void printGoodbyeMsg()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("Goodbye {0}, We hope you enjoyed our game!", m_WhitePlayer.Name);
        }

        private bool getAnotherGame()
        {
            Console.WriteLine("Do you want to play another game?{0}Y - for yes{0}N - for no", Environment.NewLine);

            return getYesNoAnswer();
        }

        private Move getMoveFromPlayer(List<Move> i_PossibleMoves)
        {
            bool isLegalInput = false;
            int x;
            int y;
            Move move = null;

            while (!isLegalInput)
            {
                string moveStr = Console.ReadLine();
                if (string.IsNullOrEmpty(moveStr))
                {
                    Console.WriteLine("Please don't enter an empty string. Try again.");
                }
                else if (moveStr.ToUpper() == "Q")
                {
                    isLegalInput = true;
                }
                else if (!m_Board.TryParseStringIndex(moveStr, out x, out y))
                {
                    Console.WriteLine("The given input is invalid. Please use board index only. Try again.");
                }
                else if (!isContainedInPossibleMoves(x, y, i_PossibleMoves, out move))
                {
                    Console.WriteLine("The given move ({0}) is not a legal move. Try again.", moveStr);
                }
                else
                {
                    isLegalInput = true;
                }
            }

            return move;
        }

        private bool isContainedInPossibleMoves(int i_X, int i_Y, List<Move> i_PossibleMoves, out Move o_Move)
        {
            o_Move = null;

            foreach (Move move in i_PossibleMoves)
            {
                if (move.X == i_X && move.Y == i_Y)
                {
                    o_Move = move;
                    break;
                }
            }

            return o_Move != null;
        }

        private void printGameOverMsg()
        {
            // WYSIWYG
            Console.WriteLine(
@"The Game has ended!
{0} you have {1} points.
{2} you have {3} points.
{4}", 
    m_WhitePlayer.Name, 
    m_Board.WhitePoints, 
    m_BlackPlayer.Name, 
    m_Board.BlackPoints, 
    getWinnerMsg());
        }

        private string getWinnerMsg()
        {
            string winnerMsg = null;
            int diffPoints = m_Board.WhitePoints - m_Board.BlackPoints;

            if (diffPoints == 0)
            {
                winnerMsg = "It's a tie! Amazing game!";
            }
            else
            {
                winnerMsg = string.Format("{0} you have won! Well done!", (diffPoints < 0) ? m_BlackPlayer.Name : m_WhitePlayer.Name);
            }

            return winnerMsg;
        }

        private void printNoMovesMsg()
        {
            Console.WriteLine(
                "Sorry {0}, you have no moves to make. Your turn is skipped.{1}Press Enter to continue.",
                m_CurrentPlayer.Name, 
                Environment.NewLine);
        }

        private void printNextMoveMsg()
        {
            Console.Write("{0}, you play as {1}, please enter your move (such as E3): ", m_CurrentPlayer.Name, m_CurrentPlayer.Piece == ePiece.Black ? 'X' : 'O');
        }

        private void printWelcomeMsg()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("Welcome to Othello Game!");
        }

        private void switchTurn()
        {
            if (m_CurrentPlayer == m_WhitePlayer)
            {
                m_CurrentPlayer = m_BlackPlayer;
            }
            else
            {
                m_CurrentPlayer = m_WhitePlayer;
            }
        }

        private void initGame(string i_FirstPlayerName, string i_SecondPlayerName, bool i_IsTwoPlayers, int i_BoardSize)
        {
            m_Board = new Board(i_BoardSize);
            m_WhitePlayer = new Player(m_Board, i_FirstPlayerName, ePiece.White);
            m_BlackPlayer = new Player(m_Board, i_SecondPlayerName, ePiece.Black, i_IsTwoPlayers);
            m_CurrentPlayer = m_WhitePlayer;
        }

        private int getBoardSize()
        {
            int size;

            Ex02.ConsoleUtils.Screen.Clear();
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
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("Is there another player?{0}Y - for yes{0}N - for no", Environment.NewLine);
            
            return getYesNoAnswer();
        }

        private string getPlayerName(int i_PlayerNum)
        {
            Console.Write("Please enter {0} player name: ", MyStringUtils.AddOrdinal(i_PlayerNum));
            string name = Console.ReadLine();
            while (string.IsNullOrEmpty(name) || name.Trim().Length == 0)
            {
                Console.WriteLine("Please don't enter an empty string. Try again.");
                name = Console.ReadLine();
            }

            return name;
        }

        private bool getYesNoAnswer()
        {
            string yesOrNoStr = Console.ReadLine();

            while (string.IsNullOrEmpty(yesOrNoStr) || (yesOrNoStr.ToUpper() != "Y" && yesOrNoStr.ToUpper() != "N"))
            {
                Console.WriteLine("Please enter Y or N. Try again.");
                yesOrNoStr = Console.ReadLine();
            }

            return yesOrNoStr.ToUpper() == "Y";
        }
    }
}
