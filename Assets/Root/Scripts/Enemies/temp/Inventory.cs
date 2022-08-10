using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Inventory : MonoBehaviour
{
    List<Item> listItems = new List<Item>();
    public List<Item> ListItems { get => listItems; }

    public event Action<Item> addItem;
    public void AddItems(Item item)
    {
        listItems.Add(item);
        addItem(item);
    }
    public bool IsItemInInventory(Item item)
    {
        int c = listItems.Where(t=>item==t).Count();
        Debug.Log(c);
        if (c == 0)
        {
            return false;
        }
        return true;
    }


}
