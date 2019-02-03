using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    public class GameManager : Singleton<GameManager>
    {
        public string m_savePath = "/TFJ_state.sav" /*TODO*/;
        public GameState m_gameState;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Init GameManager");

            m_gameState = GameState.Load(m_savePath);

            if (m_gameState.NewGame)
            {  // New game
                Debug.Log("New game");
            }
            else
            {
                Debug.Log("Loaded game");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Load()
        {
            m_gameState = GameState.Load(m_savePath);
        }

        public void Save()
        {
            m_gameState.Save(m_savePath);
        }

        public Inventory Inventory {
            get
            {
                return m_gameState.m_inventory;
            }
        }
    }
}