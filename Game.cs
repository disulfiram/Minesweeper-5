namespace Minesweeper
{
    using System;
    
    public class Game
    {
        /// <summary>
        /// Board number of rows.
        /// </summary>
        private const int MaxRows = 5;
        
        /// <summary>
        /// Board number of columns.
        /// </summary>
        private const int MaxColumns = 10;

        /// <summary>
        /// Amount of mines on the board.
        /// </summary>
        private const int MaxMines = 15;

        /// <summary>
        /// Maximum number of players in highscore board.
        /// </summary>
        private const int MaxTopPlayers = 5;

        /// <summary>
        /// Highscore board.
        /// </summary>
        private static HighScore highScore;

        /// <summary>
        /// Game board.
        /// </summary>
        private static Board board;

        public static void Main(string[] args)
        {
            Menu();
        }

        /// <summary>
        /// Initializes a game board.
        /// </summary>
        private static void InitializeGameBoard()
        {
            board = new Board(MaxRows, MaxColumns, MaxMines);
        }

        /// <summary>
        /// Initializes highscore board.
        /// </summary>
        private static void InitializeTopPlayers()
        {
            highScore = new HighScore();
        }

        /// <summary>
        /// Main method for the game.
        /// </summary>
        private static void Menu()
        {
            InitializeTopPlayers();
            InitializeGameBoard();

            string input = string.Empty;
            int chosenRow, chosenColumn;
            do
            {
                PrintHeader();
                Console.WriteLine(board.GetGameBoardRepresentation());
                Console.Write(System.Environment.NewLine + "Enter row: ");
                input = Console.ReadLine();
                bool rowParseSuccess = int.TryParse(input, out chosenRow);
                if (!rowParseSuccess)
                {
                    ProcessCommands(input);
                    continue;
                }

                Console.Write(System.Environment.NewLine + "Enter column: ");
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
            Console.WriteLine("Good bye!");
            Console.Read();
        }

        /// <summary>
        /// Prints the initial greating for the player.
        /// </summary>
        private static void PrintHeader()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the game “Minesweeper”. " +
                              "Try to reveal all cells without mines. " +
                              "Use 'top' to view the scoreboard, 'restart' to start a new game " +
                              "and 'exit' to quit the game.");
        }

        /// <summary>
        /// Processes input.
        /// </summary>
        /// <param name="input">Input from console.</param>
        private static void ProcessCommands(string input)
        {
            if (input == "restart")
            {
                InitializeGameBoard();
            }
            else if (input == "top")
            {
                highScore.ListTopPlayers();
            }
        }

        /// <summary>
        /// Performes operations concerning the turn of a player.
        /// </summary>
        /// <param name="row">Player chosen row.</param>
        /// <param name="col">Player chosen column.</param>
        private static void PlayMove(int row, int col)
        {
            if (row < 0 || row >= MaxRows ||
                col < 0 || col >= MaxColumns)
            {
                Console.WriteLine("Illegal move.");
                Console.ReadKey();
                return;
            }

            Board.Status status = board.OpenCell(row, col);
            if (status == Board.Status.SteppedOnAMine || status == Board.Status.AllFieldsAreOpened)
            {
                PrintHeader();
                Console.WriteLine(board.GetGameBoardRepresentationRevealed());
                int score = board.OpenedCellsCount;
                if (status == Board.Status.SteppedOnAMine)
                {
                    Console.WriteLine(Environment.NewLine + "Booooom! You were killed by a mine. You revealed " +
                                      score +
                                      " cells without mines.");
                }
                else
                {
                    Console.WriteLine("Congratulations! You win!!");
                }

                if (highScore.IsQualifiedForScoreBoard(score))
                {
                    SaveHighScore(score);
                }

                InitializeGameBoard();
            }
            else if (status == Board.Status.AlreadyOpened)
            {
                Console.WriteLine("Illegal move!");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Performs operations on highscore board after game is over.
        /// </summary>
        /// <param name="score">Score of player.</param>
        private static void SaveHighScore(int score)
        {
            Console.WriteLine("Please enter your name for the top scoreboard: ");
            string name = Console.ReadLine();
            Player player = new Player(name, score);
            highScore.AddPlayerToScoreBoard(player);
            highScore.ListTopPlayers();
        }
    }
}