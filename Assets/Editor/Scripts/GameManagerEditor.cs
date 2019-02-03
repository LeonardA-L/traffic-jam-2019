using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

namespace tfj
{
    [CustomEditor(typeof(GameManager))]
    public class GameManagerEditor : Editor
    {
        public Object m_newItem;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GameManager gm = (GameManager)target;

            EditorGUILayout.BeginHorizontal();

            {
                if (EditorApplication.isPlaying)
                {
                    if (GUILayout.Button("Save"))
                    {
                        gm.Save();
                    }
                }
                if (GUILayout.Button("Erase Save File"))
                {
                    string savePath = Application.persistentDataPath + gm.m_savePath;
                    if (File.Exists(savePath))
                    {
                        File.Delete(savePath);
                    }
                }
            }

            EditorGUILayout.EndHorizontal();

            if (EditorApplication.isPlaying)
            {
                EditorGUILayout.LabelField("Inventory");
                EditorGUI.indentLevel++;

                EditorGUILayout.BeginHorizontal();
                {
                    m_newItem = EditorGUILayout.ObjectField(m_newItem, typeof(Item), true);
                    if (GUILayout.Button("Add Item"))
                    {
                        if (m_newItem == null)
                        {
                            Debug.LogError("No item provided");
                            return;
                        }
                        gm.Inventory.Add((Item)m_newItem);
                        m_newItem = null;
                    }
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Separator();

                for (int i = 0; i < gm.Inventory.Items.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField("" + gm.Inventory.Items[i].Id);
                        if (GUILayout.Button("Remove"))
                        {
                            gm.Inventory.Remove(gm.Inventory.Items[i]);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUI.indentLevel--;
            }
        }
    }
}