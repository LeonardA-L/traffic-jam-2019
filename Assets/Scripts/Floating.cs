using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public float m_phase = 0;
    public float m_amplitude = 1;
    public float m_waveSpeed = 4.4f;

    private float m_origY;
    // Start is called before the first frame update
    void Start()
    {
        m_origY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, m_origY + m_amplitude * Mathf.Sin((m_phase * transform.position.x + Time.timeSinceLevelLoad) * m_waveSpeed), transform.position.z);
    }
}
