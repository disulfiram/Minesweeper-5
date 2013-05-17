using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;
using System.IO;

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

        [TestMethod]
        public void TestListTop()
        {
            HighScore testHighScore = new HighScore();
            testHighScore.AddPlayerToScoreBoard(new Player("P1", 1));
            testHighScore.AddPlayerToScoreBoard(new Player("P2", 2));
            testHighScore.AddPlayerToScoreBoard(new Player("P2", 3));
            testHighScore.AddPlayerToScoreBoard(new Player("P2", 4));
            testHighScore.AddPlayerToScoreBoard(new Player("P2", 5));

            string expected = File.ReadAllText("scoreboardtest.txt");
            string actual = testHighScore.ListTopPlayers();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAddingPlayersOverTheLimit()
        {
            HighScore testHighScore = new HighScore();
            int playersCount = 15;
            for (int i = 0; i < playersCount; i++)
            {
                testHighScore.AddPlayerToScoreBoard(new Player("Test Player", 5));
            }

            Assert.AreEqual(5, testHighScore.TopPlayers.Count);
        }

        [TestMethod]
        public void TestAddingTopScorerAfterScoreBoardIsFull()
        {
            HighScore testHighScore = new HighScore();
            int playersCount = 15;
            for (int i = 0; i < playersCount; i++)
            {
                testHighScore.AddPlayerToScoreBoard(new Player("Test Player", 5));
            }
            testHighScore.AddPlayerToScoreBoard(new Player("Best", 10));
            Assert.AreEqual(10, testHighScore.TopPlayers[0].Score);
        }
    }
}