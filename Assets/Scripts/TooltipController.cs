using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace tfj
{
    public class TooltipController : MonoBehaviour
    {
        public enum TooltipType
        {
            A,
            B,
            X,
            Y
        }

        public Text m_iconText;
        public Image m_iconBackground;
        public Text m_tooltipText;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Assert(m_iconText != null, "No Icon Text provided");
            Debug.Assert(m_iconBackground != null, "No Icon Background provided");
            Debug.Assert(m_tooltipText != null, "No Tooltip Text provided");
        }

        public void ShowTooltip(string _text, TooltipType _type)
        {
            m_tooltipText.text = _text;

// IF XBOX
            switch(_type)
            {
                case TooltipType.A:
                    m_iconText.text = "A";
                    m_iconBackground.color = new Color(0, 0.62f, 0.04f);
                    break;
                case TooltipType.B:
                    m_iconText.text = "B";
                    m_iconBackground.color = new Color(1, 0, 0);
                    break;
                case TooltipType.X:
                    m_iconText.text = "X";
                    m_iconBackground.color = new Color(0, 0, 1);
                    break;
                case TooltipType.Y:
                    m_iconText.text = "Y";
                    m_iconBackground.color = new Color(1, 0.82f, 0);
                    break;
            }
// END IF XBOX
        }
    }
}