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
        private float m_previousYawSpeed = 0;

        private float m_previousRollEffect = 0;
        private float m_previousRoll = 0;
        private float m_previousRockEffect = 0;
        private float m_rockVelocity;
        private float m_rollVelocity;
        private Vector3 m_previousPosition;
        private float m_previousMoveSpeed;
        // Start is called before the first frame update
        void Start()
        {
            m_rigidbody = m_controller.GetComponent<Rigidbody>();
            m_previousPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            float roll = 0;
            float rock = 0;

            Vector3 rotation = transform.rotation.eulerAngles;
            Vector3 position = transform.position;
            float deltaTime = Time.deltaTime;
            float newYaw = rotation.y;
            float yawSpeed = (newYaw - m_previousYaw) / deltaTime;
            float moveSpeed = Vector3.Distance(position, m_previousPosition) / deltaTime;
            float speedChange = -(moveSpeed - m_previousMoveSpeed) / deltaTime;
            float yawSpeedChange = (yawSpeed - m_previousYawSpeed) / deltaTime;

            m_previousYaw = newYaw;
            m_previousPosition = position;
            m_previousMoveSpeed = moveSpeed;
            m_previousYawSpeed = yawSpeed;
            m_previousRollEffect = Mathf.Lerp(m_previousRollEffect,
                                        Tools.LinearScale(-m_controller.ControlStrategy.MaxYawSpeed,
                                                           m_controller.ControlStrategy.MaxYawSpeed,
                                                          -m_controller.ControlStrategy.MaxRollFromYawSpeed,
                                                           m_controller.ControlStrategy.MaxRollFromYawSpeed,
                                                           yawSpeedChange)
                                       , 0.1f);
            if (speedChange > 0)
            {
                m_previousRockEffect = Mathf.Lerp(m_previousRockEffect,
                                                Tools.LinearScale(-m_controller.ControlStrategy.MaxSpeed,
                                                                   m_controller.ControlStrategy.MaxSpeed,
                                                                  -m_controller.ControlStrategy.MaxForwardRockFromSpeed,
                                                                   m_controller.ControlStrategy.MaxForwardRockFromSpeed,
                                                                   speedChange)
                                               , 0.1f);
            }
            else
            {
                m_previousRockEffect = Mathf.Lerp(m_previousRockEffect,
                                Tools.LinearScale(-m_controller.ControlStrategy.MaxSpeed,
                                                   m_controller.ControlStrategy.MaxSpeed,
                                                  -m_controller.ControlStrategy.MaxBackwardRockFromSpeed,
                                                   m_controller.ControlStrategy.MaxBackwardRockFromSpeed,
                                                   speedChange)
                               , 0.1f);
            }


            rock += Mathf.SmoothDampAngle(rotation.x, m_previousRockEffect, ref m_rockVelocity, 1);
            m_previousRoll = Mathf.SmoothDampAngle(m_previousRoll, m_previousRollEffect, ref m_rollVelocity, .4f);
            roll += m_previousRoll;
            //rock += m_previousRockEffect;
            //roll += m_previousRollEffect;
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

            transform.localRotation = Quaternion.Euler(rock, 0, roll);
        }
    }
}