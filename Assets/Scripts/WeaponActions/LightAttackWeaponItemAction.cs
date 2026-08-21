using Character.Player;
using Items;
using UnityEngine;

namespace WeaponActions
{
    
    [CreateAssetMenu(menuName = "Aphelion/Weapon Item Actions/Light Attack")]
    public class LightAttackWeaponItemAction : WeaponItemAction
    {
        [SerializeField] private string _animationName = "LightAttack_Right_01";
        public override void AttemptToPerformAction(PlayerManager playerWielding, WeaponItem weaponBeingWielded, bool rightHand, AttackTypes attackType)
        {
            base.AttemptToPerformAction(playerWielding, weaponBeingWielded, rightHand, attackType);
            // === STOPS ===
            if (playerWielding.isPerformingAction || !playerWielding.isGrounded || playerWielding.IsDead) return;
            if(playerWielding.playerStatsManager.CurrentStamina <= 0) return;
            // =============
            PerformLightAttack(playerWielding, weaponBeingWielded, rightHand, attackType);
        }

        private void PerformLightAttack(PlayerManager player, WeaponItem weapon, bool rightHand, AttackTypes attackType)
        {
            
            player.playerAnimationManager.PlayTargetAttackActionAnimation(attackType, _animationName);
        }
    }
}
