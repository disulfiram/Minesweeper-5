namespace Minesweeper
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Game
    {
        private const int MaxRows = 5;
        private const int MaxColumns = 10;
        private const int MaxMines = 15;
        private const int MaxTopPlayers = 5;

        private static Board board;
        private static List<Player> topPlayers;

        public static void Main(string[] args)
        {
            Menu();
        }

        private static void InitializeGameBoard()
        {
            board = new Board(MaxRows, MaxColumns, MaxMines);
        }

        private static void InitializeTopPlayers()
        {
            topPlayers = new List<Player>();
            topPlayers.Capacity = MaxTopPlayers;
        }

        private static bool IsQualifiedForScoreBoard(int score)
        {
            if (topPlayers.Count < topPlayers.Capacity)
            {
                return true;
            }

            foreach (Player currentPlayer in topPlayers)
            {
                if (currentPlayer.Score < score)
                {
                    return true;
                }
            }

            return false;
        }

        private static void TopAdd(ref Player player)
        {
            if (topPlayers.Capacity > topPlayers.Count)
            {
                topPlayers.Add(player);
                topPlayers.Sort();
            }
            else
            {
                topPlayers.RemoveAt(topPlayers.Capacity - 1);
                topPlayers.Add(player);
                topPlayers.Sort();
            }
        }

        private static void Top()
        {
            Console.WriteLine("Scoreboard");
            for (int i = 0; i < topPlayers.Count; i++)
            {
                Console.WriteLine(i + 1 + ". " + topPlayers[i]);
            }
        }

        private static void Menu()
        {
            InitializeTopPlayers();
            string str = "restart";
            int chosenRow = 0;
            int chosenColumn = 0;

            while (str != "exit")
            {
                if (str == "restart")
                {
                    InitializeGameBoard();
                    Console.WriteLine("Welcome to the game “Minesweeper”. " +
                                      "Try to reveal all cells without mines. " +
                                      "Use 'top' to view the scoreboard, 'restart' to start a new game" +
                                      "and 'exit' to quit the game.");
                    board.PrintGameBoard();
                }
                else if (str == "exit")
                {
                    Console.WriteLine("Good bye!");
                    Console.Read();
                }
                else if (str == "top")
                {
                    Top();
                }
                else if (str == "coordinates")
                {
                    try
                    {
                        Board.Status status = board.OpenField(chosenRow, chosenColumn);
                        if (status == Board.Status.SteppedOnAMine)
                        {
                            board.PrintAllFields();
                            int score = board.OpenedFieldsCount;
                            Console.WriteLine("Booooom! You were killed by a mine. You revealed " +
                                              score +
                                              " cells without mines.");

                            if (IsQualifiedForScoreBoard(score))
                            {
                                Console.WriteLine("Please enter your name for the top scoreboard: ");
                                string name = Console.ReadLine();
                                Player player = new Player(name, score);
                                TopAdd(ref player);
                                Top();
                            }

                            str = "restart";
                            continue;
                        }
                        else if (status == Board.Status.AlreadyOpened)
                        {
                            Console.WriteLine("Illegal move!");
                        }
                        else if (status == Board.Status.AllFieldsAreOpened)
                        {
                            board.PrintAllFields();
                            int score = board.OpenedFieldsCount;
                            Console.WriteLine("Congratulations! You win!!");
                            if (IsQualifiedForScoreBoard(score))
                            {
                                Console.WriteLine("Please enter your name for the top scoreboard: ");
                                string name = Console.ReadLine();
                                Player player = new Player(name, score);
                                TopAdd(ref player);
                                Top();
                                TopAdd(ref player);
                                Top();
                            }

                            str = "restart";
                            continue;
                        }
                        else
                        {
                            board.PrintGameBoard();
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("Illegal move");
                    }
                }

                Console.Write(System.Environment.NewLine + "Enter row and column: ");
                try
                {
                    str = Console.ReadLine();
                    chosenRow = int.Parse(str);
                    str = Console.ReadLine();
                    chosenColumn = int.Parse(str);

                    str = "coordinates";
                }
                catch (FormatException)
                {
                    continue;
                }
            }
        }
    }
}
