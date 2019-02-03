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
            if (EditorApplication.isPlaying)
            {
                if (GUILayout.Button("Save"))
                {
                    gm.Save();
                }
                if (GUILayout.Button("Reload"))
                {
                    gm.Load();
                }
            }
            if (GUILayout.Button("Erase Save File"))
            {
                string savePath = Application.persistentDataPath + gm.m_savePath;
                if(File.Exists(savePath))
                {
                    File.Delete(savePath);
                }
            }

            if (EditorApplication.isPlaying)
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
                }

                EditorGUILayout.LabelField("Inventory");
                EditorGUI.indentLevel++;
            
                for (int i = 0; i < gm.Inventory.Items.Count; i++)
                {
                    EditorGUILayout.LabelField("" + gm.Inventory.Items[i].Id);
                }
                EditorGUI.indentLevel--;
            }
        }
    }
}