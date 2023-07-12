using System;
using UnityEngine;


public enum EFlowState
{
    none,title, bar, make, store
}

public class FlowManager : MonoBehaviour
{
    private IFlow m_flow = null;
    private EFlowState m_state = EFlowState.none;
    public EFlowState GetState
    {
        get
        {
            return m_state;
        }
    }
    //[SerializeField] private Flow title, bar, make, store;
    [SerializeField] private SerializableObject<IFlow> title, bar, make, store;

    public void ChangeFlow(EFlowState goToFlow)
    {
        m_flow?.Exit();
        m_flow = GetFlow(goToFlow);
        m_state = goToFlow;
        m_flow.Enter();
    }

    private IFlow GetFlow(EFlowState goToFlow)
    {

        switch (goToFlow) 
        {
            case EFlowState.title:
                return title.Ref;
            case EFlowState.bar:
                return bar.Ref;
            case EFlowState.make:
                return make.Ref;
            case EFlowState.store:
                return store.Ref;
            default:
                return null;
        }

        
    }
}

[Serializable]
public class SerializableObject<T> where T : class
{
    [SerializeField]
    private MonoBehaviour m_RefObj;
    public T Ref 
    {
        get
        {   
            return m_RefObj as T;
        }
        set
        {
            m_RefObj = value as MonoBehaviour;
        }
    }

}


