using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace tfj
{
    public class YarnInventory : MonoBehaviour
    {
        [YarnCommand("Add")]
        public void Add(string _itemID)
        {
            GameManager.Instance.Inventory.Add(_itemID);
        }

        [YarnCommand("Remove")]
        public void Remove(string _itemID)
        {
            GameManager.Instance.Inventory.Remove(_itemID);
        }
    }
}