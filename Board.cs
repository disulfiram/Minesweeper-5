namespace Minesweeper
{
    using System;

    public class Board
    {
        private int rows;
        private int columns;
        private int minesCount;
        private FieldCell[][] field;
        private Random random;
        private int openedFields;

        /// <summary>
        /// Initializes an insance of the Board class.
        /// </summary>
        /// <param name="rows">Number of rows in the board.</param>
        /// <param name="columns">Number of columns in the board.</param>
        /// <param name="minesCount">Number of mines in the board.</param>
        public Board(int rows, int columns, int minesCount)
        {
            this.random = new Random();
            this.rows = rows;
            this.columns = columns;
            this.minesCount = minesCount;
            this.openedFields = 0;
            this.field = new FieldCell[rows][];
            for (int i = 0; i < this.field.Length; i++)
            {
                this.field[i] = new FieldCell[columns];
                for (int j = 0; j < this.field[i].Length; j++)
                {
                    this.field[i][j] = new FieldCell();
                }
            }

            this.SetMines();
        }

        /// <summary>
        /// Indicates outcome of turn.
        /// </summary>
        public enum Status
        {
            /// <summary>
            /// Mine being stepped on.
            /// </summary>
            SteppedOnAMine,

            /// <summary>
            /// Field cell that is opened.
            /// </summary>
            AlreadyOpened,

            /// <summary>
            /// Empty field cell.
            /// </summary>
            SuccessfullyOpened,

            /// <summary>
            /// All fields are opened. Win state.
            /// </summary>
            AllFieldsAreOpened
        }

        /// <summary>
        /// Gets the number of opened cells.
        /// </summary>
        /// <value>The number of cells.</value>
        public int OpenedCellsCount
        {
            get
            {
                return this.openedFields;
            }
        }

        /// <summary>
        /// Performs an open cell action.
        /// </summary>
        /// <param name="row">Row of cell to be opened.</param>
        /// <param name="column">Columnt of cell to be opened.</param>
        /// <returns>Game status after turn.</returns>
        public Status OpenCell(int row, int column)
        {
            FieldCell cell = this.field[row][column];
            Status status;

            if (cell.Status == FieldCell.CellStatus.IsAMine)
            {
                status = Status.SteppedOnAMine;
            }
            else if (cell.Status == FieldCell.CellStatus.Opened)
            {
                status = Status.AlreadyOpened;
            }
            else
            {
                cell.Value = this.ScanSurroundingCells(row, column);
                cell.Status = FieldCell.CellStatus.Opened;
                this.openedFields++;
                if (this.AllCellsAreOpened())
                {
                    status = Status.AllFieldsAreOpened;
                }
                else
                {
                    status = Status.SuccessfullyOpened;
                }
            }

            return status;
        }

        /// <summary>
        /// Prints the gameboard
        /// </summary>
        public void PrintGameBoard()
        {
            Console.Write("    ");
            for (int currentColumn = 0; currentColumn < this.columns; currentColumn++)
            {
                Console.Write(currentColumn + " ");
            }

            Console.WriteLine();
            Console.Write("   _");
            for (int i = 0; i < this.columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();
            for (int currentRow = 0; currentRow < this.rows; currentRow++)
            {
                Console.Write(currentRow);
                Console.Write(" | ");
                for (int currentColumn = 0; currentColumn < this.columns; currentColumn++)
                {
                    FieldCell currentCell = this.field[currentRow][currentColumn];
                    if (currentCell.Status == FieldCell.CellStatus.Opened)
                    {
                        Console.Write(this.field[currentRow][currentColumn].Value);
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("? ");
                    }
                }

                Console.WriteLine("|");
            }

            Console.Write("   _");
            for (int currentColumn = 0; currentColumn < this.columns; currentColumn++)
            {
                Console.Write("__");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Prints the final state of the board in the end of the game.
        /// </summary>
        public void PrintGameBoardRevealed()
        {
            Console.Write("    ");
            for (int i = 0; i < this.columns; i++)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
            Console.Write("   _");

            for (int i = 0; i < this.columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();
            for (int i = 0; i < this.rows; i++)
            {
                Console.Write(i);
                Console.Write(" | ");
                for (int j = 0; j < this.columns; j++)
                {
                    FieldCell currentCell = this.field[i][j];
                    if (currentCell.Status == FieldCell.CellStatus.Opened)
                    {
                        Console.Write(this.field[i][j].Value + " ");
                    }
                    else if (currentCell.Status == FieldCell.CellStatus.IsAMine)
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        currentCell.Value = this.ScanSurroundingCells(i, j);
                        Console.Write(this.field[i][j].Value + " ");
                    }
                }

                Console.WriteLine("|");
            }

            Console.Write("   _");
            for (int i = 0; i < this.columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Scans cell adjacent to given cell.
        /// </summary>
        /// <param name="row">Row of the cell.</param>
        /// <param name="column">Column of the cell.</param>
        /// <returns>The number of mines adjacent to the given cell.</returns>
        public int ScanSurroundingCells(int row, int column)
        {
            int minesCount = 0;
            if ((row > 0) &&
                (column > 0) &&
                (this.field[row - 1][column - 1].Status == FieldCell.CellStatus.IsAMine))
            {
                minesCount++;
            }

            if ((row > 0) &&
                (this.field[row - 1][column].Status == FieldCell.CellStatus.IsAMine))
            {
                minesCount++;
            }

            if ((row > 0) &&
                (column < this.columns - 1) &&
                (this.field[row - 1][column + 1].Status == FieldCell.CellStatus.IsAMine))
            {
                minesCount++;
            }

            if ((column > 0) &&
                (this.field[row][column - 1].Status == FieldCell.CellStatus.IsAMine))
            {
                minesCount++;
            }

            if ((column < this.columns - 1) &&
                (this.field[row][column + 1].Status == FieldCell.CellStatus.IsAMine))
            {
                minesCount++;
            }

            if ((row < this.rows - 1) &&
                (column > 0) &&
                (this.field[row + 1][column - 1].Status == FieldCell.CellStatus.IsAMine))
            {
                minesCount++;
            }

            if ((row < this.rows - 1) &&
                (this.field[row + 1][column].Status == FieldCell.CellStatus.IsAMine))
            {
                minesCount++;
            }

            if ((row < this.rows - 1) &&
                (column < this.columns - 1) &&
                (this.field[row + 1][column + 1].Status == FieldCell.CellStatus.IsAMine))
            {
                minesCount++;
            }

            return minesCount;
        }

        /// <summary>
        /// Places mines in the field.
        /// </summary>
        private void SetMines()
        {
            for (int i = 0; i < this.minesCount; i++)
            {
                int row = this.random.Next(0, this.rows);
                int column = this.random.Next(0, this.columns);

                // if we're trying to set the mine to a cell which already has a mine
                // we try again by generating new coordinates for the mine
                while (this.field[row][column].Status == FieldCell.CellStatus.IsAMine)
                {
                    row = this.random.Next(0, this.rows);
                    column = this.random.Next(0, this.columns);
                }

                this.field[row][column].Status = FieldCell.CellStatus.IsAMine;
            }
        }

        /// <summary>
        /// Checks if all fields are opened.
        /// </summary>
        /// <returns>Boolean expresion.</returns>
        public bool AllCellsAreOpened()
        {
            if ((this.openedFields + this.minesCount) == (this.rows * this.columns))
            {
                return true;
            }

            return false;
        }
    }
}