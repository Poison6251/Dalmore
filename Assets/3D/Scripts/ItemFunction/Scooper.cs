using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scooper : MonoBehaviour
{
    // �����۰� ��� �ִ� ������Ʈ�� ������
    private GameObject objectPrefab;

    // �����۰� ��� �ִ� ������Ʈ�� ����
    [SerializeField]
    private uint objectCount;

    [Tooltip("�ѹ��� ������Ʈ�� � �������ΰ�?")]
    [SerializeField] private uint Count;

    public void IceScoop()
    {
        if (objectCount != 0) return;

        // �����ۿ� ������ ��� �ִ� ����� �ǵ��� �ؾ� ��

        // objectPrefab�� ���� �������� �־������

        objectCount = Count;
    }

    public void DropIce(int Count)
    {
        if (objectCount == 0 && Count < 0) { return; }

        // ������ ������ ����ߴٴ� �Է��� �־������

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
