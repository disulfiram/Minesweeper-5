namespace Minesweeper
{
    using System;

    /// <summary>
    /// Class for player.
    /// </summary>
    public class Player : IComparable
    {
        /// <summary>
        /// Name of player.
        /// </summary>
        private string name;

        /// <summary>
        /// Score of player.
        /// </summary>
        private int score;

        /// <summary>
        /// Initializes a new instance of the Player class.
        /// </summary>
        /// <param name="name">Name of player.</param>
        /// <param name="score">Score of player.</param>
        public Player(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }

        /// <summary>
        /// Gets or sets the name of the player.
        /// </summary>
        /// <value>The name of the player.</value>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("The name of the player cannot be null.");
                }

                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets the score of the player.
        /// </summary>
        /// <value>The score to be set.</value>
        public int Score
        {
            get
            {
                return this.score;
            }

            set
            {
                this.score = value;
            }
        }

        /// <summary>
        /// Compares two instances of the Player class by their scores.
        /// </summary>
        /// <param name="obj">The other Player.</param>
        /// <returns>1 if the this player has higher score than the second one. 
        /// -1 if this player has lower score than the second one.</returns>
        public int CompareTo(object obj)
        {
            if (!(obj is Player))
            {
                throw new ArgumentException("A Player object is required for comparison.");
            }

            return -1 * this.score.CompareTo(((Player)obj).score);
        }

        /// <summary>
        /// Creates a string of information about an instance of the class Player.
        /// </summary>
        /// <returns>String containing information about the instance of the class Player.</returns>
        public override string ToString()
        {
            string result = this.name + " --> " + this.score;
            return result;
        }
    }
}