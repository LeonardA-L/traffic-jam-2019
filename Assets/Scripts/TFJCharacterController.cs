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
        [SerializeField]
        private bool m_playerMoving = false;
        private NavMeshAgent m_playerAgent;

        // Start is called before the first frame update
        void Start()
        {
            m_playerAgent = GetComponent<NavMeshAgent>();
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
        }

        public void SetPlayerAction(Vector3 goal)
        {
            /*TODO block when Dialogue is active*/
            m_playerMoving = true;
            m_playerAgent.SetDestination(goal);
        }
    }
}