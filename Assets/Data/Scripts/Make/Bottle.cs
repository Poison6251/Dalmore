using Obi;
using System.Collections;
using UnityEngine;

/*
    [Flow] ( + : 이벤트 호출 필요; # : 선택; )
    + StartEvent()  : 계산 시작
    + EndEvent()    : 계산 중단

    [채널]
    data 변수 (x : 잔량; y : 딜레이 동안의 사용량; )
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
        //// 사용할 레이어 할당받기
        //m_Layer = Layer.GetLayer;
        //if(m_Layer == -1)
        //{
        //    throw new System.Exception("Dont get layer");
        //}
        //// 글라스 가져와서 레이어 커넥팅
        //var glasses = Glass.ActiveGlasses;
        //foreach( var glass in glasses )
        //{
        //    glass.Enter(m_Layer);
        //}
        // 동작 시작
        StopAllCoroutines();
        StartCoroutine(Puor());
    }

    public void EndEvent()
    {
        // 글라스 가져와서 종료 선언
        //var glasses = Glass.ActiveGlasses;
        //foreach (var glass in glasses)
        //{
        //    glass.Exit(m_Layer,id);
        //}

        //// 레이어 돌려주기
        //Layer.GetBack(m_Layer);
        //m_Layer = -1;

        // 동작 종료
        StopAllCoroutines();
        data.y = 0;
    }
    private IEnumerator Puor()
    {
        int before = solver.simplices.count;
        while (true){ 
            yield return delay;

            // ↓ 잔량
            if(data.x <= 0)
            {
                emitter.speed = 0;
                yield break;
            }

            // 바닥을 향한 병의 기울기 (0 ~ 1).
            var ax = GetTowardFloor(transform.eulerAngles.x);      
            var az = GetTowardFloor(transform.eulerAngles.z);      
            var angle = Mathf.Max(ax, az);

            // 많이 기울수록 많이 쏟아낸다.
            emitter.speed = angle * Power;
            capacityEmitter.speed = angle * Power;

            // 데이터 처리
            data.y = (solver.simplices.count - before)/2;       // 1 딜레이 동안 사용한 양
            data.x -= data.y;                                   // 잔량
            before = solver.simplices.count;
            SendData();
        }

    }

    // 바닥을 향한 정도를 0 ~ 1로 변환
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
