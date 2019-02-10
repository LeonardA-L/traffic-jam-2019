using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

namespace tfj
{
    public class MapController : MonoBehaviour
    {
        private Dictionary<string, GameObject> m_drawings = new Dictionary<string, GameObject>();
        private VariableStorage m_variableStorage;
        private const string s_splitter = "$Map_";
        public Transform m_canvas;
        public GameObject m_drawingPrefab;

        [System.NonSerialized]
        private Dictionary<string, MapDrawing> m_references;

        // Start is called before the first frame update
        void Awake()
        {
            GameManager.Instance.SubjectsStateLoaded.Add(DrawAll);
            m_variableStorage = ((VariableStorage)(FindObjectOfType<DialogueRunner>().variableStorage));

            Debug.Assert(m_canvas != null, "No Canvas provided");
            Debug.Assert(m_drawingPrefab != null, "No Drawing Prefab provided");

            var drawings = Resources.LoadAll("MapDrawings", typeof(MapDrawing));
            m_references = new Dictionary<string, MapDrawing>();
            foreach (var i in drawings)
            {
                MapDrawing drawing = (MapDrawing)i;
                m_references.Add(drawing.Id, drawing);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DrawAll()
        {
            var variables = m_variableStorage.Export();
            foreach (var variable in variables)
            {
                string objName = VariableToObject(variable.m_key);
                if(objName != null)
                {
                    GameObject drawing = null;
                    m_drawings.TryGetValue(objName, out drawing);
                    if (drawing != null)
                    {
                        drawing.SetActive(variable.m_value.AsBool);
                    } else if(variable.m_value.AsBool)
                    {
                        Draw(objName);
                    }
                }
            }
        }

        public void Draw(string _drawingName)
        {
            GameObject drawing = Instantiate(m_drawingPrefab, m_canvas);
            MapDrawing so = m_references[_drawingName];
            drawing.GetComponent<Image>().sprite = Resources.Load<Sprite>(so.ViewSprite);
            var drawingTransform = drawing.GetComponent<RectTransform>();
            drawingTransform.sizeDelta = so.m_size;
            drawingTransform.localPosition = new Vector3(so.m_coordinates.x, so.m_coordinates.y, -0.01f * so.m_zIndex);
            drawingTransform.localScale = so.m_scale;
            m_drawings.Add(_drawingName, drawing);
        }

        private static string ObjectToVariable(string _objectName)
        {
            return s_splitter + _objectName;
        }

        private static string VariableToObject(string _variableName)
        {
            var split = _variableName.Split(new[] { s_splitter }, StringSplitOptions.None);
            if(split.Length > 1)
                return split[1];
            return null;
        }
    }
}