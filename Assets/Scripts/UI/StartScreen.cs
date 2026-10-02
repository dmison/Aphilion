
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class StartScreen : MonoBehaviour
{
    private InputAction _continueAction;
    [SerializeField] private CanvasGroup inputCanvasGroup;
    [SerializeField] private GameMenuManager _gameMenuManager;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private IEnumerator Start()
    {
        InputSystemManager.Instance.TogglePlayerControls(false);
        _continueAction = InputSystem.actions.FindAction("Start/ToContinue");
        _continueAction.Disable();
        
        yield return new WaitUntil(() => inputCanvasGroup.alpha == 1);
        _continueAction.Enable();
        
        _continueAction.performed += _ => { ContinueAction();  };
    }

    public void Show(bool visible)
    {
        InputSystemManager.Instance.ToggleMenuInputs(!visible);
        
        Debug.Log("Show: " +visible);
        if (visible) _continueAction?.Enable();
        else _continueAction?.Disable();

        if (_animator)
        {
            _animator.SetBool("Active", visible);
        }
    }

    private void ContinueAction()
    {
        Debug.Log("ContinueAction");
        Show(false);
        _gameMenuManager.ToggleMe(true);
    }
}
