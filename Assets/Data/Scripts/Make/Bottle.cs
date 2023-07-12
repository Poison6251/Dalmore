using Obi;
using System.Collections;
using UnityEngine;

/*
    [Flow] ( + : �̺�Ʈ ȣ�� �ʿ�; # : ����; )
    + StartEvent()  : ��� ����
    + EndEvent()    : ��� �ߴ�

    [ä��]
    data ���� (x : �ܷ�; y : ������ ������ ��뷮; )
 */

public class Bottle : BartenderData
{
    [SerializeField] private ObiEmitter emitter, capacityEmitter;
    [SerializeField] private ObiSolver solver;
    [SerializeField] private float Power = 1f;
    private WaitForSeconds delay = new WaitForSeconds(0.5f);
    private int m_Layer =-1;


    protected override void Awake()
    {
        base.Awake();
        emitter.KillAll();
        data.x = solver.allocParticleCount;

        StartEvent();
    }
    public void StartEvent()
    {
        //// ����� ���̾� �Ҵ�ޱ�
        //m_Layer = Layer.GetLayer;
        //if(m_Layer == -1)
        //{
        //    throw new System.Exception("Dont get layer");
        //}
        //// �۶� �����ͼ� ���̾� Ŀ����
        //var glasses = Glass.ActiveGlasses;
        //foreach( var glass in glasses )
        //{
        //    glass.Enter(m_Layer);
        //}
        // ���� ����
        StopAllCoroutines();
        StartCoroutine(Puor());
    }

    public void EndEvent()
    {
        // �۶� �����ͼ� ���� ����
        //var glasses = Glass.ActiveGlasses;
        //foreach (var glass in glasses)
        //{
        //    glass.Exit(m_Layer,id);
        //}

        //// ���̾� �����ֱ�
        //Layer.GetBack(m_Layer);
        //m_Layer = -1;

        // ���� ����
        StopAllCoroutines();
        data.y = 0;
    }
    private IEnumerator Puor()
    {
        int before = solver.simplices.count;
        while (true){ 
            yield return delay;

            // �� �ܷ�
            if(data.x <= 0)
            {
                emitter.speed = 0;
                yield break;
            }

            // �ٴ��� ���� ���� ���� (0 ~ 1).
            var ax = GetTowardFloor(transform.eulerAngles.x);      
            var az = GetTowardFloor(transform.eulerAngles.z);      
            var angle = Mathf.Max(ax, az);

            // ���� ������ ���� ��Ƴ���.
            emitter.speed = angle * Power;
            capacityEmitter.speed = angle * Power;

            // ������ ó��
            data.y = (solver.simplices.count - before)/2;       // 1 ������ ���� ����� ��
            data.x -= data.y;                                   // �ܷ�
            before = solver.simplices.count;
            SendData();
        }

    }

    // �ٴ��� ���� ������ 0 ~ 1�� ��ȯ
    private float GetTowardFloor(float pow)
    {
        if (pow == 0) return 0;
        if (pow <= 90 || 270 <= pow) return 0;
        pow = Mathf.Abs(180 - pow);
        Vector2 normal = new Vector2(pow, 90);
        normal.Normalize();
        pow = 1 - normal.x;
        return pow;
    }
}
