using System.Collections;
using Character.Player;
using Interactable;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace World
{
    public class WorldSceneManager : MonoBehaviour
    {
        #region Singleton
        public static WorldSceneManager Instance;

        // THERE CAN BE ONLY ONE!
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
        #endregion

        [SerializeField] private int worldSceneIndex = 1;
    
        //
        private void Start()
        {
            
            DontDestroyOnLoad(gameObject);
            
            // if the active scene isn't the root scene, then switch to root
            // this is a dev thing so you don't have to keep switching to the
            // root scene to run the game when working on a level
            // probably should pass the name of this scene so it will be loaded by 
            // the game manager - not an issue with only the one scene we have thus far
            // if (SceneManager.GetActiveScene().name != "MainMenu")
            // {
            //     LoadWorld();
            // }
        }


        public IEnumerator PlacePlayerAtSpawnPoint()
        {
            
            
            Debug.Log("waiting 2 seconds");
            yield return new WaitForSeconds(2f);
            Debug.Log("waiting done, loading");
            WorldSaveGameManager.instance.LoadGame();


            while (!WorldCrystalSpawnerManager.Instance.hasCurrentSpawn())
            {
                Debug.Log("no current spawn");
                yield return null;
            }
            Debug.Log("got current spawn: "+PlayerManager.Instance.currentSpawnCrystalId);
            
            // while (!WorldCrystalSpawnerManager.Instance && !PlayerManager.Instance)
            // {
            //     yield return null;
            // }
            
            SpawnCrystalManager crystal = WorldCrystalSpawnerManager.Instance.GetCrystalById(PlayerManager.Instance.currentSpawnCrystalId); 
                
            while (!crystal)
            {
                Debug.Log("no crystal install");
                crystal = WorldCrystalSpawnerManager.Instance.GetCrystalById(PlayerManager.Instance.currentSpawnCrystalId);
                yield return null;
            }

            Debug.Log("got it");
            Debug.Log("player position: "+ PlayerManager.Instance.transform.position);
            Debug.Log("spawn position: "+ crystal.spawnPoint.position);
            PlayerManager.Instance.transform.position = crystal.spawnPoint.position;
            // PlayerManager.Instance.transform.rotation = crystal.spawnPoint.rotation;
            // PlayerCameraManager.instance.ResetPivotRotationToPlayer();
            
        }
        
        public void LoadWorld()
        {
            
            StartCoroutine(LoadScene(worldSceneIndex));
        }
        
        public IEnumerator LoadScene(int sceneIndex)
        {
            // Debug.Log("before: "+SceneManager.GetActiveScene().name);
            AsyncOperation load = SceneManager.LoadSceneAsync(sceneIndex);
            
            if (load.progress < 0.9f)
            {
                LoaderScreen.Instance.SetProgress(load.progress);
                yield return null;
            }
            
            // if (SceneManager.GetActiveScene().name == "MainMenu")
            // {
            //     // Debug.Log("after: "+SceneManager.GetActiveScene().name);
            //     StartCoroutine(PlacePlayerAtSpawnPoint());
            // }

        }
    }
}
