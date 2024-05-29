using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12
{
    public class MyTree<T> where T : IInit, ICloneable, IComparable,  new()
    {
        private TreePoint<T>? root = null;  // Корень
        private int count = 0;  // Счетчик кол-ва элементов

        public int Count => count;  // Метод вывода кол-ва элементов
        public TreePoint<T>? Root => root;  // Публичное свойство для получения корня

        // Конструктор, создающий дерево заданной длины и заполняющий его случайными данными
        public MyTree(int length)
        {
            count = length;
            root = MakeTree(length, root);
        }

        // Приватный конструктор для создания глубокой копии дерева
        private MyTree(TreePoint<T>? root, int count)
        {
            this.root = root;
            this.count = count;
        }

        // Метод для печати дерева
        public void PrintTree()
        {
            Print(root);
        }

        // Метод для создания глубокой копии дерева
        public MyTree<T> DeepCopy()
        {
            TreePoint<T>? newRoot = DeepCopyTreePoint(root);
            return new MyTree<T>(newRoot, count);
        }

        private TreePoint<T>? DeepCopyTreePoint(TreePoint<T>? point)
        {
            if (point == null) return null;

            TreePoint<T> newPoint = new TreePoint<T>((T)point.Data.Clone());
            newPoint.Left = DeepCopyTreePoint(point.Left);
            newPoint.Right = DeepCopyTreePoint(point.Right);

            return newPoint;
        }

        // Метод для изменения данных в каждом узле дерева
        public void ChangeTreeData()
        {
            ChangeNodeData(root);
        }

        // Приватный метод для изменения данных в каждом узле дерева
        public void ChangeNodeData(TreePoint<T>? node)
        {
            if (node != null)
            {
                node.Data.RandomInit();
                ChangeNodeData(node.Left);
                ChangeNodeData(node.Right);
            }
        }

        // Приватный метод для создания дерева определенной длины
        private TreePoint<T>? MakeTree(int length, TreePoint<T>? point)
        {
            if (length == 0) return null;

            T data = new T();
            data.RandomInit();
            TreePoint<T> newItem = new TreePoint<T>(data);

            int nl = length / 2;
            int nr = length - nl - 1;
            newItem.Left = MakeTree(nl, newItem.Left);
            newItem.Right = MakeTree(nr, newItem.Right);
            return newItem;
        }

        // Приватный метод для печати дерева с указанием отступов между уровнями
        public void Print(TreePoint<T>? point, int spaces = 5)
        {
            if (point != null)
            {
                Print(point.Left, spaces + 5);

                for (int i = 0; i < spaces; i++) Console.Write(" ");
                Console.WriteLine(point.Data);

                Print(point.Right, spaces + 5);
            }
        }

        // Метод для вычисления среднего значения всех элементов в дереве
        public double CalculateAverage()
        {
            if (root == null) return 0;
            (double sum, int count) = CalculateSumAndCount(root);
            return sum / count;
        }

        // Приватный метод для вычисления суммы всех элементов и их количества в дереве
        private (double, int) CalculateSumAndCount(TreePoint<T>? node)
        {
            if (node == null) return (0, 0);

            (double leftSum, int leftCount) = CalculateSumAndCount(node.Left);
            (double rightSum, int rightCount) = CalculateSumAndCount(node.Right);

            double nodeValue = ((Card)(object)node.Data).num.number;

            double totalSum = leftSum + rightSum + nodeValue;
            int totalCount = leftCount + rightCount + 1;

            return (totalSum, totalCount);
        }

        // Метод для удаления дерева из памяти
        public void DeleteTree()
        {
            DeleteNode(root);
            root = null;
            count = 0;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        // Приватный метод для рекурсивного удаления узлов дерева
        private void DeleteNode(TreePoint<T>? node)
        {
            if (node != null)
            {
                DeleteNode(node.Left);
                DeleteNode(node.Right);
                node.Data = default(T);
                node.Left = null;
                node.Right = null;
            }
        }

        // Метод для добавления нового узла в дерево
        public void AddPoint(T data)
        {
            TreePoint<T> newPoint = new TreePoint<T>(data);
            if (root == null)
            {
                root = newPoint;
                count++;
                return;
            }

            TreePoint<T>? current = root;
            TreePoint<T>? parent = null;
            while (current != null)
            {
                parent = current;
                if (data.CompareTo(current.Data) < 0)
                {
                    current = current.Left;
                }
                else if (data.CompareTo(current.Data) > 0)
                {
                    current = current.Right;
                }
                else
                {
                    return;
                }
            }

            if (data.CompareTo(parent.Data) < 0)
            {
                parent.Left = newPoint;
            }
            else
            {
                parent.Right = newPoint;
            }

            count++;
        }

        // Метод для создания дерева поиска из текущего дерева
        public MyTree<T> CreateSearchTree()
        {
            // Создаем список для хранения элементов дерева
            List<T> elements = new List<T>();

            // Выполняем обход в порядке возрастания значений (in-order traversal) и сохраняем элементы в список
            InOrderTraversal(root, elements);

            // Создаем новое дерево поиска
            MyTree<T> searchTree = new MyTree<T>(0);

            // Добавляем каждый элемент из списка в новое дерево поиска
            foreach (var element in elements)
            {
                searchTree.AddPoint(element);
            }

            // Возвращаем новое дерево поиска
            return searchTree;
        }

        // Приватный метод для обхода дерева в порядке возрастания значений и сохранения элементов в список
        private void InOrderTraversal(TreePoint<T>? node, List<T> elements)
        {
            if (node == null) return;

            InOrderTraversal(node.Left, elements);   // Рекурсивный обход левого поддерева
            elements.Add(node.Data);                 // Добавление значения текущего узла в список
            InOrderTraversal(node.Right, elements);  // Рекурсивный обход правого поддерева
        }

        // Метод для преобразования текущего дерева в дерево поиска
        public void TransformToFindTree()
        {
            // Создаем список для хранения элементов дерева
            List<T> elements = new List<T>();

            // Выполняем обход в порядке возрастания значений (in-order traversal) и сохраняем элементы в список
            InOrderTraversal(root, elements);

            // Очищаем текущее дерево
            root = null;
            count = 0;

            // Добавляем каждый элемент из списка в текущее дерево (уже в виде дерева поиска)
            foreach (var element in elements)
            {
                AddPoint(element);
            }
        }

        // Метод для удаления определенного элемента из дерева поиска
        public void Delete(T key)
        {
            // Вызываем приватный метод Delete, начиная с корня дерева
            root = Delete(root, key);
        }

        // Вспомогательный метод для удаления определенного элемента из дерева поиска
        private TreePoint<T>? Delete(TreePoint<T>? node, T key)
        {
            // Если дерево пустое или узел для удаления не найден, возвращаем узел без изменений
            if (node == null)
            {
                return node;
            }

            // Рекурсивно ищем узел для удаления
            if (key.CompareTo(node.Data) < 0)
            {
                node.Left = Delete(node.Left, key);     // Рекурсивное удаление в левом поддереве
            }
            else if (key.CompareTo(node.Data) > 0)
            {
                node.Right = Delete(node.Right, key);   // Рекурсивное удаление в правом поддереве
            }
            else
            {
                // Узел для удаления найден
                if (node.Left == null)
                {
                    return node.Right;                  // Если у узла нет левого потомка, возвращаем правого потомка
                }
                else if (node.Right == null)
                {
                    return node.Left;                   // Если у узла нет правого потомка, возвращаем левого потомка
                }

                // Узел для удаления имеет обоих потомков
                node.Data = MinValue(node.Right);       // Находим минимальное значение в правом поддереве
                node.Right = Delete(node.Right, node.Data); // Удаляем найденное минимальное значение из правого поддерева
            }

            // Возвращаем измененный узел
            return node;
        }

        // Метод для поиска минимального элемента в дереве
        private T MinValue(TreePoint<T>? node)
        {
            // Находим наименьший элемент в дереве (самый левый элемент)
            T minv = node.Data!;
            while (node.Left != null)
            {
                minv = node.Left.Data!;
                node = node.Left;
            }
            return minv;
        }

        // Метод для удаления всего дерева поиска
        public void DeleteTreeFind()
        {
            // Рекурсивно удаляем все узлы дерева
            DeleteNodeTreefind(root);

            // Устанавливаем корень в null и сбрасываем счетчик элементов
            root = null;
            count = 0;

            // Вызываем сборщик мусора для освобождения памяти
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        // Вспомогательный метод для удаления всех узлов дерева
        private void DeleteNodeTreefind(TreePoint<T>? node)
        {
            // Рекурсивно обходим все узлы дерева и удаляем их
            if (node != null)
            {
                // Удаляем левое поддерево
                DeleteNodeTreefind(node.Left);
                // Удаляем правое поддерево
                DeleteNodeTreefind(node.Right);
                // Освобождаем данные узла и обнуляем ссылки на дочерние узлы
                node.Data = default(T);
                node.Left = null;
                node.Right = null;
            }
        }
    }
}