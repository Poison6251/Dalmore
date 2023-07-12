using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisitDirector : MonoBehaviour
{
    private WaitForSeconds delay = new WaitForSeconds(5f);      // ���� ������
    private List<Guest> list;                                   // �մԵ�

    [SerializeField] private Guest[] guest;                     // �մ� ������
    [SerializeField] private int maxGuestCount;                 // �ִ� �մ� ��
    

    private void Awake()
    {
        list = new List<Guest>();
        for(int i=0; i<maxGuestCount; i++)
        {
            guest[i].gameObject.SetActive(false);
            list.Add(guest[i]);
        }
    }
    private void OnEnable()
    {
        StartCoroutine(Spawn());
        
    }


    private IEnumerator Spawn()
    {
        while (true)
        {
            if (list.Count(x=>x.gameObject.activeSelf) < maxGuestCount)
            {
                var index = list.FindIndex(x => !x.gameObject.activeSelf);
                if (index>=0)
                {
                    list[index].gameObject.SetActive(true);
                    
                }
            }
                yield return delay;
        }
    }
}
