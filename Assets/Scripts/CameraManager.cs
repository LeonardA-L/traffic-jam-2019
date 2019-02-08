using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    /*
     * Handles camera movements.
     * This manager makes the camera follow the player, in a hack'n'slash type view (3D ~iso)
     */
    public class CameraManager : Singleton<CameraManager>
    {
        public enum CameraState
        {
            FOLLOW,
            SCENE
        }

        [SerializeField]
        private CameraState m_state = CameraState.FOLLOW;

        [Tooltip("Distance between the camera and the character")]
        public float m_distance = 12;
        [SerializeField]
        private float m_currentDistance;

        public float m_positionLerp = 0.1f;
        [SerializeField]
        private float m_actualLerp = 0.1f;

        public Transform m_camHolder;
        private Transform m_character;

        private Quaternion m_defaultHolderRotation;

        private Transform m_sceneView;
        private Vector3 m_lastCurveOffset;
        private Vector3 m_curveOffsetbase;
        private float m_sceneInitialDistance;
        private float m_sceneViewOffsetForce;
        private float m_transitionDuration;
        private float m_transitionTime;

        private Dictionary<string, Camera> m_cameras = new Dictionary<string, Camera>();

        protected CameraManager() { }

        void Start()
        {
            var character = (TFJCharacterController)FindObjectOfType(typeof(TFJCharacterController)); // Assume there is only one character
            Debug.Assert(character != null, "No TFJCharacterController found on scene");    // TODO get it via a PlayerManager
            m_character = character.transform;

            m_currentDistance = m_distance;
            m_actualLerp = m_positionLerp;
            m_defaultHolderRotation = m_camHolder.rotation;

            Debug.Assert(m_camHolder != null, "No CameraHolder provided");
        }

        void Update()
        {
            switch (m_state)
            {
                case CameraState.FOLLOW:
                    // Move the camera to aim at the character. The camera does not rotate.
                    m_camHolder.transform.position = Vector3.Lerp(m_camHolder.transform.position, m_character.position - m_camHolder.forward * m_distance, m_actualLerp);
                    m_camHolder.transform.rotation = Quaternion.Slerp(m_camHolder.transform.rotation, m_defaultHolderRotation, 0.005f);
                break;
                case CameraState.SCENE:

                    m_transitionTime += Time.deltaTime;
                    if (m_transitionTime > m_transitionDuration)
                    {
                        m_transitionTime = m_transitionDuration;
                    }

                    //lerp!
                    float t = m_transitionTime / m_transitionDuration;
                    //t = t * t * (3f - 2f * t);
                    t = t * t * t * (t * (6f * t - 15f) + 10f);

                    m_lastCurveOffset = m_curveOffsetbase;
                    //m_lastCurveOffset.y = 0;
                    m_lastCurveOffset.Normalize();
                    m_lastCurveOffset *= m_sceneViewOffsetForce * Mathf.Sin(m_transitionTime / m_transitionDuration * Mathf.PI);
                    //m_lastCurveOffset = Vector3.zero;
                    //Debug.Log(curveOffset);


                    m_camHolder.transform.position = Vector3.Lerp(m_camHolder.transform.position, m_sceneView.position, t) + m_lastCurveOffset;
                    m_camHolder.transform.rotation = Quaternion.Slerp(m_camHolder.transform.rotation, m_sceneView.rotation, t);
                break;
            }
        }

        public void  SetFollowState(float _posLerp = 0, float _distance = 0)
        {
            m_currentDistance = _distance == 0 ? m_distance : _distance;
            m_actualLerp = _posLerp == 0 ? m_positionLerp : _posLerp;
            m_transitionTime = 0;
            m_state = CameraState.FOLLOW;
        }

        public void SetSceneState(Transform _sceneView, Vector3 _curveOffsetBase, float _transitionDuration, float _offsetForce = 60)
        {
            m_sceneView = _sceneView;
            m_sceneInitialDistance = (_sceneView.position - transform.position).magnitude;
            m_sceneViewOffsetForce = _offsetForce;
            m_curveOffsetbase = _curveOffsetBase;
            m_transitionDuration = _transitionDuration;
            m_transitionTime = 0;
            m_state = CameraState.SCENE;
        }

        public void RegisterCamera(string _name, Camera _cam)
        {
            m_cameras.Add(_name, _cam);
        }

        public void SwitchToTradeMode()
        {
            Debug.Assert(m_cameras.ContainsKey("exploration"), "No Exploration camera registered");
            Debug.Assert(m_cameras.ContainsKey("trade"), "No Trade camera registered");
            m_cameras["exploration"].gameObject.SetActive(false);
            m_cameras["trade"].gameObject.SetActive(true);
        }

        public void SwitchToFollowMode()
        {
            Debug.Assert(m_cameras.ContainsKey("exploration"), "No Exploration camera registered");
            Debug.Assert(m_cameras.ContainsKey("trade"), "No Trade camera registered");
            m_cameras["exploration"].gameObject.SetActive(true);
            m_cameras["trade"].gameObject.SetActive(false);
        }
    }
}