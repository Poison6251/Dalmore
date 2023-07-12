using System;
using UnityEngine;

public interface IGuestMover
{
    // ���� ����
    public void MoveSeat(Transform target);
    // ����
    public void MoveExit();
    public event Action seatEvent;
}

public abstract class GuestMover : MonoBehaviour, IGuestMover
{
    protected enum State {Wait,Move,Seat}
    [SerializeField] protected Waiter waiter;                   // ������
    [SerializeField] protected Transform spawnPoint,exitPoint;  // ����, ���� ��ġ
    [SerializeField] private string seatAnimName = "Idle01";    // �ɴ� ���
    [SerializeField] private string idleAnimName = "Idle";      // ���̵� ���
    protected Animator guestAnim;                               // �մ� �ִϸ�����
    protected State m_state;                                    // �մ� ����
    public event Action seatEvent;                              // �ɰ� ���� ������ �̺�Ʈ


    protected virtual void Awake()
    {
        guestAnim = GetComponent<Animator>();
    }
    protected virtual  void OnEnable()
    {
        
        if(m_state== State.Wait)
        {
            // ���� ���
            transform.position = spawnPoint.position;
            waiter.GuestEnterEvent(MoveSeat,GetHashCode());
        }
    }

    // ���� ���� �� �ڸ��� �ɱ� ����
    public abstract void MoveSeat(Transform target);
    // �ڸ����� ���� ����
    public abstract void MoveExit();
    // ���� ������
    protected void Wait()
    {
        m_state = State.Wait;
        if(guestAnim==null) guestAnim= GetComponent<Animator>();
        guestAnim.Play(idleAnimName);
    }
    // �ɴ´�
    protected void Seat()
    {
        m_state = State.Seat;
        guestAnim.Play(seatAnimName);
        Invoke( nameof(SeatEvent),0.5f);

    }
    private void SeatEvent()
    {
        seatEvent?.Invoke();
    }
}
