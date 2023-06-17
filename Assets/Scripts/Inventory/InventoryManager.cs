using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Player player;
    //For now, this will store information of the Items that can be added to the inventory
    public List<ItemData> itemDatabase;

    //Store all the inventory slots in the scene here
    public List<InventorySlot> inventorySlots;

    //Store all the equipment slots in the scene here
    public List<EquipmentSlot> equipmentSlots;

    //Singleton implementation. Do not change anything within this region.
    #region SingletonImplementation
    private static InventoryManager instance = null;
    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "Inventory";
                    instance = go.AddComponent<InventoryManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public void UseItem(ItemData data)
    {
        // If the item is a consumable, simply add the attributes of the item to the player.
        switch(data.type) 
        {
            case ItemType.Consumable:
                player.AddAttributes(data.attributes);
                break;
            // If it is equippable, get the equipment slot that matches the item's slot.
            case ItemType.Equipabble:
                if (GetEquipmentSlotIndex(data.slotType) == -1) break;
                // Set the equipment slot's item as that of the used item
                equipmentSlots[GetEquipmentSlotIndex(data.slotType)].SetItem(data);
                break;
        }
    }

    public bool AddItem(string itemID)
    {
        for(int i = 0; i < itemDatabase.Count; i++)
        {
            if (itemDatabase[i].id == itemID)
            {
                if (!GetEmptyInventorySlot()) return false;
                GetEmptyInventorySlot().SetItem(itemDatabase[i]);

            }
        }
        return true;
    }

    public int GetEmptyInventorySlotIndex()
    {
        for(int i = 0; i < inventorySlots.Count; i++) 
        {
            if (!inventorySlots[i].HasItem())
                return i;
        }
        return -1;
    }

    public InventorySlot GetEmptyInventorySlot()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (!inventorySlots[i].HasItem())
                return inventorySlots[i];
        }
        return null;
    }

    public int GetEquipmentSlotIndex(EquipmentSlotType type)
    {
        for (int i = 0; i < equipmentSlots.Count; i++)
        {
            if (equipmentSlots[i].type == type)
                return i;
        }
        return -1;
    }
}
