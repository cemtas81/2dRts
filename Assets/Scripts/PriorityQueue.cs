using System;
using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<T> heap;
    private IComparer<T> comparer;

    public int Count => heap.Count;

    public PriorityQueue()
    {
        heap = new List<T>();
        comparer = Comparer<T>.Default;
    }

    public PriorityQueue(IComparer<T> comparer)
    {
        heap = new List<T>();
        this.comparer = comparer;
    }

    public void Enqueue(T item)
    {
        heap.Add(item);
        int childIndex = heap.Count - 1;
        while (childIndex > 0)
        {
            int parentIndex = (childIndex - 1) / 2;
            if (comparer.Compare(heap[childIndex], heap[parentIndex]) >= 0)
                break;

            Swap(childIndex, parentIndex);
            childIndex = parentIndex;
        }
    }

    public T Dequeue()
    {
        int lastIndex = heap.Count - 1;
        T removedItem = heap[0];
        heap[0] = heap[lastIndex];
        heap.RemoveAt(lastIndex);

        int currentIndex = 0;
        while (true)
        {
            int leftChildIndex = currentIndex * 2 + 1;
            int rightChildIndex = currentIndex * 2 + 2;

            if (leftChildIndex >= heap.Count)
                break;

            int childIndex = (rightChildIndex < heap.Count && comparer.Compare(heap[rightChildIndex], heap[leftChildIndex]) < 0) ?
                rightChildIndex : leftChildIndex;

            if (comparer.Compare(heap[currentIndex], heap[childIndex]) <= 0)
                break;

            Swap(currentIndex, childIndex);
            currentIndex = childIndex;
        }

        return removedItem;
    }

    public bool Contains(T item)
    {
        return heap.Contains(item);
    }

    private void Swap(int i, int j)
    {
        T temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }
}

