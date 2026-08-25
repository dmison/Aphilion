using System.Collections;
using UI;
using UnityEngine;

namespace Character.Player
{
    public class PlayerManager: CharacterManager
    {
        [HideInInspector] public PlayerMovementManager playerMovementManager;
        [HideInInspector] public PlayerAnimationManager playerAnimationManager;
        [HideInInspector] public PlayerStatsManager playerStatsManager;
        [HideInInspector] public PlayerEffectsManager playerEffectsManager;
        [HideInInspector] public PlayerEquipmentManager playerEquipmentManager;
        [HideInInspector] public PlayerInventoryManager playerInventoryManager;
        [HideInInspector] public PlayerCombatManager playerCombatManager;
        [HideInInspector] public PlayerSoundEffectsManager playerSoundEffectsManager;
        
        public bool IsDead => playerStatsManager.CurrentHealth <= 0;
        
        protected override void Awake()
        {
            
            characterController = GetComponent<CharacterController>();
            playerMovementManager = GetComponent<PlayerMovementManager>();
            playerAnimationManager = GetComponent<PlayerAnimationManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerEffectsManager = GetComponent<PlayerEffectsManager>();
            playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            playerCombatManager = GetComponent<PlayerCombatManager>();
            playerSoundEffectsManager =  GetComponent<PlayerSoundEffectsManager>();
            animator = GetComponent<Animator>();
            
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            
            PlayerCameraManager.instance.playerManager = this;
            playerStatsManager.CurrentHealth = 50;
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            PlayerCameraManager.instance.HandleCameraActions();
        }
        public override IEnumerator HandleDeath(bool manuallySelectAnimation = false)
        {
            characterAnimationManager.PlayTargetActionAnimation("Dead_01");
            yield return new WaitForSeconds(2);
            PlayerUIManager.Instance.deathMessageManager.ShowDeathMessage();
        }
    }
}