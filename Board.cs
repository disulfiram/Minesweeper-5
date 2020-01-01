namespace Minesweeper
{
    using System;
    using System.Text;

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
            AllFieldsAreOpened,

            /// <summary>
            /// Move coordinates are out of range
            /// </summary>
            OutOfRange
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
            Status status;
            if (row < 0 || row >= this.rows || column < 0 || column >= this.columns)
            {
                status = Board.Status.OutOfRange;
                return status;
            }

            FieldCell cell = this.field[row][column];
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

                if (cell.Value == 0)
                {
                    OpenSurroundingCells(row, column);
                }
            }

            return status;
        }

        /// <summary>
        /// Opens all adjacent cells.
        /// </summary>
        /// <param name="row">Row number of original cell</param>
        /// <param name="column">Column number of original cell</param>
        private void OpenSurroundingCells(int row, int column)
        {
            for (int adjacentRow = row - 1; adjacentRow <= row + 1; adjacentRow++)
            {
                if (adjacentRow < 0 || adjacentRow >= this.rows)
                {
                    continue;
                }
                else
                {
                    for (int adjacentColumn = column - 1; adjacentColumn <= column + 1; adjacentColumn++)
                    {
                        if (adjacentColumn < 0 || adjacentColumn >= this.columns || (adjacentColumn == column && adjacentRow == row))
                        {
                            continue;
                        }
                        else
                        {
                            OpenCell(adjacentRow, adjacentColumn);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Prints the gameboard
        /// </summary>
        public string GetGameBoardRepresentation()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("    ");
            for (int currentColumn = 0; currentColumn < this.columns; currentColumn++)
            {
                sb.Append(currentColumn + " ");
            }

            sb.AppendLine();
            sb.Append("   _");
            for (int i = 0; i < this.columns; i++)
            {
                sb.Append("__");
            }

            sb.AppendLine();
            for (int currentRow = 0; currentRow < this.rows; currentRow++)
            {
                sb.Append(currentRow);
                sb.Append(" | ");
                for (int currentColumn = 0; currentColumn < this.columns; currentColumn++)
                {
                    FieldCell currentCell = this.field[currentRow][currentColumn];
                    if (currentCell.Status == FieldCell.CellStatus.Opened)
                    {
                        sb.Append(this.field[currentRow][currentColumn].Value);
                        sb.Append(" ");
                    }
                    else
                    {
                        sb.Append("? ");
                    }
                }

                sb.AppendLine("|");
            }

            sb.Append("   _");
            for (int currentColumn = 0; currentColumn < this.columns; currentColumn++)
            {
                sb.Append("__");
            }

            sb.AppendLine();
            return sb.ToString();
        }

        /// <summary>
        /// Prints the final state of the board in the end of the game.
        /// </summary>
        public string GetGameBoardRepresentationRevealed()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("    ");
            for (int i = 0; i < this.columns; i++)
            {
                sb.Append(i + " ");
            }

            sb.AppendLine();
            sb.Append("   _");

            for (int i = 0; i < this.columns; i++)
            {
                sb.Append("__");
            }

            sb.AppendLine();
            for (int i = 0; i < this.rows; i++)
            {
                sb.Append(i);
                sb.Append(" | ");
                for (int j = 0; j < this.columns; j++)
                {
                    FieldCell currentCell = this.field[i][j];
                    if (currentCell.Status == FieldCell.CellStatus.Opened)
                    {
                        sb.Append(this.field[i][j].Value + " ");
                    }
                    else if (currentCell.Status == FieldCell.CellStatus.IsAMine)
                    {
                        sb.Append("* ");
                    }
                    else
                    {
                        currentCell.Value = this.ScanSurroundingCells(i, j);
                        sb.Append(this.field[i][j].Value + " ");
                    }
                }

                sb.AppendLine("|");
            }

            sb.Append("   _");
            for (int i = 0; i < this.columns; i++)
            {
                sb.Append("__");
            }

            sb.AppendLine();
            return sb.ToString();
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