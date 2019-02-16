using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using tfj;

public class ItemSpot : MonoBehaviour
{
    private bool m_triggered = false;
    private bool m_inRange = false;
    private bool m_fished = false;
    private bool m_found = false;
    public float initialChance = 0.2f;
    private float m_improvedChance;
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
        m_improvedChance = initialChance;
        UIManager.Instance.ShowTooltip("Search", TooltipController.TooltipType.A); // TODO External translation file
        m_inRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        var controller = other.gameObject.GetComponent<TFJCharacterController>();
        if (controller == null)
        {
            return;
        }
        UIManager.Instance.HideTooltip();


    }
    // Update is called once per frame
    void Update()
    {
        if (m_inRange && !m_triggered && Input.GetButtonDown("Fire1"))   // TODO better input management
        {
            m_triggered = true;
            UIManager.Instance.HideTooltip();
            StartCoroutine(FoundCor());

        }
        else if (m_fished && Input.GetButtonDown("Fire1"))
        {

            UIManager.Instance.HideTooltip();
            if (m_found)
            {
                Destroy(gameObject);
            }
            else
            {
                m_triggered = false;
                StartCoroutine(SearchAgain());

            }


        }
    }

    private IEnumerator FoundCor()
    {
        yield return new WaitForSeconds(0.8f);
        m_fished = true;
        if (Random.Range(0f, 1f) < m_improvedChance)
        {
            UIManager.Instance.ShowTooltip("Cool! un item           " + m_item.Id, TooltipController.TooltipType.A); // TODO External translation file
            GameManager.Instance.Inventory.Add(m_item);
            m_found = true;
            yield break;
        }
        else
        {
            UIManager.Instance.ShowTooltip("Zut! rien          " + m_item.Id, TooltipController.TooltipType.A); // TODO External translation file
            m_improvedChance *= 2;
            yield break;

        }
    }
    private IEnumerator SearchAgain()
    {
        yield return new WaitForSeconds(0.2f);

        if (m_inRange)
        {
            UIManager.Instance.ShowTooltip("Search Again           ", TooltipController.TooltipType.A);
        }
    }
}
