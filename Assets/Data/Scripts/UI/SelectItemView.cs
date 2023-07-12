using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SelectItemView : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> categories;
    [SerializeField]
    protected bool isKeyboard;

    protected List<SerializableObject<IMenuElement>> items;

    protected SelectHorizonES selecter;

    private InputEventData newEvent;
    private void Awake()
    {
        items = new List<SerializableObject<IMenuElement>>();
        for(int i=0; i<categories.Count; i++)
        {
            var temp = new SerializableObject<IMenuElement>();
            temp.Ref = categories[i].GetComponent<MenuElement>();
            items.Add(temp);
        }
        selecter = new SelectHorizonES(items);
        if (!isKeyboard)
        {
            newEvent = new InputEventData();
            newEvent.leftThumbstickPressed += selecter.IndexDown;
            newEvent.rightThumbstickPressed += selecter.IndexUp;
        }

        
    }

    private void Update()
    {
        if (!isKeyboard) return;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selecter.IndexUp();
            selecter.Input();
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selecter.IndexDown();
            selecter.Input();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            items[selecter.Index].Ref.Selected();
            selecter.Input();
            gameObject.SetActive(false);
        }

        
    }
    public void OnAllButton()
    {
        foreach(var i in categories)
        {
            i.SetActive(true);
        }
    }

    private void OnEnable()
    {
        if (isKeyboard) return;
        DalmoreInputEventManager.Instance.AddInputEvent(newEvent);
    }
    private void OnDisable()
    {
        if (isKeyboard) return;
        DalmoreInputEventManager.Instance.RemoveInputEvent(newEvent);
    }
}

public class SelectHorizonES : ElementSelecter
{
    public SelectHorizonES(List<SerializableObject<IMenuElement>> elements) : base(elements) { }

    public bool isDelay;

    public override void indexing()
    {

    }
    public void IndexUp()
    {
        if (isDelay) return;
        Index += 1;
    }
    public void IndexDown()
    {
        if (isDelay) return;
        Index -= 1;
    }
}