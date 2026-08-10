using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        [SerializeField] private int worldSceneIndex = 2;
    
        //
        private void Start()
        {
            
            DontDestroyOnLoad(gameObject);
            
            // if the active scene isn't the root scene, then switch to root
            // this is a dev thing so you don't have to keep switching to the
            // root scene to run the game when working on a level
            // probably should pass the name of this scene so it will be loaded by 
            // the game manager - not an issue with only the one scene we have thus far
            if (SceneManager.GetActiveScene().name != "MainMenu")
            {
                StartCoroutine(LoadScene(0));
            }
        }

    
        public IEnumerator LoadScene(int scene=1)
            {Debug.Log("before: "+SceneManager.GetActiveScene().name);
            AsyncOperation load = SceneManager.LoadSceneAsync(scene);
            
            if (load.progress < 0.9f)
            {
                Debug.Log(load.progress);
                yield return null;
            }

            Debug.Log("after: "+SceneManager.GetActiveScene().name);
            

        }
    }
}
