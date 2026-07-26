using UnityEngine;
using UnityEngine.UIElements;

namespace UI.HUD
{
    public class StatusBar: MonoBehaviour
    {
        private VisualElement _rootVe;
        
        private ProgressBar _healthBar;
        private ProgressBar _staminaBar;

        private void Start()
        {
            _rootVe = GetComponent<UIDocument>().rootVisualElement;
            _healthBar = _rootVe.Q<ProgressBar>("healthBar");
            _staminaBar = _rootVe.Q<ProgressBar>("staminaBar");
        }

        private void Update()
        {
            _healthBar.value = 145;
            _healthBar.highValue = 200;
            _healthBar.lowValue = 0;
            
            _staminaBar.value = 60;
            _staminaBar.highValue = 100;
            _staminaBar.lowValue = 0;
        }

    }
}