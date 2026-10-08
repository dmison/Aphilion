using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{

    public class StatusBar : MonoBehaviour
    {
        [SerializeField] private Slider staminaStatBar;
        [SerializeField] private Slider healthStatBar;
        
        [SerializeField] private float staminaBarScale = 2f; 
        [SerializeField] private float healthBarScale = 2f;
        [SerializeField] private GameObject staminaBox;
        [SerializeField] private GameObject healthBox;
        
        
        public void DoUpdate(float stamina, float maxStamina, int health, int maxHealth)
        {
            if (staminaStatBar != null)
            {
                staminaStatBar.value = stamina / maxStamina;
                staminaBox.transform.localScale = new Vector3(maxStamina/100, 1f, 1f);
                // staminaStatBar.highValue = Mathf.RoundToInt(maxStamina);
                // staminaStatBar.lowValue = 0;
                // calculate bar width
                // staminaStatBar.style.width = new StyleLength(maxStamina*staminaBarScale);

            }

            

            if (healthStatBar != null)
            {
                healthStatBar.value = (float)health / (float)maxHealth;
                healthBox.transform.localScale = new Vector3((float)maxHealth/100, 1f, 1f);
            //     _healthStatBar.value = health;
            //     _healthStatBar.highValue = maxHealth;
            //     _healthStatBar.lowValue = 0;
            //     // calculate bar width
            //     _healthStatBar.style.width = new StyleLength(maxHealth*healthBarScale);
            //     
            }
        }
    }
}