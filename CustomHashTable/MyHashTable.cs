using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomHashTable
{

    public class MyHashTable<TKey, TValue>
    {
        private const int DefaultCapacity = 16;
        private const double LoadFactor = 0.75;

        private LinkedList<KeyValuePair<TKey, TValue>>[] buckets;
        private int count;

        public MyHashTable() : this(DefaultCapacity) { }

        public MyHashTable(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            buckets = new LinkedList<KeyValuePair<TKey, TValue>>[capacity];
            count = 0;
        }

        public TValue this[TKey key]
        {
            get => Get(key);
            set => Put(key, value);
        }

        public TValue Get(TKey key)
        {
            if(key == null)
            {
                throw new ArgumentNullException();
            }
            int index = GetBucketIndex(key);

            if (buckets[index] == null)
            {
                throw new KeyNotFoundException();
            }

            foreach (var pair in buckets[index])
            {
                if (pair.Key.Equals(key))
                {
                    return pair.Value;
                }
            }

            throw new KeyNotFoundException();
        }

        public void Put(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }

            int index = GetBucketIndex(key);

            if (buckets[index] == null)
            {
                buckets[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
            }

            foreach (var pair in buckets[index])
            {
                if (pair.Key.Equals(key))
                {
                    buckets[index].Remove(pair);
                    break;
                }
            }

            buckets[index].AddLast(new KeyValuePair<TKey, TValue>(key, value));
            count++;

            if ((double)count / buckets.Length >= LoadFactor)
            {
                Resize();
            }
        }


        public void Remove(TKey key)
        {
            if (key == null)
            {
                return;
            }
            int index = GetBucketIndex(key);

            if (buckets[index] != null)
            {
                var node = buckets[index].First;

                while (node != null)
                {
                    if (node.Value.Key.Equals(key))
                    {
                        buckets[index].Remove(node);
                        count--;
                        return;
                    }

                    node = node.Next;
                }
            }
        }

        public bool ContainsKey(TKey key)
        {
            if (key == null)
            {
                return false;
            }
            int index = GetBucketIndex(key);

            if (buckets[index] != null)
            {
                foreach (var pair in buckets[index])
                {
                    if (pair.Key.Equals(key))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public int Count()
        {
            return count;
        }

        private int GetBucketIndex(TKey key)
        {
            int hashCode = key.GetHashCode();
            return Math.Abs(hashCode) % buckets.Length;
        }

        private void Resize()
        {
            var oldBuckets = buckets;
            buckets = new LinkedList<KeyValuePair<TKey, TValue>>[buckets.Length * 2];
            count = 0;

            foreach (var bucket in oldBuckets)
            {
                if (bucket != null)
                {
                    foreach (var pair in bucket)
                    {
                        Put(pair.Key, pair.Value);
                    }
                }
            }
        }
    }

}
