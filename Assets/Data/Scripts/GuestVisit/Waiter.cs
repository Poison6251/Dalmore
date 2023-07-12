using System;
using System.Collections.Generic;
using UnityEngine;


public class Waiter : MonoBehaviour, IWaiter
{
    private const int EmptySeat = 0;
    private List<int> seatGuest;                        // ���ڿ� ���� �մ�
    private Queue<Action<Transform>> waitingQueue;      // ��⿭
    [SerializeField] private Transform[] Seats;         // �¼�

    private void Awake()
    {
        // ������ �ʱ�ȭ
        if (Seats == null)
        {
            Seats = new Transform[0];
            Debug.LogError("�¼� ����!");
        }
        waitingQueue = new Queue<Action<Transform>>();
        seatGuest = new List<int>(Seats.Length);
        for(int i = 0; i < Seats.Length; i++)
        {
            seatGuest.Add(EmptySeat);
        }
    }
    // �մ� ����
    public void GuestEnterEvent(Action<Transform> GuestEnterAction, int guestHashcode)
     {
        if(seatGuest == null) Awake();
         
        // �¼� Ȯ��
        int seatNum = CheckSeat();
        
        //  �� ��⿭ ����               �� ����                                          
        if (waitingQueue.Count != 0 || seatNum == -1)
        {
            // ���
            waitingQueue.Enqueue(GuestEnterAction);
            return;
        }

        // �¼� ����
        GuestEnterAction?.Invoke(Seats[seatNum]);
        seatGuest[seatNum] = guestHashcode;
    }
    // �մ� ����
    public void ExitGuest(int guestHashcode)
    {
        // �¼� Ȯ��
        int index = seatGuest.IndexOf(guestHashcode);
        if (index == -1) return;

        // �մ� ����
        seatGuest[index] = EmptySeat;

        int seatNum = CheckSeat();
        //  �� ��⿭ ����               �� ����                                          
        if (waitingQueue.Count == 0 || seatNum == -1) return;

        // ���� �մ� ����
        var Guest = waitingQueue.Dequeue();
        Guest.Invoke(Seats[seatNum]);
        seatGuest[seatNum] = Guest.GetHashCode();

    }
    // �¼� Ȯ��
    private int CheckSeat()
    {
        // ���ڸ� Ȯ��
        for (int seatNum = 0; seatNum < seatGuest.Count; seatNum++)
        {
            if (seatGuest[seatNum]== EmptySeat) return seatNum;
        }
        // ����
        return -1;
    }
}


public interface IWaiter
{
    // �մ� ����
    public void GuestEnterEvent(Action<Transform> GuestEnterEvent, int guestHashcode);
    public void ExitGuest(int guestHashcode);
}
