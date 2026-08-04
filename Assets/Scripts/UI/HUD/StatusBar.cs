using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace UI.HUD
{

    public class StatusBar : MonoBehaviour
    {
        [SerializeField] private float _staminaBarScale = 2f; 
        [SerializeField] private float _healthBarScale = 2f;
        
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
                _staminaStatBar.style.width = new StyleLength(maxStamina*_staminaBarScale);
                
            }

            

            if (_healthStatBar != null)
            {
                _healthStatBar.value = health;
                _healthStatBar.highValue = maxHealth;
                _healthStatBar.lowValue = 0;
                // calculate bar width
                _healthStatBar.style.width = new StyleLength(maxHealth*_healthBarScale);
                
            }
        }
    }
}