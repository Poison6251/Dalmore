using System.Linq;
using UnityEngine;
using static DalmoreSetting;

// 서빙하는 사람
public class Server : MonoBehaviour
{
    // 손님한테 요리를 준다
    public void Serving()
    {
        // 주문이 없다.
        if (OrderSheet.GetList.Count == 0) return;

        // 손님에게 요리를 건넨다.
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

    // 점수 계산
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

    // 점수 데이터
    public struct PriceInfo 
    {
        public uint ItemID;                 // 아이템
        public float LevelOfCompletion;      // 완성도
        public float price;                 // 가격
    }

}
