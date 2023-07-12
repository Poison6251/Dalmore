using Item;
using M_DB;
using System.Collections.Generic;

public class EditableDB<T> : DB<T> where T : IQueryableToDB
{
    // DB 요소 수정 기능
    public void AddItem(T item)
    {
        if (item == null) return;
        NotNullList();
        list.Add(item);
    }
    public void RemoveItem(T item)
    {
        NotNullList();
        list.Remove(item);
    }
    private void NotNullList()
    {
        if(list == null)
        {
            list = new List<T>();
        }
    }
    public void Clear()
    {
        NotNullList();
        list.Clear();
    }
}

public struct OrderData : IQueryableToDB
{
    public RecipeData drink;
    public Guest orderGuest;
    public uint ID => drink.ID;
    public OrderData(RecipeData Drink, Guest OrderGuest)
    {
        drink = Drink;
        orderGuest = OrderGuest;
    }
}
