using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    public class DialogueProvision : MonoBehaviour
    {
        void Start()
        {
            Yarn.Unity.DialogueRunner runner = GetComponent<Yarn.Unity.DialogueRunner>();
            Debug.Assert(runner != null, "No DialogueRunner detected");

            runner.dialogue.library.RegisterFunction("has_item", 1, delegate (Yarn.Value[] parameters)
            {
                return GameManager.Instance.Inventory.HasItem(parameters[0].AsString);
            });
        }
    }
}