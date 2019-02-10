using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace tfj
{
    public class SceneBeacon : MonoBehaviour
    {
        public float m_enableRadius = 3;
        public float m_disableRadius = 4;
        public string m_sceneToLoad = "Scenes/";
        public GameObject m_prefabToLoad;
        private GameObject m_loadedPrefab = null;

        private Transform m_character;
        private bool m_enabled = false;
        //private List<GameObject> m_loadedRoots;

        void Start()
        {
            //m_loadedRoots = new List<GameObject>();
            Debug.Assert(m_enableRadius < m_disableRadius, "enableRadius should be < to disableRadius", gameObject);
            // TODO properly get player transform
            var character = (TFJCharacterController)FindObjectOfType(typeof(TFJCharacterController)); // Assume there is only one character
            Debug.Assert(character != null, "No TFJCharacterController found on scene");
            m_character = character.transform;

            Debug.Assert(m_prefabToLoad != null || m_sceneToLoad != "", "Must provide prefab or scene to load", this);
            Debug.Assert(m_prefabToLoad != null ^ m_sceneToLoad != "", "Prefab and Scene to load are exclusive", this);
        }

        // Update is called once per frame
        void Update()
        {
            float sqrDist = (transform.position - m_character.position).sqrMagnitude;
            if (!m_enabled && sqrDist <= m_enableRadius * m_enableRadius)
            {
                if (m_prefabToLoad != null)
                {
                    LoadPrefab();
                }
                else
                {
                    StartCoroutine(LoadSceneAsync(m_sceneToLoad));
                }
                m_enabled = true;
            }
            else if (m_enabled && sqrDist >= m_disableRadius * m_disableRadius)
            {
                if (m_prefabToLoad != null)
                {
                    UnloadPrefab();
                }
                else
                {
                    StartCoroutine(UnloadSceneAsync(m_sceneToLoad));
                }
                m_enabled = false;
            }
        }

        private void LoadPrefab()
        {
            if(m_loadedPrefab == null)
            {
                m_loadedPrefab = GameObject.Instantiate(m_prefabToLoad, transform);
            } else
            {
                m_loadedPrefab.SetActive(true);
            }
        }

        private void UnloadPrefab()
        {
            Debug.Assert(m_loadedPrefab != null, "This should not happen.");
            m_loadedPrefab.SetActive(false);
        }

        IEnumerator LoadSceneAsync(string _scene)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_scene, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            /*
            Scene loaded = SceneManager.GetSceneByName(_scene);
            if (!loaded.isLoaded)
                yield return null;
            if(!loaded.IsValid())
                yield return null;
            var roots = loaded.GetRootGameObjects();
            m_loadedRoots.AddRange(roots);

            foreach(GameObject root in m_loadedRoots)
            {
                Debug.Log(root.name);
                root.transform.position += transform.position;
            }
            */
        }
        IEnumerator UnloadSceneAsync(string _scene)
        {
            AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(_scene);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}