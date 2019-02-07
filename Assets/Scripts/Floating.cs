using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    public class Floating : MonoBehaviour
    {
        public float m_phase = 2;
        public float m_amplitude = 1;

        public float m_waveSpeed = 0.3f;
        public float m_waveAmplitude = 3.0f;
        public float m_waveIntensity = 5f;

        private Vector3 m_offsetY;
        // Start is called before the first frame update
        void Start()
        {
            m_offsetY = Vector3.zero;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position -= m_offsetY;

            float timeFactor = Mathf.Sin(Time.timeSinceLevelLoad * m_waveSpeed + m_phase * transform.position.x);
            float Waves = (timeFactor * m_amplitude * m_waveIntensity * 10.0f);
            m_offsetY = new Vector3(0, Waves * 0.04f, 0);

            transform.position += m_offsetY;
        }
    }
}