using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using ClassLibraryLab10;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestClass]
        public class CardTests
        {
            [TestMethod]
            public void TestCardConstructor()
            {
                // Arrange
                string id = "1234 5678 9012 3456";
                string name = "John Doe";
                string time = "12/25";
                int number = 123;

                // Act
                Card card = new Card(id, name, time, number);

                // Assert
                Assert.AreEqual(id, card.Id);
                Assert.AreEqual(name, card.Name);
                Assert.AreEqual(time, card.Time);
                Assert.AreEqual(number, card.num.number);
            }

            [TestMethod]
            [ExpectedException(typeof(Exception))]
            public void TestCardIdInvalidFormat()
            {
                // Arrange
                string id = "1234 5678";

                // Act
                Card card = new Card();
                card.Id = id;
            }

            [TestMethod]
            [ExpectedException(typeof(Exception))]
            public void TestCardNameInvalidLength()
            {
                // Arrange
                string name = "A";

                // Act
                Card card = new Card();
                card.Name = name;
            }

            [TestMethod]
            [ExpectedException(typeof(Exception))]
            public void TestCardTimeInvalidFormat()
            {
                // Arrange
                string time = "20/30";

                // Act
                Card card = new Card();
                card.Time = time;
            }

            [TestMethod]
            public void TestGenerateRandomId()
            {
                // Arrange
                Card card = new Card();

                // Act
                string randomId = card.GenerateRandomId();

                // Assert
                Assert.IsNotNull(randomId);
                Assert.IsTrue(randomId.Split(' ').Length == 4);
                Assert.IsTrue(randomId.Replace(" ", "").All(char.IsDigit));
            }

            [TestMethod]
            public void TestGenerateRandomName()
            {
                // Arrange
                Card card = new Card();

                // Act
                string randomName = card.GenerateRandomName();

                // Assert
                Assert.IsNotNull(randomName);
                Assert.IsTrue(randomName.Length >= 3 && randomName.Length <= 30);
            }

            [TestMethod]
            public void TestGenerateRandomTime()
            {
                // Arrange
                Card card = new Card();

                // Act
                string randomTime = card.GenerateRandomTime();

                // Assert
                Assert.IsNotNull(randomTime);
                Assert.IsTrue(randomTime.Length == 5);
                Assert.IsTrue(randomTime.Substring(0, 2).All(char.IsDigit));
                Assert.IsTrue(randomTime.Substring(3, 2).All(char.IsDigit));
            }
        }
    }
}
