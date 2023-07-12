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
        // 이동
        Move(exitPoint.position);

        // 퇴장 처리
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
        // 이동 로직
        m_state = State.Move;
        guestAnim.Play(walkAnimName);
        navAgent.isStopped = false;
        navAgent.SetDestination(pos);
    }
    public override void MoveSeat(Transform target)
    {
        // 이동
        navAgent.Warp(spawnPoint.position);
        Move(target.position);

        // 앉기
        StartCoroutine(ArrivedEvent(Seat));
    }

    // 도착 이벤트
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
