using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tfj;

public class ItemSpot : MonoBehaviour
{
    private bool m_triggered = false;
    private bool m_inRange = false;
    public Item m_item;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        var controller = other.gameObject.GetComponent<TFJCharacterController>();
        if (controller == null)
        {
            return;
        }

        if (m_triggered)
        {
            return;
        }

        UIManager.Instance.ShowTooltip("Pick", TooltipController.TooltipType.A); // TODO External translation file
        m_inRange = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (m_inRange && !m_triggered && Input.GetButtonDown("Fire1"))   // TODO better input management
        {
            m_triggered = true;
            UIManager.Instance.HideTooltip();
            GameManager.Instance.Inventory.Add(m_item);
            Destroy(gameObject);
        }
    }
}
