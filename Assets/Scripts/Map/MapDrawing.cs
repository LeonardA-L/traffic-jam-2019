using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    [CreateAssetMenu(menuName = "Map Drawing")]
    public class MapDrawing : ScriptableObject
    {
        public string m_id;
        [Tooltip("2D View. Must be path to a sprite, relative to the Resource folder.")]
        public string m_viewSprite;
        public Vector2 m_coordinates;
        [Range(1, 5)]
        public int m_zIndex = 1;
        public Vector2 m_scale = new Vector2(1, 1);
        public Vector2 m_size = new Vector2(600, 400);

        public string Id
        {
            get
            {
                return m_id;
            }
        }

        public string ViewSprite
        {
            get
            {
                return m_viewSprite;
            }
        }
    }
}