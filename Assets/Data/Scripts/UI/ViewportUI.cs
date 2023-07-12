using UnityEngine;
using M_DB;
using UnityEngine.UI;

public class ViewportUI<T> : MonoBehaviour where T : IQueryableToDB
{
    [Header("Viewport의 Content 오브젝트")]
    [SerializeField] protected GameObject Content;
    [Header("UI 창에 출력될 DB")]
    [SerializeField] protected EditableDB<T> ItemDB;
    [Header("Content에 표시할 아이템 프리펩")]
    [SerializeField] protected GameObject itemPre;
    [Header("아이템을 표시하는 UI창")]
    [SerializeField] protected GameObject ItemWindow;
    [Header("아이템 선택 후 출력될 확인창")]
    [SerializeField] protected GameObject ConfirmWindow;
    

    // 오픈
    public void Open()
    {
        // 창 열기
        ItemWindow.SetActive(true);
        ConfirmWindow.SetActive(false);

        // UI창의 모든 아이템 삭제
        for(int i = 0; i<Content.transform.childCount; i++)
        {
            Content.transform.GetChild(i).gameObject.SetActive(false);
        }

        // 아이템 목록 주입
        var itemList = ItemDB.GetList;
        for (int i =0; i< itemList.Count;i++)
        {
            Content.transform.GetChild(i).gameObject.SetActive(true);
            ReadData(itemList[i], Content.transform.GetChild(i).gameObject);
        }
    }
    // 클로즈
    public void Close()
    {
        // 창 닫기
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

    // 데이터 읽어오기
    protected virtual void ReadData(T item, GameObject go) { }

}
