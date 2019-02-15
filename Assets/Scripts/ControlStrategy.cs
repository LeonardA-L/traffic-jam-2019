using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace tfj
{
    public abstract class ControlStrategy : ScriptableObject
    {
        [Tooltip("Rotation speed of the character (does not impact movement)")]
        public float m_rotationSpeed = 10;
        public float m_maxRotationSpeed = 120;
        [Tooltip("Max speed for the character")]
        public float m_maxSpeed = 12;
        public float m_maxRockFromSpeed = 10;

        public float m_maxBackwardSpeed = 4;
        public float m_acceleration = 6;

        public float m_maxYawSpeed = 50;
        public float m_maxRollFromYawSpeed = 10;

        protected TFJCharacterController m_controller;
        protected Transform m_transform;
        protected CharacterController m_characterController;

        public virtual void Init(TFJCharacterController _controller)
        {
            m_controller = _controller;
            m_transform = _controller.transform;
            m_characterController = _controller.GetComponent<CharacterController>();
        }
        public abstract void Execute(Vector2 _joy);

        public float MaxYawSpeed
        {
            get
            {
                return m_maxYawSpeed;
            }
        }
        public float MaxSpeed
        {
            get
            {
                return m_maxSpeed;
            }
        }
        public float MaxRollFromYawSpeed
        {
            get
            {
                return m_maxRollFromYawSpeed;
            }
        }
        public float MaxRockFromSpeed
        {
            get
            {
                return m_maxRockFromSpeed;
            }
        }
    }
}