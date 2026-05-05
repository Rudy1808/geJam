using System;
using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<(T item, float priority)> heap = new List<(T, float)>();

    public int Count => heap.Count;

    public void Enqueue(T item, float priority)
    {
        heap.Add((item, priority));
        HeapifyUp(heap.Count - 1);
    }

    public T Dequeue()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("Queue is empty");

        T root = heap[0].item;

        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);

        HeapifyDown(0);

        return root;
    }

    public T Peek()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("Queue is empty");

        return heap[0].item;
    }

    private void HeapifyUp(int i)
    {
        while (i > 0)
        {
            int parent = (i - 1) / 2;

            if (heap[i].priority >= heap[parent].priority)
                break;

            Swap(i, parent);
            i = parent;
        }
    }

    private void HeapifyDown(int i)
    {
        int lastIndex = heap.Count - 1;

        while (true)
        {
            int left = i * 2 + 1;
            int right = i * 2 + 2;
            int smallest = i;

            if (left <= lastIndex && heap[left].priority < heap[smallest].priority)
                smallest = left;

            if (right <= lastIndex && heap[right].priority < heap[smallest].priority)
                smallest = right;

            if (smallest == i)
                break;

            Swap(i, smallest);
            i = smallest;
        }
    }

    private void Swap(int a, int b)
    {
        (heap[a], heap[b]) = (heap[b], heap[a]);
    }
}