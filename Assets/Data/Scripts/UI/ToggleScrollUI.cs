using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DalmoreSetting;

public class ToggleScrollUI : ViewportUI<OrderData>
{
    private WaitForSeconds delay = new WaitForSeconds(0.5f);        // ������
    private bool isOrdered;                                         // ������ Ʈ����

    [Header("�ֹ� Ȯ�� ��ư")]
    [SerializeField] protected Toggle ConfirmToggle;

    private void Awake()
    {
        Close();

        // �׽�Ʈ�� �ڵ�
        ItemDB.Clear();


        InputEventData newEvent = new InputEventData();
        newEvent.startButton = ToggleUI;
        DalmoreInputEventManager.Instance.AddInputEvent(newEvent);
        //#########################################################
    }

    // ������ ���� �о����
    protected override void ReadData(OrderData order, GameObject go)
    {
        go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = order.drink.Name;
        go.GetComponent<Toggle>().onValueChanged.AddListener(ConfirmEvent);


        void ConfirmEvent(bool isSelected)
        {
            if (!isSelected) return;

            // Ȯ��â ����
            ConfirmWindow.SetActive(true);

            // ��ư �̺�Ʈ ����
            ConfirmToggle.onValueChanged.RemoveAllListeners();
            ConfirmToggle.onValueChanged.AddListener((x) => { if (x) Order(order); });
        }
    }
    private void Order(OrderData data)
    {
        if (isOrdered) return;

        // �ֹ� ����
        OrderSheet.AddItem(data);
        // ���޵� �ֹ� â���� ����
        RemoveItem(data);
        // �ֹ�â �ݱ�
        Close();
        // ���� �ܰ�� �̵�
        FlowChanger.Instance.BarToMake();
        // ��ư ������
        StartCoroutine(ToggleDelay());
    }
    // �ֹ�â ��Ŭ
    private void ToggleUI()
    {
        if (ItemWindow.activeSelf)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
    // ��ư ������
    private IEnumerator ToggleDelay()
    {
        isOrdered = true;
        yield return delay;
        isOrdered = false;
    }
}
