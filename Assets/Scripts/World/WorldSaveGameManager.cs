using System.Collections;
using Character.Player;
using GameSaving;
using UnityEngine;

namespace World
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        #region Singleton
        public static WorldSaveGameManager instance;

        // THERE CAN BE ONLY ONE!
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion

        [SerializeField] private Animator _savingWidget;
        public GameSaveSlots gameSaveSlot = GameSaveSlots.Slot01;
        public CharacterSaveData characterSaveData;

        public void ToggleSavingWidget(bool state)
        {
            _savingWidget.SetBool("Active", state);
        }
        
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public string CharacterSaveFilename()
        {
            return gameSaveSlot switch
            {
                GameSaveSlots.Slot01 => "CharacterSaveSlot01",
                GameSaveSlots.Slot02 => "CharacterSaveSlot02",
                GameSaveSlots.Slot03 => "CharacterSaveSlot03",
                _ => ""
            };
            
        }
        
        public void NewGame()
        {
            characterSaveData = new CharacterSaveData();
        }

        public void LoadGame()
        {
            SaveFileDataWriter saveFileDataWriter = new SaveFileDataWriter();
            characterSaveData = saveFileDataWriter.LoadCharacterSaveData(CharacterSaveFilename());
            PlayerManager.Instance.RestoreFromSaveData(characterSaveData);
            WorldCrystalSpawnerManager.Instance.RestoreFromSaveData(characterSaveData);
            
            // StartCoroutine(WorldSceneManager.instance.LoadScene());
        }
        
        
        public void SaveGame()
        {
         
            SaveFileDataWriter saveFileDataWriter = new SaveFileDataWriter();
            PlayerManager.Instance.PopulateToSaveData(ref characterSaveData);
            WorldCrystalSpawnerManager.Instance.PopulateToSaveData(ref characterSaveData);
            
            saveFileDataWriter.CreateNewCharacterSaveFile(characterSaveData, CharacterSaveFilename());
        }

        public void DeleteSaveGame()
        {
            
        }
    }
}
