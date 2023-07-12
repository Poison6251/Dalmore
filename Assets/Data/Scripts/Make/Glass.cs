using Item;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DalmoreSetting;

public class Glass : ItemDataInteractable
{
    //                               ↓ Layer 2    ↓ Layer 3
    [SerializeField] CapacityChecker leftChecker, rightChecker;
    private static List<Glass> activeGlasses = new List<Glass>();
    public static List<Glass> ActiveGlasses
    {
        get
        {
            return activeGlasses.ToList();
        }
    }

    private void OnEnable()
    {
        ActiveGlasses.Add(this);
    }
    private void OnDisable()
    {
        ActiveGlasses.Remove(this);
    }

    public void Enter(int layer)
    {
        // 초기화

        if(layer == 2)
        {
            leftChecker.Reset();
            leftChecker.gameObject.SetActive(true);
        }
        else if(layer ==3)
        {
            rightChecker.Reset();
            rightChecker.gameObject.SetActive(true);
        }
    }
    public void Exit(int layer, uint id)
    {
        // 아이템 데이터 검색
        ItemData data;
        if (!DB_Item.TryGetItemData(id, out data)) return;

        // 글라스에 술 정보 갱신하기
        Ingredients ingredients = new Ingredients();
        switch (layer)
        {
            case 2:
                if (leftChecker.GetCapacity != 0)
                {
                    ingredients.itemData = data;
                    ingredients.Capacity = leftChecker.GetCapacity;
                }
                leftChecker.gameObject.SetActive(false);
                break;
            case 3:
                if (rightChecker.GetCapacity != 0)
                {
                    ingredients.itemData = data;
                    ingredients.Capacity = rightChecker.GetCapacity;
                }
                rightChecker.gameObject.SetActive(false);
                break;
        }
        itemData.Add(ingredients);
    }
}
