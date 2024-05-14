using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClassLibraryLab10;

namespace LabWork12
{
    // Обобщённый класс
    internal class BiList_01<T> where T: IInit, ICloneable, new()
    {
        BiList_01<T>? beg = null;   // Поле: Начало списка
        BiList_01<T>? end = null;   // Поле Конец списка
        public object data { get; private set; } // Поле информации

        int count = 0;      // Счетчик элементов

        public int Count => count;  // Функция чтения переменной count

        public BiList_01<T> pred { get; private set; }    // Свойства поля "pred" - ссылка узла на предыдущий
        public BiList_01<T> next { get; private set; }    // Свойства поля "next" - ссылка узла на слудкющий

        // Создание радномного объекта + ссылки
        public BiList_01<T> MakeRandomData()
        {
            T data = new T();           // Конструктор без параметров
            data.RandomInit();          // Заполнение ДСЧ
            return new BiList_01<T>(data); // Возвращаем новый узел
        }

        // Создание радномного объекта:     data
        public T MakeRandomItem()
        {
            T data = new T();   // Конструктор без параметров
            data.RandomInit();  // Заполнение ДСЧ
            return data;        // Возвращаем значение нового объека
        }

        // Метод добавления в начало списка
        public void AddToBegin(T item)
        {
            T newData = (T)item.Clone();                // Глубокое копирование
            BiList_01<T> newItem = new BiList_01<T>(newData);   // Создаем новую структыру в таким же основанием(значением)
            count++; // Счетчик
            if (beg != null)
            {
                /* Если до объекта есть ещё какие-то значения, то меняем их местами,
                двигая объект в самое начало */
                beg.pred = newItem;
                newItem.next = beg;
                beg = newItem;
            }
            else
            {
                // Если список пуст, то и end и beg будут стоять на одном элементе
                beg = newItem;
                end = beg;
            }
        }


        // Метод добавления в конец списка
        public void AddToEnd(T item)
        {
            T newData = (T)item.Clone();  // Глубокое копирование
            BiList_01<T> newItem = new BiList_01<T>(newData);
            count++;
            if (end != null)
            {
                /* Если до объекта есть ещё какие-то значения, то меняем их местами,
                двигая объект в самый конец */
                end.next = newItem;
                newItem.pred = end;
                end = newItem;
            }
            else
            {
                // Если список пуст, то и end и beg будут стоять на одном элементе
                beg = newItem;
                beg = newItem;
                end = beg;
            }
        }

        // Конструктор без параметров
        public BiList_01() { }

        // Конструктор с параметром
        public BiList_01(T data) { }

        // Конструктор с параметром
        public BiList_01(int size)
        {
            if (size <= 0) throw new Exception("Размер меньше нуля.");
            beg = MakeRandomData();
            end = beg;
            for (int i = 1; i < size; i++)
            {
                T newItem = MakeRandomItem();
                AddToEnd(newItem);
            }
        }

        // Конструктор Списка
        public BiList_01(T[] collection)
        {
            if (collection == null) { throw new Exception("Коллекция пуста: null"); }
            if (collection.Length == 0) { throw new Exception("Коллекция пуста: 0"); }
            T newData = (T)collection.Clone();
            beg = new BiList_01<T>(newData);
            end = beg;
            for (int i = 0; i < collection.Length; i++)
            {
                AddToEnd(collection[i]);
            }
        }

        // Метод - Печать массива
        public void PrintList()
        {
            if (count == 0) {Console.WriteLine("Cписок пуст");}
            BiList_01<T>? current = beg;
            for (int i = 0; current != null; i++)
            {
                Console.WriteLine(current);
                current = current.next;
            }
        }

        // Метод - Поиска элемента
        public BiList_01<T>? FindItem(T item) // ? - разрешаем присвоить null
        {
            BiList_01<T>? current = beg;
            while (current != null)
            {
                // проверяет каждый объект с начала, пока не найдёт, если нет, то null
                if (current.data == null) { throw new Exception("Данные отсутствуют"); }
                if (current.data.Equals(item)) { return null; }
                current = current.next;
            }
            return null;
        }

        // Метод - Удаления элемента
        public bool RemoveItem(T item)
        {
            if (beg == null) { throw new Exception("Пустой список"); }
            BiList_01<T>? pos = FindItem(item);
            if (pos == null) return false;
            count--;

            // В списке один элемент
            if (beg == end)
            {
                beg = end = null;
                return true;
            }
            // Первый элемент
            if (pos.pred == null)
            {
                beg = beg?.next;
                beg.pred = null;
                return true;
            }

            // Последний элемент
            if (beg == end)
            {
                end = end.pred;
                end.next = null;
                return true;
            }
            
            // Элемент внутри списка
            BiList_01<T> next = pos.next;
            BiList_01<T> pred = pos.pred;
            pos.next.pred = pred;
            pos.pred.next = next;
            return true;
        }
    }
}
