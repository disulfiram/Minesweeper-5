namespace Minesweeper
{
    using System;

    /// <summary>
    /// Represents a single square on the board.
    /// </summary>
    public class Field
    {
        /// <summary>
        /// Initializes a new instance of the Field class.
        /// </summary>
        public Field()
        {
            this.value = 0;
            this.status = FieldStatus.Closed;
        }

        /// <summary>
        /// Represents the number of adjacent mines.
        /// </summary>
        private int value;

        /// <summary>
        /// Status of the field.
        /// </summary>
        private FieldStatus status;

        /// <summary>
        /// Possible statuses of a field.
        /// </summary>
        public enum FieldStatus
        {
            Closed,
            Opened,
            IsAMine
        }

        /// <summary>
        /// Gets or sets the number of adjacent mines.
        /// </summary>
        public int Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Gets or sets status of the field.
        /// </summary>
        public FieldStatus Status
        {
            get
            {
                return this.status;
            }

            set
            {
                this.status = value;
            }
        }
    }
}