using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace tfj
{
    public class TFJCharacterController : MonoBehaviour
    {
        public string m_groundLayer = "Water";
        public string m_verticalAxis = "Vertical";
        public string m_horizontalAxis = "Horizontal";
        public Transform m_camera;
        public float m_commandOffset = 7;

        public ControlStrategy m_controlStrategy;

        [SerializeField]
        private bool m_playerMoving = false;
        private NavMeshAgent m_playerAgent;

        private Vector3 m_command;

        // Start is called before the first frame update
        void Start()
        {
            m_playerAgent = GetComponent<NavMeshAgent>();
            Debug.Assert(m_camera != null, "No Camera provided.", this);
            Debug.Assert(m_controlStrategy != null, "No Control Strategy provided.", this);
            m_controlStrategy.Init(this);
        }

        private void OnValidate()
        {
            Debug.Assert(m_controlStrategy != null, "No Control Strategy provided.", this);
            m_controlStrategy.Init(this);
        }

        // Update is called once per frame
        void Update()
        {
            if (!PlayerManager.Instance.AllowMovement || GameManager.Instance.CurrentGameMode == GameManager.GameMode.TRADE)
            {
                return;
            }
            // Fetch gamepad input
            Vector2 joy = new Vector2(Input.GetAxis(m_horizontalAxis), Input.GetAxis(m_verticalAxis));
            float magnitude = Mathf.Clamp(joy.magnitude, 0, 1);
            joy.Normalize();
            joy *= magnitude;

            m_controlStrategy.Execute(joy);
        }

        void OnDrawGizmos()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(m_command, 1);
        }

        public ControlStrategy ControlStrategy
        {
            get
            {
                return m_controlStrategy;
            }
        }
    }
}