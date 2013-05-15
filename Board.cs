namespace Minesweeper
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Board
    {
        private int rows;
        private int columns;
        private int minesCount;
        private FieldCell[][] field;
        private Random random;
        private int openedFields;

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
        /// Indicates the current state of the cell
        /// </summary>
        public enum Status
        {
            SteppedOnAMine, AlreadyOpened, SuccessfullyOpened, AllFieldsAreOpened
        }

        public int OpenedFieldsCount
        {
            get
            {
                return this.openedFields;
            }
        }

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

        public Status OpenField(int row, int column)
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
                cell.Value = this.ScanSurroundingFields(row, column);
                cell.Status = FieldCell.CellStatus.Opened;
                this.openedFields++;
                if (this.CheckIfWin())
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

        public void PrintAllFields()
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
                        currentCell.Value = this.ScanSurroundingFields(i, j);
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

        private int ScanSurroundingFields(int row, int column)
        {
            int mines = 0;
            if ((row > 0) &&
                (column > 0) &&
                (this.field[row - 1][column - 1].Status == FieldCell.CellStatus.IsAMine))
            {
                mines++;
            }

            if ((row > 0) &&
                (this.field[row - 1][column].Status == FieldCell.CellStatus.IsAMine))
            {
                mines++;
            }

            if ((row > 0) &&
                (column < this.columns - 1) &&
                (this.field[row - 1][column + 1].Status == FieldCell.CellStatus.IsAMine))
            {
                mines++;
            }

            if ((column > 0) &&
                (this.field[row][column - 1].Status == FieldCell.CellStatus.IsAMine))
            {
                mines++;
            }

            if ((column < this.columns - 1) &&
                (this.field[row][column + 1].Status == FieldCell.CellStatus.IsAMine))
            {
                mines++;
            }

            if ((row < this.rows - 1) &&
                (column > 0) &&
                (this.field[row + 1][column - 1].Status == FieldCell.CellStatus.IsAMine))
            {
                mines++;
            }

            if ((row < this.rows - 1) &&
                (this.field[row + 1][column].Status == FieldCell.CellStatus.IsAMine))
            {
                mines++;
            }

            if ((row < this.rows - 1) &&
                (column < this.columns - 1) &&
                (this.field[row + 1][column + 1].Status == FieldCell.CellStatus.IsAMine))
            {
                mines++;
            }

            return mines;
        }

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

        private bool CheckIfWin()
        {
            if ((this.openedFields + this.minesCount) == (this.rows * this.columns))
            {
                return true;
            }

            return false;
        }
    }
}
