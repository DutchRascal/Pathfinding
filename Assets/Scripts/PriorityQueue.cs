using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T> where T : IComparable<T>
{
    List<T> data;

    public PriorityQueue()
    {
        this.data = new List<T>();
    }

    public void Enqueue(T item)
    {
        data.Add(item);
        int childindex = data.Count - 1;
        while (childindex > 0)
        {
            int parentindex = (childindex - 1) / 2;
            if (data[childindex].CompareTo(data[parentindex]) >= 0)
            {
                break;
            }
            T tmp = data[childindex];
            data[childindex] = data[parentindex];
            data[parentindex] = tmp;
            childindex = parentindex;
        }
    }
}
