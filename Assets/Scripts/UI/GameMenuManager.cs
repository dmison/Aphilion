using System;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using World;

public class GameMenuManager : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private InputSystemUIInputModule _inputModule;
    [SerializeField] private Button _continueButton;
    [SerializeField] private EventSystem _eventSystem;
    
    private InputAction _backButton;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _backButton = InputSystem.actions.FindAction("GameMenu/Cancel");
        _backButton.performed += GoBackListener;
    }

    private void OnDestroy()
    {
        _backButton.performed -= GoBackListener;
    }

    private void Update()
    {
        _continueButton.interactable = HasContinue();
        // _continueButton
    }

    public void ShowMenu(bool visible)
    {
        ToggleMe(visible);
    }

    public void GoBack()
    {
        ToggleMe(false);   
        _startScreen.Show(true);
    }
    
    private void GoBackListener(InputAction.CallbackContext context)
    {
        GoBack();
    }
    
    public void ToggleMe(bool visible)
    {


        
        if (visible)
        {
            if (_animator)
            {
                _animator.SetTrigger("Show");
            }

            if (_inputModule)
            {
                _inputModule.enabled = true;
                
            }
            InputSystem.actions.FindActionMap("GameMenu").Enable();
            _eventSystem.SetSelectedGameObject(_eventSystem.firstSelectedGameObject);
        }
        else
        {
            InputSystem.actions.FindActionMap("GameMenu").Disable();
            if (_animator)
            {
                _animator.SetTrigger("Hide");
            }

            if (_inputModule)
            {
                _inputModule.enabled = false;
            }
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
        WorldSaveGameManager.instance.NewSaveGame();  // create new save
        Debug.Log("after new save");
        
        StartCoroutine(WorldSceneManager.Instance.NewGameStart()); // load world
        
        
    }

    public void LoadGame()
    {
        // WorldSaveGameManager.instance.ToggleSavingWidget(true);
    }

    public void ContinueGame()
    {
        
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
