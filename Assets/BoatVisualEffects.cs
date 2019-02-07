using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    public class BoatVisualEffects : MonoBehaviour
    {
        public TFJCharacterController m_controller;
        public float m_rollFrequency = 1;
        public float m_randRollAmplitude = 4;
        private Rigidbody m_rigidbody;
        private float m_previousYaw = 0;
        private float m_previousRollEffect = 0;
        // Start is called before the first frame update
        void Start()
        {
            m_rigidbody = m_controller.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            float roll = 0;

            Vector3 rotation = transform.rotation.eulerAngles;
            float newYaw = rotation.y;
            float yawSpeed = (newYaw - m_previousYaw) / Time.deltaTime;
            m_previousYaw = newYaw;

            m_previousRollEffect = Mathf.Lerp(m_previousRollEffect,
                                        Tools.LinearScale(-m_controller.ControlStrategy.MaxYawSpeed,
                                                           m_controller.ControlStrategy.MaxYawSpeed,
                                                          -m_controller.ControlStrategy.MaxRollFromYawSpeed,
                                                           m_controller.ControlStrategy.MaxRollFromYawSpeed,
                                                           yawSpeed)
                                       , 0.1f);

            roll += m_previousRollEffect;

            //Debug.Log(yawSpeed);

            float noise = m_randRollAmplitude * (2 * Mathf.PerlinNoise(m_rollFrequency * Time.timeSinceLevelLoad, m_rollFrequency * Time.timeSinceLevelLoad) - 0.5f);
            /*
            Vector3 horizon = transform.right;
            horizon.y = 0;
            horizon.Normalize();

            Vector3 to = (horizon + noise * Vector3.up).normalized;

            Debug.DrawLine(transform.position, transform.position + horizon * 10, Color.red);
            Debug.DrawLine(transform.position, transform.position + transform.right * 10, Color.blue);
            */

            roll += noise;

            //Debug.Log(noise);

            transform.localRotation = Quaternion.Euler(0, 0, roll);
        }
    }
}