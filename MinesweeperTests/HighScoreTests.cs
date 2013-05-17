using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;

namespace MinesweeperTests
{
    [TestClass]
    public class HighScoreTests
    {
        [TestMethod]
        public void IsQualifiedForSB_True()
        {
            HighScore testHighScore = new HighScore();
            bool actual = testHighScore.IsQualifiedForScoreBoard(0);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsQualifiedForSB_False()
        {
            HighScore testHighScore = new HighScore();
            testHighScore.AddPlayerToScoreBoard(new Player("P1", 1));
            testHighScore.AddPlayerToScoreBoard(new Player("P2", 2));
            testHighScore.AddPlayerToScoreBoard(new Player("P2", 3));
            testHighScore.AddPlayerToScoreBoard(new Player("P2", 4));
            testHighScore.AddPlayerToScoreBoard(new Player("P2", 5));
            bool actual = testHighScore.IsQualifiedForScoreBoard(0);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsQualifiedForSB_FalseMaybe()
        {
            HighScore testHighScore = new HighScore();
            testHighScore.AddPlayerToScoreBoard(new Player("P1", 1));
            testHighScore.AddPlayerToScoreBoard(new Player("P2", 2));
            testHighScore.AddPlayerToScoreBoard(new Player("P2", 3));
            testHighScore.AddPlayerToScoreBoard(new Player("P2", 4));
            testHighScore.AddPlayerToScoreBoard(new Player("P2", 5));
            bool actual = testHighScore.IsQualifiedForScoreBoard(1);
            Assert.IsFalse(actual);
        }
    }
}