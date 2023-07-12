using Item;
using System;
using UnityEngine;

[Flags]
public enum ItemModifier    // 아이템 수식어
{
    None =0,
    Stir = 1<<0,
    Shake = 1<<1,
    Float = 1<<2,
    Bland = 1<<3,
    Cold = 1<<4
}


public class BartenderAction : MonoBehaviour
{
    [SerializeField] public RecipeData target;              // 글라스, 셰이커
    [SerializeField] private ItemModifier modifier;         // 아이템 수식어

    public void Generate()
    {
        if (target == null || target.data.Count == 0) return;
        for(int i =0; i<target.data.Count;i++)
        {
            // 수식어 추가            
            target.data[i].modifier |= modifier;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var interaction = other.gameObject.GetComponent<IItemDataInteractable>();
        if (interaction != null)
        {
            target = interaction.GetInteractionData();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var interaction = other.gameObject.GetComponent<IItemDataInteractable>();
        if (interaction != null && target == interaction.GetInteractionData())
        {
            target = null;
        }
    }
}
