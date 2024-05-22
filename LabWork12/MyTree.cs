using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12
{
    internal class MyTree<T> where T : IInit, ICloneable, IComparable, new()
    {
        private TreePoint<T>? root = null;  // Корень
        private int count = 0;  // Счетчик кол-ва элементов

        public int Count => count;  // Метод вывода кол-ва элементов
        public TreePoint<T>? Root => root;  // Публичное свойство для получения корня

        public MyTree(int length)
        {
            count = length;
            root = MakeTree(length, root);
        }

        private MyTree(TreePoint<T>? root, int count)
        {
            this.root = root;
            this.count = count;
        }

        public void PrintTree()
        {
            Print(root);
        }

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

        public void ChangeTreeData()
        {
            ChangeNodeData(root);
        }

        private void ChangeNodeData(TreePoint<T>? node)
        {
            if (node != null)
            {
                node.Data.RandomInit();
                ChangeNodeData(node.Left);
                ChangeNodeData(node.Right);
            }
        }

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

        private void Print(TreePoint<T>? point, int spaces = 5)
        {
            if (point != null)
            {
                Print(point.Left, spaces + 5);

                for (int i = 0; i < spaces; i++) Console.Write(" ");
                Console.WriteLine(point.Data);

                Print(point.Right, spaces + 5);
            }
        }

        public double CalculateAverage()
        {
            if (root == null) return 0;
            (double sum, int count) = CalculateSumAndCount(root);
            return sum / count;
        }

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

        public void DeleteTree()
        {
            DeleteNode(root);
            root = null;
            count = 0;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

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

        public MyTree<T> CreateSearchTree()
        {
            List<T> elements = new List<T>();
            InOrderTraversal(root, elements);

            MyTree<T> searchTree = new MyTree<T>(0);
            foreach (var element in elements)
            {
                searchTree.AddPoint(element);
            }

            return searchTree;
        }

        private void InOrderTraversal(TreePoint<T>? node, List<T> elements)
        {
            if (node == null) return;

            InOrderTraversal(node.Left, elements);
            elements.Add(node.Data);
            InOrderTraversal(node.Right, elements);
        }

        public void TransformToFindTree()
        {
            List<T> elements = new List<T>();
            InOrderTraversal(root, elements);

            root = null;
            count = 0;
            foreach (var element in elements)
            {
                AddPoint(element);
            }
        }


        // Метод удаления опр. элемента из Дерева поиска
        public void Delete(T key)
        {
            // Вызываем приватный метод Delete, начиная с корня дерева
            root = Delete(root, key);
        }

        // Метод удаления опр. элемента из Дерева поиска - Вспомогательный
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
                // Если ключ меньше ключа текущего узла, идем в левое поддерево
                node.Left = Delete(node.Left, key);
            }
            else if (key.CompareTo(node.Data) > 0)
            {
                // Если ключ больше ключа текущего узла, идем в правое поддерево
                node.Right = Delete(node.Right, key);
            }
            else
            {
                // Узел найден, начинаем его удаление
                // Узел с одним или без детей
                if (node.Left == null)
                {
                    // Если у узла нет левого ребенка, возвращаем правого ребенка (или null)
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    // Если у узла нет правого ребенка, возвращаем левого ребенка
                    return node.Left;
                }

                // Узел с двумя детьми: найдем наименьший элемент в правом поддереве
                node.Data = MinValue(node.Right);

                // Удалить найденный элемент из правого поддерева
                node.Right = Delete(node.Right, node.Data);
            }

            // Возвращаем измененный узел
            return node;
        }

        // Метод поиска минимального элемента
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


        public void DeleteTreeFind()
        {
            // Вызываем вспомогательный метод для удаления всех узлов дерева
            DeleteNodeTreefind(root);

            // Устанавливаем корень в null и сбрасываем счетчик элементов
            root = null;
            count = 0;

            // Вызываем сборщик мусора для освобождения памяти
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

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