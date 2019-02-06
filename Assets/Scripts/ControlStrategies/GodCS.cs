using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    [CreateAssetMenu(menuName = "Control Strategies/God", order = 2)]
    public class GodCS : ControlStrategy
    {
        public override void Init(TFJCharacterController _controller)
        {
            base.Init(_controller);

            m_playerAgent.acceleration = m_acceleration;
            m_playerAgent.angularSpeed = m_rotationSpeed;
            m_playerAgent.speed = m_maxSpeed;
            Debug.Log(m_playerAgent, m_playerAgent);
        }

        public override void SetPlayerGoal(Vector3 _goal)
        {
            m_playerAgent.SetDestination(_goal);
        }
    }
}