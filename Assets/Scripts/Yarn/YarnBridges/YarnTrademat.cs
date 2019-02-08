using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace tfj
{
    public class YarnTrademat : MonoBehaviour
    {
        public List<Transform> m_hotspots = new List<Transform>();

        [YarnCommand("Show")]
        public void Show(string _itemID, string _itemSlot)
        {
            int slot = int.Parse(_itemSlot);
            Item item = GameManager.Instance.Inventory.GetItemReferenceById(_itemID);

            RemoveFromSlot(slot);

            var item3D = GameObject.Instantiate(item.m_view3D, m_hotspots[slot]);
            item3D.transform.localPosition = Vector3.zero;
        }

        [YarnCommand("Hide")]
        public void Remove(string _itemSlot)
        {
            int slot = int.Parse(_itemSlot);
            RemoveFromSlot(slot);
        }

        private void RemoveFromSlot(int _slotId)
        {
            foreach (Transform child in m_hotspots[_slotId])
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}