namespace Minesweeper
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents a single square on the board.
    /// </summary>
    public class Field
    {
        /// <summary>
        /// Initializes a new instance of the Field class.
        /// </summary>
        public class FieldCell
        {
            /// <summary>
            /// Represents the number of adjacent mines.
            /// </summary>
            private int value;

            /// <summary>
            /// Status of the field
            /// </summary>
            private FieldStatus status;

            public FieldCell()
            {
                this.value = 0;
                this.status = FieldStatus.Closed;
            }

            /// <summary>
            /// Possible statuses of a field.
            /// </summary>
            public enum FieldStatus
            {
                // TO DO
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
}
