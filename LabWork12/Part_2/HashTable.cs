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
        public HPoint<T>?[] table;
        public int Capacity => table.Length;
        public int Count { get; private set; } // Свойство для отслеживания количества элементов

        public HashTable(int length = 10)
        {
            table = new HPoint<T>[length];
            Count = 0;
        }

        public void PrintTable()
        {
            for (int i = 0; i < table.Length; i++)
            {
                Console.WriteLine($"{i + 1}:");
                if (table[i] != null)
                {
                    Console.WriteLine(table[i].Data);
                    if (table[i].Next != null)
                    {
                        HPoint<T>? current = table[i].Next;
                        while (current != null)
                        {
                            Console.WriteLine(current.Data);
                            current = current.Next;
                        }
                    }
                }
            }
        }

        public void AddPoint(T data)
        {
            T dataCopy = (T)data.Clone(); // Создаем копию объекта перед добавлением
            int index = GetIndex(dataCopy);

            if (table[index] == null)
            {
                table[index] = new HPoint<T>(dataCopy);
                Count++; // Увеличиваем счетчик при добавлении нового элемента
            }
            else
            {
                HPoint<T>? current = table[index];
                while (current != null)
                {
                    if (current.Data.Equals(dataCopy)) { return; }
                    if (current.Next == null) break;
                    current = current.Next;
                }

                current.Next = new HPoint<T>(dataCopy);
                current.Next.Prev = current;
                Count++; // Увеличиваем счетчик при добавлении нового элемента в цепочку
            }
        }

        public bool Contains(T data)
        {
            int index = GetIndex(data);
            if (table == null) { throw new Exception("Ошибка: Данной таблицы не существует."); }
            if (table[index] == null) { return false; }
            if (table[index].Data.Equals(data)) { return true; }
            else
            {
                HPoint<T>? current = table[index];
                while (current != null)
                {
                    if (current.Data.Equals(data)) { return true; }
                    current = current.Next;
                }
            }
            return false;
        }

        public T? Find(T data)
        {
            int index = GetIndex(data);
            if (table == null) { throw new Exception("Ошибка: Данной таблицы не существует."); }
            if (table[index] == null) { return default; }
            if (table[index].Data.Equals(data)) { return table[index].Data; }
            else
            {
                HPoint<T>? current = table[index];
                while (current != null)
                {
                    if (current.Data.Equals(data)) { return current.Data; }
                    current = current.Next;
                }
            }
            return default;
        }

        public bool RemoveData(T data)
        {
            HPoint<T>? current;
            int index = GetIndex(data);
            if (table[index] == null)
            {
                return false;
            }
            if (table[index].Data.Equals(data))
            {
                if (table[index].Next == null)
                {
                    table[index] = null;
                }
                else
                {
                    table[index] = table[index].Next;
                    table[index].Prev = null;
                }
                Count--; // Уменьшаем счетчик при удалении элемента
                return true;
            }
            else
            {
                current = table[index];
                while (current != null)
                {
                    if (current.Data.Equals(data))
                    {
                        HPoint<T>? prev = current.Prev;
                        HPoint<T>? next = current.Next;
                        if (prev != null) prev.Next = next;
                        if (next != null) next.Prev = prev;
                        Count--; // Уменьшаем счетчик при удалении элемента из цепочки
                        return true;
                    }
                    current = current.Next;
                }
            }
            return false;
        }

        public int GetIndex(T data)
        {
            return Math.Abs(data.GetHashCode()) % Capacity;
        }
    }
}