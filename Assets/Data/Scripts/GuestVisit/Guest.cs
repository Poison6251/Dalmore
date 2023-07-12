using UnityEngine;
using static Server;
using static DalmoreSetting;
public class Guest : MonoBehaviour
{
    [SerializeField] private IGuestMover mover;         // �̵����
    private void Awake()
    {
        if(mover == null) mover = GetComponent<IGuestMover>();
    }

    private void OnEnable()
    {
        mover.seatEvent += Order;
    }

    // �ֹ�
    public void Order() 
    {
        // �ֹ��� Ȯ��
        var list = DalmoreMenu.GetList;
        if (list == null || list.Count == 0) return;

        // �޴� ����
        var select = list[Random.Range(0, list.Count)];

        // �ֹ�
        OrderData order = new OrderData(select,this);
        OrderWindowList.AddItem(order);

        // �ֹ� �Ϸ�, �̺�Ʈ ����
        mover.seatEvent -= Order;
    }
    // �丮 �ޱ�
    public void Take(PriceInfo drink)
    {
        mover.MoveExit();
        print(drink.price.ToString() + "�� ����");
        string ment = drink.LevelOfCompletion > 0.7f ? "���ִ�" : drink.LevelOfCompletion > 0.4f ? "..." : "��湮 �ǻ� ����";
        print(ment);
    }

}
