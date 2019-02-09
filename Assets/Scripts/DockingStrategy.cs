using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    public class DockingStrategy : MonoBehaviour
    {
        public string m_dialogNode;
        public string m_cameraAnimTrigger;
        private DockingArea m_dockingArea;
        // Start is called before the first frame update
        void Start()
        {
        }

        public void Execute()
        {
            CameraManager.Instance.SetAnimState(m_cameraAnimTrigger, this);
        }

        public void StartDocking()
        {
            StartCoroutine(Dock());
        }

        IEnumerator Dock()
        {
            GameManager.Instance.SwitchToTradeMode(m_dialogNode);
            return null;
        }
    }
}