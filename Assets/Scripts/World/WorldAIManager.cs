using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace World
{
    public class WorldAIManager : MonoBehaviour
    {
        public static WorldAIManager Instance;

        [Header("Characters")] 
        [SerializeField] GameObject[] AiCharacters;
        [SerializeField] List<GameObject> SpawnedInCharacters;
        
        
        
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

        // private void Start()
        // {
        //     StartCoroutine(WaitForSceneToLoadThenSpawnCharacters());
        // }

        private IEnumerator WaitForSceneToLoadThenSpawnCharacters()
        {
            while (!SceneManager.GetActiveScene().isLoaded)
            {
                yield return null;
            }
        }

        private void SpawnAllCharacters()
        {
            foreach (GameObject character in AiCharacters)
            {
                GameObject newCharacter = Instantiate(character);
                SpawnedInCharacters.Add(newCharacter);
            }
        }

        private void DestroyAllSpawnedCharacters()
        {
            foreach (GameObject character in SpawnedInCharacters)
            {
                Destroy(character);
            }
        }

        private void DisableAllSpawnedCharacters()
        {
            
        }
    }
}
