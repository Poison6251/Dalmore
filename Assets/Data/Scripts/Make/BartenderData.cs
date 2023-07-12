using UnityEngine;

[RequireComponent(typeof(BartenderAction))]
public class BartenderData : MonoBehaviour
{
    private BartenderAction action;
    private BartenderDirector director;
    protected Vector3 data;
    protected virtual void Awake()
    {
        action = GetComponent<BartenderAction>();
        director = GetComponent<BartenderDirector>();
    }
    protected void SendData()
    {
        director?.receiveData(data);
    }
    public void Success()
    {
        action?.Generate();
        print("¼º°ø");
    }
}