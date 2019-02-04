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
        [SerializeField]
        private bool m_playerMoving = false;
        private NavMeshAgent m_playerAgent;

        private Vector3 m_command;

        // Start is called before the first frame update
        void Start()
        {
            m_playerAgent = GetComponent<NavMeshAgent>();
            Debug.Assert(m_camera != null, "No Camera provided.", this);
        }

        // Update is called once per frame
        void Update()
        {
            // Detect click on ground
            if (Input.GetMouseButtonDown(0))
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
                        SetPlayerAction(hit.point);
                    }
                }
            }

            // Fetch gamepad input
            Vector2 joy = new Vector2(Input.GetAxis(m_horizontalAxis), Input.GetAxis(m_verticalAxis));
            float magnitude = Mathf.Clamp(joy.magnitude, 0, 1);
            joy.Normalize();
            joy *= magnitude;

            Vector3 camForward = m_camera.forward;
            camForward.y = 0;
            //Debug.Assert(camForward.sqrMagnitude != 0);   // With the hack'n'slash view we assume we don't need this
            camForward.Normalize();

            m_command = transform.position
                      + (joy.y * camForward + joy.x * m_camera.right)
                      * m_commandOffset;

            SetPlayerAction(m_command);
        }

        public void SetPlayerAction(Vector3 goal)
        {
            if(DialogueManager.Instance.IsActive)
            {
                return;
            }
            m_playerMoving = true;
            m_playerAgent.SetDestination(goal);
        }

        void OnDrawGizmos()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(m_command, 1);
        }
    }
}