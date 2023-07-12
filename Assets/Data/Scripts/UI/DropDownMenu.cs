using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public enum EMenuType
{
    vertical, horizontal, grid
}
public class DropDownMenu : MonoBehaviour
{
    
    [SerializeField]
    private List<SerializableObject<IMenuElement>> items;

    [SerializeField]
    private EMenuType type;

    private IElementSelecter menu;

    public void AddItem(IMenuElement item)
    {
        var temp = new SerializableObject<IMenuElement>();
        temp.Ref = item;
        items.Add(temp);
    }

    public void RemoveItem(IMenuElement item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Ref == item)
            {
                items.RemoveAt(i);
                break;
            }
        }
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            menu.Input();
        }
    }
    private void Awake()
    {
        switch (type) 
        {
            case EMenuType.vertical:
                menu = new VRVerticalES(items);
                break;
            case EMenuType.horizontal:
                menu = new VRHorizontalES(items);
                break;
            case EMenuType.grid:
                break;
        }
    }
}


#region classes
public class VRHorizontalES : ElementSelecter
{
    public VRHorizontalES(List<SerializableObject<IMenuElement>> elements) : base(elements) { }

    public override void indexing()
    {
        Index += GetAxis("Horizontal");
    }


}
public class VRVerticalES : ElementSelecter
{
    public VRVerticalES(List<SerializableObject<IMenuElement>> elements) : base(elements) { }

    public override void indexing()
    {
        Index += GetAxis("Vertical");
    }
}
#endregion
#region interfaces
public interface IElementSelecter
{
    public IMenuElement Input();
    public List<SerializableObject<IMenuElement>> Elements { get; }
    public int Index { get; }
}
public interface IMenuElement
{
    public void Enter();
    public void Exit();
    public void Selected();
}
public abstract class ElementSelecter : IElementSelecter
{
    private string selectKey = "Fire1";
    public List<SerializableObject<IMenuElement>> Elements
    {
        get
        {
            return m_elements.ToList();
        }
    }
    private List<SerializableObject<IMenuElement>> m_elements;
    protected UnityEvent selectedEvent;
    public int Index
    {
        get
        {
            return index;
        }
        protected set
        {
            index = Mathf.Clamp(value, 0, Elements.Count - 1);
        }
    }
    protected int index;
    public ElementSelecter(List<SerializableObject<IMenuElement>> elements)
    {
        m_elements = elements;
    }
    public IMenuElement Input() 
    {
        if(m_elements==null || m_elements.Count == 0)
        {
            return null;
        }

        // 입력 계산
        indexing();
        Debug.Log(index);
        // 인덱스 이벤트 처리
        for (int i = 0; i < Elements.Count; i++)
        {
            if (i == Index)
            {
                Elements[i].Ref.Enter();
            }
            else
            {
                Elements[i].Ref.Exit();
            }
        }

        // 요소 선택키 입력
        if (selectKey!=null && selectKey !="" && GetAxis(selectKey)!=0)
        {
            Elements[Index].Ref.Selected();
            return Elements[Index].Ref;
        }

        // 선택된 요소 없음
        return null;
    }
    protected Func<string, int> GetAxis = (string key) =>
    {
        return (int)UnityEngine.Input.GetAxisRaw(key);
    };
    public abstract void indexing();
}
#endregion