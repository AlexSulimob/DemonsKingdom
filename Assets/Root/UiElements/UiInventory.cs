using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiInventory : MonoBehaviour
{
    public Image[] InventorySlots;
    public Inventory inventory;
    void Start()
    {
        inventory.addItem += delegate (Item item)
        {
            for (int i = 0; i < inventory.ListItems.Count; i++)
            {
                if (i>= InventorySlots.Length)
                {
                    break;
                }
                InventorySlots[i].gameObject.SetActive(true);
                InventorySlots[i].sprite = inventory.ListItems[i].Icon;
            }

            
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
