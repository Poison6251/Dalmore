using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �߻� Ŭ����  => �ڽ� Ŭ����
public abstract class Flow : MonoBehaviour, IFlow
{
    // �߻� �޼���
    public abstract void Enter();
    public abstract void Exit();
   
}


public interface IFlow
{
    public void Enter();
    public void Exit();
}

