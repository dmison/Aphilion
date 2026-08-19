using System.Collections;
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
        private InputAction _seekLockOnLeft;
        private InputAction _seekLockOnRight;
        
        [SerializeField] private float lockOnRadius = 20;
        [SerializeField] private float lockOnViewAngle = 100;
        
        public bool lockedOn;
        private List<CharacterManager> _validLockOnTargets = new List<CharacterManager>();
        public CharacterManager currentLockTarget;
        private CharacterManager closestTarget;
        private CharacterManager closestRightTarget;
        private CharacterManager closestLeftTarget;
        private Coroutine _lockOnCoroutine;
        
        protected override void Awake()
        {
            base.Awake();
            _player = GetComponent<PlayerManager>();
            
            _rbAction = InputSystem.actions.FindAction("RB_Action");
            _rbAction.performed += _ => { RB_Action_Performed(); };
            
            _lockOnClick = InputSystem.actions.FindAction("LockOn");
            _lockOnClick.performed += _ => { LockOnActionPerformed(); };

            _seekLockOnLeft = InputSystem.actions.FindAction("SeekLockOnLeft");
            _seekLockOnLeft.performed += _ => { FindLockTargetToLeft(); };
            
            _seekLockOnRight = InputSystem.actions.FindAction("SeekLockOnRight");
            _seekLockOnRight.performed += _ => { FindLockTargetToRight(); };
            
        }

        private void FindLockTargetToLeft()
        {
            if (!lockedOn) return;
            FindLockOnTargets();
            currentLockTarget = closestLeftTarget;
            PlayerCameraManager.instance.CurrentLockOnTarget = currentLockTarget;
        }

        private void FindLockTargetToRight()
        {
            if(!lockedOn) return;
            FindLockOnTargets();
            currentLockTarget = closestRightTarget;
            PlayerCameraManager.instance.CurrentLockOnTarget = currentLockTarget;
        }
        
        protected override void Update()
        {
            if (currentLockTarget)
            {
                if (currentLockTarget.isDead)
                {
                    if (_lockOnCoroutine != null)
                    {
                        StopCoroutine(_lockOnCoroutine);
                    }
                    _lockOnCoroutine = StartCoroutine(WaitThenFindNewTarget());
                }
                PlayerCameraManager.instance.CurrentLockOnTarget = currentLockTarget;
            }
            else
            {
                PlayerCameraManager.instance.CurrentLockOnTarget = null;
            } 
            
            
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator WaitThenFindNewTarget()
        {
            if (_player.isPerformingAction)
            {
                yield return null;
            }
            ReleaseLockOn();
            FindLockOnTargets();
            yield return null;
        }
        
        private void ReleaseLockOn()
        {
            lockedOn = false;
            currentLockTarget = null;
            PlayerCameraManager.instance.CurrentLockOnTarget = null;
            PlayerCameraManager.instance.SetLockCameraHeight();
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
            FindLockOnTargets();
            currentLockTarget = closestTarget;
            if (currentLockTarget != null)
            {
                PlayerCameraManager.instance.SetLockCameraHeight();
            }
        }

        private void FindLockOnTargets()
        {
            _validLockOnTargets.Clear();

            float shortestDistance = Mathf.Infinity;
            float shortestDistanceRightOfTarget = Mathf.Infinity;
            float shortestDistanceLeftOfTarget = -Mathf.Infinity;

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
                    else  // we can see it
                    {
                        _validLockOnTargets.Add(cm);                         // add it to valid targets list
                        if (targetDistance < shortestDistance)
                        {
                            // skip if distance is not closer than shortestDistances
                            shortestDistance = targetDistance; // set shortest distances to this one
                            closestTarget = cm;
                        } // set current lockon to this one

                        // if we are locked on, then check for right/left targets
                        // skip if already locked onto this target
                        if (lockedOn && (cm != currentLockTarget))
                        {
                            // get local position of target being checked relative to player
                            Vector3 relativeEnemyPosition = _player.transform.InverseTransformPoint(cm.transform.position);

                            // closest left
                            if (relativeEnemyPosition.x <= 0 && relativeEnemyPosition.x > shortestDistanceLeftOfTarget)
                            {
                                shortestDistanceLeftOfTarget = relativeEnemyPosition.x;
                                closestLeftTarget = cm;
                            }

                            // closest right
                            if (relativeEnemyPosition.x >= 0 && relativeEnemyPosition.x < shortestDistanceRightOfTarget)
                            {
                                shortestDistanceRightOfTarget = relativeEnemyPosition.x;
                                closestRightTarget = cm;
                            }
                        }
                    }
                }
                
                
            }

            // if (lockedOn)
            // {
            //     //
            // }
            //
            if (!lockedOn && closestTarget != null)
            {
                currentLockTarget = closestTarget;
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