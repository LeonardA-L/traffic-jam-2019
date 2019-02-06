using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    [CreateAssetMenu(menuName = "Control Strategies/Simple Boat", order = 1)]
    public class BoatCS : ControlStrategy
    {
        public override void Init(TFJCharacterController _controller)
        {
            base.Init(_controller);

            m_playerAgent.acceleration = m_acceleration;
            m_playerAgent.angularSpeed = m_rotationSpeed;
            m_playerAgent.speed = m_maxSpeed;
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

            m_playerAgent.SetDestination(goal);
        }
    }
}