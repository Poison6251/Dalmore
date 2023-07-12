using UnityEngine;

public class DalmorePlayer : MonoBehaviour
{
    private uint money; public uint Money => money;
    public bool isEnable { get; protected set; }
    public virtual void Enalbe()
    {

    }
    public virtual void Disalbe()
    {

    }
}