using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    [RequireComponent(typeof(Camera))]
    public class CameraRegisterer : MonoBehaviour
    {
        public string m_name;
        public bool m_activeByDefault = false;
        void Start()
        {
            CameraManager.Instance.RegisterCamera(m_name, GetComponent<Camera>());
            gameObject.SetActive(m_activeByDefault);
        }
    }
}