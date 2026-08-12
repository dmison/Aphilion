using Character.Player;
using Items;
using UnityEngine;

namespace WeaponActions
{
    
    [CreateAssetMenu(menuName = "Aphelion/Weapon Item Actions/Light Attack")]
    public class LightAttackWeaponItemAction : WeaponItemAction
    {
        [SerializeField] private string _animationName = "LightAttack_Right_01";
        public override void AttemptToPerformAction(PlayerManager playerWielding, WeaponItem weaponBeingWielded, bool rightHand)
        {
            base.AttemptToPerformAction(playerWielding, weaponBeingWielded, rightHand);
            // === STOPS ===
            if (playerWielding.isPerformingAction || !playerWielding.isGrounded || playerWielding.isDead) return;
            if(playerWielding.playerStatsManager.CurrentStamina <= 0) return;
            // =============
            PerformLightAttack(playerWielding, weaponBeingWielded, rightHand);
        }

        private void PerformLightAttack(PlayerManager player, WeaponItem weapon, bool rightHand)
        {
            player.playerAnimationManager.PlayTargetAttackActionAnimation(_animationName);
        }
    }
}
