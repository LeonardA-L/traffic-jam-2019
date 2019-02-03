using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using System;

namespace tfj
{
    [System.Serializable]
    public class GameState
    {
        [SerializeField]
        private bool m_newGame = true;
        [NonSerialized]
        public Inventory m_inventory = new Inventory();
        [SerializeField]
        [HideInInspector]
        private InventorySerialized m_inventorySerialized = new InventorySerialized();

        public void Init()
        {
            m_inventory = new Inventory();
            m_inventory.Init(m_inventorySerialized);
        }

        private void PrepareForSerialization()
        {
            m_inventorySerialized.IDs.Clear();
            foreach (var item in m_inventory.Items)
            {
                m_inventorySerialized.Add(item.Id);
            }
        }

        public void Save(string _path /*= TODO*/)
        {
            string savePath = Application.persistentDataPath + _path;
            m_newGame = false;

            PrepareForSerialization();

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(savePath);

            bf.Serialize(file, this);
            file.Close();

            Debug.Log("Inventory saved");
        }

        public static GameState Load(string _path, bool _failOnError = false)
        {
            string savePath = Application.persistentDataPath + _path;
            Debug.Log(savePath);
            if (!File.Exists(savePath))
            {
                Debug.LogError("Savegame doesn't exist");
                if (_failOnError)
                    throw new FileNotFoundException();
                GameState newGameState = new GameState();
                newGameState.Init();
                return newGameState;
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);

            GameState save = (GameState)bf.Deserialize(file);
            file.Close();

            if (save == null)
            {
                Debug.LogError("Savegame doesn't exist");
                if (_failOnError)
                    throw new FormatException();
                GameState newGameState = new GameState();
                newGameState.Init();
                return newGameState;
            }

            save.Init();
            return save;
        }

        public static void Save(GameState _state, string _path)
        {
            _state.Save(_path);
        }

        public bool NewGame
        {
            get
            {
                return m_newGame;
            }
        }
    }
}