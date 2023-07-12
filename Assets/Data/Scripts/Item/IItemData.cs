using M_DB;
using UnityEngine;

namespace Item
{
    public abstract class ItemData : ScriptableObject, IQueryableToDB
    {
        [SerializeField] private uint _id; public uint ID => _id;
        [TextArea, SerializeField] private string _name; public string Name => _name;
        [TextArea, SerializeField] private string _discription; public string Discription
                                                                         => _discription;
        [SerializeField] private uint price; public uint Price => price;


    }

    public abstract class ModifierItemData : ItemData
    {
        public ItemModifier Modifier;
    }

    public interface IItemDataInteractable
    {
        public RecipeData GetInteractionData();
    }
}
