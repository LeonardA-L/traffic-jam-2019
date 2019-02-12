using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tfj;

public class InventoryButton : MonoBehaviour
{
    // Start is called before the first frame update
    InventoryUI m_inventoryUI;
    Item m_item;
    private void Awake()
    {
        m_inventoryUI = GetComponentInParent<InventoryUI>();
    }
    public void Select()
    {
        m_inventoryUI.SelectItem(this.gameObject);
    }
}
