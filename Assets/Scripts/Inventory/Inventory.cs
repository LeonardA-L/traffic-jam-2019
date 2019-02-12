using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    [System.Serializable]
    public class Inventory
    {
        private InventoryUI m_inventoryUI;

        [SerializeField]
        private List<Item> m_items = new List<Item>();

        [System.NonSerialized]
        private Dictionary<string, Item> m_references;
        public void Init(InventorySerialized _serialized)
        {
            m_items.Clear();

            var items = Resources.LoadAll("Items", typeof(Item));
            m_references = new Dictionary<string, Item>();
            foreach (var i in items)
            {
                Item item = (Item)i;
                m_references.Add(item.Id, item);
            }

            foreach (var id in _serialized.IDs)
            {
                m_items.Add(m_references[id]);
            }

            m_inventoryUI = TradingUIManager.Instance.m_inventoryUI;
            Debug.Assert(m_inventoryUI != null, "No InventoryUI component on scene.");
        }

        public void Add(Item _item)
        {
            m_items.Add(_item);
            m_inventoryUI.AddItem(_item);
        }

        public void Add(string _itemID)
        {
            if (!m_references.ContainsKey(_itemID))
            {
                Debug.LogError("No item with ID " + _itemID);
                return;
            }
            Add(m_references[_itemID]);
        }

        public void Remove(Item _item)
        {
            m_items.Remove(_item);
            m_inventoryUI.RemoveItem(_item);

        }

        public void Remove(string _itemID)
        {
            if (!m_references.ContainsKey(_itemID))
            {
                Debug.LogError("No item with ID " + _itemID);
                return;
            }
            Remove(m_references[_itemID]);
        }

        public bool HasItem(Item _item)
        {
            return m_items.Contains(_item);
        }

        public bool HasItem(string _itemID)
        {
            if (!m_references.ContainsKey(_itemID))
            {
                Debug.LogError("No item with ID " + _itemID);
                return false;
            }
            return HasItem(m_references[_itemID]);
        }

        public Item GetItemReferenceById(string _itemID)
        {
            return m_references[_itemID];
        }

        public List<Item> Items
        {
            get
            {
                return m_items;
            }
        }
    }

    [System.Serializable]
    public class InventorySerialized
    {
        [SerializeField]
        private List<string> m_itemsIds = new List<string>();

        public void Add(string _id)
        {
            m_itemsIds.Add(_id);
        }

        public List<string> IDs
        {
            get
            {
                return m_itemsIds;
            }
        }
    }
}