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
        [SerializeField] private int startRegionIndex = 2;
        [SerializeField] private GameObject PlayerPrefab;
        [SerializeField] private GameObject PlayerCamera;
        [SerializeField] private GameObject PlayerUIManagerPrefab;

        private AsyncOperation _loadWorld;
        private AsyncOperation _loadRegion;
        
        private bool _worldReady = false;
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
        
        public IEnumerator LoadWorld()
        {
            _worldReady = false;
            AsyncOperation load = SceneManager.LoadSceneAsync(worldSceneIndex);
            
            if (load.progress < 0.9f)
            {
                LoaderScreen.Instance.SetProgress(load.progress/4);
                yield return null;
            }
            LoaderScreen.Instance.SetProgress(0.25f);
            _worldReady = true;
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

        
        public IEnumerator LoadPlayer()
        {
            // yield return new WaitForSeconds(2);
            GameObject playerUI = Instantiate(PlayerUIManagerPrefab);
            playerUI.SetActive(false);
                
            
            GameObject player = Instantiate(PlayerPrefab);
            PlayerManager playerManager = player.GetComponent<PlayerManager>();
            playerManager.IsActive = false; 
            Instantiate(PlayerCamera);
            // PlayerCameraManager.instance.playerManager = playerManager;
            // PlayerCameraManager.instance.CameraReset();
            // PlayerCameraManager.instance.SetLockCameraHeight();
            
            // update player with save data
            
            Debug.Log(playerManager);
            Debug.Log(WorldSaveGameManager.instance.characterSaveData);
            
            playerManager.RestoreFromSaveData((WorldSaveGameManager.instance.characterSaveData));
            
            
            // ============================================================
            Debug.Log(WorldSaveGameManager.instance.characterSaveData.rightHandQuickSlots.Length);
            Debug.Log("player setup");
                
            // wait until region setup is done
            // hmmmm, wait for spawnpoints to be in 
            LoaderScreen.Instance.SetProgress(0.8f);
            while (!WorldCrystalSpawnerManager.Instance.hasCurrentSpawn())
            {
                Debug.Log("waiting for crystal");
                yield return null;
            }
            WorldCrystalSpawnerManager.Instance.RestoreFromSaveData(WorldSaveGameManager.instance.characterSaveData);
            Debug.Log("got crystal");
            
            // position player in world
            playerManager.PlaceAtSpawnCrystal();
                
            // make player active
            playerManager.IsActive = true;
            // start player spawning animation
            // TODO: player spawn animation, standing from kneel?
            playerManager.characterAnimationManager.PlayTargetActionAnimation(
                "A_Crouch_ToStand_Femn", 
                true, 
                false, 
                false, 
                false
                );
            
            // hide loading screen
            LoaderScreen.Instance.SetProgress(1.0f);
            yield return new WaitForSeconds(0.5f);
            LoaderScreen.Instance.ToggleLoader(false);
                
            // enable player inputs
            InputSystemManager.Instance.TogglePlayerControls(true);
            playerUI.SetActive(true);
            // save game
            WorldSaveGameManager.instance.SaveGame();
            yield return true;
            
        }
        
        public IEnumerator LoadRegion()
        {
            // yield return new WaitForSeconds(1);
            _loadRegion = SceneManager.LoadSceneAsync(startRegionIndex,  LoadSceneMode.Additive);
            _loadRegion.completed += _ => { StartCoroutine(LoadPlayer()); };
            if (_loadRegion.progress < 0.9f)
            {
                Debug.Log("region loading: "+_loadRegion.progress/3);
                LoaderScreen.Instance.SetProgress(_loadRegion.progress/3+0.3f);
                yield return null;
            }
        }
        
        public IEnumerator NewGameStart()
        {
            
            Debug.Log("NewGameStart");
            // deactivate all inputs
            InputSystemManager.Instance.TogglePlayerControls(false);
            InputSystemManager.Instance.ToggleMenuInputs(false);
            
            // load world scene
            // wait until world scene loads
            _loadWorld = SceneManager.LoadSceneAsync(worldSceneIndex,  LoadSceneMode.Single);
            _loadWorld.completed += _ =>
            {
                LoaderScreen.Instance.SetProgress(0.3f);
                StartCoroutine(LoadRegion());
            };
            // yield return new WaitForSeconds(1);
            if (_loadWorld.progress < 0.9f)
            {
                Debug.Log("world loading: "+_loadWorld.progress/3);
                LoaderScreen.Instance.SetProgress(_loadWorld.progress/3);
                yield return null;
            }
            // AsyncOperation load = SceneManager.LoadSceneAsync(worldSceneIndex,  LoadSceneMode.Single);
            // load.allowSceneActivation = true;
            // if (load.progress < 0.9f)
            // {
            //     Debug.Log("world loading: "+load.progress+ " Done"+load.isDone);
            //     // LoaderScreen.Instance.SetProgress(load.progress/4);
            //     yield return null;
            // }
            // LoaderScreen.Instance.SetProgress(0.25f);
            // ~~wait until all singletons are available~~
            // Debug.Log("world loading: "+load.progress);
            // Debug.Log("world loaded Done"+load.isDone);
            // load region scene
            // wait until region is loaded
            // SceneManager.LoadScene(startRegionIndex, LoadSceneMode.Additive);
            // AsyncOperation regionLoad = SceneManager.LoadSceneAsync(startRegionIndex,  LoadSceneMode.Additive);
            //
            // regionLoad.allowSceneActivation = true;
            //
            // if (regionLoad.progress < 0.9f)
            // {
            //     Debug.Log("region loading: "+regionLoad.progress+" Done"+regionLoad.isDone);
            //     LoaderScreen.Instance.SetProgress(0.25f + (regionLoad.progress/4*3));
            //     yield return null;
            // }
            //
            // Debug.Log("region loading: " + regionLoad.progress);
            // Debug.Log("region Loaded Done:"+regionLoad.isDone);            
            // ============================================================ instantiate player inactive
            
        }
    }
}
