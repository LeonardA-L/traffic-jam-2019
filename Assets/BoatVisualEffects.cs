using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    public class BoatVisualEffects : MonoBehaviour
    {
        public TFJCharacterController m_controller;
        public float m_rollFrequency = 1;
        public float m_rollAmplitude = 4;
        private Rigidbody m_rigidbody;
        // Start is called before the first frame update
        void Start()
        {
            m_rigidbody = m_controller.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            float noise = m_rollAmplitude * (2 * Mathf.PerlinNoise(m_rollFrequency * Time.timeSinceLevelLoad, m_rollFrequency * Time.timeSinceLevelLoad) - 0.5f);

            Vector3 horizon = transform.right;
            horizon.y = 0;
            horizon.Normalize();

            Vector3 to = (horizon + noise * Vector3.up).normalized;

            Debug.DrawLine(transform.position, transform.position + horizon * 10, Color.red);
            Debug.DrawLine(transform.position, transform.position + transform.right * 10, Color.blue);

            float roll = Vector3.SignedAngle(horizon, to, transform.forward);

            transform.localRotation = Quaternion.Euler(0, 0, roll);
        }
    }
}