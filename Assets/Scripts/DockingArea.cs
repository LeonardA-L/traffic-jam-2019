using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    public class DockingArea : MonoBehaviour
    {
        private bool m_triggered = false;
        private bool m_inRange = false;
        private DockingStrategy m_dockingStrategy;
        // Start is called before the first frame update
        void Start()
        {
            m_dockingStrategy = GetComponent<DockingStrategy>();
        }

        void Update()
        {
            if(m_inRange && !m_triggered && Input.GetButtonDown("Fire1"))   // TODO better input management
            {
                m_triggered = true;
                UIManager.Instance.HideTooltip();
                PlayerManager.Instance.SetAllowMovement(false);
                m_dockingStrategy.Execute();
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