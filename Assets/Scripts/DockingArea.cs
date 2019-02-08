using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    public class DockingArea : MonoBehaviour
    {
        public string m_dialogNode;
        // Start is called before the first frame update
        void Start()
        {

        }

        void Update()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            var controller = other.attachedRigidbody.gameObject.GetComponent<TFJCharacterController>();
            if (controller == null)
            {
                return;
            }

            UIManager.Instance.ShowTooltip("Dock", TooltipController.TooltipType.A); // TODO External translation file
        }

        private void OnTriggerExit(Collider other)
        {
            var controller = other.attachedRigidbody.gameObject.GetComponent<TFJCharacterController>();
            if (controller == null)
            {
                return;
            }

            UIManager.Instance.HideTooltip();
        }
    }
}