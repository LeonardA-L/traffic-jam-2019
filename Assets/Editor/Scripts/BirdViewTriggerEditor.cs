using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace tfj
{
    [CustomEditor(typeof(BirdViewTrigger))]
    public class BirdViewTriggerEditor : Editor
    {
        void OnSceneGUI()
        {
            BirdViewTrigger beacon = (BirdViewTrigger)target;

            Handles.color = Color.green;
            Handles.DrawWireDisc(beacon.transform.position, new Vector3(0, 1, 0), beacon.m_triggerRadius);
        }
    }
}