using UnityEngine;
using static DalmoreSetting;

public class MakeFlow : Flow
{
    [SerializeField] private GameObject bar, make, store, title;
    [SerializeField] private Server server;                         // ������

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

        // �Է� �̺�Ʈ
        makeEvent = new InputEventData();
        makeEvent.aButton = ()=>server.Serving();
        DalmoreInputEventManager.Instance.AddInputEvent(makeEvent);

        // ����ǥ �ʱ�ȭ
        //MakingLog.Clear();
    }

    public override void Exit()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        make.SetActive(false);

        // �Է� �̺�Ʈ ����
        DalmoreInputEventManager.Instance.RemoveInputEvent(makeEvent);
    }

}
