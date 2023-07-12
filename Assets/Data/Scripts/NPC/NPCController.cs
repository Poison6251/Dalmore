using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    private Queue<Vector3> paths;
    private Vector3 pos;
    public float moveSpeed = 30;
    private float delay = 0.3f;

    private void Awake()
    {
        paths = new Queue<Vector3>();
    }

    public void MoveTo(Vector3 pos)
    {
        
        this.pos = pos;
        agent.SetDestination(pos);
        GetComponent<Animator>()?.Play("WalkForward");
        /*
        paths.Enqueue(pos);
        if (paths.Count == 1) this.pos = pos;
        StartCoroutine(Walk());
        */
    }
    public void Seating()
    {
        agent.Warp(pos);
        agent.ResetPath();
        transform.LookAt(GameObject.Find("Player").transform);
        GetComponent<Animator>()?.Play("Idle01");
    }

    private IEnumerator Walk()
    {
        GetComponent<Animator>()?.Play("WalkForward");
        var forward = (pos - transform.position).normalized;
        transform.LookAt(pos);
        GetComponent<Rigidbody>().AddForce(forward* moveSpeed);
        yield return new WaitForSeconds(delay);

        if (Vector3.Distance(transform.position, pos) <= 0.5)
        {
            if(paths.Count!=0) paths.Dequeue();
            if (paths.Count == 0)
            {
                transform.position = pos;
                var player = GameObject.Find("Player");
                if (player != null)
                {
                    transform.LookAt(player.transform);
                }
                
                GetComponent<Animator>()?.Play("Idle01");
                yield break;
            }
            pos = paths.Peek();
        }

        StartCoroutine(Walk());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
