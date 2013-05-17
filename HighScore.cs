namespace Minesweeper
{
    using System;
    using System.Collections.Generic;
    
    class HighScore
    {
        /// <summary>
        /// The number of highscores the list contains
        /// </summary>
        const int MaxPlayers = 5;

        /// <summary>
        /// List of players that stores the highscores
        /// </summary>
        private List<Player> topPlayers;

        /// <summary>
        /// Initializes an instance of the Highscore class.
        /// </summary>
        public HighScore()
        {
            this.topPlayers = new List<Player>(MaxPlayers);
        }

        /// <summary>
        /// Checks if player score is high enough to be entered in the board.
        /// </summary>
        /// <param name="score">Score of player.</param>
        /// <returns>True if player has high enough score. False if player does not have high enough score.</returns>
        internal bool IsQualifiedForScoreBoard(int score)
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

        /// <summary>
        /// Adds player to the scoreboard.
        /// </summary>
        /// <param name="player">Player that is being added to scoreboard.</param>
        internal void AddPlayerToScoreBoard(Player player)
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

        /// <summary>
        /// Prints highscores on the console.
        /// </summary>
        internal void ListTopPlayers()
        {
            Console.WriteLine("Scoreboard");
            for (int i = 0; i < topPlayers.Count; i++)
            {
                Console.WriteLine(i + 1 + ". " + topPlayers[i]);
            }

            Console.ReadKey();
        }
    }
}