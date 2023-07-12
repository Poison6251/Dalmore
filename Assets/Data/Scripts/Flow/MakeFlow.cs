using UnityEngine;
using static DalmoreSetting;

public class MakeFlow : Flow
{
    [SerializeField] private GameObject bar, make, store, title;
    [SerializeField] private Server server;                         // 종업원

    public uint OrderNum { get; private set; }
    InputEventData makeEvent;
    public override void Enter()
    {
        bar.SetActive(false);
        make.SetActive(true);
        store.SetActive(false);
        title.SetActive(false);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        // 입력 이벤트
        makeEvent = new InputEventData();
        makeEvent.aButton = ()=>server.Serving();
        DalmoreInputEventManager.Instance.AddInputEvent(makeEvent);

        // 성분표 초기화
        //MakingLog.Clear();
    }

    public override void Exit()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        make.SetActive(false);

        // 입력 이벤트 제거
        DalmoreInputEventManager.Instance.RemoveInputEvent(makeEvent);
    }

}
