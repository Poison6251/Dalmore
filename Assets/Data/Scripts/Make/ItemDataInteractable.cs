using Item;
using UnityEngine;

public class ItemDataInteractable : MonoBehaviour, IItemDataInteractable
{
    [SerializeField]
    protected RecipeData itemData;
    public RecipeData GetInteractionData()
    {
        return itemData;
    }
}
