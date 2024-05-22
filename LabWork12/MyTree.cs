using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12
{
    internal class MyTree<T> where T: IInit, ICloneable, IComparable, new()
    {
        TreePoint<T>? root = null;  // Корень

        int count = 0;  // Счетчик  кол-во эл

        public int Count => count;  // Метод вывода кол-ва эл.
        public TreePoint<T>? Root => root;  // Публичное свойство для получения корня


        // Конструктор - ( Параметр - длина )
        public MyTree(int length)
        {
            count = length;
            root = MakeTree(length, root);
        }

        // Конструктор для создания дерева из корня
        private MyTree(TreePoint<T>? root, int count)
        {
            this.root = root;
            this.count = count;
        }

        public void PrintTree()
        {
            Print(root);
        }

        // Метод глубокого копирования
        public MyTree<T> DeepCopy()
        {
            TreePoint<T>? newRoot = DeepCopyTreePoint(root);
            return new MyTree<T>(newRoot, count);
        }
         // Метод глубокого копирования узла списка
        private TreePoint<T>? DeepCopyTreePoint(TreePoint<T>? point)
        {
            if (point == null) return null;

            TreePoint<T> newPoint = new TreePoint<T>((T)point.Data.Clone());
            newPoint.Left = DeepCopyTreePoint(point.Left);
            newPoint.Right = DeepCopyTreePoint(point.Right);

            return newPoint;
        }

        // Метод для изменения данных дерева
        public void ChangeTreeData()
        {
            ChangeNodeData(root);
        }

        // Метод для изменения данных дерева - ( Вспомогательный )
        private void ChangeNodeData(TreePoint<T>? node)
        {
            if (node != null)
            {
                node.Data.RandomInit(); // Генерируем новые случайные данные для узла
                ChangeNodeData(node.Left);
                ChangeNodeData(node.Right);
            }
        }

        // ИСД - Вспомогательный метод - ( Вспомогательный )
        TreePoint<T>? MakeTree (int length, TreePoint<T>? point)
        {
            T data = new T();   // Создаём новый эл.
            data.RandomInit();  // Запонляем ДСЧ
            TreePoint<T> newItem = new TreePoint<T>(data);  // Создаём новый узел
            if (length == 0) { return null; }   // Проверка на пустоту
            int nl = length / 2;        // nl - кол-во узлов слева
            int nr = length - nl - 1;   // nr - кол-во узлов справа
            newItem.Left = MakeTree(nl, newItem.Left);      // Заполняем данные в корне
            newItem.Right = MakeTree(nl, newItem.Right);    // Заполняем данные в корне
            return newItem;
        }
        
        // Метод - Вывод дерева - ( Вспомогательный )
        private void Print(TreePoint<T>? point, int spaces = 5)  // Корень дерева и кол-во сдвигов
        {
            if (point != null)  // Корень != null
            {
                Print(point.Left, spaces + 5);   // Печать левого поддерева + 5 пробелов

                // Печать элемента
                for (int i = 0; i < spaces; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(point.Data);
                Print(point.Right, spaces + 5);
            }
        }

        // Метод для вычисления среднего арифметического значений num.number
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

            double nodeValue = ((Card)(object)node.Data).num.number; // Преобразование типа T к Card и получение значения num.number

            double totalSum = leftSum + rightSum + nodeValue;
            int totalCount = leftCount + rightCount + 1;

            return (totalSum, totalCount);
        }

        // Метод для полного удаления дерева
        public void DeleteTree()
        {
            DeleteNode(root);
            root = null;
            count = 0;
            GC.Collect(); // Принудительный вызов сборщика мусора
            GC.WaitForPendingFinalizers(); // Ожидание завершения финализации
        }

        // Метод для полного удаления узла дерева
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

        // Дерево поиска - Добавление элемента
        void AddPoint(T data)
        {
            TreePoint<T>? point = root;
            TreePoint<T>? current = null;
            bool isExist = false;
            while(point != null && !isExist)
            {
                current = point;
                if (point.Data.CompareTo(data) == 0)
                {
                    isExist = true;
                }
                else
                {
                    if (point.Data.CompareTo(data) < 0)
                    {
                        point = point.Left;
                    }
                    else    // Ищем место
                    {
                        if (point.Data.CompareTo(data) < 0)
                        {
                            point = point.Left;
                        }
                        else
                        {
                            point = point.Right;
                        }
                    }
                }
                // Нашли место
                if (isExist)
                {
                    return; // Ничего не добавили
                }
                TreePoint<T> newPoint = new TreePoint<T>(data);
                if (current.Data.CompareTo(data) < 0)
                {
                    current.Left = newPoint;
                }
                else
                {
                    current.Right = newPoint;
                }
            }
        }

        // Метод для создания дерева поиска из идеально сбалансированного дерева
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

        // Вспомогательный метод для in-order обхода дерева и сохранения элементов в список
        private void InOrderTraversal(TreePoint<T>? node, List<T> elements)
        {
            if (node == null) return;

            InOrderTraversal(node.Left, elements);
            elements.Add(node.Data);
            InOrderTraversal(node.Right, elements);
        }

        // Метод для добавления элемента в дерево поиска
        private void AddPointTreeFind(T data)
        {
            TreePoint<T>? newPoint = new TreePoint<T>(data);
            if (root == null)
            {
                root = newPoint;
            }
            else
            {
                TreePoint<T>? current = root;
                TreePoint<T>? parent = null;

                while (current != null)
                {
                    parent = current;
                    if (data.CompareTo(current.Data) < 0)
                    {
                        current = current.Left;
                    }
                    else
                    {
                        current = current.Right;
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
            }

            count++;
        }

        // Метод перехода от идеал. сбалан. БД к дереву Поиска
        public void TransformToFindTree()
        {
            T[] array = new T[count];
            int current = 0;
            TransformArray(root, array, ref current);

            root = new TreePoint<T>(array[0]);
            count = 0;
            for (int i = 0; i < array.Length; i++)
            {
                AddPoint(array[i]);
            }
        }

        // Метод перехода от идеал. сбалан. БД к дереву Поиска - Вспомогательный
        private void TransformArray(TreePoint<T>? point, T[] array, ref int current)
        {
            if (point != null)  // Проверка корня на пустоту
            {
                TransformArray(point.Left, array, ref current);     // Идём в левое поддерево
                array[current] = point.Data;    // Записываем информацию
                current++;
                TransformArray(point.Right, array, ref current);    // Идём в правые подъузел - повторяем дейтвия
            }
        }

    }
}
