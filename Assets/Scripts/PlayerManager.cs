using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        [SerializeField]
        private bool m_allowMovement = true;
        private bool m_lastAllowMovement = true;

        public void SetAllowMovement(bool _v)
        {
            m_lastAllowMovement = m_allowMovement;
            m_allowMovement = _v;
        }

        public void ResetAllowMovement()
        {
            m_allowMovement = m_lastAllowMovement;
        }

        public bool AllowMovement
        {
            get
            {
                return m_allowMovement;
            }
        }
    }
}