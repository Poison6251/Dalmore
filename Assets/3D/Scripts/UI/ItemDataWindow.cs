using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ItemDataComponent))]
public class ItemDataWindow : MonoBehaviour
{
#if UNITY_EDITOR
    private GameObject UIWindow;                // 아이템 정보창
#else
    private static GameObject UIWindow;
#endif
    private static GameObject focusObject;      // 정보창에 출력할 대상
    private ItemDataComponent data;             // 데이터
    private IItemDataReader reader;             // 리더기
    private Text ui;                            // 텍스트 UI

    // 에디터 테스트용 출력 기능
#if UNITY_EDITOR
    private void OnMouseEnter()
    {
        View(true);
    }
    private void OnMouseExit()
    {
        View(false);
    }
#endif
    

    void Awake()
    {
        data = GetComponent<ItemDataComponent>();
        reader = data.GetReader();
    }
    void Update()
    {
        // 포커싱 된 아이템만 출력
        if (focusObject != gameObject)
        {
            View(false);
            return;
        }

        // 데이터 읽어오기
        ui.text = "";
        foreach (var t in reader.Read(data.GetItemData))
            ui.text += t.ToString();
    }

    // 창 출력
    public void View(bool isView)
    {
        if(isView)
        {
#if UNITY_EDITOR
            UIWindow = Instantiate(Resources.Load<GameObject>("ItemDataWindow"));
#else
        if (UIWinodw == null)
        {
            UIWinodw = Instantiate(Resources.Load<GameObject>("ItemDataWindow"));
        }
#endif
            ui = UIWindow.GetComponentInChildren<Text>();
            focusObject = gameObject;
            UIWindow.SetActive(true);
            UIWindow.transform.position = transform.position;
            enabled = true;
        }
        else
        {
#if UNITY_EDITOR
            Destroy(UIWindow);
#else
            if (focusObject == gameObject) UIWindow.SetActive(false);
#endif
            enabled = false;
        }
    }
}
