using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Yarn.Unity;

namespace tfj
{
    /// An extremely simple implementation of DialogueUnityVariableStorage, which
    /// just stores everything in a Dictionary.
    public class VariableStorage : VariableStorageBehaviour
    {

        /// Where we actually keeping our variables
        Dictionary<string, Yarn.Value> variables = new Dictionary<string, Yarn.Value>();

        [Header("Optional debugging tools")]
        /// A UI.Text that can show the current list of all variables. Optional.
        public UnityEngine.UI.Text debugTextView;

        /// Reset to our default values when the game starts
        void Awake()
        {
            ResetToDefaults();
        }

        /// Erase all variables and reset to default values
        public override void ResetToDefaults()
        {
            Clear();
        }

        /// Set a variable's value
        public override void SetValue(string variableName, Yarn.Value value)
        {
            // Copy this value into our list
            variables[variableName] = new Yarn.Value(value);
        }

        /// Get a variable's value
        public override Yarn.Value GetValue(string variableName)
        {
            // If we don't have a variable with this name, return the null value
            if (variables.ContainsKey(variableName) == false)
                return Yarn.Value.NULL;

            return variables[variableName];
        }

        /// Erase all variables
        public override void Clear()
        {
            variables.Clear();
        }

        /// If we have a debug view, show the list of all variables in it
        void Update()
        {
            if (debugTextView != null)
            {
                var stringBuilder = new System.Text.StringBuilder();
                foreach (KeyValuePair<string, Yarn.Value> item in variables)
                {
                    stringBuilder.AppendLine(string.Format("{0} = {1}",
                                                             item.Key,
                                                             item.Value));
                }
                debugTextView.text = stringBuilder.ToString();
            }
        }

    }
}