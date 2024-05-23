using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryLab10;
using LabWork12;

namespace UnitTestList
{
    [TestClass]
    public class BiListTests
    {
        [TestClass]
        public class UnitTestBiList
        {
            [TestMethod]
            public void Constructor_WithoutParameters_CreatesEmptyList()
            {
                // Arrange & Act
                BiList<Card> list = new BiList<Card>();

                // Assert
                Assert.IsNull(list.beg);
                Assert.IsNull(list.end);
                Assert.AreEqual(0, list.Count);
            }

            [TestMethod]
            public void AddToEnd_AddsItemToEndOfList()
            {
                // Arrange
                BiList<Card> list = new BiList<Card>();
                Card card = new Card("1234 5678 9012 3456", "John Doe", "12/25", 123);

                // Act
                list.AddToEnd(card);

                // Assert
                Assert.AreEqual(card, list.end.Data);
                Assert.AreEqual(1, list.Count);
            }

            [TestMethod]
            public void Length_ReturnsCorrectLength()
            {
                // Arrange
                BiList<Card> list = new BiList<Card>();
                Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 123);
                Card card2 = new Card("5678 1234 9012 3456", "Jane Smith", "06/23", 456);
                list.AddToEnd(card1);
                list.AddToEnd(card2);

                // Act
                int length = list.Length();

                // Assert
                Assert.AreEqual(2, length);
            }
        }
    }
}
