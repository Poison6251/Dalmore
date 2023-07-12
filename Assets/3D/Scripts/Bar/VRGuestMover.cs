using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class VRGuestMover : GuestMover
{
    private WaitForSeconds delay = new WaitForSeconds(0.5f);
    private NavMeshAgent navAgent;
    private string walkAnimName = "WalkForward";

    protected override void Awake()
    {
        base.Awake();
        navAgent = GetComponent<NavMeshAgent>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    public override void MoveExit()
    {
        // �̵�
        Move(exitPoint.position);

        // ���� ó��
        StartCoroutine(ArrivedEvent(Exit));
        waiter.ExitGuest(GetHashCode());
        
    }

    private void Exit()
    {
        Wait();
        gameObject.SetActive(false);
    }
    private void Move(Vector3 pos)
    {
        // �̵� ����
        m_state = State.Move;
        guestAnim.Play(walkAnimName);
        navAgent.isStopped = false;
        navAgent.SetDestination(pos);
    }
    public override void MoveSeat(Transform target)
    {
        // �̵�
        navAgent.Warp(spawnPoint.position);
        Move(target.position);

        // �ɱ�
        StartCoroutine(ArrivedEvent(Seat));
    }

    // ���� �̺�Ʈ
    private IEnumerator ArrivedEvent(Action action)
    {
        while (true)
        {
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            if(!navAgent.pathPending) break;

            yield return delay;
        }
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        action?.Invoke();
    }
}
