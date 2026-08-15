using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.InputSystem;
using WeaponActions;

namespace Character.Player
{
    public class PlayerCombatManager: CharacterCombatManager
    {
        private PlayerManager _player;
        // public WeaponItem currentWeaponBeingUsed;
        // public AttackTypes currentAttackType;
        public bool currentlyUsingRightHand;
        private InputAction _rbAction;
        private InputAction _lockOnClick;

        [SerializeField] private float lockOnRadius = 20;
        [SerializeField] private float lockOnViewAngle = 100;
        
        public bool lockedOn;
        public CharacterManager currentLockTarget;
        private List<CharacterManager> _validLockOnTargets = new List<CharacterManager>();
        
        protected override void Awake()
        {
            base.Awake();
            _player = GetComponent<PlayerManager>();
            
            _rbAction = InputSystem.actions.FindAction("RB_Action");
            _rbAction.performed += _ => { RB_Action_Performed(); };
            
            _lockOnClick = InputSystem.actions.FindAction("LockOn");
            _lockOnClick.performed += _ => { LockOnActionPerformed(); };

        }

        protected override void Update()
        {
            if (currentLockTarget)
            {
                if (currentLockTarget.isDead)
                {
                    ReleaseLockOn();
                }
                PlayerCameraManager.instance.CurrentLockOnTarget = currentLockTarget;
            }
            else
            {
                PlayerCameraManager.instance.CurrentLockOnTarget = null;
            } 
            
            
        }

        private void ReleaseLockOn()
        {
            lockedOn = false;
            currentLockTarget = null;
            PlayerCameraManager.instance.CurrentLockOnTarget = null;
        }
        
        private void LockOnActionPerformed()
        {
            // if already locked on then release lock
            if (lockedOn)
            {
                ReleaseLockOn();
                return;
            }
            
            // else attempt lock on
            AttemptLockOn();
            
        }

        private void AttemptLockOn()
        {
            _validLockOnTargets.Clear();
            float shortestDistance = Mathf.Infinity;

            Collider[] colliders = Physics.OverlapSphere(transform.position, lockOnRadius, WorldUtilityManager.Instance.GetCharacterLayerMask());
            
            for (int i=0;i<colliders.Length;i++)
            {
                CharacterManager cm  = colliders[i].GetComponent<CharacterManager>();
                
                if (cm == null) continue;                                         // null for some reason
                if (cm.isDead) continue;                                   // target is dead
                if (cm.transform.root == _player.transform.root) continue; // target is player
                
                Vector3 targetDirection = cm.transform.position - transform.position;
                float targetDistance = Vector3.Distance(_player.transform.position, cm.transform.position);
                
                float targetViewAngle = Vector3.Angle(targetDirection, PlayerCameraManager.instance.playerCamera.transform.forward);
                
                // distance check?  no need as it will need to be in the radius
                
                if ((targetViewAngle > -(lockOnViewAngle / 2)) && targetViewAngle < lockOnViewAngle / 2)
                {
                    RaycastHit hit;
                    
                    if (Physics.Linecast(lockOnTransform.position, cm.characterCombatManager.lockOnTransform.position,
                            out hit, WorldUtilityManager.Instance.GetEnvironmentLayerMask()))
                    {
                        // something blocking LOS
                        continue;
                    }
                    else
                    {
                        _validLockOnTargets.Add(cm);
                        if (!(targetDistance < shortestDistance)) continue;
                        shortestDistance = targetDistance;
                        currentLockTarget = cm;
                    }
                }
                // else
                // {
                //     // NOT IN VIEW ANGLE
                //     Debug.Log("not in view angle");
                // }
                
            }

            lockedOn = currentLockTarget != null;
            
        }
        
        private void RB_Action_Performed()
        {
            WeaponItem weaponItem = _player.playerInventoryManager.currentRightHandWeapon;
            currentWeaponBeingUsed = weaponItem;
            currentAttackType = AttackTypes.LightAttack01;
            currentlyUsingRightHand = true;
            PerformWeaponItemBasedAction(AttackTypes.LightAttack01, weaponItem.OH_RB_Action, weaponItem, _player, currentlyUsingRightHand);
        }

        private void PerformWeaponItemBasedAction(AttackTypes attackType, WeaponItemAction action, WeaponItem weaponItem, PlayerManager player, bool rightHand )
        {
            action.AttemptToPerformAction(player, weaponItem, rightHand, attackType);
            
        }

        // ANIMATION EVENT
        public void ReduceStaminaBasedOnAttack()
        {
            float staminaCost = currentWeaponBeingUsed.baseStaminaCost;
            staminaCost *= currentAttackType switch
            {
                AttackTypes.LightAttack01 => currentWeaponBeingUsed.lightAttackStaminaModifier,
                AttackTypes.LightAttack02 or AttackTypes.LightAttack03 => 1,
                _ => 1
            };
            _player.playerStatsManager.CurrentStamina -= staminaCost;
        }
    }
}