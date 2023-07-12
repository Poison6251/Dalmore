using Item;
using UnityEngine;
using static DalmoreSetting;

public class MakeManager : Singleton<MakeManager>
{
    private FlowChanger flowChanger;
    private DrinkItemData drink;

    public void TakeOrder(uint drinkID)
    {
        if (!GetItemDB() || !GetDrinkData(drinkID) || !GetFlowChanger()) return;

        // Enter Make Flow
        FlowChanger.Instance.BarToMake();

        // Control Key Change (스터, 빌드, 쉐이크, 플로트, 블랜딩, 확인)

        // Timer Start

    }
    private bool GetItemDB()
    {
        if (DB_Item == null)
        {
            Debug.LogError("Can't Find ItemDB/Storage");
            return false;
        }
        return true;
    }
    private bool GetDrinkData(uint drinkID)
    {
        if (DB_Item.TryGetItemData(drinkID, out ItemData item))
        {
            drink = item as DrinkItemData;
        }
        if (drink == null)
        {
            Debug.LogError("Can't Find ItemData");
            return false;
        }
        return true;
    }
    private bool GetFlowChanger()
    {
        if (flowChanger == null)
        {
            flowChanger = FlowChanger.Instance;
        }
        if (flowChanger == null)
        {
            Debug.LogError("Can't Find FlowChanger");
            return false;
        }
        return true;
    }
}
