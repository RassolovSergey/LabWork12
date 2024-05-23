using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LabWork12
{
    // Обобщённый класс двунаправленного списка
    public class BiList<T> where T : IInit, ICloneable, new()
    {
        public PointBiList<T> beg;      // Начальный узел списка
        public PointBiList<T> end;      // Конечный узел списка
        private int count = 0;      // Счетчик элементов
        public int Count => count;  // Функция чтения переменной count

        public T[] Collection { get; }

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

        public BiList(T[] collection)
        {
            Collection = collection;
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
            return new BiList<T>(data);     // Возвращаем новый узел
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

        // Метод для добавления элемента в конец списка
        public void AddToEnd(T data)
        {
            T newData = (T)data.Clone();
            PointBiList<T> newItem = new PointBiList<T>(newData);
            count++;
            if (end != null)
            {
                end.Next = newItem;
                newItem.Prev = end;
                end = newItem;
            }
            else
            {
                beg = newItem;
                end = beg;
            }
        }

        // Метод поиска элемента списка
        public bool FindItem(T data)
        {
            PointBiList<T> current = beg;   // Создаем переменную и ставим её в начало списка
            while (current != null)     // Пока она не равна null ( До конца списка )
            {
                if (current.Data.Equals(data)) // Сравниваем data и current.Data
                {
                    return true;
                }
                current = current.Next; // Переход а следующий элемент списка
            }
            return false;
        }

        // Печать списка
        public void PrintList()
        {
            if (beg == null) { throw new Exception("Ваш список пуст!"); }
            PointBiList<T> current = beg;   // Назначает текущий узел - первым в списке
            while (current != null)         // Перебираем все элементы списка, пока он не кончится
            {
                Console.WriteLine(current.Data);    // Выводит информацию текущего
                current = current.Next;             // Записываем в текущий следующий объект
            }
        }
        // Метод удаления узла
        public void Remove(PointBiList<T> nodeToRemove)
        {
            if (nodeToRemove == null)
            {
                throw new ArgumentNullException(nameof(nodeToRemove), "Узел для удаления не может быть null.");
            }

            // Проверка, является ли узел началом списка
            if (nodeToRemove == beg)
            {
                beg = nodeToRemove.Next; // Обновляем начало списка, указывая на следующий элемент

                if (beg != null)
                {
                    beg.Prev = null; // Обнуляем ссылку на предыдущий элемент для нового начала списка
                }
            }
            else
            {
                nodeToRemove.Prev.Next = nodeToRemove.Next; // Обновляем ссылку на следующий элемент предыдущего узла

                if (nodeToRemove.Next != null) // Проверяем, существует ли следующий элемент узла для удаления
                {
                    nodeToRemove.Next.Prev = nodeToRemove.Prev; // Обновляем ссылку на предыдущий элемент следующего узла
                }
            }

            // Проверка, является ли узел концом списка
            if (nodeToRemove == end)
            {
                end = nodeToRemove.Prev; // Обновляем конец списка, указывая на предыдущий элемент узла для удаления
            }

            count--; // Уменьшаем счетчик элементов списка
        }


        public void RemoveEven()
        {
            int number = 1;                 // Порядковый номер текущего элемента в коллекции
            PointBiList<T> current = beg;   // Инициализация current - текущий объект, ставим его в начало

            while (current != null)         // Перебор всего списка
            {
                PointBiList<T> next = current.Next; // Сохраняем ссылку на следующий элемент коллекции перед удалением текущего
                if (number % 2 == 0)     // Проверка четности
                {
                    Remove(current);    // Удаляем узел
                }
                number++;               // Переход на следующий индекс (Следующий элемент)
                current = next;         // Переход к следующему элементу коллекции
            }
        }


        // Удаление списка из памяти
        public void Dispose()
        {
            PointBiList<T> current = beg;   // Указатель на начало списка

            while (current != null)
            {
                PointBiList<T> next = current.Next; // Сохраняем ссылку на следующий узел перед удалением текущего

                // Освобождаем ресурсы текущего узла
                current.Data = default; // Сбрасываем данные в null или значение по умолчанию
                current.Next = null;    // Обнуляем ссылку на следующий узел
                current.Prev = null;    // Обнуляем ссылку на предыдущий узел

                // Удаляем текущий узел
                current = next; // Переходим к следующему узлу
            }

            // После удаления всех узлов, список теперь пустой
            beg = null; // Обнуляем ссылку на начальный узел
            end = null; // Обнуляем ссылку на конечный узел
            count = 0;  // Сбрасываем счетчик элементов
        }


        // Метод поиска длины двунаправленного списка
        public int Length()
        {
            int length = 0;                  // Переменная для хранения длины списка
            PointBiList<T> current = beg;    // Инициализация current - текущий объект, ставим его в начало
            while (current != null)          // Перебор всего списка
            {
                length++;                    // Увеличение счетчика элементов
                current = current.Next;      // Переход к следующему узлу
            }
            return length;                   // Возвращаем длину списка
        }
    }
}
