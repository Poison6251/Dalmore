using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DalmoreSetting;

public class ToggleScrollUI : ViewportUI<OrderData>
{
    private WaitForSeconds delay = new WaitForSeconds(0.5f);        // 딜레이
    private bool isOrdered;                                         // 딜레이 트리거

    [Header("주문 확인 버튼")]
    [SerializeField] protected Toggle ConfirmToggle;

    private void Awake()
    {
        Close();

        // 테스트용 코드
        ItemDB.Clear();


        InputEventData newEvent = new InputEventData();
        newEvent.startButton = ToggleUI;
        DalmoreInputEventManager.Instance.AddInputEvent(newEvent);
        //#########################################################
    }

    // 아이템 정보 읽어오기
    protected override void ReadData(OrderData order, GameObject go)
    {
        go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = order.drink.Name;
        go.GetComponent<Toggle>().onValueChanged.AddListener(ConfirmEvent);


        void ConfirmEvent(bool isSelected)
        {
            if (!isSelected) return;

            // 확인창 열기
            ConfirmWindow.SetActive(true);

            // 버튼 이벤트 주입
            ConfirmToggle.onValueChanged.RemoveAllListeners();
            ConfirmToggle.onValueChanged.AddListener((x) => { if (x) Order(order); });
        }
    }
    private void Order(OrderData data)
    {
        if (isOrdered) return;

        // 주문 전달
        OrderSheet.AddItem(data);
        // 전달된 주문 창에서 제거
        RemoveItem(data);
        // 주문창 닫기
        Close();
        // 제작 단계로 이동
        FlowChanger.Instance.BarToMake();
        // 버튼 딜레이
        StartCoroutine(ToggleDelay());
    }
    // 주문창 토클
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
    // 버튼 딜레이
    private IEnumerator ToggleDelay()
    {
        isOrdered = true;
        yield return delay;
        isOrdered = false;
    }
}
