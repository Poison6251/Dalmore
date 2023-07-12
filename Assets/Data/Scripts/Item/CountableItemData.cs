using Item;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemData/CountItemData")]
public class CountableItemData : ItemData
{
    [SerializeField] private uint count;
}