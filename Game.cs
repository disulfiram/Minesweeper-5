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

        private static void ListTopPlayers()
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
            InitializeGameBoard();

            string input = string.Empty;
            int chosenRow, chosenColumn;
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to the game “Minesweeper”. " +
                                          "Try to reveal all cells without mines. " +
                                          "Use 'top' to view the scoreboard, 'restart' to start a new game " +
                                          "and 'exit' to quit the game.");
                board.PrintGameBoard();
                Console.Write(System.Environment.NewLine + "Enter row and column: ");
                
                input = Console.ReadLine();
                bool rowParseSuccess = int.TryParse(input, out chosenRow);
                if (!rowParseSuccess)
                {
                    ProcessCommands(input);
                    continue;
                }

                input = Console.ReadLine();
                bool colParseSuccess = int.TryParse(input, out chosenColumn);
                if (!colParseSuccess)
                {
                    ProcessCommands(input);
                    continue;
                }

                PlayMove(chosenRow, chosenColumn);
            }
            while (input != "exit");
        }

        private static void ProcessCommands(string input)
        {
            if (input == "restart")
            {
                InitializeGameBoard();
            }
            else if (input == "exit")
            {
                Console.WriteLine("Good bye!");
                Console.Read();
            }
            else if (input == "top")
            {
                ListTopPlayers();
            }
        }

        private static void PlayMove(int row, int col)
        {
            if (row < 0 || row >= MaxRows
                || col < 0 || col >= MaxColumns)
            {
                Console.WriteLine("Illegal move.");
                Console.ReadKey();
                return;
            }

            Board.Status status = board.OpenField(row, col);
            if (status == Board.Status.SteppedOnAMine || status == Board.Status.AllFieldsAreOpened)
            {
                board.PrintAllFields();
                int score = board.OpenedFieldsCount;
                if (status == Board.Status.SteppedOnAMine)
                {
                    Console.WriteLine("Booooom! You were killed by a mine. You revealed " +
                                        score +
                                        " cells without mines.");
                }
                else
                {
                    Console.WriteLine("Congratulations! You win!!");
                }

                if (IsQualifiedForScoreBoard(score))
                {
                    SaveHighScore(score);
                }

                InitializeGameBoard();
            }
            else if (status == Board.Status.AlreadyOpened)
            {
                Console.WriteLine("Illegal move!");
            }
        }

        private static void SaveHighScore(int score)
        {
            Console.WriteLine("Please enter your name for the top scoreboard: ");
            string name = Console.ReadLine();
            Player player = new Player(name, score);
            TopAdd(ref player);
            ListTopPlayers();
        }
    }
}