using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LabWork12;
using ClassLibraryLab10;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;

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

        [TestMethod]
        public void TestEquals_SameObject()
        {
            // Arrange
            HPoint<int> point = new HPoint<int>(42);

            // Act
            bool result = point.Equals(point);

            // Assert
            Assert.IsTrue(result, "Equals should return true for the same object.");
        }

        [TestMethod]
        public void TestEquals_DifferentObjectDifferentData()
        {
            // Arrange
            HPoint<int> point1 = new HPoint<int>(42);
            HPoint<int> point2 = new HPoint<int>(43);

            // Act
            bool result = point1.Equals(point2);

            // Assert
            Assert.IsFalse(result, "Equals should return false for different objects with different data.");
        }

        [TestMethod]
        public void TestEquals_NullObject()
        {
            // Arrange
            HPoint<int> point = new HPoint<int>(42);

            // Act
            bool result = point.Equals(null);

            // Assert
            Assert.IsFalse(result, "Equals should return false when comparing with null.");
        }

        [TestMethod]
        public void TestEquals_DifferentType()
        {
            // Arrange
            HPoint<int> point = new HPoint<int>(42);

            // Act
            bool result = point.Equals("42");

            // Assert
            Assert.IsFalse(result, "Equals should return false when comparing with a different type.");
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
            hashTable.Add(card);

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
            hashTable.Add(card1);
            hashTable.Add(card2);

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
            hashTable.Add(card);

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
            hashTable.Add(card1);

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
            hashTable.Add(card);

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
            hashTable.Add(card1);

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
            hashTable.Add(card);

            // Act
            bool result = hashTable.Remove(card);

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
            hashTable.Add(card1);

            // Act
            bool result = hashTable.Remove(card2);

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
            bool result = hashTable.Remove(card);

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
            hashTable.Add(card1);
            hashTable.Add(card2);

            // Act
            bool result1 = hashTable.Remove(card1);
            bool result2 = hashTable.Remove(card2);

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
            hashTable.Add(card1);
            hashTable.Add(card2);

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
            hashTable.Add(card);

            // Act
            hashTable.Add(card);

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
            hashTable.Add(card1);
            hashTable.Add(card2);
            hashTable.Add(card3);

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


        [TestMethod]
        public void TestPrintTable_EmptyTable()
        {
            // Arrange
            HashTable<Card> hashTable = new HashTable<Card>();

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                hashTable.PrintTable();

                // Assert
                string result = sw.ToString();
                for (int i = 0; i < hashTable.Capacity; i++)
                {
                    StringAssert.Contains(result, $"{i}:");
                }
            }
        }

        [TestMethod]
        public void TestRemoveData_FromEmptyTable()
        {
            // Подготовка
            HashTable<Card> hashTable = new HashTable<Card>();
            Card card = new Card("1234 5678 9012 3456", "Иван Иванов", "12/25", 1);

            // Действие
            bool result = hashTable.Remove(card);

            // Проверка
            Assert.IsFalse(result, "Метод RemoveData должен возвращать false при удалении из пустой таблицы.");
        }


        [TestMethod]
        public void TestRemoveData_ChainHandling_FirstElement()
        {
            // Подготовка
            HashTable<Card> hashTable = new HashTable<Card>(1); // Принудительно вызываем коллизии, установив ёмкость в 1
            Card card1 = new Card("1234 5678 9012 3456", "Иван Иванов", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Мария Петрова", "11/24", 2);
            hashTable.Add(card1);
            hashTable.Add(card2);

            // Действие
            bool result = hashTable.Remove(card1);

            // Проверка
            Assert.IsTrue(result, "Метод RemoveData должен возвращать true для первого элемента в цепочке.");
            Assert.IsFalse(hashTable.Contains(card1), "Хеш-таблица не должна содержать удалённый первый элемент.");
            Assert.IsTrue(hashTable.Contains(card2), "Хеш-таблица должна содержать второй элемент.");
        }

        [TestMethod]
        public void TestRemoveData_ChainHandling_MiddleElement()
        {
            // Подготовка
            HashTable<Card> hashTable = new HashTable<Card>(1); // Принудительно вызываем коллизии, установив ёмкость в 1
            Card card1 = new Card("1234 5678 9012 3456", "Иван Иванов", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Мария Петрова", "11/24", 2);
            Card card3 = new Card("3456 7890 1234 5678", "Алексей Смирнов", "10/23", 3);
            hashTable.Add(card1);
            hashTable.Add(card2);
            hashTable.Add(card3);

            // Действие
            bool result = hashTable.Remove(card2);

            // Проверка
            Assert.IsTrue(result, "Метод RemoveData должен возвращать true для среднего элемента в цепочке.");
            Assert.IsFalse(hashTable.Contains(card2), "Хеш-таблица не должна содержать удалённый средний элемент.");
            Assert.IsTrue(hashTable.Contains(card1), "Хеш-таблица должна содержать первый элемент.");
            Assert.IsTrue(hashTable.Contains(card3), "Хеш-таблица должна содержать последний элемент.");
        }

        [TestMethod]
        public void TestRemoveData_ChainHandling_LastElement()
        {
            // Подготовка
            HashTable<Card> hashTable = new HashTable<Card>(1); // Принудительно вызываем коллизии, установив ёмкость в 1
            Card card1 = new Card("1234 5678 9012 3456", "Иван Иванов", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Мария Петрова", "11/24", 2);
            Card card3 = new Card("3456 7890 1234 5678", "Алексей Смирнов", "10/23", 3);
            hashTable.Add(card1);
            hashTable.Add(card2);
            hashTable.Add(card3);

            // Действие
            bool result = hashTable.Remove(card3);

            // Проверка
            Assert.IsTrue(result, "Метод RemoveData должен возвращать true для последнего элемента в цепочке.");
            Assert.IsFalse(hashTable.Contains(card3), "Хеш-таблица не должна содержать удалённый последний элемент.");
            Assert.IsTrue(hashTable.Contains(card1), "Хеш-таблица должна содержать первый элемент.");
            Assert.IsTrue(hashTable.Contains(card2), "Хеш-таблица должна содержать средний элемент.");
        }

        [TestMethod]
        public void TestRemoveData_AllElements()
        {
            // Подготовка
            HashTable<Card> hashTable = new HashTable<Card>(1); // Принудительно вызываем коллизии, установив ёмкость в 1
            Card card1 = new Card("1234 5678 9012 3456", "Иван Иванов", "12/25", 1);
            Card card2 = new Card("2345 6789 0123 4567", "Мария Петрова", "11/24", 2);
            Card card3 = new Card("3456 7890 1234 5678", "Алексей Смирнов", "10/23", 3);
            hashTable.Add(card1);
            hashTable.Add(card2);
            hashTable.Add(card3);

            // Действие
            bool result1 = hashTable.Remove(card1);
            bool result2 = hashTable.Remove(card2);
            bool result3 = hashTable.Remove(card3);

            // Проверка
            Assert.IsTrue(result1, "Метод RemoveData должен возвращать true для первого элемента в цепочке.");
            Assert.IsFalse(hashTable.Contains(card1), "Хеш-таблица не должна содержать удалённый первый элемент.");
            Assert.IsTrue(result2, "Метод RemoveData должен возвращать true для второго элемента в цепочке.");
            Assert.IsFalse(hashTable.Contains(card2), "Хеш-таблица не должна содержать удалённый второй элемент.");
            Assert.IsTrue(result3, "Метод RemoveData должен возвращать true для третьего элемента в цепочке.");
            Assert.IsFalse(hashTable.Contains(card3), "Хеш-таблица не должна содержать удалённый третий элемент.");
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
            list.Add(card);

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
            list.Add(card1);
            list.Add(card2);

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
            list.Add(card1);
            list.Add(card2);

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
            list.Add(card1);
            list.Add(card2);

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
            list.Add(card1);
            list.Add(card2);

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
        public void Constructor_ValidData_ShouldInitializeProperties()
        {
            // Arrange
            var card = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);

            // Act
            var treePoint = new TreePoint<Card>(card);

            // Assert
            Assert.IsNotNull(treePoint);
            Assert.AreEqual(card, treePoint.Data);
            Assert.IsNull(treePoint.Left);
            Assert.IsNull(treePoint.Right);
        }

        [TestMethod]
        public void ToString_ValidData_ShouldReturnDataString()
        {
            // Arrange
            var card = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            var treePoint = new TreePoint<Card>(card);

            // Act
            var result = treePoint.ToString();

            // Assert
            Assert.AreEqual(card.ToString(), result);
        }

        [TestMethod]
        public void ToString_NullData_ShouldReturnEmptyString()
        {
            // Arrange
            var treePoint = new TreePoint<Card>(null);

            // Act
            var result = treePoint.ToString();

            // Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void Equals_SameObject_ShouldReturnTrue()
        {
            // Arrange
            var card = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            var treePoint1 = new TreePoint<Card>(card);
            var treePoint2 = new TreePoint<Card>(card);

            // Act
            var result = treePoint1.Equals(treePoint2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_DifferentObject_ShouldReturnFalse()
        {
            // Arrange
            var card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            var card2 = new Card("6543 2109 8765 4321", "Jane Smith", "11/24", 2);
            var treePoint1 = new TreePoint<Card>(card1);
            var treePoint2 = new TreePoint<Card>(card2);

            // Act
            var result = treePoint1.Equals(treePoint2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_NullObject_ShouldReturnFalse()
        {
            // Arrange
            var card = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            var treePoint = new TreePoint<Card>(card);

            // Act
            var result = treePoint.Equals(null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_DifferentType_ShouldReturnFalse()
        {
            // Arrange
            var card = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            var treePoint = new TreePoint<Card>(card);
            var differentTypeObject = new object();

            // Act
            var result = treePoint.Equals(differentTypeObject);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetHashCode_SameObject_ShouldReturnSameHash()
        {
            // Arrange
            var card = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            var treePoint1 = new TreePoint<Card>(card);
            var treePoint2 = new TreePoint<Card>(card);

            // Act
            var hash1 = treePoint1.GetHashCode();
            var hash2 = treePoint2.GetHashCode();

            // Assert
            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void GetHashCode_DifferentObject_ShouldReturnDifferentHash()
        {
            // Arrange
            var card1 = new Card("1234 5678 9012 3456", "John Doe", "12/25", 1);
            var card2 = new Card("6543 2109 8765 4321", "Jane Smith", "11/24", 2);
            var treePoint1 = new TreePoint<Card>(card1);
            var treePoint2 = new TreePoint<Card>(card2);

            // Act
            var hash1 = treePoint1.GetHashCode();
            var hash2 = treePoint2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hash1, hash2);
        }

        [TestMethod]
        public void GetHashCode_NullData_ShouldReturnConsistentHash()
        {
            // Arrange
            var treePoint1 = new TreePoint<Card>(null);
            var treePoint2 = new TreePoint<Card>(null);

            // Act
            var hash1 = treePoint1.GetHashCode();
            var hash2 = treePoint2.GetHashCode();

            // Assert
            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void GetHashCode_BothLeftAndRightNull_ShouldReturnSameHash()
        {
            // Arrange
            var treePoint1 = new TreePoint<int>(10);
            var treePoint2 = new TreePoint<int>(10);

            // Act
            var hash1 = treePoint1.GetHashCode();
            var hash2 = treePoint2.GetHashCode();

            // Assert
            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void GetHashCode_LeftNotNull_ShouldReturnDifferentHash()
        {
            // Arrange
            var treePoint1 = new TreePoint<int>(10);
            treePoint1.Left = new TreePoint<int>(5);
            var treePoint2 = new TreePoint<int>(10);

            // Act
            var hash1 = treePoint1.GetHashCode();
            var hash2 = treePoint2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hash1, hash2);
        }

        [TestMethod]
        public void GetHashCode_RightNotNull_ShouldReturnDifferentHash()
        {
            // Arrange
            var treePoint1 = new TreePoint<int>(10);
            treePoint1.Right = new TreePoint<int>(15);
            var treePoint2 = new TreePoint<int>(10);

            // Act
            var hash1 = treePoint1.GetHashCode();
            var hash2 = treePoint2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hash1, hash2);
        }

        [TestMethod]
        public void GetHashCode_BothLeftAndRightNotNull_ShouldReturnDifferentHash()
        {
            // Arrange
            var treePoint1 = new TreePoint<int>(10);
            treePoint1.Left = new TreePoint<int>(5);
            treePoint1.Right = new TreePoint<int>(15);
            var treePoint2 = new TreePoint<int>(10);

            // Act
            var hash1 = treePoint1.GetHashCode();
            var hash2 = treePoint2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hash1, hash2);
        }
    }

    [TestClass]
    public class MyTreetests01
    {
        private MyTree<Card> tree;

        [TestInitialize]
        public void Initialize()
        {
            tree = new MyTree<Card>();
        }

        [TestMethod]
        public void Constructor_Length_ShouldInitializeTreeWithSpecifiedLength()
        {
            // Arrange
            int length = 5;

            // Act
            var treeWithLength = new MyTree<Card>(length);

            // Assert
            Assert.IsNotNull(treeWithLength);
            Assert.AreEqual(length, treeWithLength.Count);
        }

        private int GetHeight(TreePoint<Card>? node)
        {
            if (node == null)
                return 0;

            int leftHeight = GetHeight(node.Left);
            int rightHeight = GetHeight(node.Right);

            return Math.Max(leftHeight, rightHeight) + 1;
        }

        [TestMethod]
        public void Add_NewCard_ShouldIncreaseCount()
        {
            // Arrange
            Card card = new Card("1234 5678 9012 3456", "John Doe", "06/25", 1);

            // Act
            tree.Add(card);

            // Assert
            Assert.AreEqual(1, tree.Count);
        }

        [TestMethod]
        public void Add_DuplicateCard_ShouldNotIncreaseCount()
        {
            // Arrange
            Card card1 = new Card("1234 5678 9012 3456", "John Doe", "06/25", 1);
            Card card2 = new Card("1234 5678 9012 3456", "John Doe", "06/25", 1);

            // Act
            tree.Add(card1);
            tree.Add(card2);

            // Assert
            Assert.AreEqual(1, tree.Count);
        }

        [TestMethod]
        public void Find_ExistingCard_ShouldReturnCard()
        {
            // Arrange
            Card card = new Card("1234 5678 9012 3456", "John Doe", "06/25", 1);
            tree.Add(card);

            // Act
            var foundCard = tree.Find(card);

            // Assert
            Assert.IsNotNull(foundCard);
            Assert.AreEqual(card, foundCard);
        }

        [TestMethod]
        public void Count_EmptyTree_ShouldReturnZero()
        {
            // Arrange

            // Act
            int count = tree.Count;

            // Assert
            Assert.AreEqual(0, count);
        }
    }

    [TestClass]
    public class MyTreeTests02
    {
        [TestMethod]
        public void Constructor_WithCollection_ShouldBuildBalancedTree()
        {
            // Arrange
            Card[] cards = GenerateCardsArray(5); // Generating an array of 5 cards

            // Act
            var tree = new MyTree<Card>(cards);

            // Assert
            Assert.IsNotNull(tree);
            Assert.AreEqual(cards.Length, tree.Count);
            Assert.IsTrue(IsBalanced(tree.Root));
            Assert.IsTrue(IsSorted(tree.Root));
        }

        private Card[] GenerateCardsArray(int length)
        {
            Card[] cards = new Card[length];
            for (int i = 0; i < length; i++)
            {
                cards[i] = new Card($"1234 5678 9012 {i:D4}", $"User {i}", $"{(i % 12 + 1):D2}/{30 + i % 20:D2}", i + 1);
            }
            Array.Sort(cards); // Ensure the array is sorted for balanced tree construction
            return cards;
        }

        private bool IsBalanced(TreePoint<Card>? node)
        {
            return GetHeight(node) != -1;
        }

        private int GetHeight(TreePoint<Card>? node)
        {
            if (node == null)
                return 0;

            int leftHeight = GetHeight(node.Left);
            if (leftHeight == -1) return -1;

            int rightHeight = GetHeight(node.Right);
            if (rightHeight == -1) return -1;

            if (Math.Abs(leftHeight - rightHeight) > 1)
                return -1;

            return Math.Max(leftHeight, rightHeight) + 1;
        }

        private bool IsSorted(TreePoint<Card>? node)
        {
            return IsSorted(node, null, null);
        }

        private bool IsSorted(TreePoint<Card>? node, Card? min, Card? max)
        {
            if (node == null)
                return true;

            if ((min != null && node.Data.CompareTo(min) <= 0) || (max != null && node.Data.CompareTo(max) >= 0))
                return false;

            return IsSorted(node.Left, min, node.Data) && IsSorted(node.Right, node.Data, max);
        }
    }

    [TestClass]
    public class MyTreeTests03
    {
        [TestMethod]
        public void RemoveISBD_ShouldRemoveExistingItem()
        {
            // Arrange
            Card[] cards = GenerateCardsArray(5);
            var tree = new MyTree<Card>(cards);
            var itemToRemove = cards[2]; // Choose an item to remove

            // Act
            bool result = tree.RemoveISBD(itemToRemove);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(4, tree.Count);
            Assert.IsFalse(TreeContains(tree.Root, itemToRemove));
            Assert.IsTrue(IsBalanced(tree.Root));
            Assert.IsTrue(IsSorted(tree.Root));
        }

        [TestMethod]
        public void RemoveISBD_ShouldHandleRemovingRoot()
        {
            // Arrange
            Card[] cards = GenerateCardsArray(5);
            var tree = new MyTree<Card>(cards);
            var rootItem = cards[2]; // Root item in a balanced tree

            // Act
            bool result = tree.RemoveISBD(rootItem);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(4, tree.Count);
            Assert.IsFalse(TreeContains(tree.Root, rootItem));
            Assert.IsTrue(IsBalanced(tree.Root));
            Assert.IsTrue(IsSorted(tree.Root));
        }

        private Card[] GenerateCardsArray(int length)
        {
            Card[] cards = new Card[length];
            for (int i = 0; i < length; i++)
            {
                cards[i] = new Card($"1234 5678 9012 {i:D4}", $"User {i}", $"{(i % 12 + 1):D2}/{30 + i % 20:D2}", i + 1);
            }
            Array.Sort(cards); // Ensure the array is sorted for balanced tree construction
            return cards;
        }

        private bool TreeContains(TreePoint<Card>? node, Card item)
        {
            if (node == null)
                return false;

            if (node.Data.Equals(item))
                return true;

            if (item.CompareTo(node.Data) < 0)
                return TreeContains(node.Left, item);
            else
                return TreeContains(node.Right, item);
        }

        private bool IsBalanced(TreePoint<Card>? node)
        {
            return GetHeight(node) != -1;
        }

        private int GetHeight(TreePoint<Card>? node)
        {
            if (node == null)
                return 0;

            int leftHeight = GetHeight(node.Left);
            if (leftHeight == -1) return -1;

            int rightHeight = GetHeight(node.Right);
            if (rightHeight == -1) return -1;

            if (Math.Abs(leftHeight - rightHeight) > 1)
                return -1;

            return Math.Max(leftHeight, rightHeight) + 1;
        }

        private bool IsSorted(TreePoint<Card>? node)
        {
            return IsSorted(node, null, null);
        }

        private bool IsSorted(TreePoint<Card>? node, Card? min, Card? max)
        {
            if (node == null)
                return true;

            if ((min != null && node.Data.CompareTo(min) <= 0) || (max != null && node.Data.CompareTo(max) >= 0))
                return false;

            return IsSorted(node.Left, min, node.Data) && IsSorted(node.Right, node.Data, max);
        }
    }

    [TestClass]
    public class MyTreeTests04
    {
        [TestMethod]
        public void CopyTo_ShouldCopyElementsSuccessfully()
        {
            // Arrange
            Card[] cards = GenerateCardsArray(5);
            var tree = new MyTree<Card>(cards);
            Card[] targetArray = new Card[5];

            // Act
            tree.CopyTo(targetArray, 0);

            // Assert
            CollectionAssert.AreEqual(cards, targetArray);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyTo_ShouldThrowArgumentNullException_WhenArrayIsNull()
        {
            // Arrange
            Card[] cards = GenerateCardsArray(5);
            var tree = new MyTree<Card>(cards);

            // Act
            tree.CopyTo(null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyTo_ShouldThrowArgumentOutOfRangeException_WhenArrayIndexIsOutOfRange()
        {
            // Arrange
            Card[] cards = GenerateCardsArray(5);
            var tree = new MyTree<Card>(cards);
            Card[] targetArray = new Card[5];

            // Act
            tree.CopyTo(targetArray, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyTo_ShouldThrowArgumentOutOfRangeException_WhenArrayIndexIsTooHigh()
        {
            // Arrange
            Card[] cards = GenerateCardsArray(5);
            var tree = new MyTree<Card>(cards);
            Card[] targetArray = new Card[5];

            // Act
            tree.CopyTo(targetArray, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyTo_ShouldThrowArgumentException_WhenArrayIsTooSmall()
        {
            // Arrange
            Card[] cards = GenerateCardsArray(5);
            var tree = new MyTree<Card>(cards);
            Card[] targetArray = new Card[4];

            // Act
            tree.CopyTo(targetArray, 0);
        }

        private Card[] GenerateCardsArray(int length)
        {
            Card[] cards = new Card[length];
            for (int i = 0; i < length; i++)
            {
                cards[i] = new Card($"1234 5678 9012 {i:D4}", $"User {i}", $"{(i % 12 + 1):D2}/{30 + i % 20:D2}", i + 1);
            }
            Array.Sort(cards); // Ensure the array is sorted for balanced tree construction
            return cards;
        }
    }

    [TestClass]
    public class MyTreeTests05
    {
        [TestMethod]
        public void GetEnumerator_ShouldReturnElementsInOrder()
        {
            // Arrange
            Card[] cards = GenerateCardsArray(5);
            var tree = new MyTree<Card>(cards);

            // Act
            List<Card> result = new List<Card>();
            foreach (var card in tree)
            {
                result.Add(card);
            }

            // Assert
            CollectionAssert.AreEqual(cards, result);
        }

        [TestMethod]
        public void IEnumerable_GetEnumerator_ShouldReturnElementsInOrder()
        {
            // Arrange
            Card[] cards = GenerateCardsArray(5);
            var tree = new MyTree<Card>(cards);

            // Act
            List<Card> result = new List<Card>();
            IEnumerable enumerableTree = (IEnumerable)tree;
            foreach (var card in enumerableTree)
            {
                result.Add((Card)card);
            }

            // Assert
            CollectionAssert.AreEqual(cards, result);
        }

        private Card[] GenerateCardsArray(int length)
        {
            Card[] cards = new Card[length];
            for (int i = 0; i < length; i++)
            {
                cards[i] = new Card($"1234 5678 9012 {i:D4}", $"User {i}", $"{(i % 12 + 1):D2}/{30 + i % 20:D2}", i + 1);
            }
            Array.Sort(cards); // Ensure the array is sorted for balanced tree construction
            return cards;
        }
    }

    [TestClass]
    public class MyTreeTests06
    {
        [TestMethod]
        public void DeepCopy_ShouldCreateExactCopyOfTree()
        {
            // Arrange
            Card[] cards = GenerateCardsArray(5);
            var originalTree = new MyTree<Card>(cards);

            // Act
            MyTree<Card> copiedTree = originalTree.DeepCopy();

            // Assert
            Assert.AreEqual(originalTree.Count, copiedTree.Count);
            CollectionAssert.AreEqual(GetTreeElementsInOrder(originalTree), GetTreeElementsInOrder(copiedTree));
        }

        [TestMethod]
        public void DeepCopy_ShouldCreateIndependentCopy()
        {
            // Arrange
            Card[] cards = GenerateCardsArray(5);
            var originalTree = new MyTree<Card>(cards);
            MyTree<Card> copiedTree = originalTree.DeepCopy();

            // Act
            copiedTree.Remove(cards[0]);

            // Assert
            Assert.AreNotEqual(originalTree.Count, copiedTree.Count);
            CollectionAssert.AreEqual(GetTreeElementsInOrder(originalTree), cards); // Original tree remains unchanged
            CollectionAssert.DoesNotContain(GetTreeElementsInOrder(copiedTree), cards[0]); // Copied tree has one less element
        }

        private Card[] GenerateCardsArray(int length)
        {
            Card[] cards = new Card[length];
            for (int i = 0; i < length; i++)
            {
                cards[i] = new Card($"1234 5678 9012 {i:D4}", $"User {i}", $"{(i % 12 + 1):D2}/{30 + i % 20:D2}", i + 1);
            }
            Array.Sort(cards); // Ensure the array is sorted for balanced tree construction
            return cards;
        }

        private List<Card> GetTreeElementsInOrder(MyTree<Card> tree)
        {
            List<Card> elements = new List<Card>();
            foreach (var card in tree)
            {
                elements.Add(card);
            }
            return elements;
        }
    }

    [TestClass]
    public class MyTreeTests07
    {
        [TestMethod]
        public void Clear_ShouldRemoveAllElements()
        {
            // Arrange
            var tree = new MyTree<Card>(GenerateCardsArray(5));

            // Act
            tree.Clear();

            // Assert
            Assert.AreEqual(0, tree.Count);
            Assert.IsNull(tree.root);
        }

        [TestMethod]
        public void Contains_ShouldReturnTrueIfElementExists()
        {
            // Arrange
            var card = new Card("1234 5678 9012 0001", "User 1", "01/25", 100);
            var tree = new MyTree<Card>(new Card[] { card });

            // Act
            bool contains = tree.Contains(card);

            // Assert
            Assert.IsTrue(contains);
        }

        private Card[] GenerateCardsArray(int length)
        {
            Card[] cards = new Card[length];
            for (int i = 0; i < length; i++)
            {
                cards[i] = new Card($"1234 5678 9012 {i:D4}", $"User {i}", $"{(i % 12 + 1):D2}/{30 + i % 20:D2}", i + 1);
            }
            Array.Sort(cards); // Ensure the array is sorted for balanced tree construction
            return cards;
        }
    }

    [TestClass]
    public class MyTreeTests08
    {
        [TestMethod]
        public void Add_ShouldAddElementToTree()
        {
            // Arrange
            var tree = new MyTree<Card>();
            var card1 = new Card("1234 5678 9012 0001", "User 1", "01/25", 100);
            var card2 = new Card("1234 5678 9012 0002", "User 2", "02/25", 200);
            var card3 = new Card("1234 5678 9012 0003", "User 3", "03/25", 300);

            // Act
            tree.Add(card1);
            tree.Add(card2);
            tree.Add(card3);

            // Assert
            Assert.AreEqual(3, tree.Count);
            Assert.IsTrue(tree.Contains(card1));
            Assert.IsTrue(tree.Contains(card2));
            Assert.IsTrue(tree.Contains(card3));
        }


        [TestMethod]
        public void Add_ShouldMaintainBinarySearchTreeProperty()
        {
            // Arrange
            var tree = new MyTree<Card>();
            var card1 = new Card("1234 5678 9012 0001", "User 1", "01/25", 100);
            var card2 = new Card("1234 5678 9012 0002", "User 2", "02/25", 200);
            var card3 = new Card("1234 5678 9012 0003", "User 3", "03/25", 300);

            // Act
            tree.Add(card2); // Insert in random order
            tree.Add(card1);
            tree.Add(card3);

            // Assert
            Assert.AreEqual(3, tree.Count);

            // Check inorder traversal to ensure binary search tree property is maintained
            var cardsInOrder = new Card[] { card1, card2, card3 };
            int i = 0;
            foreach (var node in tree)
            {
                Assert.AreEqual(cardsInOrder[i], node);
                i++;
            }
        }

        [TestMethod]
        public void Add_ShouldHandleEmptyTree()
        {
            // Arrange
            var tree = new MyTree<Card>();
            var card = new Card("1234 5678 9012 0001", "User 1", "01/25", 100);

            // Act
            tree.Add(card);

            // Assert
            Assert.AreEqual(1, tree.Count);
            Assert.IsTrue(tree.Contains(card));
        }
    }

    [TestClass]
    public class MyTreeTests09
    {

        [TestMethod]
        public void MakeTree_ShouldCreateTreeWithSpecifiedLength()
        {
            // Arrange
            int length = 5;
            var tree = new MyTree<Card>(length);

            // Act

            // Assert
            Assert.AreEqual(length, tree.Count);
        }

        [TestMethod]
        public void CalculateAverage_ShouldCalculateCorrectAverage()
        {
            // Arrange
            var tree = new MyTree<Card>();
            var cards = new List<Card>
            {
                new Card("1234 5678 9012 0001", "User 1", "01/25", 100),
                new Card("1234 5678 9012 0002", "User 2", "02/25", 200),
                new Card("1234 5678 9012 0003", "User 3", "03/25", 300),
                new Card("1234 5678 9012 0004", "User 4", "04/25", 400),
                new Card("1234 5678 9012 0005", "User 5", "05/25", 500)
            };

            foreach (var card in cards)
            {
                tree.Add(card);
            }

            double expectedAverage = (100 + 200 + 300 + 400 + 500) / 5.0;

            // Act
            double actualAverage = tree.CalculateAverage();

            // Assert
            Assert.AreEqual(expectedAverage, actualAverage, 0.001);
        }

        [TestMethod]
        public void CreateSearchTree_ShouldCreateSearchTreeFromCurrentTree()
        {
            // Arrange
            var tree = new MyTree<Card>();
            var cards = new List<Card>
            {
                new Card("1234 5678 9012 0001", "User 1", "01/25", 100),
                new Card("1234 5678 9012 0002", "User 2", "02/25", 200),
                new Card("1234 5678 9012 0003", "User 3", "03/25", 300),
                new Card("1234 5678 9012 0004", "User 4", "04/25", 400),
                new Card("1234 5678 9012 0005", "User 5", "05/25", 500)
            };

            foreach (var card in cards)
            {
                tree.Add(card);
            }

            // Act
            var searchTree = tree.CreateSearchTree();

            // Assert
            Assert.AreEqual(tree.Count, searchTree.Count);

            foreach (var card in cards)
            {
                Assert.IsTrue(searchTree.Contains(card));
            }
        }
    }

    [TestClass]
    public class MyTreeTests
    {
        [TestMethod]
        public void PrintTree_ShouldPrintTree()
        {
            // Arrange
            var tree = new MyTree<Card>();
            var cards = new List<Card>
            {
                new Card("1234 5678 9012 0001", "User 1", "01/25", 100),
                new Card("1234 5678 9012 0002", "User 2", "02/25", 200),
                new Card("1234 5678 9012 0003", "User 3", "03/25", 300),
                new Card("1234 5678 9012 0004", "User 4", "04/25", 400),
                new Card("1234 5678 9012 0005", "User 5", "05/25", 500)
            };

            foreach (var card in cards)
            {
                tree.Add(card);
            }

            // Act
            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            tree.PrintTree();
            string printedTree = stringWriter.ToString();

            // Assert
            Assert.IsNotNull(printedTree);
            Console.SetOut(Console.Out);
        }
    }
}
