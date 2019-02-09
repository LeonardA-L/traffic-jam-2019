using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    public class UIManager : Singleton<UIManager>
    {
        public TooltipController m_tooltipController;

        public void ShowTooltip(string _text, TooltipController.TooltipType _type)
        {
            m_tooltipController.gameObject.SetActive(true);
            m_tooltipController.ShowTooltip(_text, _type);
        }
        public void HideTooltip()
        {
            m_tooltipController.gameObject.SetActive(false);
        }
    }
}