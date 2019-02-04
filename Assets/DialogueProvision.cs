using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    public class DialogueProvision : MonoBehaviour
    {
        private DialogueUI m_dialogUI;
        void Start()
        {
            m_dialogUI = GetComponent<DialogueUI>();
            Debug.Assert(m_dialogUI != null, "No DialogUI");

            Yarn.Unity.DialogueRunner runner = GetComponent<Yarn.Unity.DialogueRunner>();
            Debug.Assert(runner != null, "No DialogueRunner detected");

            runner.dialogue.library.RegisterFunction("has_item", 1, delegate (Yarn.Value[] parameters)
            {
                return GameManager.Instance.Inventory.HasItem(parameters[0].AsString);
            });

            runner.dialogue.library.RegisterFunction("random", 1, delegate (Yarn.Value[] parameters)
            {
                return (int)Random.Range(0, parameters[0].AsNumber);
            });

            runner.dialogue.library.RegisterFunction("offered_item", 0, delegate (Yarn.Value[] parameters)
            {
                Debug.Log("Offer" + m_dialogUI.itemOffer.Id);
                return m_dialogUI.itemOffer.Id;
            });
        }
    }
}