using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelFader : MonoBehaviour
{
    Animator m_anim;
    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Character")
        {
            m_anim.SetTrigger("Fade");
        }
    }
}
