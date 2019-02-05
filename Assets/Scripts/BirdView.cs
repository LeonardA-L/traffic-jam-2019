using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    public class BirdView : MonoBehaviour
    {
        public BirdViewTrigger m_trigger;
        public float m_offsetForce = 60;
        public float m_timeFactor = 25;
        public Vector3 m_offsetBase = - new Vector3(1, 0, 1);

        private Transform m_character;
        private bool m_triggered = false;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Assert(m_trigger != null, "No BirdViewTrigger provided");
            var character = (TFJCharacterController)FindObjectOfType(typeof(TFJCharacterController)); // Assume there is only one character
            Debug.Assert(character != null, "No TFJCharacterController found on scene");
            m_character = character.transform;
        }

        // Update is called once per frame
        void Update()
        {
            float sqrDist = (m_trigger.transform.position - m_character.position).sqrMagnitude;
            if (!m_triggered && sqrDist <= m_trigger.m_triggerRadius * m_trigger.m_triggerRadius)
            {
                StartCoroutine(WaitforEndOfBirdEye());
            }
        }

        IEnumerator WaitforEndOfBirdEye()
        {
            CameraManager.Instance.SetSceneState(transform, m_offsetBase, m_timeFactor, m_offsetForce);
            m_triggered = true;

            yield return new WaitForSeconds(m_timeFactor * 0.55f);

            CameraManager.Instance.SetFollowState(0.03f);
            var shake = EZCameraShake.CameraShaker.Instance.StartShake(4.0f, 5.0f, 0.2f);

            yield return new WaitForSeconds(0.8f);
            shake.StartFadeOut(0.1f);

            yield return new WaitForSeconds(3);

            CameraManager.Instance.SetFollowState();
        }
    }
}