using System;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathMessageManager : MonoBehaviour
{
    // private VisualElement _rootElement;
    // private VisualElement _panel;
    // private Label _deathMessage;
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        // _rootElement = GetComponent<UIDocument>().rootVisualElement;
        // _panel = _rootElement.Q<VisualElement>("DeathPanel");
        
    }

    public void ShowDeathMessage()
    {
        _animator.SetBool("Active", true);
    }

    public void HideDeathMessage()
    {
        _animator.SetBool("Active", false);
    }
    
}
