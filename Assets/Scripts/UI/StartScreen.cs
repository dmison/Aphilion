
using System;
using System.Collections;
using UI;
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

    private void OnDestroy()
    {
        InputSystem.actions.FindActionMap("Start").Disable();
        _continueAction.performed -= ContinueAction;
    }

    private IEnumerator Start()
    {
        InputSystemManager.Instance.TogglePlayerControls(false);
        InputSystem.actions.FindActionMap("Start").Disable();
        InputSystem.actions.FindActionMap("GameMenu").Disable();
        
        yield return new WaitUntil(() => inputCanvasGroup.alpha > 0.3f);
        
        InputSystem.actions.FindActionMap("Start").Enable();
        _continueAction = InputSystem.actions.FindAction("Start/ToContinue");
        _continueAction.performed += ContinueAction;
    }

    public void Show(bool visible)
    {
        InputSystemManager.Instance.ToggleMenuInputs(false);
        InputSystemManager.Instance.TogglePlayerControls(false);
        InputSystem.actions.FindActionMap("Start").Disable();
        InputSystem.actions.FindActionMap("GameMenu").Disable();
        if (_animator)
        {
            _animator.SetBool("Active", visible);
        }
        
        // Debug.Log("Show: " +visible);
        if (visible) InputSystem.actions.FindActionMap("Start").Enable();
        // else InputSystem.actions.FindActionMap("Start").Disable();

        // if (_animator)
        // {
        //     _animator.SetBool("Active", visible);
        // }
    }

    private void ContinueAction(InputAction.CallbackContext context)
    {
        InputSystem.actions.FindActionMap("Start").Disable();
        Debug.Log("ContinueAction");
        Show(false);
        _gameMenuManager.ToggleMe(true);
    }
}
