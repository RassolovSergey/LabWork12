using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LabWork12;
using ClassLibraryLab10;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace UnitTest
{
    [TestClass]
    public class HPoint
    {

        [TestMethod]
        public void TestConstructorWithData()
        {
            // Arrange
            int data = 42;

            // Act
            HPoint<int> point = new HPoint<int>(data);

            // Assert
            Assert.AreEqual(data, point.Data);
            Assert.IsNull(point.Next);
            Assert.IsNull(point.Prev);
        }

        [TestMethod]
        public void TestToString()
        {
            // Arrange
            int data = 42;
            HPoint<int> point = new HPoint<int>(data);

            // Act
            string result = point.ToString();

            // Assert
            Assert.AreEqual("42", result);
        }

        [TestMethod]
        public void TestToStringWithNullData()
        {
            // Arrange
            HPoint<string> point = new HPoint<string>();

            // Act
            string result = point.ToString();

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void TestGetHashCode()
        {
            // Arrange
            int data = 42;
            HPoint<int> point = new HPoint<int>(data);

            // Act
            int hashCode = point.GetHashCode();

            // Assert
            Assert.AreEqual(data.GetHashCode(), hashCode);
        }

        [TestMethod]
        public void TestGetHashCodeWithNullData()
        {
            // Arrange
            HPoint<string> point = new HPoint<string>();

            // Act
            int hashCode = point.GetHashCode();

            // Assert
            Assert.AreEqual(0, hashCode);
        }

        [TestMethod]
        public void TestDataProperty()
        {
            // Arrange
            HPoint<int> point = new HPoint<int>();
            int data = 42;

            // Act
            point.Data = data;

            // Assert
            Assert.AreEqual(data, point.Data);
        }

        [TestMethod]
        public void TestNextProperty()
        {
            // Arrange
            HPoint<int> point1 = new HPoint<int>();
            HPoint<int> point2 = new HPoint<int>();

            // Act
            point1.Next = point2;

            // Assert
            Assert.AreEqual(point2, point1.Next);
        }

        [TestMethod]
        public void TestPrevProperty()
        {
            // Arrange
            HPoint<int> point1 = new HPoint<int>();
            HPoint<int> point2 = new HPoint<int>();

            // Act
            point1.Prev = point2;

            // Assert
            Assert.AreEqual(point2, point1.Prev);
        }
    }

    [TestClass]
    public class HashTableTests
    {
        [TestMethod]
        public void TestDefaultConstructor()
        {
            // Act
            HashTable<Card> hashTable = new HashTable<Card>();

            // Assert
            Assert.AreEqual(10, hashTable.Capacity, "Default capacity should be 10.");
            for (int i = 0; i < hashTable.Capacity; i++)
            {
                Assert.IsNull(hashTable.Find(new Card()), $"Element at index {i} should be null.");
            }
        }

        [TestMethod]
        public void TestConstructorWithLength()
        {
            // Arrange
            int length = 20;

            // Act
            HashTable<Card> hashTable = new HashTable<Card>(length);

            // Assert
            Assert.AreEqual(length, hashTable.Capacity, $"Capacity should be {length}.");
            for (int i = 0; i < hashTable.Capacity; i++)
            {
                Assert.IsNull(hashTable.Find(new Card()), $"Element at index {i} should be null.");
            }
        }
        [TestMethod]
        public void TestAddPoint()
        {
            // Arrange
            HashTable<Card> hashTable = new HashTable<Card>();
            Card card = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);

            // Act
            hashTable.AddPoint(card);

            // Assert
            Assert.IsTrue(hashTable.Contains(card), "HashTable should contain the added card.");
            Card foundCard = hashTable.Find(card);
            Assert.IsNotNull(foundCard, "Found card should not be null.");
            Assert.AreEqual(card.Id, foundCard.Id, "Card ID should match.");
            Assert.AreEqual(card.Name, foundCard.Name, "Card name should match.");
            Assert.AreEqual(card.Time, foundCard.Time, "Card time should match.");
            Assert.AreEqual(card.num.number, foundCard.num.number, "Card number should match.");
        }

        [TestMethod]
        public void TestPrintTable()
        {
            // Arrange
            HashTable<Card> hashTable = new HashTable<Card>();
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Jane Doe", "11/24", 2);
            hashTable.AddPoint(card1);
            hashTable.AddPoint(card2);

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                hashTable.PrintTable();

                // Assert
                string result = sw.ToString();
                StringAssert.Contains(result, "1:");
                StringAssert.Contains(result, "2:");
                StringAssert.Contains(result, card1.ToString());
                StringAssert.Contains(result, card2.ToString());
            }
        }

        [TestMethod]
        public void TestContains_ExistingElement()
        {
            // Arrange
            HashTable<Card> hashTable = new HashTable<Card>();
            Card card = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            hashTable.AddPoint(card);

            // Act
            bool result = hashTable.Contains(card);

            // Assert
            Assert.IsTrue(result, "HashTable should contain the added card.");
        }

        [TestMethod]
        public void TestContains_NonExistingElement()
        {
            // Arrange
            HashTable<Card> hashTable = new HashTable<Card>();
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Jane Doe", "11/24", 2);
            hashTable.AddPoint(card1);

            // Act
            bool result = hashTable.Contains(card2);

            // Assert
            Assert.IsFalse(result, "HashTable should not contain a non-added card.");
        }

        [TestMethod]
        public void TestFind_ExistingElement()
        {
            // Arrange
            HashTable<Card> hashTable = new HashTable<Card>();
            Card card = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            hashTable.AddPoint(card);

            // Act
            Card foundCard = hashTable.Find(card);

            // Assert
            Assert.IsNotNull(foundCard, "Found card should not be null.");
            Assert.AreEqual(card.Id, foundCard.Id, "Card ID should match.");
            Assert.AreEqual(card.Name, foundCard.Name, "Card name should match.");
            Assert.AreEqual(card.Time, foundCard.Time, "Card time should match.");
            Assert.AreEqual(card.num.number, foundCard.num.number, "Card number should match.");
        }

        [TestMethod]
        public void TestFind_NonExistingElement()
        {
            // Arrange
            HashTable<Card> hashTable = new HashTable<Card>();
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Jane Doe", "11/24", 2);
            hashTable.AddPoint(card1);

            // Act
            Card foundCard = hashTable.Find(card2);

            // Assert
            Assert.IsNull(foundCard, "Find should return null for a non-added card.");
        }

        [TestMethod]
        public void TestRemoveData_ExistingElement()
        {
            // Arrange
            HashTable<Card> hashTable = new HashTable<Card>();
            Card card = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            hashTable.AddPoint(card);

            // Act
            bool result = hashTable.RemoveData(card);

            // Assert
            Assert.IsTrue(result, "RemoveData should return true for an existing element.");
            Assert.IsFalse(hashTable.Contains(card), "HashTable should not contain the removed card.");
        }

        [TestMethod]
        public void TestRemoveData_NonExistingElement()
        {
            // Arrange
            HashTable<Card> hashTable = new HashTable<Card>();
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Jane Doe", "11/24", 2);
            hashTable.AddPoint(card1);

            // Act
            bool result = hashTable.RemoveData(card2);

            // Assert
            Assert.IsFalse(result, "RemoveData should return false for a non-existing element.");
        }

        [TestMethod]
        public void TestRemoveData_EmptyTable()
        {
            // Arrange
            HashTable<Card> hashTable = new HashTable<Card>();
            Card card = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);

            // Act
            bool result = hashTable.RemoveData(card);

            // Assert
            Assert.IsFalse(result, "RemoveData should return false for an empty table.");
        }

        [TestMethod]
        public void TestRemoveData_ChainHandling()
        {
            // Arrange
            HashTable<Card> hashTable = new HashTable<Card>();
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("1234 5678 9012 3457", "Jane Doe", "11/24", 2);
            hashTable.AddPoint(card1);
            hashTable.AddPoint(card2);

            // Act
            bool result1 = hashTable.RemoveData(card1);
            bool result2 = hashTable.RemoveData(card2);

            // Assert
            Assert.IsTrue(result1, "RemoveData should return true for the first element in the chain.");
            Assert.IsFalse(hashTable.Contains(card1), "HashTable should not contain the removed first element.");
            Assert.IsTrue(result2, "RemoveData should return true for the second element in the chain.");
            Assert.IsFalse(hashTable.Contains(card2), "HashTable should not contain the removed second element.");
        }

        [TestMethod]
        public void TestGetIndex_ConsistentIndexing()
        {
            // Arrange
            HashTable<Card> hashTable = new HashTable<Card>();
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Jane Doe", "11/24", 2);

            // Act
            int index1 = hashTable.GetIndex(card1);
            int index2 = hashTable.GetIndex(card2);
            int index1Again = hashTable.GetIndex(card1);

            // Assert
            Assert.AreEqual(index1, index1Again, "GetIndex should return consistent indices for the same object.");
        }

        [TestMethod]
        public void TestGetIndex_DifferentObjectsDifferentIndices()
        {
            // Arrange
            HashTable<Card> hashTable = new HashTable<Card>();
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Jane Doe", "11/24", 2);

            // Act
            int index1 = hashTable.GetIndex(card1);
            int index2 = hashTable.GetIndex(card2);

            // Assert
            Assert.AreNotEqual(index1, index2, "GetIndex should return different indices for different objects.");
        }


        [TestMethod]
        public void TestGetIndex_HandleCollisions()
        {
            // Arrange
            HashTable<Card> hashTable = new HashTable<Card>(1); // Force collisions by setting capacity to 1
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Jane Doe", "11/24", 2);

            // Act
            int index1 = hashTable.GetIndex(card1);
            int index2 = hashTable.GetIndex(card2);

            // Assert
            Assert.AreEqual(index1, index2, "GetIndex should handle collisions correctly when capacity is 1.");
        }

        [TestMethod]
        public void TestAddPoint_CreateChain()
        {
            // Arrange
            HashTable<Card> hashTable = new HashTable<Card>(1); // Force collisions by setting capacity to 1
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Jane Doe", "11/24", 2);

            // Act
            hashTable.AddPoint(card1);
            hashTable.AddPoint(card2);

            // Assert
            int index = hashTable.GetIndex(card1);
            HPoint<Card> firstPoint = hashTable.table[index];
            HPoint<Card> secondPoint = firstPoint.Next;

            Assert.IsNotNull(firstPoint, "First point in the chain should not be null.");
            Assert.AreEqual(card1, firstPoint.Data, "First point in the chain should contain card1.");

            Assert.IsNotNull(secondPoint, "Second point in the chain should not be null.");
            Assert.AreEqual(card2, secondPoint.Data, "Second point in the chain should contain card2.");
            Assert.AreEqual(firstPoint, secondPoint.Prev, "Second point's Prev should point to the first point.");
        }

        [TestMethod]
        public void TestAddPoint_ElementAlreadyExists()
        {
            // Arrange
            HashTable<Card> hashTable = new HashTable<Card>();
            Card card = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            hashTable.AddPoint(card);

            // Act
            hashTable.AddPoint(card);

            // Assert
            int index = hashTable.GetIndex(card);
            HPoint<Card> current = hashTable.table[index];

            int count = 0;
            while (current != null)
            {
                count++;
                current = current.Next;
            }

            Assert.AreEqual(1, count, "There should be only one instance of the added element in the chain.");
        }

        [TestMethod]
        public void TestAddPoint_MultipleElementsInChain()
        {
            // Arrange
            HashTable<Card> hashTable = new HashTable<Card>(1); // Force collisions by setting capacity to 1
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Jane Doe", "11/24", 2);
            Card card3 = new Card("3456 7890 1234 5678", "Jim Doe", "10/23", 3);

            // Act
            hashTable.AddPoint(card1);
            hashTable.AddPoint(card2);
            hashTable.AddPoint(card3);

            // Assert
            int index = hashTable.GetIndex(card1);
            HPoint<Card> current = hashTable.table[index];

            int count = 0;
            while (current != null)
            {
                count++;
                current = current.Next;
            }

            Assert.AreEqual(3, count, "There should be three elements in the chain.");
        }
    }

    [TestClass]
    public class PointBiListTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            // Arrange
            Card card = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);

            // Act
            PointBiList<Card> point = new PointBiList<Card>(card);

            // Assert
            Assert.IsNotNull(point, "PointBiList object should be created.");
            Assert.AreEqual(card, point.Data, "Data should be set correctly.");
            Assert.IsNull(point.Next, "Next should be initialized to null.");
            Assert.IsNull(point.Prev, "Prev should be initialized to null.");
        }

        [TestMethod]
        public void TestLinkingNext()
        {
            // Arrange
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Jane Doe", "11/24", 2);
            PointBiList<Card> point1 = new PointBiList<Card>(card1);
            PointBiList<Card> point2 = new PointBiList<Card>(card2);

            // Act
            point1.Next = point2;

            // Assert
            Assert.AreEqual(point2, point1.Next, "Next should be linked correctly.");
            Assert.IsNull(point1.Prev, "Prev should still be null.");
        }

        [TestMethod]
        public void TestLinkingPrev()
        {
            // Arrange
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Jane Doe", "11/24", 2);
            PointBiList<Card> point1 = new PointBiList<Card>(card1);
            PointBiList<Card> point2 = new PointBiList<Card>(card2);

            // Act
            point2.Prev = point1;

            // Assert
            Assert.AreEqual(point1, point2.Prev, "Prev should be linked correctly.");
            Assert.IsNull(point2.Next, "Next should still be null.");
        }

        [TestMethod]
        public void TestBidirectionalLinking()
        {
            // Arrange
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Jane Doe", "11/24", 2);
            PointBiList<Card> point1 = new PointBiList<Card>(card1);
            PointBiList<Card> point2 = new PointBiList<Card>(card2);

            // Act
            point1.Next = point2;
            point2.Prev = point1;

            // Assert
            Assert.AreEqual(point2, point1.Next, "Next should be linked correctly.");
            Assert.AreEqual(point1, point2.Prev, "Prev should be linked correctly.");
        }

        [TestMethod]
        public void TestBidirectionalLinkingWithData()
        {
            // Arrange
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Jane Doe", "11/24", 2);
            PointBiList<Card> point1 = new PointBiList<Card>(card1);
            PointBiList<Card> point2 = new PointBiList<Card>(card2);

            // Act
            point1.Next = point2;
            point2.Prev = point1;

            // Assert
            Assert.AreEqual(point2, point1.Next, "Next should be linked correctly.");
            Assert.AreEqual(point1, point2.Prev, "Prev should be linked correctly.");
            Assert.AreEqual(card1, point1.Data, "First point data should be correct.");
            Assert.AreEqual(card2, point2.Data, "Second point data should be correct.");
        }
    }

    [TestClass]
    public class BiListTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            // Arrange & Act
            BiList<Card> list = new BiList<Card>();

            // Assert
            Assert.IsNotNull(list, "BiList object should be created.");
            Assert.IsNull(list.beg, "Begin of the list should be null.");
            Assert.IsNull(list.end, "End of the list should be null.");
            Assert.AreEqual(0, list.Count, "Count should be zero.");
        }

        [TestMethod]
        public void TestConstructorWithSize()
        {
            // Arrange
            int size = 5;

            // Act
            BiList<Card> list = new BiList<Card>(size);

            // Assert
            Assert.AreEqual(size, list.Count, "Count should be equal to the initialized size.");
        }

        [TestMethod]
        public void TestAddToEnd()
        {
            // Arrange
            BiList<Card> list = new BiList<Card>();
            Card card = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);

            // Act
            list.AddToEnd(card);

            // Assert
            Assert.AreEqual(1, list.Count, "Count should be one.");
            Assert.AreEqual(card, list.beg.Data, "Data of the first element should match.");
            Assert.AreEqual(card, list.end.Data, "Data of the last element should match.");
        }

        [TestMethod]
        public void TestFindItem()
        {
            // Arrange
            BiList<Card> list = new BiList<Card>();
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Jane Doe", "11/24", 2);
            list.AddToEnd(card1);
            list.AddToEnd(card2);

            // Act
            bool foundCard1 = list.FindItem(card1);
            bool foundCard2 = list.FindItem(card2);
            bool foundCard3 = list.FindItem(new Card("3456 7890 1234 5678", "Jake Doe", "10/23", 3));

            // Assert
            Assert.IsTrue(foundCard1, "Card1 should be found.");
            Assert.IsTrue(foundCard2, "Card2 should be found.");
            Assert.IsFalse(foundCard3, "Card3 should not be found.");
        }

        [TestMethod]
        public void TestRemove()
        {
            // Arrange
            BiList<Card> list = new BiList<Card>();
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Jane Doe", "11/24", 2);
            list.AddToEnd(card1);
            list.AddToEnd(card2);

            // Act
            list.Remove(list.beg);

            // Assert
            Assert.AreEqual(1, list.Count, "Count should be one after removal.");
            Assert.AreEqual(card2, list.beg.Data, "Data of the remaining element should match.");
            Assert.AreEqual(card2, list.end.Data, "Data of the remaining element should match.");
        }

        [TestMethod]
        public void TestDeepClone()
        {
            // Arrange
            BiList<Card> list = new BiList<Card>();
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Jane Doe", "11/24", 2);
            list.AddToEnd(card1);
            list.AddToEnd(card2);

            // Act
            BiList<Card> clonedList = list.DeepClone();

            // Assert
            Assert.AreEqual(list.Count, clonedList.Count, "Counts should be equal.");
            Assert.AreNotSame(list.beg, clonedList.beg, "Begin nodes should not be the same instance.");
            Assert.AreNotSame(list.end, clonedList.end, "End nodes should not be the same instance.");
            Assert.AreEqual(list.beg.Data, clonedList.beg.Data, "Data of the first element should match.");
            Assert.AreEqual(list.end.Data, clonedList.end.Data, "Data of the last element should match.");
        }

        [TestMethod]
        public void TestAddAfter()
        {
            // Arrange
            BiList<Card> list = new BiList<Card>();
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Jane Doe", "11/24", 2);
            Card card3 = new Card("3456 7890 1234 5678", "Jake Doe", "10/23", 3);
            list.AddToEnd(card1);
            list.AddToEnd(card2);

            // Act
            list.AddAfter(card1, card3);

            // Assert
            Assert.AreEqual(3, list.Count, "Count should be three after adding.");
            Assert.AreEqual(card3, list.beg.Next.Data, "Data of the middle element should match.");
        }

        [TestMethod]
        public void TestRemoveEven()
        {
            // Arrange
            BiList<Card> list = new BiList<Card>(4);
            Card card1 = list.beg.Data;
            Card card2 = list.beg.Next.Data;
            Card card3 = list.beg.Next.Next.Data;
            Card card4 = list.beg.Next.Next.Next.Data;

            // Act
            list.RemoveEven();

            // Assert
            Assert.AreEqual(2, list.Count, "Count should be two after removing even elements.");
            Assert.IsTrue(list.FindItem(card1), "First card should still be in the list.");
            Assert.IsFalse(list.FindItem(card2), "Second card should be removed.");
            Assert.IsTrue(list.FindItem(card3), "Third card should still be in the list.");
            Assert.IsFalse(list.FindItem(card4), "Fourth card should be removed.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructorWithZeroSizeThrowsException()
        {
            // Arrange
            int size = 0;

            // Act
            BiList<Card> list = new BiList<Card>(size);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructorWithNegativeSizeThrowsException()
        {
            // Arrange
            int size = -1;

            // Act
            BiList<Card> list = new BiList<Card>(size);

            // Assert is handled by ExpectedException
        }

        [TestMethod]
        public void TestConstructorWithZeroSizeThrowsExceptionWithCorrectMessage()
        {
            // Arrange
            int size = 0;
            string expectedMessage = "Размер должен быть больше нуля.";

            // Act
            try
            {
                BiList<Card> list = new BiList<Card>(size);
            }
            catch (ArgumentException ex)
            {
                // Assert
                StringAssert.Contains(ex.Message, expectedMessage);
                return;
            }

            Assert.Fail("Expected ArgumentException was not thrown.");
        }

        [TestMethod]
        public void TestConstructorWithNegativeSizeThrowsExceptionWithCorrectMessage()
        {
            // Arrange
            int size = -1;
            string expectedMessage = "Размер должен быть больше нуля.";

            // Act
            try
            {
                BiList<Card> list = new BiList<Card>(size);
            }
            catch (ArgumentException ex)
            {
                // Assert
                StringAssert.Contains(ex.Message, expectedMessage);
                return;
            }

            Assert.Fail("Expected ArgumentException was not thrown.");
        }


    }

    [TestClass]
    public class TreePointTests
    {
        [TestMethod]
        public void TreePointCreation_Test()
        {
            // Arrange
            Card card = new Card("1234 5678 9012 3456", "John Doe", "12/25", 123);
            TreePoint<Card> treePoint = new TreePoint<Card>(card);

            // Act

            // Assert
            Assert.IsNotNull(treePoint);
            Assert.AreEqual(card, treePoint.Data);
            Assert.IsNull(treePoint.Left);
            Assert.IsNull(treePoint.Right);
        }

        [TestMethod]
        public void TreePointChildren_Test()
        {
            // Arrange
            Card parentCard = new Card("1234 5678 9012 3456", "John Doe", "12/25", 123);
            Card leftChildCard = new Card("5678 1234 9012 3456", "Jane Doe", "06/24", 456);
            Card rightChildCard = new Card("9012 5678 1234 3456", "Michael Smith", "09/23", 789);

            TreePoint<Card> parentNode = new TreePoint<Card>(parentCard);
            TreePoint<Card> leftChildNode = new TreePoint<Card>(leftChildCard);
            TreePoint<Card> rightChildNode = new TreePoint<Card>(rightChildCard);

            // Act
            parentNode.Left = leftChildNode;
            parentNode.Right = rightChildNode;

            // Assert
            Assert.AreEqual(parentNode.Left, leftChildNode);
            Assert.AreEqual(parentNode.Right, rightChildNode);
            Assert.IsNull(leftChildNode.Left);
            Assert.IsNull(leftChildNode.Right);
            Assert.IsNull(rightChildNode.Left);
            Assert.IsNull(rightChildNode.Right);
        }

        [TestMethod]
        public void TreePointDataModification_Test()
        {
            // Arrange
            Card originalCard = new Card("1234 5678 9012 3456", "John Doe", "12/25", 123);
            Card modifiedCard = new Card("5678 1234 9012 3456", "Jane Doe", "06/24", 456);
            TreePoint<Card> treePoint = new TreePoint<Card>(originalCard);

            // Act
            treePoint.Data = modifiedCard;

            // Assert
            Assert.AreEqual(modifiedCard, treePoint.Data);
        }

        [TestMethod]
        public void TreePointConstructorDefault_Test()
        {
            // Arrange & Act
            TreePoint<Card> treePoint = new TreePoint<Card>();

            // Assert
            Assert.IsNotNull(treePoint);
            Assert.IsNull(treePoint.Data);
            Assert.IsNull(treePoint.Left);
            Assert.IsNull(treePoint.Right);
        }

        [TestMethod]
        public void TreePointToString_Test()
        {
            // Arrange
            Card card = new Card("1234 5678 9012 3456", "John Doe", "12/25", 123);
            TreePoint<Card> treePoint = new TreePoint<Card>(card);

            // Act
            string result = treePoint.ToString();

            // Assert
            string expected = "1234 5678 9012 3456 | John Doe | 12/25 | 123";
            Assert.AreEqual(expected, result);
        }
    }

    [TestClass]
    public class MyTreeTests
    {
        [TestMethod]
        public void MyTreeConstructor_Test()
        {
            // Arrange
            int length = 5;

            // Act
            MyTree<Card> myTree = new MyTree<Card>(length);

            // Assert
            Assert.IsNotNull(myTree);
            Assert.AreEqual(length, myTree.Count);
            Assert.IsNotNull(myTree.Root);
        }


        [TestMethod]
        public void MyTreeDeepCopy_Test()
        {
            // Arrange
            MyTree<Card> myTree = new MyTree<Card>(5);

            // Act
            MyTree<Card> copyTree = myTree.DeepCopy();

            // Assert
            Assert.IsNotNull(copyTree);
            Assert.AreNotSame(myTree, copyTree);
            Assert.AreEqual(myTree.Count, copyTree.Count);
        }

        [TestMethod]
        public void MyTree_CalculateAverage()
        {
            // Arrange
            MyTree<Card> myTree = new MyTree<Card>(0);

            // Создаем несколько карт с известными значениями номера
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("9876 5432 1098 7654", "Jane Smith", "05/23", 2);
            Card card3 = new Card("1111 2222 3333 4444", "Alice Brown", "03/24", 3);

            // Добавляем карты в дерево
            myTree.AddPoint(card1);
            myTree.AddPoint(card2);
            myTree.AddPoint(card3);

            // Act
            double average = myTree.CalculateAverage();

            // Assert
            Assert.AreEqual(2.0, average); // Среднее значение для номеров 1, 2, 3 должно быть 2.0
        }

        [TestMethod]
        public void MyTree_DeleteTree()
        {
            // Arrange
            MyTree<Card> myTree = new MyTree<Card>(0);

            // Создаем несколько карт с известными значениями номера
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            Card card2 = new Card("9876 5432 1098 7654", "Jane Smith", "05/23", 2);
            Card card3 = new Card("1111 2222 3333 4444", "Alice Brown", "03/24", 3);

            // Добавляем карты в дерево
            myTree.AddPoint(card1);
            myTree.AddPoint(card2);
            myTree.AddPoint(card3);

            // Act
            myTree.DeleteTree();

            // Assert
            Assert.AreEqual(0, myTree.Count);  // Проверяем, что количество элементов в дереве равно 0
            Assert.IsNull(myTree.Root);         // Проверяем, что корень дерева равен null
        }

        [TestMethod]
        public void TransformToFindTree_TransformsToSearchTree()
        {
            // Arrange
            MyTree<Card> tree = new MyTree<Card>(0);

            Card card1 = new Card();
            tree.AddPoint(new Card(card1));
            Card card2 = new Card();
            tree.AddPoint(new Card(card2));
            Card card3 = new Card();
            tree.AddPoint(new Card(card3));

            // Act
            tree.TransformToFindTree();

            // Assert
            Assert.IsTrue(IsSearchTree(tree.Root));
        }

        private bool IsSearchTree(TreePoint<Card> node)
        {
            if (node == null) return true;

            // Проверяем, что в левом поддереве все значения меньше текущего узла
            if (node.Left != null && node.Left.Data.CompareTo(node.Data) >= 0)
                return false;

            // Проверяем, что в правом поддереве все значения больше текущего узла
            if (node.Right != null && node.Right.Data.CompareTo(node.Data) <= 0)
                return false;

            // Рекурсивно проверяем поддеревья
            return IsSearchTree(node.Left) && IsSearchTree(node.Right);
        }

    }
}
