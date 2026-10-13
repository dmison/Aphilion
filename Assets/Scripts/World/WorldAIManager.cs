using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace World
{
    public class WorldAIManager : MonoBehaviour
    {
        public static WorldAIManager Instance;

        [Header("Characters")] 
        public List<AiSpawnPoint> AiSpawns;
        public List<GameObject> SpawnedInCharacters;

        public void CollectSpawnPoints()
        {
            DestroyAllSpawnedCharacters();
            AiSpawns = GameObject.FindObjectsByType<AiSpawnPoint>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        }

        public void RemoveAllSpawnPoints()
        {
            DestroyAllSpawnedCharacters();
            AiSpawns.Clear();
        }
        
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
        
        public void SpawnAllCharacters()
        {   
            foreach (AiSpawnPoint spawn in AiSpawns)
            {
                GameObject newCharacter = Instantiate(spawn.AiPrefab, spawn.transform.position, spawn.transform.rotation);
                SpawnedInCharacters.Add(newCharacter);

            }
        }

        public void DestroyAllSpawnedCharacters()
        {
            foreach (GameObject character in SpawnedInCharacters)
            {
                Destroy(character);
            }
            SpawnedInCharacters.Clear();
            
        }

        public void ResetAllCharacters()
        {
            ResetCharactersWait();
        }
        
        private void ResetCharactersWait()
        {
            DestroyAllSpawnedCharacters();
            // yield return new WaitForSeconds(1);
            SpawnAllCharacters();
        }
    }
}
