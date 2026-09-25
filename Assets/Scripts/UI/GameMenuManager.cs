using UI;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using World;

public class GameMenuManager : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private InputSystemUIInputModule _inputModule;
    [SerializeField] private Button _continueButton;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _continueButton.interactable = HasContinue();
    }

    public void ShowMenu(bool visible)
    {
        ToggleMe(visible);
    }

    public void ToggleMe(bool visible)
    {
        if (visible)
        {
            _animator.SetTrigger("Show");
            _inputModule.enabled = true;
        }
        else
        {
            _animator.SetTrigger("Hide");
            _inputModule.enabled = false;
        }
    }

    public bool HasContinue()
    {
        return false;
    }

    public void NewGame()
    {
        // for actual game we would go to a pick slot screen
        // but for the demo we force a single slot
        LoaderScreen.Instance.ToggleLoader(true);           // show loader
        ToggleMe(false);                                    // hide this menu
        WorldSaveGameManager.instance.NewGame();  // create new save
        WorldSceneManager.Instance.LoadWorld(); // load world
        
        
    }

    public void LoadGame()
    {
        // WorldSaveGameManager.instance.ToggleSavingWidget(true);
    }

    public void ContinueGame()
    {
        
    }
    
    public void GoToTitle()
    {
        // _animator.SetTrigger("Hide");
        // ShowMenu(false);
        ToggleMe(false);   
        _startScreen.Show(true);

    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        // Quit the Game
        Application.Quit();
    }
}
