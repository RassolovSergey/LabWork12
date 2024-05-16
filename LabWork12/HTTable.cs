using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12
{
    public class HTable<T> where T : IInit, ICloneable, new()
    {
        // Поля класса.
        public LPoint<T>[] table; // Массив элементов хеш-таблицы.
        public int Size;           // Размер хеш-таблицы.

        // Конструктор класса.
        public HTable(int size = 10)
        {
            Size = size;
            table = new LPoint<T>[Size]; // Создание массива указанного размера.
        }

        // Метод добавления элемента в хеш-таблицу.
        public bool Add(T val)
        {
            LPoint<T> point = new LPoint<T>(val); // Создание нового элемента.
            if (val == null) return false;        // Проверка на null.
            int index = Math.Abs(point.GetHashCode()) % Size; // Вычисление индекса в массиве.
            if (table[index] == null)
                table[index] = point; // Если ячейка пуста, добавляем элемент.
            else
            {
                LPoint<T> cur = table[index]; // Получение текущего элемента.
                if (string.Compare(cur.ToString(), point.ToString()) == 0) return false; // Проверка на совпадение.
                while (cur.next != null)
                {
                    if (string.Compare(cur.ToString(), point.ToString()) == 0) return false; // Проверка на совпадение.
                    cur = cur.next;
                }
                cur.next = point; // Добавление элемента в конец цепочки.
            }
            return true; // Возврат успешного выполнения операции.
        }

        // Метод печати хеш-таблицы.
        public void Print()
        {
            if (table == null) { Console.WriteLine("Таблица пустая!"); return; } // Проверка на пустоту таблицы.
            for (int i = 0; i < Size; i++)
            {
                if (table[i] == null) Console.WriteLine(i + " : "); // Если ячейка пуста, выводим только индекс.
                else
                {
                    Console.Write(i + " : ");
                    LPoint<T> p = table[i];
                    while (p != null)
                    {
                        Console.Write(p.ToString() + "\t"); // Выводим все элементы цепочки.
                        p = p.next;
                    }
                    Console.WriteLine();
                }
            }
        }

        // Метод поиска элемента в хеш-таблице.
        public bool FindPoint(T val)
        {
            LPoint<T> lp = new LPoint<T>(val); // Создание элемента для поиска.
            int code = Math.Abs(lp.GetHashCode()) % Size; // Вычисление индекса в массиве.
            if (String.Compare(table[code].value.ToString(), val.ToString()) == 0) // Проверка на совпадение.
                return true;
            lp = table[code];
            while (lp != null)
            {
                if (string.Compare(lp.value.ToString(), val.ToString()) == 0) return true; // Проверка на совпадение.
                lp = lp.next;
            }
            return false; // Возврат неудачи поиска.
        }

        // Метод удаления элемента из хеш-таблицы.
        public T DelPoint(T val)
        {
            LPoint<T> lp = new LPoint<T>(val); // Создание элемента для удаления.
            int code = Math.Abs(lp.GetHashCode()) % Size; // Вычисление индекса в массиве.
            lp = table[code];
            if (table[code] == null) return default(T); // Проверка на пустоту ячейки.
            if (table[code] != null && String.Compare(table[code].value.ToString(), val.ToString()) == 0) // Проверка первого элемента в ячейке.
            {
                lp = table[code];
                table[code] = table[code].next; // Удаление первого элемента.
                return lp.value;
            }
            while (lp.next != null && (string.Compare(lp.next.value.ToString(), val.ToString()) != 0)) // Поиск элемента для удаления.
                lp = lp.next;
            if (lp.next != null)
            {
                val = lp.next.value;
                lp.next = lp.next.next; // Удаление найденного элемента.
                return val;
            }
            return default(T); // Возврат неудачи удаления.
        }
    }
}
