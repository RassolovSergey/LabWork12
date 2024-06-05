using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12
{
    public class MyTree<T> where T : IInit, ICloneable, IComparable, ISummable,  new()
    {
        public T[] Collection { get; }
        public TreePoint<T>? root = null;  // Корень
        private int count = 0;              // Счетчик кол-ва элементов ИСБД
        private int countFindTree = 0;      // Счетчик кол-ва элементов Дерева поиска

        public int Count => count;  // Метод вывода кол-ва элементов
        public int CountFindTree => countFindTree;  // Метод вывода кол-ва элементов Дерева поиска
        public TreePoint<T>? Root => root;  // Публичное свойство для получения корня

        // Конструктор: без параметра
        public MyTree()
        {
            root = null;
            count = 0;
        }

        // Конструктор ( Параметр - Длина )
        public MyTree(int length)
        {
            count = length;
            root = MakeTree(length);
        }


        public MyTree(T[] collection)
        {
            Collection = collection;
        }


        // Приватный конструктор глубокого копирования
        private MyTree(TreePoint<T>? root, int count)
        {
            this.root = root;
            this.count = count;
        }

        // Метод для создания глубокой копии дерева
        public MyTree<T> DeepCopy()
        {
            TreePoint<T>? newRoot = DeepCopyTreePoint(root);
            return new MyTree<T>(newRoot, count);
        }

        // Метод глубокого копирования
        private TreePoint<T>? DeepCopyTreePoint(TreePoint<T>? point)
        {
            // Если текущий узел null, возвращаем null
            if (point == null) return null;

            // Клонируем данные текущего узла, используя интерфейс ICloneable
            T clonedData = (T)((ICloneable)point.Data).Clone();

            // Создаем новый узел дерева с клонированными данными
            TreePoint<T> newPoint = new TreePoint<T>(clonedData);

            // Рекурсивно создаем глубокие копии левого и правого поддеревьев
            newPoint.Left = DeepCopyTreePoint(point.Left);
            newPoint.Right = DeepCopyTreePoint(point.Right);


            // Возвращаем новый узел - Глубокую копию со всеми потомками
            return newPoint;
        }



        // Метод - Печати
        public void PrintTree()
        {
            Print(root);
        }

        // Приватный Метод - Печати
        public void Print(TreePoint<T>? point, int spaces = 5)
        {
            // Если текущий узел не null, выполняем печать
            if (point != null)
            {
                // Рекурсивно печатаем левое поддерево, увеличивая отступы
                Print(point.Left, spaces + 5);

                // Печатаем отступы для визуализации уровня дерева
                for (int i = 0; i < spaces; i++) Console.Write(" ");
                // Печатаем данные текущего узла
                Console.WriteLine(point.Data);

                // Рекурсивно печатаем правое поддерево, увеличивая отступы
                Print(point.Right, spaces + 5);
            }
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
        private TreePoint<T>? MakeTree(int length)
        {
            // Если длина равна 0, возвращаем null (пустое поддерево)
            if (length == 0) return null;

            T data = new T();   // Создаем новый объект типа T
            data.RandomInit();  // Заполняем объект случайными данными
            TreePoint<T> newItem = new TreePoint<T>(data);  // Создаем новый узел дерева с данными

            // Вычисляем количество элементов в левом и правом поддеревьях
            int nl = length / 2;        // Левое поддерево будет половиной длины
            int nr = length - nl - 1;   // Правое поддерево будет длиной минус левое поддерево и текущий узел 

            // Рекурсивно строим левое поддерево
            newItem.Left = MakeTree(nl);

            // Рекурсивно строим правое поддерево
            newItem.Right = MakeTree(nr);

            // Возвращаем созданный узел со всеми его поддеревьями
            return newItem;
        }



        // Метод для вычисления среднего значения всех элементов в дереве
        public double CalculateAverage()
        {
            if (root == null) return 0;

            double sum = 0;
            int count = 0;
            CalculateSumAndCount(root, ref sum, ref count);

            return sum / count;
        }

        // Метод для вычисления суммы всех элементов и их количества в дереве
        private void CalculateSumAndCount(TreePoint<T>? node, ref double sum, ref int count)
        {
            if (node == null) return;

            CalculateSumAndCount(node.Left, ref sum, ref count);
            CalculateSumAndCount(node.Right, ref sum, ref count);

            double nodeValue = node.Data.GetValueForSum();
            sum += nodeValue;
            count++;
        }



        // Метод для удаления дерева из памяти
        public void DeleteTree()
        {
            DeleteNode(root);   // Удаляем все узлы дерева, начиная с корня
            root = null;        // Приравниваем корень к null
            count = 0;          // Сбрасываем счетчик
            GC.Collect();       // Запускаем сборщик мусора
            GC.WaitForPendingFinalizers();  // Ожидаем завершения всех финализаторов, чтобы гарантировать полное освобождение памяти
        }

        // Приватный метод для рекурсивного удаления узлов дерева
        private void DeleteNode(TreePoint<T>? node)
        {
            // Проверяем, что текущий узел не равен null
            if (node != null)
            {
                // Рекурсивно удаляем левое поддерево
                DeleteNode(node.Left);

                // Рекурсивно удаляем правое поддерево
                DeleteNode(node.Right);

                // Освобождаем данные текущего узла
                node.Data = default(T);

                // Обнуляем ссылки на левый и правый дочерние узлы
                node.Left = null;
                node.Right = null;
            }
        }



        // Метод для добавления нового узла в дерево
        public void AddPoint(T data)
        {
            // Создаем новый узел с заданными данными
            TreePoint<T> newPoint = new TreePoint<T>(data);

            // Если дерево пустое, новый узел становится корнем
            if (root == null)
            {
                root = newPoint;
                count++; // Увеличиваем счетчик элементов дерева
                return;
            }

            // Инициализируем текущий узел корнем дерева
            TreePoint<T>? current = root;
            TreePoint<T>? parent = null;

            // Ищем подходящее место для нового узла
            while (current != null)
            {
                parent = current;

                // Если данные нового узла меньше данных текущего узла, идем в левое поддерево
                if (data.CompareTo(current.Data) < 0)
                {
                    current = current.Left;
                }
                // Если данные нового узла больше данных текущего узла, идем в правое поддерево
                else if (data.CompareTo(current.Data) > 0)
                {
                    current = current.Right;
                }
                // Если данные нового узла равны данным текущего узла, узел не добавляется (дубликаты не разрешены)
                else
                {
                    return;
                }
            }

            // Вставляем новый узел в найденное место
            if (data.CompareTo(parent.Data) < 0)
            {
                parent.Left = newPoint;
            }
            else
            {
                parent.Right = newPoint;
            }

            // Увеличиваем счетчик элементов дерева
            count++;
        }

        
        
        
        // Метод для создания дерева поиска из текущего дерева
        public MyTree<T> CreateSearchTree()
        {
            // Создаем список для хранения элементов дерева
            List<T> elements = new List<T>();

            // Выполняем обход в порядке возрастания значений и сохраняем элементы в список
            InOrderTraversal(root, elements);

            // Создаем новое дерево поиска
            MyTree<T> searchTree = new MyTree<T>(0);

            // Добавляем каждый элемент из списка в новое дерево поиска
            foreach (var element in elements)
            {
                searchTree.AddPoint(element);
            }

            // Возвращаем новое дерево поиска
            countFindTree = count;
            return searchTree;
        }

        // Приватный метод для обхода дерева в порядке возрастания значений и сохранения элементов в список
        private void InOrderTraversal(TreePoint<T>? node, List<T> elements)
        {
            if (node == null) { return; }

            InOrderTraversal(node.Left, elements);   // Рекурсивный обход левого поддерева
            elements.Add(node.Data);                 // Добавление значения текущего узла в список
            InOrderTraversal(node.Right, elements);  // Рекурсивный обход правого поддерева
        }


        // Метод для удаления определенного элемента из дерева поиска
        public bool Delete(T key)
        {
            TreePoint<T>? result = Delete(root, key); // Вызываем вспомогательный метод

            if (result != null)
            {
                root = result;       // Присваиваем новый корень, если он изменился
                countFindTree--;     // Уменьшаем счетчик элементов дерева
                return true;         // Возвращаем успешное завершение операции удаления
            }

            return false;            // Возвращаем неудачное завершение операции удаления
        }

        // Вспомогательный метод для удаления определенного элемента из дерева поиска
        public TreePoint<T>? Delete(TreePoint<T>? node, T key)
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

            return node;
        }

        // Метод для поиска минимального элемента в дереве
        public T MinValue(TreePoint<T>? node)
        {
            // Инициализируем переменную для хранения минимального значения
            T minv = node.Data!;
            // Проходим по левым потомкам до конца, чтобы найти самый левый узел
            while (node.Left != null)
            {
                minv = node.Left.Data!;
                node = node.Left;
            }
            // Возвращаем минимальное значение
            return minv;
        }

        public T? Find(T data)
        {
            TreePoint<T>? parent;
            TreePoint<T>? result = Find(data, out parent);
            return result.Data;
        }

        private TreePoint<T>? Find(T data, out TreePoint<T>? parent)
        {
            parent = null;
            TreePoint<T>? current = root;

            while (current != null)
            {
                int cmp = data.CompareTo(current.Data);
                if (cmp == 0)
                {
                    return current;
                }
                else if (cmp < 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else
                {
                    parent = current;
                    current = current.Right;
                }
            }

            return null;
        }

    }
}