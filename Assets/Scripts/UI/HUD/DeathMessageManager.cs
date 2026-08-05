using System;
using UnityEngine;
using UnityEngine.UIElements;

public class DeathMessageManager : MonoBehaviour
{
    private VisualElement _rootElement;
    private VisualElement _panel;
    private Label _deathMessage;

    private void Awake()
    {
        _rootElement = GetComponent<UIDocument>().rootVisualElement;
        _panel = _rootElement.Q<VisualElement>("DeathPanel");
        
        Debug.Log(_panel);
    }

    public void ShowDeathMessage()
    {
        _panel.AddToClassList("show");
    }

    public void HideDeathMessage()
    {
        _panel.RemoveFromClassList("show");
    }
    
}
