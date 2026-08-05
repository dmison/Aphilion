using UnityEngine;

namespace Character
{
    public class CharacterStatsManager : MonoBehaviour
    {   
        [HideInInspector] public CharacterManager character;
        [SerializeField] protected float staminaRegenPerSecond = 30;
        [SerializeField] protected  float staminaRegenDelay = 0.5f;
        protected float StaminaRegenTimer = 0;
        
        
        [SerializeField] protected int endurance = 10;
        public int Endurance
        {
            get => endurance;
            set { endurance = value; UpdateUI();}
        }
        
        [SerializeField] protected int constitution = 10;
        public int Constitution
        {
            get => constitution;
            set { constitution = value; UpdateUI();}
        }
        
        [SerializeField] protected int strength = 10;
        public int Strength
        {
            get => strength;
            set { strength = value; UpdateUI();}
        }
        
        [SerializeField] protected int dexterity = 10;
        public int Dexterity
        {
            get => dexterity;
            set { dexterity = value; UpdateUI();}
        }

        // STAMINA
        public float MaxStamina => endurance * 10;
        protected float currentStamina;
        public float CurrentStamina
        {
            get => currentStamina;
            set {  currentStamina = value;  UpdateUI();}
        }
        // HEALTH
        
        public int MaxHealth => constitution * 12;
        protected int currentHealth;
        public int CurrentHealth
        {
            get => currentHealth;
            set {  currentHealth = Mathf.Clamp(value, 0, MaxHealth);  UpdateUI();}
        }

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {
            CurrentHealth = MaxHealth;
            CurrentStamina = MaxStamina;
        }

        protected virtual void RegenStamina()
        {
            
        }

        protected void Update()
        {
            RegenStamina();
        }

        protected virtual void UpdateUI()
        {
            CheckIfDead();
            
        }

        protected virtual void CheckIfDead()
        {
            if (CurrentHealth <= 0)
            {
                StartCoroutine(character.HandleDeath());
            }
        }
    }
}