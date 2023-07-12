using Item;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ItemData/RecipeData")]
public class RecipeData : ItemData
{
    [Header("Capacity : ml (1 oz == 30 ml) or ����")]
    [SerializeField] public List<Ingredients> data;
    public void Add(Ingredients ingredient)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i].Equals(ingredient))
            {
                data[i].Capacity += ingredient.Capacity;
                return;
            }
        }
        data.Add(ingredient);

    }
}

[Serializable]
public class Ingredients
{
    public ItemData itemData;
    public ItemModifier modifier;
    /// <summary> ml (1 oz == 30 ml) or ���� </summary>
    public float Capacity;

    public override bool Equals(object obj)
    {
        if (obj is Ingredients)
        {
            var ing = obj as Ingredients;
            return ing.itemData.ID == itemData.ID && ing.modifier == modifier;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(itemData, modifier, Capacity);
    }
}