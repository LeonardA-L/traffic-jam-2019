using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Yarn.Unity;

namespace tfj
{
    public class YarnEditor : EditorWindow
    {
        private string m_nodeToStart = "";

        [MenuItem("Window/Yarn Editor")]
        static void Init()
        {
            EditorWindow.GetWindow(typeof(YarnEditor));
        }

        void OnEnable()
        {
        }

        void OnGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Yarn Editor", EditorStyles.boldLabel);
            GUILayout.EndHorizontal();

            GUILayout.Label("Start a dialog at specific node:");

            GUILayout.BeginHorizontal();
            m_nodeToStart = EditorGUILayout.TextField(m_nodeToStart);
            if (GUILayout.Button("Start"))
            {
                if (m_nodeToStart != "")
                {
                    FindObjectOfType<DialogueRunner>().StartDialogue(m_nodeToStart);
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Switch to Trade View"))
            {
                GameManager.Instance.SwitchToTradeMode();
            }
            if (GUILayout.Button("Switch to Exploration View"))
            {
                GameManager.Instance.SwitchToExplorationMode();
            }
            GUILayout.EndHorizontal();
        }
    }
}