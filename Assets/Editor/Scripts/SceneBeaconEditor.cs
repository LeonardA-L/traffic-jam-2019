using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneBeacon))]
public class SceneBeaconEditor : Editor
{
    void OnSceneGUI()
    {
        SceneBeacon beacon = (SceneBeacon)target;

        Handles.color = Color.green;
        Handles.DrawWireDisc(beacon.transform.position, new Vector3(0, 1, 0), beacon.m_enableRadius);
        Handles.color = Color.red;
        Handles.DrawWireDisc(beacon.transform.position, new Vector3(0, 1, 0), beacon.m_disableRadius);
    }
}
