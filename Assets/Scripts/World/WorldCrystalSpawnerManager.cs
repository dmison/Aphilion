using System.Collections.Generic;
using Interactable;
using UnityEngine;

namespace World
{
    public class WorldCrystalSpawnerManager : MonoBehaviour
    {
        public static WorldCrystalSpawnerManager Instance;

        [SerializeField] private List<SpawnCrystalManager> _spawners = new List<SpawnCrystalManager>(); 

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

        public SpawnCrystalManager GetCrystalById(int id)
        {
            return _spawners.Find(crystal => crystal.crystalId == id);
        }
        
        public void ReportIn(SpawnCrystalManager crystal)
        {
            _spawners.Add(crystal);
        }

        public void RemoveMe(SpawnCrystalManager crystal)
        {
            _spawners.Remove(crystal);
        }
        
        
        private void Start()
        {
            Debug.Log("start");
            DontDestroyOnLoad(gameObject);
        }


    }
}
