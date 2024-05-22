using ClassLibraryLab10;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12.Part_4
{
    internal class MyCollection<T>: BiList<T>, IEnumerable<T> where T: IInit, ICloneable, new()
    {
        public MyCollection() : base() { }
        public MyCollection(int size) : base(size) { }
        public MyCollection(T[] collection) : base(collection) { }


        // Реализация интерфейса - IEnumerable<T> - 1
        public IEnumerator<T> GetEnumerator()
        {
            return new MyEnumerator<T>(this);
        }

        // Реализация интерфейса - IEnumerable - 2 (ненужный)
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    internal class MyEnumerator<T> : IEnumerator<T> where T : IInit, ICloneable, new()
    {
        PointBiList<T> beg;     // Начало коллекции
        PointBiList<T> current; // Текущий элемент

        public MyEnumerator(MyCollection<T> collection)
        {
            beg = collection.beg;
            current = beg;
        }

        public T Current => current.Data;

        // Можкм не трогать
        object IEnumerator.Current => throw new NotImplementedException();

        // Реализация интерфейса IEnumerator - 1
        public void Dispose()
        {
            // Оставляем пустым
        }

        // Реализация интерфейса IEnumerator - 2
        public bool MoveNext()
        {
            if (current.Next == null)
            {
                Reset();
                return false;
            }
            else
            {
                current = current.Next;
                return true;
            }
        }

        // Реализация интерфейса IEnumerator - 3
        public void Reset()
        {
            current = beg;
        }
    }
}
