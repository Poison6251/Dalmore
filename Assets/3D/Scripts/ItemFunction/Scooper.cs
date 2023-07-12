using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scooper : MonoBehaviour
{
    // 스쿠퍼가 담고 있는 오브젝트의 프리팹
    private GameObject objectPrefab;

    // 스쿠퍼가 담고 있는 오브젝트의 개수
    [SerializeField]
    private uint objectCount;

    [Tooltip("한번에 오브젝트를 몇개 담을것인가?")]
    [SerializeField] private uint Count;

    public void IceScoop()
    {
        if (objectCount != 0) return;

        // 스쿠퍼에 얼음이 담겨 있는 모습이 되도록 해야 함

        // objectPrefab에 얼음 프리팹을 넣어줘야함

        objectCount = Count;
    }

    public void DropIce(int Count)
    {
        if (objectCount == 0 && Count < 0) { return; }

        // 컵한테 얼음을 드롭했다는 입력을 넣어줘야함

        if(objectCount < Count)
        {
            objectCount = 0;
        }
        else
        {
            objectCount -= (uint)Count;
        }
    }
}
