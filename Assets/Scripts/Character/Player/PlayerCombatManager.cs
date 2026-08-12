using Items;
using UnityEngine.InputSystem;
using WeaponActions;

namespace Character.Player
{
    public class PlayerCombatManager: CharacterCombatManager
    {
        private PlayerManager _player;
        public WeaponItem currentWeaponBeingUsed;
        private InputAction _rbAction;

        protected override void Awake()
        {
            base.Awake();
            _player = GetComponent<PlayerManager>();
            
            _rbAction = InputSystem.actions.FindAction("RB_Action");
            _rbAction.performed += _ => { RB_Action_Performed(); };
        }

        private void RB_Action_Performed()
        {
            WeaponItem weaponItem = _player.playerInventoryManager.currentRightHandWeapon;
            PerformWeaponItemBasedAction(weaponItem.OH_RB_Action, weaponItem, _player, true);
        }

        private void PerformWeaponItemBasedAction(WeaponItemAction action, WeaponItem weaponItem, PlayerManager player, bool rightHand )
        {
            action.AttemptToPerformAction(player, weaponItem, rightHand);
        }
    }
}