﻿namespace DoublyLinkedList
{
    public class DoublyLinkedList<T>
    {
        Node<T>? first;
        Node<T>? last;
        public int Count { get; private set; }
        public DoublyLinkedList()
        {
            first = null;
            last = null;
            Count = 0;
        }

        public void Add(T value)
        {
            if (first == null) first = new Node<T>(value);
            else if (last == null)
            {
                last = new Node<T>(first, value);
                first.next = last;
            }
            else
            {
                var newNode = new Node<T>(last, value);
                last.next = newNode;
            }
            Count++;
        }

        public bool Remove(T value)
        {
            var currentNode = first;
            while (currentNode != null)
            {
                if (currentNode.data.Equals(value))
                {
                    if (currentNode.previous != null) currentNode.previous.next = currentNode.next;
                    if (currentNode.next != null) currentNode.next.previous = currentNode.previous;
                    Count--;
                    return true;
                }
                currentNode = currentNode.next;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (!(0 <= index && index < Count))
            {
                throw new ArgumentOutOfRangeException("Passed argument was out of the range of the list.");
            }
            var currentNode = first;
            for (int i = 0; i < index; i++) currentNode = currentNode.next;
            if (currentNode.previous != null) currentNode.previous.next = currentNode.next;
            if (currentNode.next != null) currentNode.next.previous = currentNode.previous;
            Count--;
        }

        public int IndexOf(T value)
        {
            var currentNode = first;
            int index = 0;
            while (currentNode != null)
            {
                if (currentNode.data.Equals(value)) return index;
                currentNode = currentNode.next;
            }
            return -1;
        }


    }
}
