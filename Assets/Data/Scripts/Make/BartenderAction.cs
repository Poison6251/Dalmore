using Item;
using System;
using UnityEngine;

[Flags]
public enum ItemModifier    // ������ ���ľ�
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
    [SerializeField] public RecipeData target;              // �۶�, ����Ŀ
    [SerializeField] private ItemModifier modifier;         // ������ ���ľ�

    public void Generate()
    {
        if (target == null || target.data.Count == 0) return;
        for(int i =0; i<target.data.Count;i++)
        {
            // ���ľ� �߰�            
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
