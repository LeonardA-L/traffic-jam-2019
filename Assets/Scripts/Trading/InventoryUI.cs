using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using tfj;

public class InventoryUI : MonoBehaviour
{
    private struct ItemUIRef
    {
        public Item m_item;
        public GameObject m_uiElement;

        public ItemUIRef(Item _item, GameObject _go)
        {
            m_item = _item;
            m_uiElement = _go;

        }
    }

    public GameObject InventoryButton;
    public Image ItemButtonImage;
    Inventory m_inventory;
    List<ItemUIRef> m_items = new List<ItemUIRef>();

    private void Update()
    {
        if (m_inventory == null)
            m_inventory = GameManager.Instance.Inventory;
    }
    public void AddItem(Item _item)
    {
        GameObject button = Instantiate(InventoryButton, transform);
        m_items.Add(new ItemUIRef(_item, button));
        button.GetComponent<Image>().sprite = Resources.Load<Sprite>(_item.ViewSprite);

    }
    public void RemoveItem(Item _item)
    {
        foreach (ItemUIRef itemUI in m_items)
        {
            if (itemUI.m_item.Equals(_item))
            {
                Destroy(itemUI.m_uiElement.gameObject);
                m_items.Remove(itemUI);
                break;
            }
        }
    }
    public void CloseInventory()
    {
        gameObject.SetActive(false);
    }
    public void SelectItem(GameObject _uiElement)
    {
        ItemUIRef itemUIRef = m_items.Find(i => i.m_uiElement == _uiElement);
        DialogueManager.Instance.itemOffer = itemUIRef.m_item;
        ItemButtonImage.sprite = Resources.Load<Sprite>(itemUIRef.m_item.ViewSprite);
        CloseInventory();
    }
}
