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
            // Fetch gamepad input
            Vector2 joy = new Vector2(Input.GetAxisRaw(m_horizontalAxis), Input.GetAxisRaw(m_verticalAxis));
            float magnitude = Mathf.Clamp(joy.magnitude, 0, 1);
            joy.Normalize();
            joy *= magnitude;

            Vector3 camForward = m_camera.forward;
            camForward.y = 0;
            camForward.Normalize();

            Vector3 command = transform.position
                      + (joy.y * camForward + joy.x * m_camera.right)
                      * m_commandOffset;

            // Detect click on ground
            if (Input.GetMouseButton(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, LayerMask.GetMask(m_groundLayer)))
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer(m_groundLayer))
                    {
                        command = hit.point;
                    }
                }
            }

            SetPlayerAction(command);
        }

        public void SetPlayerAction(Vector3 goal)
        {
            if(DialogueManager.Instance.IsActive)
            {
                return;
            }

            m_playerMoving = true;
            m_command = goal;

            m_controlStrategy.SetPlayerGoal(m_command);
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