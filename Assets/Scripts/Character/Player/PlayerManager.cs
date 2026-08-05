using System.Collections;
using UnityEngine;

namespace Character.Player
{
    public class PlayerManager: CharacterManager
    {
        [HideInInspector] public PlayerMovementManager playerMovementManager;
        [HideInInspector] public PlayerAnimationManager playerAnimationManager;
        [HideInInspector] public PlayerStatsManager playerStatsManager;
        [HideInInspector] public PlayerEffectsManager playerEffectsManager;
        
        protected override void Awake()
        {
            base.Awake();
            playerMovementManager = GetComponent<PlayerMovementManager>();
            playerAnimationManager = GetComponent<PlayerAnimationManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerEffectsManager = GetComponent<PlayerEffectsManager>();
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
        // public override IEnumerator HandleDeath(bool manuallySelectAnimation = false)
        // {
        //     base.HandleDeath(manuallySelectAnimation);
        //     
        // }
    }
}