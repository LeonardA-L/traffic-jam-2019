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
        public delegate void CameraAnimationEndCallback();

        public enum CameraState
        {
            FOLLOW,
            ANIMATED
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

        private Quaternion m_defaultRotation;

        private Dictionary<string, Camera> m_cameras = new Dictionary<string, Camera>();

        public Animator m_animator;
        private CameraAnimationEndCallback m_animationCallback;

        protected CameraManager() { }

        void Start()
        {
            var character = (TFJCharacterController)FindObjectOfType(typeof(TFJCharacterController)); // Assume there is only one character
            Debug.Assert(character != null, "No TFJCharacterController found on scene");    // TODO get it via a PlayerManager
            m_character = character.transform;

            m_currentDistance = m_distance;
            m_actualLerp = m_positionLerp;
            m_defaultRotation = transform.rotation;

            Debug.Assert(m_camHolder != null, "No CameraHolder provided");

            m_animator.enabled = false;
        }

        void Update()
        {
            switch (m_state)
            {
                case CameraState.FOLLOW:
                    // Move the camera to aim at the character. The camera does not rotate.
                    transform.position = Vector3.Lerp(transform.position, m_character.position, m_actualLerp);
                    m_camHolder.transform.position = Vector3.Lerp(m_camHolder.transform.position, transform.position - m_camHolder.transform.forward * m_currentDistance, m_actualLerp);
                    transform.rotation = Quaternion.Slerp(transform.rotation, m_defaultRotation, 0.005f);
                break;
                
            }
        }

        public void  SetFollowState(float _posLerp = 0, float _distance = 0)
        {
            m_currentDistance = _distance == 0 ? m_distance : _distance;
            m_actualLerp = _posLerp == 0 ? m_positionLerp : _posLerp;
            m_state = CameraState.FOLLOW;
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

        public void SetAnimState(string _animTrigger, CameraAnimationEndCallback _callback)
        {
            m_state = CameraState.ANIMATED;
            m_animator.enabled = true;
            m_animator.SetTrigger(_animTrigger);
            m_animationCallback = _callback;
        }

        public void EndAnimState()
        {
            if (m_animationCallback != null)
                m_animationCallback();
            m_animationCallback = null;
        }

    }
}