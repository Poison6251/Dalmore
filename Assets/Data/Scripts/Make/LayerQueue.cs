using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LayerQueue : ScriptableObject
{
    [SerializeField]
    private List<int> Layer;

    private Queue<int> queue;
    private Queue<int> Queue
    {
        get
        {
            if (queue == null)
            {
                queue = new Queue<int>();
                foreach (var item in Layer)
                {
                    queue.Enqueue(item);
                }
            }
            return queue;
        }

    }

    public int GetLayer
    {
        get
        {
            if (Queue.Count == 0) return - 1;
            return Queue.Dequeue();
        }
    }
    public void GetBack(int layer)
    {
        if (layer < 0 || queue.Contains(layer)) return;
        queue.Enqueue(layer);
    }
}
