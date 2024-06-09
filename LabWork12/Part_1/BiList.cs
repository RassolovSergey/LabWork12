using ClassLibraryLab10;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LabWork12
{
    // Обобщённый класс двунаправленного списка
    public class BiList<T> : IList<T> where T : IInit, ICloneable, new()
    {
        public PointBiList<T> beg;  // Начальный узел списка
        public PointBiList<T> end;  // Конечный узел списка
        private int count = 0;      // Счетчик элементов
        public int Count => count;  // Функция чтения переменной count

        // IList<T>  - Получает значение, указывающее, является ли объект ICollection<T> доступным только для чтения.
        public bool IsReadOnly => false;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                PointBiList<T> current = beg;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
                return current.Data;
            }
            set
            {
                if (index < 0 || index >= count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                PointBiList<T> current = beg;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
                current.Data = value;
            }
        }

        // Конструктор без параметров: Пустой список
        public BiList()
        {
            beg = null;     // Инициализация начала списка как null
            end = null;     // Инициализация конца списка как null
        }

        // Конструктор с параметром: Только информация
        public BiList(T data)
        {
            Add(data);
        }

        // Конструктор, инициализирующий список случайными элементами
        public BiList(int size)
        {
            if (size <= 0) throw new ArgumentException("Размер должен быть больше нуля.", nameof(size));
            for (int i = 0; i < size; i++)
            {
                Add(MakeRandomData());
            }
        }

        public BiList(T[] collection)
        {
            foreach (var item in collection)
            {
                Add(item);
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
                Add(inputData);    // Добавляем объект в конец списка
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
                newList.Add(clonedData);

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


        // Удаление четных элементов
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





        // IList<T> - Определяет индекс заданного элемента коллекции IList<T>
        public int IndexOf(T item)
        {
            int index = 0;
            PointBiList<T> current = beg;
            while (current != null)
            {
                if (current.Data.Equals(item))
                {
                    return index;
                }
                current = current.Next;
                index++;
            }
            return -1;
        }

        // IList<T> - Вставляет элемент в список IList<T> по указанному индексу.
        public void Insert(int index, T item)
        {
            if (index < 0 || index > count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index == count)
            {
                Add(item);
                return;
            }

            PointBiList<T> newPoint = new PointBiList<T>(item);
            PointBiList<T> current = beg;

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            newPoint.Next = current;
            newPoint.Prev = current.Prev;

            if (current.Prev != null)
            {
                current.Prev.Next = newPoint;
            }
            else
            {
                beg = newPoint;
            }

            current.Prev = newPoint;
            count++;
        }

        // IList<T> - Удаляет элемент IList<T> по указанному индексу.
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            PointBiList<T> current = beg;

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            Remove(current);
        }

        // IList<T> - Добавляет элемент в коллекцию ICollection<T>.
        public void Add(T item) // В конец списка
        {
            T newData = (T)item.Clone();
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

        // IList<T> - Удаляет все элементы из коллекции ICollection<T>
        public void Clear()
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

        // IList<T> - Определяет, содержит ли коллекция ICollection<T> указанное значение
        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        // IList<T> - Копирует элементы коллекции ICollection<T> в массив Array, начиная с указанного индекса массива Array
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < count)
                throw new ArgumentException("Недостаточно места в целевом массиве.");

            PointBiList<T> current = beg;
            for (int i = arrayIndex; current != null; i++)
            {
                array[i] = current.Data;
                current = current.Next;
            }
        }

        // IList<T> - Удаляет первое вхождение указанного объекта из коллекции ICollection<T>
        public bool Remove(T item)
        {
            PointBiList<T> current = beg;
            while (current != null)
            {
                if (current.Data.Equals(item))
                {
                    Remove(current);
                    return true;
                }
                current = current.Next;
            }
            return false;
        }


        // Реализация - IEnumerator<T>
        public IEnumerator<T> GetEnumerator()
        {
            PointBiList<T> current = beg;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
        // Реализация - IEnumerator
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
