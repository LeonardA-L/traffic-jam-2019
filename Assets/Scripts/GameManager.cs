using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    public class GameManager : Singleton<GameManager>
    {
        public enum GameMode
        {
            EXPLORATION,
            TRADE
        }

        public string m_savePath = "/TFJ_state.sav" /*TODO*/;
        public GameState m_gameState;
        public GameMode m_gameMode = GameManager.GameMode.EXPLORATION;
        private Yarn.Unity.DialogueRunner m_dialogRunner;
        public VariableStorage m_yarnStorage; // TODO
        public delegate void SubjectStateLoaded();
        public List<SubjectStateLoaded> m_subjectsStateLoaded = new List<SubjectStateLoaded>();

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Init GameManager");

            Load();

            if (m_gameState.NewGame)
            {  // New game
                Debug.Log("New game");
            }
            else
            {
                Debug.Log("Loaded game");
            }

            m_dialogRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Load()
        {
            m_gameState = GameState.Load(m_savePath);

            foreach(var subject in m_subjectsStateLoaded)
            {
                subject();
            }
        }

        public void Save()
        {
            m_gameState.m_yarnVariables.Clear();
            m_gameState.m_yarnVariables.AddRange(m_yarnStorage.Export());
            m_gameState.Save(m_savePath);
        }

        public void SwitchToTradeMode(string _node = null)
        {
            m_gameMode = GameMode.TRADE;
            CameraManager.Instance.SwitchToTradeMode();
            if(_node != null)
                m_dialogRunner.StartDialogue(_node);
            PlayerManager.Instance.SetAllowMovement(false);
        }

        public void SwitchToExplorationMode()
        {
            m_gameMode = GameMode.EXPLORATION;
            CameraManager.Instance.SwitchToFollowMode();
            PlayerManager.Instance.SetAllowMovement(true);
        }

        public Inventory Inventory {
            get
            {
                return m_gameState.m_inventory;
            }
        }

        public GameManager.GameMode CurrentGameMode
        {
            get
            {
                return m_gameMode;
            }
        }

        public List<SubjectStateLoaded> SubjectsStateLoaded
        {
            get
            {
                return m_subjectsStateLoaded;
            }
        }
    }
}