using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryLab10;
using LabWork12;

namespace UnitTestList
{
    [TestClass]
    public class PointBiListTests
    {
        [TestMethod]
        public void Constructor_WithData_CreatesInstanceWithCorrectData()
        {
            // Arrange
            Card testData = new Card();

            // Act
            PointBiList<Card> point = new PointBiList<Card>(testData);

            // Assert
            Assert.AreEqual(testData, point.Data);
        }

        [TestMethod]
        public void Constructor_WithData_SetsNextAndPrevToNull()
        {
            // Arrange
            Card testData = new Card();

            // Act
            PointBiList<Card> point = new PointBiList<Card>(testData);

            // Assert
            Assert.IsNull(point.Next);
            Assert.IsNull(point.Prev);
        }

        [TestMethod]
        public void Data_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            Card testData = new Card();
            PointBiList<Card> point = new PointBiList<Card>(testData);
            Card newData = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);

            // Act
            point.Data = newData;

            // Assert
            Assert.AreEqual(newData, point.Data);
        }
    }
}
