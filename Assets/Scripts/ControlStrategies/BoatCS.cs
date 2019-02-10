using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    [CreateAssetMenu(menuName = "Control Strategies/Simple Boat", order = 1)]
    public class BoatCS : ControlStrategy
    {
        private Vector3 m_direction;
        public override void Init(TFJCharacterController _controller)
        {
            base.Init(_controller);

            m_playerAgent.acceleration = m_acceleration;
            m_playerAgent.angularSpeed = m_rotationSpeed;
            m_playerAgent.speed = m_maxSpeed;

            m_direction = _controller.transform.forward;
        }

        public override void SetPlayerGoal(Vector3 _goal)
        {
            Vector3 trajectory = _goal - m_transform.position;
            Vector3 goal = _goal;
            if (Vector3.Dot(m_transform.forward, trajectory) < 0)
            {
                if (Vector3.Dot(m_transform.right, trajectory) >= 0)
                {
                    goal = m_transform.position + trajectory.magnitude * m_transform.right;
                }
                else
                {
                    goal = m_transform.position - trajectory.magnitude * m_transform.right;
                }
            }

            float mag = (goal - m_transform.position).magnitude;
            Vector3 direction = (goal - m_transform.position).normalized;

            m_direction = Vector3.Slerp(m_direction, direction, 0.008f).normalized;

            m_playerAgent.SetDestination(m_transform.position + mag * m_direction);
            Debug.DrawLine(m_transform.position, m_transform.position + m_direction * mag, Color.red);
        }
    }
}