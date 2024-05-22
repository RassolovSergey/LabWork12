using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ClassLibraryLab10;

namespace LabWork12
{
    // Класс описывает эллемент списка
    internal class MonoList<T> where T : IInit, new()
    {
        int count = 0;                      // Сцетчик элементов
        public T? data { get; private set; }        // Поле с информацией - объектом
        public MonoList<T>? next { get; private set; } // Поле указатель на адрем следующего элемента списка
        public MonoList<T>? pred { get; private set; } // Поле указатель на адрем предыдущего элемента списка
        MonoList<T> beg = null;   // Начало списка

        /*
                +-------+      +-------+      +-------+      +-------+
        | Data: | ---> | Data: | ---> | Data: | ---> | Data: |
        +-------+      +-------+      +-------+      +-------+
        | Next: | ---> | Next: |      | Next: |      | Next: | ---> null
        +-------+      +-------+      +-------+      +-------+
          pointFirst     beg           element1       element2
       */


        // Конструктор без параметра
        public MonoList()
        {
            this.data = default(T); // Задаёт объекту базавое значение (использует конструктор без параметров)
            this.pred = null;       // Пустое значение ссылки
            this.next = null;       // Пустое значение ссылки
        }


        // Конструктор с параметром:    Data
        public MonoList(T data)
        {
            this.data = data;   // Задаёт объекту введеное в конструктор значение:  Data
            this.pred = null;   // Пустое значение ссылки
            this.next = null;   // Пустое значение ссылки
        }

        // Конструктор с параметром Конструктор с параметром: ( data  |  pred  |  next )
        public MonoList(T data, MonoList<T> pred, MonoList<T> next)
        {
            this.data = data;   // Назначаем базавому значению data - значение data из конструктора
            this.pred = pred;   // Назначаем базавому значению pred - значение pred из конструктора
            this.next = next;   // Назначаем базавому значению next - значение next из конструктора
        }

        // Метод ToString
        public override string ToString()
        {
            return data + " ";
        }

        // Создание элемента списка
        static MonoList<T> MakePoint(T firstEllement)
        {
            MonoList<T> newFirstEllement = new MonoList<T>(firstEllement);    // Создание нового экземпляра класса Point с переданным значением firstEllement
            return newFirstEllement;    // Возврат созданного экземпляра
        }

        // Метод: Добавление в начало однонаправленного списка
        static MonoList<T> MakeList(int size)  // Параметр - размер массива
        {
            T firstElement = new T();                   // Создаем пустой элемент типа Point
            Console.WriteLine("Элемент {0} добавляет...", firstElement);    // Выводим сообщение
            MonoList<T> beg = MakePoint(firstElement);     // Cоздаем первый элемент

            // Заполняем первый элемент рандомными значениями
            for (int i = 1; i < size; i++)
            {
                firstElement.RandomInit(); // Заполняем эллемент рандомными значениями
            }
            Console.WriteLine("Элемент {0} добавляет...", firstElement);    // Выводим сообщение

            // Создаем элемент и добавляем в начало списка
            MonoList<T> pointFirst = MakePoint(firstElement);
            pointFirst.next = beg;  // Связываем новый элемент с текущим первым элементом 
            beg = pointFirst;       // Обновляем начало списка
            return beg;             // Возвращаем начало списка
        }


        // Добавление в конец списка
        static MonoList<T> MakeListToEnd(int size)
        {
            T newInfo = new T();                // Создаем новый элемент
            Console.WriteLine("Элемент {0} добавляет...", newInfo); // Выводим сообщение
            MonoList<T> beg = MakePoint(newInfo);  // Первый элемент
            MonoList<T> r = beg;                   // Переменная хранит адрес конца списка 

            for (int i = 1; i < size; i++)
            {
                newInfo.RandomInit();   // Заполняем элемент рандомными значениями (data)
                Console.WriteLine("Элемент {0} добавляет...", newInfo); // Выводим сообщение

                // Создаем элемент и добавляем в конец списка
                MonoList<T> newElement = MakePoint(newInfo);
                r.next = newElement;    // Связываем новый элемент с текущим первым элементом 
                r = newElement;         // Обновляем начало списка
            }
            return beg;                 // Возвращаем начало списка
        }

        // Метод вывода списка
        static void ShowList(MonoList<T> beg)
        {
            // Проверка наличия элементов в списке
            if (beg == null)
            {
                Console.WriteLine("Список пуст!");  // Вывод сообщения
                return;                             // Завершаем выполнение метода
            }
            MonoList<T> timePoint = beg;
            while (timePoint != null)
            {
                Console.Write(timePoint);   // Вывод сообщения
                timePoint = timePoint.next; // Переход к следующему элементу
            }
            Console.WriteLine();    // Отступ
        }

        static MonoList<T> AddPoint(MonoList<T> beg, int number)
        {
            T newInfo = new T();    // Создаем новый элемент
            newInfo.RandomInit();   // Заполняем элемент рандомными значениями (data)

            Console.WriteLine("Элемент {0} добавляет...", newInfo);

            // Создаем новый элемент
            MonoList<T> NewPoint = MakePoint(newInfo);

            // Список пустой
            if (beg == null)
            {
                beg = MakePoint(newInfo = new T());
                return beg;
            }

            // Добавление в начало списка
            if (number == 1) 
            {
                NewPoint.next = beg;
                beg = NewPoint;
                return beg;
            }

            // Вспом. переменная для прохода по списку
            MonoList<T> timePoint = beg;

            // Идем по списку до нужного элемента
            for (int i = 1; i < number - 1 && timePoint != null; i++)
            {
                timePoint = timePoint.next;
            }

            // Элемент не найден
            if (timePoint == null)  
            {
                Console.WriteLine("Ошибка! Размер списка меньше введённого числа.");
                return beg;
            }

            // Добавляем новый элемент
            NewPoint.next = timePoint.next;
            timePoint.next = NewPoint;
            return beg;
        }

        // Метод удаления первого элемента
        static MonoList<T> DelElement(MonoList<T> beg, int number)
        {
            if (beg == null)    // Пустой список
            {
                Console.WriteLine("Ошибка! Лист Пуст.");
                return null;
            }
            if (number == 1)    // Удаляем первый элемент
            {
                beg = beg.next;
                return beg;
            }

            MonoList<T> timePoint = beg;

            // Ищем элемент для удаления и встаем на предыдущий
            for (int i = 1; i < number - 1 && timePoint != null; i++)
            {
                timePoint = timePoint.next;
            }

            // Если элемент не найден
            if (timePoint == null)
            {
                Console.WriteLine("Ошибка! Размер списка меньше введённого числа.");
                return beg;
            }

            // Исключаем элемент из списка
            timePoint.next = timePoint.next.next; // Переписываем ссылку через одного
            return beg;                           // Возвращаем результат
        }
    }
}