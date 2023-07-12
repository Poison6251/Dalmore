using System.Linq;
using UnityEngine;
using static DalmoreSetting;

// �����ϴ� ���
public class Server : MonoBehaviour
{
    // �մ����� �丮�� �ش�
    public void Serving()
    {
        // �ֹ��� ����.
        if (OrderSheet.GetList.Count == 0) return;

        // �մԿ��� �丮�� �ǳٴ�.
        var orders = OrderSheet.GetList;
        foreach (var order in orders)
        {
            PriceInfo price = new PriceInfo() {
                price = 0, ItemID = order.ID, LevelOfCompletion = 0};
            if(order.ID == MakingLog.ID)
            {
                price = SetPrice(MakingLog);
            }
            order.orderGuest.Take(price);
            OrderSheet.RemoveItem(order);
        }
        FlowChanger.Instance.MakeToStore();
    }

    // ���� ���
    private PriceInfo SetPrice(RecipeData drink)
    {
        PriceInfo priceInfo = new PriceInfo();
        if (DB_Recipe.TryGetItemData(drink.ID, out RecipeData recipe))
        {
            int count = 0;
            foreach(var i in recipe.data)
            {
                if (drink.data.Any(x=>x.Equals(i))) count ++;
            }
            priceInfo.ItemID = drink.ID;
            priceInfo.LevelOfCompletion = (float)count / (float)recipe.data.Count;
            priceInfo.price = recipe.Price * priceInfo.LevelOfCompletion;
        }
        else
        {
            Debug.LogError("Data Not Found");
        }
        return priceInfo;
    }

    // ���� ������
    public struct PriceInfo 
    {
        public uint ItemID;                 // ������
        public float LevelOfCompletion;      // �ϼ���
        public float price;                 // ����
    }

}
