using UnityEngine;
using M_DB;
using UnityEngine.UI;

public class ViewportUI<T> : MonoBehaviour where T : IQueryableToDB
{
    [Header("Viewport�� Content ������Ʈ")]
    [SerializeField] protected GameObject Content;
    [Header("UI â�� ��µ� DB")]
    [SerializeField] protected EditableDB<T> ItemDB;
    [Header("Content�� ǥ���� ������ ������")]
    [SerializeField] protected GameObject itemPre;
    [Header("�������� ǥ���ϴ� UIâ")]
    [SerializeField] protected GameObject ItemWindow;
    [Header("������ ���� �� ��µ� Ȯ��â")]
    [SerializeField] protected GameObject ConfirmWindow;
    

    // ����
    public void Open()
    {
        // â ����
        ItemWindow.SetActive(true);
        ConfirmWindow.SetActive(false);

        // UIâ�� ��� ������ ����
        for(int i = 0; i<Content.transform.childCount; i++)
        {
            Content.transform.GetChild(i).gameObject.SetActive(false);
        }

        // ������ ��� ����
        var itemList = ItemDB.GetList;
        for (int i =0; i< itemList.Count;i++)
        {
            Content.transform.GetChild(i).gameObject.SetActive(true);
            ReadData(itemList[i], Content.transform.GetChild(i).gameObject);
        }
    }
    // Ŭ����
    public void Close()
    {
        // â �ݱ�
        ItemWindow.SetActive(false);
        ConfirmWindow.SetActive(false);
    }
    public void AddItem(T item)
    {
        ItemDB.AddItem(item);
    }
    public void RemoveItem(T item)
    {
        ItemDB.RemoveItem(item);
    }

    // ������ �о����
    protected virtual void ReadData(T item, GameObject go) { }

}
