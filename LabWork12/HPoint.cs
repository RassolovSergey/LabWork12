
using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12
{
    public class HPoint<T>
    {
        public T? Data { get; set; }            // Поле с информацией об объекте
        public HPoint<T>? Next { get; set; }    // Поле - ссылка на следующий элемент
        public HPoint<T>? Prev { get; set; }    // Поле - ссылка на предыдущий элемент

        static Random rnd = new Random();       // Статический генератор случайных чисел

        // Конструктор - ( Без параметров )
        public HPoint()
        {
            this.Data = default;
            this.Prev = null;
            this.Next = null;
        }

        // Конструктор - ( Параметр - Data )
        public HPoint(T data)
        {
            this.Data = data;
            this.Prev = null;
            this.Next = null;
        }

        // Метод - ToString
        public override string? ToString()
        {
            return Data == null ? "" : Data.ToString();
        }

        // Метод - GetHashCode
        public override int GetHashCode()
        {
            return Data == null ? 0 : Data.GetHashCode();
        }
    }
}

