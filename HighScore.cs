namespace Minesweeper
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    public class HighScore
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

        public List<Player> TopPlayers
        {
            get
            {
                return this.topPlayers;
            }
        }

        /// <summary>
        /// Checks if player score is high enough to be entered in the board.
        /// </summary>
        /// <param name="score">Score of player.</param>
        /// <returns>True if player has high enough score. False if player does not have high enough score.</returns>
        public bool IsQualifiedForScoreBoard(int score)
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
        public void AddPlayerToScoreBoard(Player player)
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
        public string ListTopPlayers()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Scoreboard");
            for (int i = 0; i < topPlayers.Count; i++)
            {
                sb.AppendLine(i + 1 + ". " + topPlayers[i]);
            }

            return sb.ToString();
        }
    }
}