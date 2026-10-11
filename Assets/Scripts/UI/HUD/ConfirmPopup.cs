using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConfirmPopup : MonoBehaviour
{
    private Animator _animator;
    private InputAction _confirmAction;
    private InputAction _cancelAction;
    private Action ConfirmAction;
    private Action CancelAction;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        InputSystem.actions.FindActionMap("PopupActions").Disable();
        _confirmAction = InputSystem.actions.FindAction("PopupActions/Activate");
        _cancelAction = InputSystem.actions.FindAction("PopupActions/Cancel");
        _confirmAction.performed += Confirm;
        _cancelAction.performed += Cancel;
    }

    private void OnDestroy()
    {
        _confirmAction.performed -= Confirm;
        _cancelAction.performed -= Cancel;
    }

    public void Show(Action confirm, Action cancel)
    {
        _animator.SetBool("Active", true);
        InputSystem.actions.FindActionMap("PopupActions").Enable();
        ConfirmAction = confirm;
        CancelAction = cancel;
    }

    public void Hide()
    {
        _animator.SetBool("Active", false);
        InputSystem.actions.FindActionMap("PopupActions").Disable();
        ConfirmAction = null;
        CancelAction = null;
    }
    
    private void Cancel(InputAction.CallbackContext context)
    {
        if(CancelAction != null)CancelAction.Invoke();
        Hide();
    }
    private void Confirm(InputAction.CallbackContext context)
    {
        if(ConfirmAction != null)ConfirmAction.Invoke();
        Hide();
    }
    
}
