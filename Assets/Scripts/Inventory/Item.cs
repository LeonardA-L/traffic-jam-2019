using UnityEngine;

namespace tfj
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Inventory Item")]
    public class Item : ScriptableObject
    {
        public string m_id;
        [Tooltip("3D View. Must be a prefab located in the Resource folder.")]
        public GameObject m_view3D;
        [Tooltip("2D View. Must be path to a sprite, relative to the Resource folder.")]
        public string m_viewSprite;

        public string Id
        {
            get
            {
                return m_id;
            }
        }

        public string ViewSprite {
            get
            {
                return m_viewSprite;
            }
        }

        public GameObject View {
            get
            {
                return m_view3D;
            }
        }
    }
}