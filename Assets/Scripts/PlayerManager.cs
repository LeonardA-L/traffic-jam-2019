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

        public TFJCharacterController m_player;

        public void SetAllowMovement(bool _v)
        {
            m_lastAllowMovement = m_allowMovement;
            m_allowMovement = _v;
        }

        public void ResetAllowMovement()
        {
            m_allowMovement = m_lastAllowMovement;
        }

        public TFJCharacterController PlayerCharacter
        {
            get
            {
                return m_player;
            }
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