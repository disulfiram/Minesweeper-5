namespace Minesweeper
{
    using System;

    /// <summary>
    /// Represents a single square on the board.
    /// </summary>
    public class FieldCell
    {
        /// <summary>
        /// Represents the number of adjacent mines.
        /// </summary>
        private int value;

        /// <summary>
        /// Status of the field.
        /// </summary>
        private CellStatus status;

        public FieldCell()
        {
            this.value = 0;
            this.status = CellStatus.Closed;
        }

        /// <summary>
        /// Possible statuses of a field.
        /// </summary>
        public enum CellStatus
        {
            /// <summary>
            /// The field is closed.
            /// </summary>
            Closed,

            /// <summary>
            /// The field is opened.
            /// </summary>
            Opened,

            /// <summary>
            /// The field is a mine.
            /// </summary>
            IsAMine
        }

        /// <summary>
        /// Gets or sets the number of adjacent mines.
        /// </summary>
        /// <value>The number of adjacent mines.</value>
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
        /// <value>The status of the cell.</value>
        public CellStatus Status
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