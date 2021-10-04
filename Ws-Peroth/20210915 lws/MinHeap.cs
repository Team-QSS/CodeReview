using System;
using System.Collections.Generic;

public class MinHeap<T> where T : IComparable<T>
{ 
    private readonly List<T> heap = new List<T>();

    public int Count
    {
        get => heap.Count;
    }

    public void Add(T node)
    {
        heap.Add(node);

        for (int i = heap.Count - 1; i > 0;)
        {
            int parent = (i - 1) / 2;
            if (heap[parent].CompareTo(heap[i]) > 0)  // heap[parent] > heap[i]
            {
                Swap(parent, i);
                i = parent;
            }
            else break;
        }
    }

    public T Remove()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException();

        T root = heap[0];

        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);

        for (int i = 0, last = heap.Count - 1; i < last;)
        {
            int child = i * 2 + 1;

            if (child < last && heap[child].CompareTo(heap[child + 1]) > 0)    // heap[child] > heap[child + 1]
                child++;

            if (child > last || heap[i].CompareTo(heap[child]) <= 0)   // heap[i] <= heap[child]
                break;

            Swap(i, child);
            i = child;
        }

        return root;
    }

    private void Swap(int indexA, int indexB)
    {
        T temp = heap[indexA];
        heap[indexA] = heap[indexB];
        heap[indexB] = temp;
    }
}
