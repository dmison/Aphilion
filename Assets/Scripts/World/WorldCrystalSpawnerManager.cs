using System.Collections.Generic;
using System.Linq;
using Character.Player;
using GameSaving;
using Interactable;
using UnityEngine;

namespace World
{
    public class WorldCrystalSpawnerManager : MonoBehaviour
    {
        public static WorldCrystalSpawnerManager Instance;

        [SerializeField] private List<SpawnCrystalManager> _spawners = new List<SpawnCrystalManager>(); 
        private List<int> _activatedCrystalIds = new List<int>();
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public bool hasCurrentSpawn()
        {
            int crystalId = PlayerManager.Instance.currentSpawnCrystalId;
            return _spawners.Exists(crystal => crystal.crystalId == crystalId);
        }
        
        public SpawnCrystalManager GetCrystalById(int id)
        {
            return _spawners.Find(crystal => crystal.crystalId == id);
        }
        
        public void ReportIn(SpawnCrystalManager crystal)
        {
            if (crystal.activated)
            {
                List<int> thisone = new List<int>();
                thisone.Add(crystal.crystalId);
                _activatedCrystalIds = _activatedCrystalIds.Union(thisone).ToList();
            }

            if (_activatedCrystalIds.Contains(crystal.crystalId))
            {
                crystal.activated = true;
            }
            _spawners.Add(crystal);
        }

        public void RemoveMe(SpawnCrystalManager crystal)
        {
            _spawners.Remove(crystal);
        }
        
        
        // private void Start()
        // {
        //     Debug.Log("start");
        //     DontDestroyOnLoad(gameObject);
        // }

        public void PopulateToSaveData(ref CharacterSaveData data)
        {
            // List<int> activated = data.activatedCrystals;
            List<int> activated = _spawners
                .FindAll(crystal => crystal.activated)
                .Select(crystal => crystal.crystalId).ToList();
            List<int> result = data.activatedCrystals.Union(activated).ToList();
            data.activatedCrystals = result;
        }
          
        public void RestoreFromSaveData(CharacterSaveData data)
        {
            _activatedCrystalIds = _activatedCrystalIds.Union(data.activatedCrystals).ToList();

            for (var i = 0; i < _spawners.Count; i++)
            {
                if (_activatedCrystalIds.Contains(_spawners[i].crystalId))
                {
                    _spawners[i].ActivateMe();
                }
            }
        }

    }
}
