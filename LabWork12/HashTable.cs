using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12
{
    public class HashTable<T> where T : IInit, ICloneable, IKey, new()
    {
        HPoint<T>?[] table; // Массив из лементов типа HPoint<T> (элементы могут быть null)
        public int Capacity => table.Length;    // Размер таблицы

        // Конструктор - ( Параметр - длина ) (Можно использовать как констр. без параметров)
        public HashTable(int lenght = 10)
        {
            table = new HPoint<T>[lenght];
        }

        // Вывод таблицы
        public void PrintTable()
        {
            for (int i = 0; i < table.Length; i++)
            {
                Console.WriteLine($"{i+1}:");
                if (table[i] != null) // Если строка не пустая
                {
                    Console.WriteLine(table[i].Data);   // Вывод информации
                    if (table[i].Next != null)          // Если есть следующий элемент
                    {
                        HPoint<T>? current = table[i].Next; // Идём по цепочке
                        while (current != null)
                        {
                            Console.WriteLine(current.Data);
                            current = current.Next;
                        }
                    }
                }
            }
        }


        // Метод - Добавления
        public void AddPoint(T data)
        {
            int index = GetIndex(data);
            // Позиция пуста
            if (table[index] == null)
            {
                table[index] = new HPoint<T>(data); // Добавляем новый объект в цепочку
            }
            else // Есть цепочка
            {
                HPoint<T>? current = table[index]; // Ставим объект на нужный индекс (||)

                // Идём до конца списка
                while (current.Next != null)
                {
                    if (current.Equals(data)) { return; }   // Если одинаковые, то заканчиваем
                    current = current.Next; // Переставляем объект на следующее место
                }
                current.Next = new HPoint<T>(data); // Присваеваем объекту ссылку но новый последний объект
                current.Next.Prev = current;        // Даём предпоследнему объекту ссылку на последний
            }
        }


        // Метод поиска
        public bool Contains(T data)
        {
            int index = GetIndex(data); // Вычисляем индекс для data
            if (table == null) { throw new Exception("Ошибка: Данной таблицы не существует."); } // Проверка на пустоту таблицы
            if (table[index] == null) { return false; } // Проверка на пустоту индекса
            if (table[index].Data.Equals(data)) { return true; } // Попали на нужный элемент
            else
            {
                HPoint<T>? current = table[index]; // Ставим объект на нужный индекс
                while (current != null)
                {
                    if (current.Data.Equals(data)) { return true; } // Сравниваем
                    current = current.Next; // Переставляем на следующий элемент
                }
            }
            return false; // Если дошли до сюда => ничего не нашли
        }


        // Удаление из цепочки
        public bool RemoveData(T data)
        {
            HPoint<T>? current;
            int index = GetIndex(data); // Считаем индекс
            if (table[index] == null) // Проверяем на наличие
            {
                return false;
            }
            if (table[index].Data.Equals(data)) // Проверяем на совпадения
            {
                if (table[index].Next == null) // Проверка на пустоту
                {
                    table[index] = null; // Удаляем пустоту (очищаем память)
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
                while (current != null)
                {
                    if (current.Data.Equals(data)) // Нашли совпадение и удаляем
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


        // Метод - GetIndex
        private int GetIndex(T data)
        {
            return Math.Abs(data.GetHashCode()) % Capacity;
        }
    }
}
