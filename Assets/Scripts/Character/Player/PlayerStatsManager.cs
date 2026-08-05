using UnityEngine;

namespace Character.Player
{
    public class PlayerStatsManager : CharacterStatsManager
    {
        private PlayerManager _playerManager;   
        protected override void RegenStamina()
        {
            base.RegenStamina();
            if ( _playerManager.playerMovementManager.IsSprinting || 
                 _playerManager.isPerformingAction || 
                 !(CurrentStamina < MaxStamina))
            {
                StaminaRegenTimer = 0;
                return;
            }
            
            if (StaminaRegenTimer < staminaRegenDelay)
            {
                StaminaRegenTimer += Time.deltaTime;
                return;
            }

            CurrentStamina += (staminaRegenPerSecond *  Time.deltaTime);
        }

        protected override void UpdateUI()
        {
            base.UpdateUI();
            PlayerUIManager.instance.statsBar.DoUpdate(currentStamina, MaxStamina, currentHealth, MaxHealth);
        }

        protected override void Awake()
        {
            base.Awake();
            _playerManager  =  GetComponent<PlayerManager>();
        }

        protected override void Start()
        {
            base.Start();
            DontDestroyOnLoad(gameObject);
        }


    }
    
}