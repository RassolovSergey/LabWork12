﻿using ClassLibraryLab10;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12.Part_4
{
    public class MyCollectionTree<T> : MyTree<T>, IEnumerable<T>, ICollection<T> where T : IInit, ICloneable, IComparable, ISummable, new()
    {
        public MyCollectionTree() : base() { }

        public MyCollectionTree(int data) : base(data) { }

        public MyCollectionTree(T[] collection) : base(collection) { }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyTreeEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => base.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            base.Add(item);
        }

        public void Clear()
        {
            base.Clear();
        }

        public bool Contains(T item)
        {
            return base.Find(item) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentException("Ваш массив пуст!");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Индекс массива должен быть неотрицательным");
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException("Данный массив недостаточно велик для размещения элементов");

            foreach (var item in this)
            {
                array[arrayIndex++] = item;
            }
        }

        public bool Remove(T item)
        {
            return base.RemoveISBD(item);
        }

        private class MyTreeEnumerator : IEnumerator<T>
        {
            private TreePoint<T>? root;
            private TreePoint<T>? current;
            private Stack<TreePoint<T>> stack;
            private bool disposed = false; // флаг для отслеживания освобождения

            public MyTreeEnumerator(MyCollectionTree<T> collection)
            {
                root = collection.root;
                current = null;
                stack = new Stack<TreePoint<T>>();
            }

            public T Current => current!.Data;

            object? IEnumerator.Current => Current;

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposed)
                {
                    if (disposing)
                    {
                        // Освобождаем управляемые ресурсы
                        stack.Clear();
                    }
                    // Освобождаем неуправляемые ресурсы, если есть

                    disposed = true;
                }
            }

            public bool MoveNext()
            {
                if (stack.Count == 0 && root != null && current == null)
                {
                    stack.Push(root);
                }

                while (stack.Count > 0)
                {
                    var node = stack.Pop();
                    if (node != null)
                    {
                        current = node;
                        stack.Push(node.Right);
                        stack.Push(node.Left);
                        return true;
                    }
                }

                return false;
            }

            public void Reset()
            {
                current = null;
                stack.Clear();
                if (root != null)
                {
                    stack.Push(root);
                }
            }

            ~MyTreeEnumerator()
            {
                Dispose(false);
            }
        }
    }
}
