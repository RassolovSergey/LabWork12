using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12
{
    internal class TreePoint<T> where T: IComparable
    {
        public T? Data { get; set; }
        public TreePoint<T>? Left { get; set; }
        public TreePoint<T>? Right { get; set; }


        // Конструктор - ( Без параметра )
        public TreePoint()
        {
            this.Data = default(T);
            this.Left = null;
            this.Right = null;
        }

        // Конструктор -  ( Параметор - data )
        public TreePoint(T data) 
        {
            this.Data = data;
            this.Left = null;
            this.Right = null;
        }

        // Метод - ToString
        public override string ToString()
        {
            if (Data == null) { return ""; }
            else { return Data.ToString(); }
        }

        // Метод - CompareTo
        public int CompareTo(TreePoint<T> other)
        {
            return Data.CompareTo(other);
        }
    }
}
