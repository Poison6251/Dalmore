using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ItemDataComponent))]
public class ItemDataWindow : MonoBehaviour
{
#if UNITY_EDITOR
    private GameObject UIWindow;                // ������ ����â
#else
    private static GameObject UIWindow;
#endif
    private static GameObject focusObject;      // ����â�� ����� ���
    private ItemDataComponent data;             // ������
    private IItemDataReader reader;             // ������
    private Text ui;                            // �ؽ�Ʈ UI

    // ������ �׽�Ʈ�� ��� ���
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
        // ��Ŀ�� �� �����۸� ���
        if (focusObject != gameObject)
        {
            View(false);
            return;
        }

        // ������ �о����
        ui.text = "";
        foreach (var t in reader.Read(data.GetItemData))
            ui.text += t.ToString();
    }

    // â ���
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
