using System.Collections;
using Colliders;
using UI;
using UnityEngine;

namespace Character.Player
{
    public class PlayerDeathManager: CharacterDeathManager
    {
        private PlayerManager _playerManager;
        public bool drowning = false;
        
        protected override void Awake()
        {
            base.Awake();
            _playerManager = GetComponent<PlayerManager>();
        }

        public override IEnumerator Drown(DrowningCollider source)
        {
            if (drowning) yield break;
            // swim_drowning
            if (!_playerManager.IsActive) yield break;
            drowning = true;
            InputSystemManager.Instance.TogglePlayerControls(false);
            // InputSystemManager.Instance.ToggleMenuInputs(true);

            _characterSoundEffectsManager.PlayDrown();
            _characterAnimationManager.PlayTargetActionAnimation("swim_drowning");

            yield return new WaitForSeconds(8);
            PlayerUIManager.Instance.deathMessageManager.ShowDeathMessage();
            source.Reset();
        }
    }
}