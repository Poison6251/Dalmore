using UnityEngine;

public class TitleFlow : Flow
{
    [SerializeField] private GameObject bar, make, store, title;
    private InputEventData titleInputEvent = new InputEventData();
    public override void Enter()
    {
        bar.SetActive(false);
        make.SetActive(false);
        store.SetActive(false);
        title.SetActive(true);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        var manager = DalmoreInputEventManager.Instance;

        titleInputEvent.aButton = FlowChanger.Instance.TitleToBar;
        manager.AddInputEvent(titleInputEvent);
    }

    public override void Exit()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        var manager = DalmoreInputEventManager.Instance;
        manager.RemoveInputEvent(titleInputEvent);

        title.SetActive(false);
    }
}
