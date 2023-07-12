using UnityEngine;
using UnityEngine.Events;

public class MenuElement : MonoBehaviour, IMenuElement
{
    [SerializeField]
    private UnityEvent enter, exit, selected;
    public void Enter()
    {
        enter?.Invoke();
    }

    public void Exit()
    {
        exit?.Invoke();
    }

    public void Selected()
    {
        selected?.Invoke();
    }
}
