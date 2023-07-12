using UnityEngine;

public class FlowChanger : MonoBehaviour
{
    public static FlowChanger Instance { get; private set; }
    private FlowManager flowManager;

    private void Awake()
    {
        flowManager = GetComponent<FlowManager>();
        if (Instance == null)
        {
            Instance = this;
            flowManager.ChangeFlow(EFlowState.title);
        }
    }

    [ContextMenu("TitleToBar")]
    public void TitleToBar()
    {
        if (flowManager.GetState == EFlowState.title)
        {
            flowManager.ChangeFlow(EFlowState.bar);
        }
    }

    [ContextMenu("BarToMake")]
    public void BarToMake()
    {
        if (flowManager.GetState == EFlowState.bar)
        {
            flowManager.ChangeFlow(EFlowState.make);
        }
    }

    [ContextMenu("MakeToStore")]
    public void MakeToStore()
    {
        if (flowManager.GetState == EFlowState.make)
        {
            flowManager.ChangeFlow(EFlowState.store);
        }
    }
    [ContextMenu("StoreToBar")]
    public void StoreToBar()
    {
        if (flowManager.GetState == EFlowState.store)
        {
            flowManager.ChangeFlow(EFlowState.bar);
        }
    }

}
