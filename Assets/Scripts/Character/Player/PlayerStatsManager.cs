using System;
using UI.HUD;
using UnityEngine;

namespace Character.Player
{
    public class PlayerStatsManager : CharacterStatsManager
    {
        private PlayerManager _playerManager;
        
        [SerializeField] private float staminaRegenPerSecond = 30;
        [SerializeField] private float staminaRegenDelay = 0.5f;
        private float staminaRegenTimer = 0;
        
        
        [SerializeField] private int endurance = 10;
        public int Endurance
        {
            get => endurance;
            set { endurance = value; UpdateUI();}
        }
        
        [SerializeField] private int constitution = 10;
        public int Constitution
        {
            get => constitution;
            set { constitution = value; UpdateUI();}
        }
        
        [SerializeField] private int strength = 10;
        public int Strength
        {
            get => strength;
            set { strength = value; UpdateUI();}
        }
        
        [SerializeField] private int dexterity = 10;
        public int Dexterity
        {
            get => dexterity;
            set { dexterity = value; UpdateUI();}
        }

        public float MaxStamina => endurance * 10;
        private float currentStamina;
        public float CurrentStamina
        {
            get => currentStamina;
            set {  currentStamina = value;  UpdateUI();}
        }
        
        public int MaxHealth => constitution * 12;
        private int currentHealth;
        public int CurrentHealth
        {
            get => currentHealth;
            set {  currentHealth = value; UpdateUI();}
        }

        private void Awake()
        {
            _playerManager = GetComponent<PlayerManager>();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            CurrentHealth = MaxHealth;
            CurrentStamina = MaxStamina;
        }

        private void RegenStamina()
        {
            if ( _playerManager.playerMovementManager.IsSprinting || 
                 _playerManager.isPerformingAction || 
                !(CurrentStamina < MaxStamina))
            {
                staminaRegenTimer = 0;
                return;
            }
            
            if (staminaRegenTimer < staminaRegenDelay)
            {
                staminaRegenTimer += Time.deltaTime;
                return;
            }

            CurrentStamina += (staminaRegenPerSecond *  Time.deltaTime);
        }

        private void Update()
        {
            RegenStamina();
        }

        private void UpdateUI()
        {
            PlayerUIManager.instance.statsBar.DoUpdate(currentStamina, MaxStamina, currentHealth, MaxHealth);
        }
    }
}