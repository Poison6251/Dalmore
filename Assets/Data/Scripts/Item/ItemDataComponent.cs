using Item;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataComponent : MonoBehaviour
{
    [SerializeField] private ItemData ItemDataOrigin;
    private ItemData m_ItemData;
    public ItemData GetItemData
    {
        get
        {
            if (m_ItemData == null)
            {
                if (ItemDataOrigin == null) ItemDataOrigin = null;
                else
                {
                    m_ItemData = Instantiate(ItemDataOrigin);
                }

            }
            return m_ItemData;
        }
    }
    public IItemDataReader GetReader()
    {
        if (GetItemData.GetType() == typeof(ItemData)) { return new ItemdataReader(); }
        else if (GetItemData.GetType() == typeof(DrinkItemData)) { return new DrinkItemReader(); }
        else if(GetItemData.GetType() == typeof(RecipeData)) { return new RecipeReader(); }
        else return null;

    }

}

public interface IItemDataReader 
{
    public List<string> Read(ItemData data);
}
public class ItemdataReader : IItemDataReader
{
    public List<string> Read(ItemData data)
    {
        var read = new List<string>
        {
            data.Name,
            data.Discription
        };
        return read;
    }
}
public class DrinkItemReader : IItemDataReader
{
    public List<string> Read(ItemData data)
    {
        var m_data = data as DrinkItemData;
        if(m_data ==null) return null;

        var read = new List<string>
        {
            m_data.Name+"\n",
            m_data.ml.ToString()+" ml"
        };
        return read;
    }
}

public class RecipeReader : IItemDataReader
{
    public List<string> Read(ItemData data)
    {
        var m_data = data as RecipeData;
        if (m_data == null) return null;

        var read = new List<string>();
        foreach(var i in m_data.data)
        {
            read.Add(i.itemData.Name + " " + i.Capacity+"\n");
        }
        if (read.Count != 0)
        {
            read[read.Count - 1]=read[read.Count - 1].Replace("\n", "");
        }
        return read;
    }
}
