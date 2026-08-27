using System.Collections;
using UI;
using UnityEngine;

namespace Character.Player
{
    public class PlayerManager: CharacterManager
    {
        [HideInInspector] public PlayerMovementManager playerMovementManager;
        [HideInInspector] public PlayerEquipmentManager playerEquipmentManager;
        [HideInInspector] public PlayerInventoryManager playerInventoryManager;
        
        
        protected override void Awake()
        {
            
            playerMovementManager = GetComponent<PlayerMovementManager>();
            playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();

            
            characterController = GetComponent<CharacterController>();
            
            characterEffectsManager = GetComponent<PlayerEffectsManager>();
            characterAnimationManager = GetComponent<PlayerAnimationManager>();
            characterStatsManager = GetComponent<PlayerStatsManager>();
            characterCombatManager = GetComponent<PlayerCombatManager>();
            characterSoundEffectsManager =  GetComponent<PlayerSoundEffectsManager>();
            characterCombatManager = GetComponent<PlayerCombatManager>();

            animator = GetComponent<Animator>();
            
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            
            PlayerCameraManager.instance.playerManager = this;
            characterStatsManager.CurrentHealth = 50;
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