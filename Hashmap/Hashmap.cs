using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hashmap
{
    public class Hashmap<TKey, TValue>
    {
        LinkedList<KeyValuePair<TKey, TValue>>[] vals;
        Func<TKey, int> hashFunc;
        bool hasUserHash = false;

        public int Count { get; private set; }

        public TValue this[TKey key]
        {
            get
            {
                LinkedList<KeyValuePair<TKey, TValue>> ll = vals[hashInRange(key)];
                if(ll == null)
                {
                    throw new KeyNotFoundException();
                }
                LinkedListNode<KeyValuePair<TKey, TValue>> node = ll.First;
                for (int i = 0; i < ll.Count; i++)
                {
                    if (node.Value.Key.Equals(key))
                    {
                        return node.Value.Value;
                    }
                    node = node.Next;
                }
                throw new KeyNotFoundException();
            }
            set
            {
                LinkedList<KeyValuePair<TKey, TValue>> ll = vals[hashInRange(key)];
                if(ll == null)
                {
                    throw new KeyNotFoundException();
                }
                LinkedListNode<KeyValuePair<TKey, TValue>> node = ll.First;
                for (int i = 0; i < ll.Count; i++)
                {
                    if (node.Value.Key.Equals(key))
                    {
                        node.Value = new KeyValuePair<TKey, TValue>(key, value);
                    }
                    node = node.Next;
                }
                throw new KeyNotFoundException();
            }
        }

        public Hashmap(Func<TKey, int> hashFunction, int capacity = 10) : this(capacity)
        {
            hashFunc = hashFunction;
            hasUserHash = true;
        }
        public Hashmap(int capacity = 10)
        {
            vals = new LinkedList<KeyValuePair<TKey, TValue>>[capacity];
            Count = 0;
        }

        public void Insert(TKey key, TValue value)
        {
            if (Contains(key))
            {
                throw new ArgumentException();
            }
            Count++;
            vals[hashInRange(key)].AddLast(new KeyValuePair<TKey, TValue>(key, value));

            if (Count == vals.Length)
            {
                var tempVals = new LinkedList<KeyValuePair<TKey, TValue>>[vals.Length * 2];
                for (int i = 0; i < vals.Length; i++)
                {
                    var node = vals[i].First;
                    for (int j = 0; j < vals[i].Count; j++)
                    {
                        tempVals[hash(node.Value.Key) % tempVals.Length].AddLast(node.Value);
                        node = node.Next;
                    }
                }
                vals = tempVals;
            }

        }
        public bool Delete(TKey key)
        {
            LinkedList<KeyValuePair<TKey, TValue>> ll = vals[hashInRange(key)];
            LinkedListNode<KeyValuePair<TKey, TValue>> node = ll.First;
            for (int i = 0; i < ll.Count; i++)
            {
                if (node.Value.Key.Equals(key))
                {
                    vals[hashInRange(key)].Remove(node);
                    Count--;
                    return true;
                }
                node = node.Next;
            }
            return false;
        }
        public bool Contains(TKey key)
        {
            LinkedList<KeyValuePair<TKey, TValue>> linkedList = vals[hashInRange(key)];
            if(linkedList == null)
            {
                return false;
            }
            LinkedListNode<KeyValuePair<TKey, TValue>> node = linkedList.First;
            for (int i = 0; i < linkedList.Count; i++)
            {
                if (node.Value.Key.Equals(key))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Contains(TValue value)
        {
            for (int j = 0; j < vals.Length; j++)
            {
                LinkedList<KeyValuePair<TKey, TValue>> linkedList = vals[j];
                if(linkedList == null)
                {
                    continue;
                }
                LinkedListNode<KeyValuePair<TKey, TValue>> node = linkedList.First;
                for (int i = 0; i < linkedList.Count; i++)
                {
                    if (node.Value.Value.Equals(value))
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        public bool Contains(TKey key, TValue value)
        {
            return vals[hashInRange(key)].Contains(new KeyValuePair<TKey, TValue>(key, value));
        }

        int hashInRange(TKey key)
        {
            return Math.Abs(hash(key) % vals.Length);
        }

        int hash(TKey key)
        {
            if (hasUserHash)
            {
                return hashFunc(key);
            }
            return key.GetHashCode();
        }
    }
}
