using Item;
using System.Collections;
using UnityEngine;

/*
    [Flow] ( + : 이벤트 호출 필요; # : 선택; )
    + Grab(true)    : 계산 허용
    + StartEvent()  : 계산 시작
  # + Grab(false)   : 계산 일시중지
  # + Grab(true)    : 계산 다시시작
    + EndEvent()    : 계산 중단
    + Grab(false)   : 계산 허용 X

    [채널]
    data 변수 (x : angle; y : 0 계산 중단, 1 계산 중, 2 성공;)
 */

public class Barspoon : BartenderData
{
    private float angle = 0f;                          
    private bool isGrabed;
    private Collider selectCol;
    private WaitForSeconds delay = new WaitForSeconds(0.3f);    // 계산 딜레이
    private InputEventData inputEventData;
    [Header("섞이는 힘"),SerializeField] private float pow = 1f;
    [Header("회전 횟수"),SerializeField] private int count;


    // isGarbed가 true여야 동작함
    public void Grab(bool _is)
    {
        print("grab  "+_is);
        isGrabed  = _is;
    }

    // 계산 시작
    public void StartEvent()
    {
        EndEvent();
        StartCoroutine(Stir());
    }
    // 계산 중단
    public void EndEvent()
    {
        StopAllCoroutines();
        // 컨트롤러 위치 감지 OFF
        DalmoreInputEventManager.Instance.RemoveInputEvent(inputEventData);
    }
    
    // 계산
    private IEnumerator Stir()
    {
        // 데이터 초기화
        Vector3 before= Vector3.zero;
        Vector3 after = Vector3.zero;
        angle = 0;
        data.y = 1f;

        // 컨트롤러 위치 감지 ON
        inputEventData = new InputEventData();
        inputEventData.rightHandPosition += (x) => {after = x; };
        DalmoreInputEventManager.Instance.AddInputEvent(inputEventData);

        while (true)
        {
            if (angle > 2 * Mathf.PI * count) break;
            if (isGrabed)
            {
                // 파워 계산
                var distance = Vector3.Distance(before, after);
                angle += pow * distance;
                before = after;

                // 데이터 전송 (x 채널 사용)
                data.x = angle;
                SendData();
            }

            yield return delay;
        }

        // 성공
        Success();
        data.y = 2f;
        SendData();

        // 데이터 전송 종료를 알려 줌 (y 채널 사용)
        data.y = 0f;
        SendData();

        // 컨트롤러 위치 감지 OFF
        DalmoreInputEventManager.Instance.RemoveInputEvent(inputEventData);
    }

    #region 3D
    private void OnTriggerEnter(Collider other)
    {
        // 상호작용 가능한 타입인지 검사
        var interaction = other.gameObject.GetComponent<IItemDataInteractable>();
        if (interaction != null)
        {
            print("start");
            // 상호작용
            StartEvent();
            // 타겟 확인
            selectCol = other;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // 타겟에서 벗어나면 종료
        if (selectCol == other)
        {
            print("end");
            EndEvent();
            selectCol = null;
        }

    }
    #endregion
}