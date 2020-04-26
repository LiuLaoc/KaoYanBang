using System;
using System.Collections;
using System.Collections.Generic;

class PriorityQueue<T>
{
    IComparer<T> comparer;
    T[] heap;

    public int Count { get; private set; }

    public PriorityQueue() : this(null) { }
    public PriorityQueue(int capacity) : this(capacity, null) { }
    public PriorityQueue(IComparer<T> comparer) : this(16, comparer) { }

    public PriorityQueue(int capacity, IComparer<T> comparer)
    {
        this.comparer = (comparer == null) ? Comparer<T>.Default : comparer;
        this.heap = new T[capacity];
    }

    public void Push(T v)
    {
        if (Count >= heap.Length) Array.Resize(ref heap, Count * 2);
        heap[Count] = v;
        SiftUp(Count++);
    }

    public T Peek()
    {
        var v = Top();
        return v;
    }
    public T Pop()
    {
        var v = Top();
        if (Count > 0)
        {
            heap[0] = heap[--Count];
            SiftDown(0);
        }
        return v;
    }

    public T Top()
    {
        if (Count > 0) return heap[0];
        return default(T);
    }

    void SiftUp(int n)
    {
        var v = heap[n];
        for (var n2 = n / 2; n > 0 && comparer.Compare(v, heap[n2]) < 0; n = n2, n2 /= 2) heap[n] = heap[n2];
        heap[n] = v;
    }
    void SiftUp(int k,T x)
    {
        while (k > 0)
        {
            int parent = (k - 1) >> 1;//parentNo = (nodeNo-1)/2
            T t = heap[parent];
            if (comparer.Compare(x, (T)t) <= 0)//调用比较器的比较方法
                break;
            heap[k] = t;
            k = parent;
        }
        heap[k] = x;
    }
    void SiftDown(int n)
    {
        var v = heap[n];
        for (var n2 = n * 2; n2 < Count; n = n2, n2 *= 2)
        {
            if (n2 + 1 < Count && comparer.Compare(heap[n2 + 1], heap[n2]) < 0) n2++;
            if (comparer.Compare(v, heap[n2]) >= 0) break;
            heap[n] = heap[n2];
        }
        heap[n] = v;
    }
    void SiftDown(int k,T x)
    {
        int half = Count >> 1;
        while (k < half)
        {
            int child = (k << 1) + 1;
            T c = heap[child];
            int right = child + 1;
            if (right < Count &&
                comparer.Compare((T)c, (T)heap[right]) < 0)
                c = heap[child = right];
            if (comparer.Compare(x, (T)c) >= 0)
                break;
            heap[k] = c;
            k = child;
        }
        heap[k] = x;
    }
    public void Clear()
    {
        while (Count > 0)
            Pop();
    }

    public void Remove(T t)
    {
        int i = IndexOf(t);
        if (i == -1) return;
        int s = --Count;
        if (s == i)
            heap[i] = default(T);
        else
        {
            T moved = (T)heap[s];
            SiftDown(i, moved);
        }
    }
    public T RemoveAt(int index)
    {
        int i = index;
        if (i == -1) return default;
        int s = --Count;
        T t = heap[i];
        if (s == i)
        {
            heap[i] = default;
        }
        else
        {
            T moved = (T)heap[s];
            SiftDown(i, moved);
        }
        return t;
    }

    public void Update(int index)
    {
        var move = RemoveAt(index);
        Push(move);
    }


    public int IndexOf(T t)
    {
        for(var i =0;i<Count;i++)
        {
            if (heap[i].Equals(t))
                return i;
        }
        return -1;
    }


}
