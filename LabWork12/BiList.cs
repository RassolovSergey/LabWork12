
using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12
{
    // Обобщённый класс двунаправленного списка
    internal class BiList<T> where T : IInit, ICloneable, new()
    {
        private PointBiList<T> beg;      // Начальный узел списка
        private PointBiList<T> end;      // Конечный узел списка
        private int count = 0;      // Счетчик элементов
        public int Count => count;  // Функция чтения переменной count

        // Конструктор без параметров: Пустой список
        public BiList()
        {
            beg = null;     // Инициализация начала списка как null
            end = null;     // Инициализация конца списка как null
        }
        // Конструктор с параметром:    Только информация
        public BiList(T data) { }

        // Конструктор, инициализирующий список случайными элементами
        public BiList(int size)
        {
            if (size <= 0) throw new ArgumentException("Размер должен быть больше нуля.", nameof(size));
            for (int i = 0; i < size; i++)
            {
                AddToEnd(MakeRandomData());
            }
        }

        // Метод для создания списка из данных, введенных вручную
        public void CreateListInit(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException("Размер должен быть больше нуля.", nameof(size));
            }

            Console.WriteLine($"Введите {size} элементов для списка:");
            for (int i = 0; i < size; i++)
            {
                Console.Write($"Элемент {i + 1}: ");
                // Считываем данные с консоли и добавляем их в список
                T inputData = new T();  // Создаем новый объект
                inputData.Init();       // Заполняем его вручную    
                AddToEnd(inputData);    // Добавляем объект в конец списка
            }
        }

        // Создание радномного объекта:     data
        public T MakeRandomData()
        {
            T data = new T();   // Конструктор без параметров
            data.RandomInit();  // Заполнение ДСЧ
            return data;        // Возвращаем значение нового объека
        }

        // Создание радномного объекта + ссылки
        public BiList<T> MakeRandomItem()
        {
            T data = new T();               // Конструктор без параметров
            data.RandomInit();              // Заполнение ДСЧ
            return new BiList<T>(data); // Возвращаем новый узел
        }

        // Создание глубокой копии
        public BiList<T> DeepClone()
        {
            BiList<T> newList = new BiList<T>();    // Создание нового пустого списка
            PointBiList<T> current = beg;           // Указатель на начало исходного списка

            // Перебор всех элементов исходного списка
            while (current != null)
            {
                // Клонирование данных текущего узла и добавление их в новый список
                T clonedData = (T)current.Data.Clone();
                newList.AddToEnd(clonedData);

                // Переход к следующему узлу в исходном списке
                current = current.Next;
            }
            return newList;     // Возвращаем глубокую копию списка
        }

        // Метод для добавления нового узла после узла с указанным значением данных.
        public void AddAfter(T targetData, T newData)
        {
            PointBiList<T> current = beg;   // Инициализация переменной current начальным значением beg 
            while (current != null)         // Цикл будет выполняться, пока текущий элемент не станет null
            {
                if (current.Data.Equals(targetData))
                {
                    PointBiList<T> newPoint = new PointBiList<T>(newData);   // Создание нового узла с указанными данными.
                    newPoint.Next = current.Next;    // Установка ссылки на следующий элемент нового узла равной ссылке на следующий элемент текущего узла.
                    newPoint.Prev = current;         // Установка ссылки на предыдущий элемент нового узла равной текущему узлу.
                    if (current.Next != null)       // Проверка, существует ли следующий элемент после текущего узла.
                    {
                        current.Next.Prev = newPoint;// Если да, то установка ссылки на предыдущий элемент следующего узла равной новому узлу.
                    }
                    current.Next = newPoint;     // Установка ссылки на следующий элемент текущего узла равной новому узлу.
                    if (current == end)         // Проверка, является ли текущий узел последним узлом списка.
                    {
                        end = newPoint;          // Если да, то обновление конца списка, установкой ссылки на новый узел.
                    }
                    count++;    // Увеличение счетчика элементов списка.
                    break;      // Выход из цикла, так как узел был добавлен после текущего узла.
                }
                current = current.Next;
            }
        }


        // Метод для добавления нового узла в конец списка
        public void AddToEnd(T data)
        {
            PointBiList<T> newNode = new PointBiList<T>(data);
            if (beg == null)
            {
                beg = newNode;
                end = newNode;
            }
            else
            {
                end.Next = newNode;
                newNode.Prev = end;
                end = newNode;
            }
            count++;  // Увеличение счетчика
        }

        // Метод поиска элемента списка
        public void FindItem(T data)
        {
            PointBiList<T> current = beg;   // Создаем переменную и ставим её в начало списка
            while (current != null)     // Пока она не равна null ( До конца списка )
            {
                // 
                if (current.Data.Equals(data)) // Сравниваем data и current.Data
                {
                    Console.WriteLine($"Элемент найден: {current.Data}");
                    return;
                }
                current = current.Next; // Переход а следующий элемент списка
            }
            Console.WriteLine("Элемент не найден");
        }


        // Печать списка
        public void PrintList()
        {
            if (beg == end) { throw new Exception("Ваш список пуст!"); }
            PointBiList<T> current = beg;   // Назначает текущий узел - первым в списке
            while (current != null)         // Перебираем все элементы списка, пока он не кончится
            {
                Console.WriteLine(current.Data);    // Выводит информацию текущего
                current = current.Next;             // Записываем в текущий следующий объект
            }
        }

        // Метод удаления узла
        private void Remove(PointBiList<T> nodeToRemove) 
        {
            // Проверка, первым элементом списка?
            if (nodeToRemove == beg)  
            {
                beg = nodeToRemove.Next; // Обновляем начало списка, указывая на следующий элемент

                // Проверяем, не является ли новое начало списка null.
                if (beg != null)
                {
                    beg.Prev = null; // Обнуляем ссылку на предыдущий элемент для нового начала списка
                }
            }
            else
            {
                nodeToRemove.Prev.Next = nodeToRemove.Next; // Обновляем ссылку на следующий элемент предыдущего узла так, чтобы она указывала на следующий элемент узла для удаления.
                if (nodeToRemove.Next != null) // Проверяем, существует ли следующий элемент узла для удаления.
                {
                    nodeToRemove.Next.Prev = nodeToRemove.Prev; // Если да, то обновляем ссылку на предыдущий элемент следующего узла, чтобы она указывала на предыдущий элемент узла для удаления.
                }
                if (nodeToRemove == end) // Проверяем, является ли узел для удаления последним элементом списка.
                {
                    end = nodeToRemove.Prev; // Если да, то обновляем конец списка, указывая на предыдущий элемент узла для удаления.
                }
            }
            count--; // Уменьшаем счетчик элементов списка.
        }

        // Метод удаления каждого четного элемента из коллекции
        public void RemoveEven()
        {
            int index = 0;                  // Порядковый номер текущего элемента в коллекции
            PointBiList<T> current = beg;   // Инициализация current - текущий объект, ставим его в начало
            while (current != null)         // Перебор всего списка
            {
                PointBiList<T> next = current.Next; // Инициализация переменной next значением ссылки на следующий элемент коллекции.
                if (index % 2 == 0)     // Проверка четности
                {
                    Remove(current);    // Удаляем узел
                }
                index++;        // Переход на следующий индекс (Следующий эллемент)
                current = next; // Переменная current присваивает значение next для перехода к следующему элементу коллекции.
            }
        }


        // Отчистка списка
        public void RemoveAll()
        {
            beg = null;
            end = null;
            count = 0;  // Сброс счетчика
        }
    }
}
