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
            CameraManager.Instance.SetAnimState(m_cameraAnimTrigger, BirdEyeViewCallback);
        }

        public void BirdEyeViewCallback()
        {
            StartCoroutine(EndBirdEyeView());
        }

        IEnumerator Dock()
        {
            GameManager.Instance.SwitchToTradeMode(m_dialogNode);
            return null;
        }

        public IEnumerator EndBirdEyeView()
        {
            CameraManager.Instance.SetFollowState(0.03f);
            var shake = EZCameraShake.CameraShaker.Instance.StartShake(4.0f, 5.0f, 0.2f);

            yield return new WaitForSeconds(0.8f);
            shake.StartFadeOut(0.1f);

            yield return new WaitForSeconds(3);

            CameraManager.Instance.SetFollowState();
            Dock();
        }
    }
}