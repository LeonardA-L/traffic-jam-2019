using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    [System.Serializable]
    public class Inventory
    {
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
        }

        public void Add(Item _item)
        {
            m_items.Add(_item);
        }

        public void Add(string _itemID)
        {
            Add(m_references[_itemID]);
        }

        public void Remove(Item _item)
        {
            m_items.Remove(_item);
        }

        public void Remove(string _itemID)
        {
            Remove(m_references[_itemID]);
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