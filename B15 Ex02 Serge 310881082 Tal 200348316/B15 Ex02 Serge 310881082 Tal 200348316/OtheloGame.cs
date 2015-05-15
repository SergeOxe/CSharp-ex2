

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
                List<Move> possibleMoves = getPossibleMoves(m_CurrentPlayer.Piece);
                if (possibleMoves.Count == 0)
                {
                    printNoMovesMsg();
                    switchTurn();
                    continue;
                }
                printNextMoveMsg();
                Move nextMove = m_CurrentPlayer.GetMove(possibleMoves);
                excecuteMove(nextMove);
                switchTurn();
            }

            BoardUI.DrawBoard(m_Board);
            printGameOverMsg();
            //bool anotherGame = getAnotherGame();
            // TODO: Refactor this function so if anotherGame == true we start again inside while loop

            Console.ReadKey();
        }

        private void printGameOverMsg()
        {
            Console.WriteLine("Game has ended with the score of {0} - {1}", m_Board.WhitePoints, m_Board.BlackPoints);
        }

        private void printNoMovesMsg()
        {
            Console.WriteLine("Sorry {0}, you have no moves to make. Your turn is skipped.", m_CurrentPlayer.Name);
        }

        private void printNextMoveMsg()
        {
            Console.Write("{0}, you play as {1}, please enter your move (such as E3): ", m_CurrentPlayer.Name, m_CurrentPlayer.Piece == ePiece.Black? 'X': 'O');
        }

        private void printWelcomeMsg()
        {
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
            m_BlackPlayer = new Player(m_Board, i_SecondPlayerName, ePiece.Black, !i_IsTwoPlayers);
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
            Console.WriteLine("Is there another player?{0}Y - for yes{0}N - for no", Environment.NewLine);
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
