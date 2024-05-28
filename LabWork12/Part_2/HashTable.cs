using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12
{
    public class HashTable<T> where T : IInit, ICloneable, new()
    {
        HPoint<T>?[] table; // Массив из элементов типа HPoint<T> (элементы могут быть null)
        public int Capacity => table.Length;    // Размер таблицы

        // Конструктор - ( Параметр: Длина ) (Можно использовать как констр. без параметров)
        public HashTable(int lenght = 10)
        {
            table = new HPoint<T>[lenght];
        }

        // Вывод таблицы
        public void PrintTable()
        {
            for (int i = 0; i < table.Length; i++)  // Проход по всей таблице
            {
                Console.WriteLine($"{i+1}:");       // Выводим номер
                if (table[i] != null)               // Если строка не пустая, то...
                {
                    Console.WriteLine(table[i].Data);   // Вывод информации из ячейки
                    if (table[i].Next != null)          // Проверяем наличие сылки на следующий объект
                    {
                        HPoint<T>? current = table[i].Next; // Встаём на цепочку

                        // Проходим по цепочке
                        while (current != null)
                        {
                            Console.WriteLine(current.Data);
                            current = current.Next;
                        }
                    }
                }
            }
        }

        // Метод - Добавление
        public void AddPoint(T data)
        {
            int index = GetIndex(data); // Вычисляем индекс объекта

            // Ячейка пусна
            if (table[index] == null)
            {
                table[index] = new HPoint<T>(data); // Добавляем новый объект в цепочку
            }
            else // Есть цепочка
            {
                HPoint<T>? current = table[index]; // Ставим объект на нужный индекс (||)

                // Проверка на наличие элемента в цепочке
                // Идем до свободной ячейки таблицы
                while (current != null)
                {
                    if (current.Data.Equals(data)) { return; }  // Если элемент уже существует, заканчиваем метод
                    if (current.Next == null) break;            // Выходим
                    current = current.Next;                     // Переставляем объект на следующее место
                }

                current.Next = new HPoint<T>(data); // Записываем объект в новую пустую ячейку
                current.Next.Prev = current;        // Даём предпоследнему объекту ссылку на последний
            }
        }

        // Метод поиска (проверка на наличие)
        public bool Contains(T data)
        {
            int index = GetIndex(data); // Вычисляем индекс для data
            if (table == null) { throw new Exception("Ошибка: Данной таблицы не существует."); } // Проверка на пустоту таблицы
            if (table[index] == null) { return false; }             // Проверка на пустоту строки
            if (table[index].Data.Equals(data)) { return true; }    // Попали на нужный элемент
            else
            {
                HPoint<T>? current = table[index]; // Ставим объект на нужную строку

                // Идем до последнего объекта цепочки
                while (current != null)
                {
                    if (current.Data.Equals(data)) { return true; } // Сравниваем
                    current = current.Next;                         // Переставляем на следующий элемент
                }
            }
            return false; // Если дошли до сюда => ничего не нашли
        }

        // Новый метод поиска (Возвращает сам элемент)
        public T? Find(T data)
        {
            int index = GetIndex(data); // Вычисляем индекс для data
            if (table == null) { throw new Exception("Ошибка: Данной таблицы не существует."); } // Проверка на пустоту таблицы
            if (table[index] == null) { return default; }             // Проверка на пустоту строки
            if (table[index].Data.Equals(data)) { return table[index].Data; }    // Попали на нужный элемент
            else
            {
                HPoint<T>? current = table[index]; // Ставим объект на нужную строку

                // Идем до последнего объекта цепочки
                while (current != null)
                {
                    if (current.Data.Equals(data)) { return current.Data; } // Сравниваем
                    current = current.Next;                         // Переставляем на следующий элемент
                }
            }
            return default; // Если дошли до сюда => ничего не нашли
        }

        // Удаление из цепочки
        public bool RemoveData(T data)
        {
            HPoint<T>? current;
            int index = GetIndex(data); // Считаем индекс
            if (table[index] == null)   // Проверяем на наличие строки
            {
                return false;
            }
            if (table[index].Data.Equals(data)) // Проверяем на совпадения
            {
                if (table[index].Next == null)  // Проверка на пустоту
                {
                    table[index] = null;        // Удаляем пустоту (очищаем память)
                }
                else
                {
                    table[index] = table[index].Next; // Добавляем новый элемент в цепочку (индекс тот же)
                    table[index].Prev = null;         // Обнуляем предыдущий индекс
                }
                return true;
            }
            else
            {
                current = table[index]; // Ставим объект на нужный индекс

                // Проходим по всеё цепочке
                while (current != null)
                {
                    if (current.Data.Equals(data)) // Если нашли совпадение
                    {
                        HPoint<T>? prev = current.Prev; // Создаем переменную prev - записываем предыдущий элемент
                        HPoint<T>? next = current.Next; // Создаем переменную next - записываем следующий элемент
                        prev.Next = next;
                        current.Prev = null;
                        if (next != null)
                        {
                            next.Prev = prev;
                        }
                        return true;
                    }
                    current = current.Next;
                }
            }
            return false;
        }


        // Метод - Вычисления индекса объекта
        private int GetIndex(T data)
        {
            // Используем хэш-код объекта, который должен учитывать все необходимые поля
            return Math.Abs(data.GetHashCode()) % Capacity;
        }
    }
}
