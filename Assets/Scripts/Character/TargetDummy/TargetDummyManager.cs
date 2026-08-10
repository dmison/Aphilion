using System.Collections;
using UnityEngine;

namespace Character.TargetDummy
{
    public class TargetDummyManager : CharacterManager
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected override void Awake()
        {
            // characterController = GetComponent<CharacterController>();
            characterStatsManager = GetComponent<CharacterStatsManager>();
            characterAnimationManager = GetComponent<CharacterAnimationManager>();
            characterEffectsManager = GetComponent<CharacterEffectsManager>();
            animator = GetComponent<Animator>();
            DontDestroyOnLoad(this);
        }

        public override IEnumerator HandleDeath(bool manuallySelectAnimation = false)
        {
            characterStatsManager.CurrentHealth = characterStatsManager.MaxHealth;
            return null;
        }
    }
}
