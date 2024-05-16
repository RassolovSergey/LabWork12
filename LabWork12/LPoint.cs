
using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12
{
    public class LPoint<T> where T : IInit, ICloneable, new()
    {
        // Поле ключа.
        public int key;

        // Поле значения.
        public T value;

        // Ссылка на следующий элемент.
        public LPoint<T> next;

        // Статическая переменная для генерации случайных чисел.
        static Random rnd = new Random();

        // Конструктор, принимающий значение типа T.
        public LPoint(T val)
        {
            // Присвоение значения.
            value = val;

            // Вычисление хеша на основе значения.
            key = GetHashCode();

            // Инициализация следующего элемента как null.
            next = null;
        }

        // Переопределение метода ToString().
        public override string ToString()
        {
            // Возвращает строку, содержащую ключ и значение элемента.
            return $"{key}: {value}";
        }

        // Переопределение метода GetHashCode().
        public override int GetHashCode()
        {
            // Инициализация переменной для хранения хеш-кода.
            int code = 0;

            // Вычисление хеш-кода на основе суммы кодов символов в значении.
            foreach (char c in value.ToString())
                code += (int)c;

            // Возврат вычисленного хеш-кода.
            return code;
        }
    }
}
