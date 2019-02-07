using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace tfj
{
    public abstract class ControlStrategy : ScriptableObject
    {
        [Tooltip("Rotation speed of the character (does not impact movement)")]
        public float m_rotationSpeed = 120;
        [Tooltip("Max speed for the character")]
        public float m_maxSpeed = 12;
        public float m_acceleration = 6;

        public float m_maxYawSpeed = 50;
        public float m_maxRollFromYawSpeed = 10;

        protected NavMeshAgent m_playerAgent;
        protected TFJCharacterController m_controller;
        protected Transform m_transform;

        public virtual void Init(TFJCharacterController _controller)
        {
            m_controller = _controller;
            m_playerAgent = m_controller.GetComponent<NavMeshAgent>();
            m_transform = _controller.transform;
        }
        public abstract void SetPlayerGoal(Vector3 _goal);

        public float MaxYawSpeed
        {
            get
            {
                return m_maxYawSpeed;
            }
        }

        public float MaxRollFromYawSpeed
        {
            get
            {
                return m_maxRollFromYawSpeed;
            }
        }
    }
}