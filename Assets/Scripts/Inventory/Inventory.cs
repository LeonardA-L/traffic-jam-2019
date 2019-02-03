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

        public void Init(InventorySerialized _serialized)
        {
            m_items.Clear();

            var items = Resources.LoadAll("Items", typeof(Item));
            var itemsById = new Dictionary<string, Item>();
            foreach (var i in items)
            {
                Item item = (Item)i;
                itemsById.Add(item.Id, item);
            }

            foreach (var id in _serialized.IDs)
            {
                m_items.Add(itemsById[id]);
            }
        }

        public void Add(Item _item)
        {
            m_items.Add(_item);
        }

        public void Remove(Item _item)
        {
            m_items.Remove(_item);
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