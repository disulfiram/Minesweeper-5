using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;

namespace MinesweeperTests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            int rows = 15, cols = 15, minesCount = 15;
            Board board = new Board(cols, rows, minesCount);
            Assert.AreEqual(0, board.OpenedCellsCount);
            Assert.IsFalse(board.AllCellsAreOpened());
        }

        [TestMethod]
        public void TestBoardNoMines()
        {
            int rows = 15, cols = 15, minesCount = 0;
            int fieldsCount = rows * cols;
            Board board = new Board(rows, cols, minesCount);
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    board.OpenCell(row, col);
                }
            }
            Assert.IsTrue(board.AllCellsAreOpened());
        }

        [TestMethod]
        public void TestBoardMaxMines()
        {
            int rows = 15, cols = 15;
            int fieldsCount = rows * cols, minesCount = fieldsCount;

            Board board = new Board(rows, cols, minesCount);
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Board.Status actual = board.OpenCell(row, col);
                    Assert.AreEqual(Board.Status.SteppedOnAMine, board.OpenCell(row, col));
                }
            }
        }

        [TestMethod]
        public void TestOpenCellAlreadyOpened()
        {
            int rows = 15, cols = 15;
            int fieldsCount = rows * cols, minesCount = 0;

            Board board = new Board(rows, cols, minesCount);
            Board.Status exprected = Board.Status.AlreadyOpened;
            board.OpenCell(0, 0);
            Board.Status actual = board.OpenCell(0, 0);
            Assert.AreEqual(exprected, actual);
        }

        [TestMethod]
        public void TestOpenCell()
        {
            int rows = 15, cols = 15;
            int fieldsCount = rows * cols, minesCount = 0;

            Board board = new Board(rows, cols, minesCount);
            Board.Status actual;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    actual = board.OpenCell(row, col);
                    Assert.AreEqual(Board.Status.SuccessfullyOpened, actual);
                    if (row == rows - 1 && col == cols - 2)
                    {
                        break;
                    }
                }
            }
            actual = board.OpenCell(rows - 1, cols - 1);
            Assert.AreEqual(Board.Status.AllFieldsAreOpened, actual);
        }

        [TestMethod]
        public void TestMinesScanFullBoard()
        {
            int rows = 15, cols = 15;
            int fieldsCount = rows * cols, minesCount = fieldsCount;

            Board board = new Board(rows, cols, minesCount);
            Assert.AreEqual(3, board.ScanSurroundingCells(0, 0));
            Assert.AreEqual(8, board.ScanSurroundingCells(5, 5));
        }


    }
}
