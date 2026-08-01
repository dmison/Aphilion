using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace UI.HUD
{

    public class StatusBar : MonoBehaviour
    {
        private VisualElement _rootVe;
        private ProgressBar _staminaStatBar;
        private ProgressBar _healthStatBar;
        
        
        private void Start()
        {
            _rootVe = GetComponent<UIDocument>().rootVisualElement;
            _staminaStatBar = _rootVe.Q<ProgressBar>("staminaStatBar");
            _healthStatBar = _rootVe.Q<ProgressBar>("healthStatBar");
        }

        
        public void DoUpdate(float stamina, float maxStamina, int health, int maxHealth)
        {
            if (_staminaStatBar != null)
            {   
                _staminaStatBar.value = Mathf.RoundToInt(stamina);
                _staminaStatBar.highValue = Mathf.RoundToInt(maxStamina);
                _staminaStatBar.lowValue = 0;
                // calculate bar width
                // _healthStatBar.style.width = new StyleLength(240);
            }

            if (_healthStatBar != null)
            {
                _healthStatBar.value = health;
                _healthStatBar.highValue = maxHealth;
                _healthStatBar.lowValue = 0;
                // calculate bar width
                // _healthStatBar.style.width = new StyleLength(240);
            }
        }
    }
}