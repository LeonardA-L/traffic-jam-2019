using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{

    public class TradingUIManager : Singleton<TradingUIManager>
    {
        public InventoryUI m_inventoryUI;
        public GameObject m_itemButton;

        // Start is called before the first frame update
        private void Awake()
        {
            m_inventoryUI = tfj.TradingUIManager.Instance.m_inventoryUI;
        }
        public void OpenInventory()
        {
            m_inventoryUI.gameObject.SetActive(true);
        }
        public void ShowItemButton(bool _needItem)
        {
            m_itemButton.SetActive(_needItem);
        }
    }

}
