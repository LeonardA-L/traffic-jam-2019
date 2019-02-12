using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    [CreateAssetMenu(menuName = "Control Strategies/Simple Boat", order = 1)]
    public class BoatCS : ControlStrategy
    {
        public float m_friction = 0.01f;
        public float m_rotFriction = 0.01f;
        [SerializeField]
        private float m_forwardSpeed = 0;
        [SerializeField]
        private float m_rotSpeed = 0;
        public static readonly float s_epsilon = 0.1f;

        public override void Init(TFJCharacterController _controller)
        {
            base.Init(_controller);
            m_forwardSpeed = 0;
            m_rotSpeed = 0;
        }

        public override void Execute(Vector2 _joy)
        {

            // Translation

            m_forwardSpeed += _joy.y * m_acceleration;
            m_forwardSpeed = Mathf.Clamp(m_forwardSpeed, -m_maxBackwardSpeed, m_maxSpeed);
            if (Mathf.Abs(_joy.y) <= s_epsilon)
                m_forwardSpeed = Mathf.Lerp(m_forwardSpeed, 0, m_friction);

            m_characterController.Move(m_transform.forward * m_forwardSpeed * Time.deltaTime);

            // Rotation
            m_rotSpeed += _joy.x * m_rotationSpeed;
            m_rotSpeed = Mathf.Clamp(m_rotSpeed, -m_maxRotationSpeed, m_maxRotationSpeed);
            if (Mathf.Abs(_joy.x) <= s_epsilon)
                m_rotSpeed = Mathf.Lerp(m_rotSpeed, 0, m_rotFriction);

            m_transform.Rotate(0, m_rotSpeed * Time.deltaTime, 0);
        }
    }
}