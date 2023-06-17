using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private Image defaultIcon;
    [SerializeField] private Image itemIcon;
    public EquipmentSlotType type;

    private ItemData itemData;

    public void SetItem(ItemData data)
    {
        // Set the item data the and icons here
        itemData = data;
        InventoryManager.Instance.player.AddAttributes(itemData.attributes);
        defaultIcon.gameObject.SetActive(false);
        itemIcon.gameObject.SetActive(true);
        itemIcon.sprite = data.icon;
    }

    public void Unequip()
    {
        InventorySlot emptySlot = InventoryManager.Instance.GetEmptyInventorySlot();
        if (emptySlot != null)
        {
            emptySlot.SetItem(itemData);
            InventoryManager.Instance.player.RemoveAttributes(itemData.attributes);
            defaultIcon.gameObject.SetActive(true);
            itemIcon.gameObject.SetActive(false);
            itemIcon.sprite = null;
            itemData = null;
        }
    }

    public bool HasItem()
    {
        return itemData != null;
    }
}
