

using System;
using System.Collections.Generic;

namespace B15_Ex02_Serge_310881082_Tal_200348316
{

    enum ePiece : byte
    { None, Black, White };

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

                    BoardUI.DrawBoard(m_Board);
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
                        //nextMove = getMoveFromComputer(possibleMoves);
                    }

                    if (nextMove == null)
                    {
                        isSignledQuit = true;
                        break;
                    }
                    //Move nextMove = m_CurrentPlayer.GetMove(possibleMoves);
                    excecuteMove(nextMove);
                    switchTurn();
                }

                if (!isSignledQuit)
                {
                    BoardUI.DrawBoard(m_Board);
                    printGameOverMsg();
                    isSignledQuit = !getAnotherGame();
                }
            }

            printGoodbyeMsg();

            //while (!GameLogic.IsGameOver(m_Board))
            //{
            //    Move nextMove = null;

            //    BoardUI.DrawBoard(m_Board);
            //    List<Move> possibleMoves = GameLogic.GetPossibleMoves(m_Board, m_CurrentPlayer.Piece);
            //    if (possibleMoves.Count == 0)
            //    {
            //        printNoMovesMsg();
            //        switchTurn();
            //        Console.ReadLine();
            //        continue;
            //    }
            //    printNextMoveMsg();
            //    if (m_CurrentPlayer.IsHuman)
            //    {
            //        nextMove = getMoveFromPlayer(possibleMoves);
            //    }
            //    else
            //    {
            //        //nextMove = getMoveFromComputer(possibleMoves);
            //    }

            //    if (nextMove == null)
            //    {
            //        //printGoodbyeMsg();
            //        break;
            //    }
            //    //Move nextMove = m_CurrentPlayer.GetMove(possibleMoves);
            //    excecuteMove(nextMove);
            //    switchTurn();
            //}

            //BoardUI.DrawBoard(m_Board);
            //printGameOverMsg();
            //bool anotherGame = getAnotherGame();

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
                else if (!m_Board.TryParse(moveStr, out x, out y))
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
{4}", m_WhitePlayer.Name, m_Board.WhitePoints, m_BlackPlayer.Name, m_Board.BlackPoints, getWinnerMsg());

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
                winnerMsg = string.Format("{0} you have won! Well done!", (diffPoints < 0)? m_BlackPlayer.Name: m_WhitePlayer.Name);
            }

            return winnerMsg;
        }

        private void printNoMovesMsg()
        {
            Console.WriteLine("Sorry {0}, you have no moves to make. Your turn is skipped.{1}Press Enter to continue.",
                m_CurrentPlayer.Name, Environment.NewLine);
        }

        private void printNextMoveMsg()
        {
            Console.Write("{0}, you play as {1}, please enter your move (such as E3): ", m_CurrentPlayer.Name, m_CurrentPlayer.Piece == ePiece.Black? 'X': 'O');
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

        private void excecuteMove(Move i_NextMove)
        {
            m_Board[i_NextMove.X, i_NextMove.Y] =  m_CurrentPlayer.Piece;
            foreach (Direction direction in i_NextMove.Directions)
            {
                turnPieces(i_NextMove.X + direction.X, i_NextMove.Y + direction.Y, direction.X, direction.Y);
            }
        }

        private void turnPieces(int i_X, int i_Y, int i_XDirection, int i_YDirection)
        {
            while (m_Board[i_X, i_Y] != m_CurrentPlayer.Piece)
            {
                m_Board[i_X, i_Y] = m_CurrentPlayer.Piece;
                i_X += i_XDirection;
                i_Y += i_YDirection;
            }

        }

        private List<Move> getPossibleMoves(ePiece i_Piece)
        {
            List<Move> possibleMoves = new List<Move>();

            for (int x = 0; x < m_Board.Size; x++)
            {
                for (int y = 0; y < m_Board.Size; y++)
                {
                    Move move = getPossibleMoveAtXY(x, y, i_Piece);
                    if (move != null)
                    {
                        possibleMoves.Add(move);
                    }
                }
            }

            return possibleMoves;
        }

        private Move getPossibleMoveAtXY(int i_X, int i_Y, ePiece i_Piece)
        {
            Move move = null;
            List<Direction> directions = new List<Direction>();
            if (m_Board[i_X, i_Y] == ePiece.None)
            {
                for (int xDirection = -1; xDirection <= 1; xDirection++)
                {
                    for (int yDirection = -1; yDirection <= 1; yDirection++)
                    {
                        if (isOppositePiece(i_X + xDirection, i_Y + yDirection, i_Piece) &&
                            endWithMyPiece(i_X + xDirection, i_Y + yDirection, xDirection, yDirection, i_Piece))
                        {
                            directions.Add(new Direction(xDirection, yDirection));
                        }
                    }
                }
            }

            if (directions.Count > 0)
            {
                move = new Move(i_X, i_Y, directions);
            }

            return move;
        }

        private bool endWithMyPiece(int i_X, int i_Y, int i_XDirection, int i_YDirection, ePiece i_Piece)
        {
            bool hasEncounteredMyPiece = false;

            while (m_Board.IsInBounds(i_X, i_Y))
            {
                if (m_Board[i_X, i_Y] == ePiece.None)
                {
                    break;
                }
                if(m_Board[i_X, i_Y] == i_Piece)
                {
                    hasEncounteredMyPiece = true;
                    break;
                }

                i_X += i_XDirection;
                i_Y += i_YDirection;
            }

            return hasEncounteredMyPiece;
        }

        private bool isOppositePiece(int i_X, int i_Y, ePiece i_Piece)
        {
            bool isOpposite = true;

            if (!m_Board.IsInBounds(i_X, i_Y))
            {
                isOpposite = false;
            }
            else if (m_Board[i_X, i_Y] == ePiece.None)
            {
                isOpposite = false;
            }
            else if (m_Board[i_X, i_Y] == i_Piece)
            {
                isOpposite = false;
            }

            return isOpposite;
        }

        private bool isGameOver()
        {
            bool isGameOver = true;

            for (int x = 0; x < m_Board.Size; x++)
            {
                for (int y = 0; y < m_Board.Size; y++)
                {
                    if (m_Board[x, y] != ePiece.None)
                    {
                        continue;
                    }

                    if (getPossibleMoveAtXY(x, y, ePiece.Black) != null)
                    {
                        isGameOver = false;
                        break;
                    }
                    if (getPossibleMoveAtXY(x, y, ePiece.White) != null)
                    {
                        isGameOver = false;
                        break;
                    }
                }
            }

            return isGameOver;
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
