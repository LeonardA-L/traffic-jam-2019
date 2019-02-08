using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    public class DockingArea : MonoBehaviour
    {
        public string m_dialogNode;
        private bool m_triggered = false;
        private bool m_inRange = false;
        // Start is called before the first frame update
        void Start()
        {

        }

        void Update()
        {
            if(m_inRange && !m_triggered && Input.GetButtonDown("Fire1"))   // TODO better input management
            {
                m_triggered = true;
                UIManager.Instance.HideTooltip();
                GameManager.Instance.SwitchToTradeMode(m_dialogNode);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var controller = other.attachedRigidbody.gameObject.GetComponent<TFJCharacterController>();
            if (controller == null)
            {
                return;
            }

            if(m_triggered)
            {
                return;
            }

            UIManager.Instance.ShowTooltip("Dock", TooltipController.TooltipType.A); // TODO External translation file
            m_inRange = true;
        }

        private void OnTriggerExit(Collider other)
        {
            var controller = other.attachedRigidbody.gameObject.GetComponent<TFJCharacterController>();
            if (controller == null)
            {
                return;
            }

            UIManager.Instance.HideTooltip();
            m_triggered = false;
            m_inRange = false;
        }
    }
}