using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class collections : MonoBehaviour
{
    public itemsScript items;
    public uChanController uChanControl;
    public SpriteRenderer spriteItemID;
    public inventoryAddItem addingNewItem;

    public int itemIndex;

    // Use this for initialization
    void Start()
    {
        items = GetComponent<itemsScript>();
        uChanControl = GetComponent<uChanController>();
        addingNewItem = GetComponent<inventoryAddItem>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            itemIndex = items.itemList[col.gameObject.tag];
            items.itemPlayerAmount[itemIndex]++;
            addingNewItem.indexNewItem = itemIndex;
            addingNewItem.newItem();
            Destroy(col.gameObject);
        }

        if (col.gameObject.layer == LayerMask.NameToLayer("Lantern"))
        {
            uChanControl.LanternCatch = 1;
            Destroy(col.gameObject);
        }
    }
}
