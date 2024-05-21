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
    }
}
