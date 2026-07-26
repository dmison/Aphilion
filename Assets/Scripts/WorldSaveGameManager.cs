using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private int worldSceneIndex = 1;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator LoadScene()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(worldSceneIndex);
        yield return null;

    }
}
