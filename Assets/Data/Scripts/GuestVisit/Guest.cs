using UnityEngine;
using static Server;
using static DalmoreSetting;
public class Guest : MonoBehaviour
{
    [SerializeField] private IGuestMover mover;         // 이동기능
    private void Awake()
    {
        if(mover == null) mover = GetComponent<IGuestMover>();
    }

    private void OnEnable()
    {
        mover.seatEvent += Order;
    }

    // 주문
    public void Order() 
    {
        // 주문판 확인
        var list = DalmoreMenu.GetList;
        if (list == null || list.Count == 0) return;

        // 메뉴 선택
        var select = list[Random.Range(0, list.Count)];

        // 주문
        OrderData order = new OrderData(select,this);
        OrderWindowList.AddItem(order);

        // 주문 완료, 이벤트 삭제
        mover.seatEvent -= Order;
    }
    // 요리 받기
    public void Take(PriceInfo drink)
    {
        mover.MoveExit();
        print(drink.price.ToString() + "원 지불");
        string ment = drink.LevelOfCompletion > 0.7f ? "맛있다" : drink.LevelOfCompletion > 0.4f ? "..." : "재방문 의사 없음";
        print(ment);
    }

}
