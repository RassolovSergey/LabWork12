using ClassLibraryLab10;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork12.Part_4
{
    /*
    public class MyCollectionTree<T> : IEnumerable<T>, ICollection<T> where T : ICloneable, IComparable, IInit, new()
    {
        private MyTree<T> tree;

        public MyCollectionTree()
        {
            tree = new MyTree<T>(0);
        }

        public MyCollectionTree(int length)
        {
            tree = new MyTree<T>(length);
        }

        public MyCollectionTree(MyCollectionTree<T> c)
        {
            tree = c.tree.DeepCopy();
        }

        // Реализация интерфейса IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal(tree.Root).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Реализация интерфейса ICollection<T>
        public int Count => tree.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            tree.AddPoint(item);
        }

        public void Clear()
        {
            tree.DeleteTree();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        // Реализация интерфейса IDictionary<int, T>
        public T this[int key]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ICollection<int> Keys => throw new NotImplementedException();

        public ICollection<T> Values => throw new NotImplementedException();

        public void Add(int key, T value)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(int key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(int key, out T value)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<T> InOrderTraversal(TreePoint<T>? node)
        {
            if (node == null)
                yield break;

            foreach (var item in InOrderTraversal(node.Left))
                yield return item;

            yield return node.Data!;

            foreach (var item in InOrderTraversal(node.Right))
                yield return item;
        }
    }
    */
}