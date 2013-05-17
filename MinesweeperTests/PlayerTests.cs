using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;

namespace MinesweeperTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Player_NullException()
        {
            Player testPlayer = new Player(null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Player_OutOfRange()
        {
            Player testPlayer = new Player("Arthur Pendragon", -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Name_NullException()
        {
            Player testPlayer = new Player("Arthur Pendragon", 0);
            testPlayer.Name = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Score_OutOfRange()
        {
            Player testPlayer = new Player("Arthur Pendragon", 0);
            testPlayer.Score = -10;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CompareTo_FailCompare()
        {
            Player testPlayer = new Player("Arthur Pendragon", 0);
            testPlayer.CompareTo("string");
        }

        [TestMethod]
        public void CompareTo_ThisIsBigger()
        {
            Player firstPlayer = new Player("Arthur Pendragon", 10);
            Player secondPlayer = new Player("Lancelot du Lac", 5);

            int actual = firstPlayer.CompareTo(secondPlayer);
            int expected = -1;
            Assert.AreEqual(actual, expected);
        }


        [TestMethod]
        public void CompareTo_ThisIsSmaller()
        {
            Player firstPlayer = new Player("Arthur Pendragon", 10);
            Player secondPlayer = new Player("Lancelot du Lac", 5);

            int actual = secondPlayer.CompareTo(firstPlayer);
            int expected = 1;
            Assert.AreEqual(actual, expected);
        }


        [TestMethod]
        public void CompareTo_Equal()
        {
            Player firstPlayer = new Player("Arthur Pendragon", 10);
            Player secondPlayer = new Player("Lancelot du Lac", 10);

            int actual = firstPlayer.CompareTo(secondPlayer);
            int expected = 0;
            Assert.AreEqual(actual, expected);
        }
        
        [TestMethod]
        public void ToString_Correct()
        {
            Player testPlayer = new Player("Arthur Pendragon", 10);
            string actual = testPlayer.ToString();
            string expected = "Arthur Pendragon --> 10";
            Assert.AreEqual(actual, expected);
        }
    }
}