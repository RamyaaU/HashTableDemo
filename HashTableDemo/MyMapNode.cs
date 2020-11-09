using System;
using System.Collections.Generic;
using System.Text;

namespace HashTableDemo
{
    public class MyMapNode<K,V>
    {
        public readonly int size;
        public readonly LinkedList<keyValue<K, V>>[] items;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyMapNode{K, V}"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        public MyMapNode(int size)
        {
            this.size = size;
            this.items = new LinkedList<keyValue<K, V>>[size];
        }

        /// <summary>
        /// get set methods 
        /// </summary>
        /// <typeparam name="k"></typeparam>
        /// <typeparam name="v"></typeparam>
        public struct keyValue<k, v>
        {
            public k key { get; set; }
            public v value { get; set; }
        }

        /// <summary>
        /// Gets the linkedlist.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        protected LinkedList<keyValue<K,V>> GetLinkedlist(int position)
        {
            LinkedList<keyValue<K, V>> linkedlist = items[position];

            if(linkedlist==null)
            {
                linkedlist = new LinkedList<keyValue<K, V>>();
                items[position] = linkedlist;
            }

            return linkedlist;
        }

        /// <summary>
        /// Gets the array position.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected int GetArrayPosition(K key)
        {
            //gethashcode gives position value using inbuilt function
            int position = key.GetHashCode() % size;
            //returning absolute position
            return Math.Abs(position);
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public V Get(K key)
        {
            int position = GetArrayPosition(key);

            LinkedList<keyValue<K, V>> linkedlist = GetLinkedlist(position);

            foreach (keyValue<K, V> item in linkedlist)
            {
                if (item.key.Equals(key))
                {
                    return item.value;
                }
            }
            return default;
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add (K key, V value)
        {
            int position = GetArrayPosition(key);
            LinkedList<keyValue<K, V>> linkedlist = GetLinkedlist(position);
            keyValue<K, V> item = new keyValue<K, V>() { key = key, value = value };

            linkedlist.AddLast(item);
        }

        public void Remove(K key)
        {
            int position = GetArrayPosition(key);
            LinkedList<keyValue<K, V>> linkedlist = GetLinkedlist(position);

            bool itemFound = false;

            keyValue<K, V> foundItem = default(keyValue<K, V>);
            
             foreach(keyValue<K,V> item in linkedlist)
            {
                if(item.key.Equals(key))
                {
                    itemFound = true;
                    foundItem = item;
                }
            }

            if(itemFound)
            {
                linkedlist.Remove(foundItem);
            }
        }
    }
}
