using System;
using System.Collections;
using Items;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    public class PlayerEquipmentManager : CharacterEquipmentManager
    {
        private PlayerManager playerManager;
        public WeaponModelInstanceSlot rightHandSlot;
        public WeaponModelInstanceSlot leftHandSlot;
        public GameObject rightHandWeaponModel;
        public GameObject leftHandWeaponModel;
        
        private WeaponManager _leftHandWeaponManager;
        private WeaponManager _rightHandWeaponManager;

        private InputAction _changeItemRight;
        private InputAction _changeItemLeft;

        private InputAction _useTopItemAction;
        private InputAction _useBottomItemAction;

        private ConsumableItem topSlot;

        
        
        protected override void Start()
        {
            base.Start();
            LoadAllWeaponSlots();
            
        }

        protected override void Awake()
        {
            base.Awake();
            playerManager = GetComponent<PlayerManager>();
            
            _changeItemRight = InputSystem.actions.FindAction("SwapRight");
            _changeItemRight.performed += SwitchRightHandWeapon;
            
            _changeItemLeft = InputSystem.actions.FindAction("SwapLeft");
            _changeItemLeft.performed += SwitchLeftHandWeapon;
            
            _useTopItemAction = InputSystem.actions.FindAction("EquipmentManager/ActionUp");
            _useTopItemAction.performed += UseTopItem;
            
            InitializeWeaponSlots();
            // StartCoroutine(WaitToUpdate());
            
        }

        // private IEnumerator WaitToUpdate()
        // {
        //     while (PlayerUIManager.Instance == null && topSlot == null)
        //     {
        //         yield return null;
        //     }
        //
        //     Debug.Log(PlayerUIManager.Instance);
        //     UpdateUI();
        // }
        
        private void UpdateUI()
        {
            if (topSlot)
            {
                PlayerUIManager.Instance.topSlot.label.text = topSlot.currentCharges.ToString();
                PlayerUIManager.Instance.topSlot.icon.sprite = topSlot.ItemIcon;
                
            }
        }
        
        private void OnDestroy()
        {
            _changeItemRight.performed -= SwitchRightHandWeapon;
            _changeItemLeft.performed -= SwitchLeftHandWeapon;
            _useTopItemAction.performed -= UseTopItem;
        }

        private void UseTopItem(InputAction.CallbackContext context)
        {
            Debug.Log("using top item");
            Debug.Log(playerManager.playerInventoryManager.upperItemSlot);
            if (topSlot)
            {
                topSlot.UseItem(playerManager);
                UpdateUI();
                
            }
        }
        
        public void RestoreItemsForRest()
        {
            if (topSlot)
            {
                topSlot.RecoverAtRest();
                UpdateUI();
            }   
        }
        
        private void InitializeWeaponSlots()
        {
            WeaponModelInstanceSlot[] weaponSlots = GetComponentsInChildren<WeaponModelInstanceSlot>();
            foreach (WeaponModelInstanceSlot weaponSlot in weaponSlots)
            {
                switch (weaponSlot.weaponModelSlot)
                {
                    case WeaponModelSlot.RightHand:
                        rightHandSlot = weaponSlot;
                        break;
                    case WeaponModelSlot.LeftHand:
                        leftHandSlot = weaponSlot;
                        break;
                }
            }
        }

        public void LoadAllWeaponSlots()
        {
            LoadLeftHandWeapon();
            LoadRightHandWeapon();
            InitialiseItems();
        }

        public void InitialiseItems()
        {
            // hardcoding for demo
            if (playerManager.playerInventoryManager.upperItemSlot)
            {
                topSlot = Instantiate(playerManager.playerInventoryManager.upperItemSlot);
            }
            UpdateUI();
        }
        
        
        public void LoadLeftHandWeapon()
        {
            if (playerManager.playerInventoryManager.currentLeftHandWeapon != null)
            {
                leftHandSlot.UnLoadWeaponModel();
                leftHandWeaponModel =
                    Instantiate(playerManager.playerInventoryManager.currentLeftHandWeapon.weaponModel);
                leftHandSlot.LoadWeaponModel(leftHandWeaponModel);
                _leftHandWeaponManager = leftHandWeaponModel.GetComponent<WeaponManager>();
                _leftHandWeaponManager.SetWeaponDamage(playerManager.playerInventoryManager.currentLeftHandWeapon, playerManager);
            }
        }

        public void LoadRightHandWeapon()
        {
            if (playerManager.playerInventoryManager.currentRightHandWeapon != null)
            {
                rightHandSlot.UnLoadWeaponModel();
                rightHandWeaponModel =
                    Instantiate(playerManager.playerInventoryManager.currentRightHandWeapon.weaponModel);
                rightHandSlot.LoadWeaponModel(rightHandWeaponModel);
                _rightHandWeaponManager = rightHandWeaponModel.GetComponent<WeaponManager>();
                _rightHandWeaponManager.SetWeaponDamage(playerManager.playerInventoryManager.currentRightHandWeapon, playerManager);
            }
        }

        private void SwitchRightHandWeapon(InputAction.CallbackContext context)
        {
            if (!playerManager.isPerformingAction)
            {
                playerManager.playerInventoryManager.SwapToNextRhQS();
                LoadRightHandWeapon();
            }
        }

        private void SwitchLeftHandWeapon(InputAction.CallbackContext context)
        {
            if (!playerManager.isPerformingAction)
            {
                playerManager.playerInventoryManager.SwapToNextLhQS();
                LoadLeftHandWeapon();
            }
        }

        public void OpenDamageColliders()
        {
            
            if (playerManager.characterCombatManager.currentlyUsingRightHand)
            {
                _rightHandWeaponManager.damageCollider.EnableDamageCollider();
                _rightHandWeaponManager.ToggleWeaponTrail(true);
                playerManager.characterSoundEffectsManager.PlaySoundFX(
                    WorldSoundFxManager.instance.ChooseRandomClip(
                        playerManager.playerInventoryManager.currentRightHandWeapon.whooshSounds
                        )
                    );
            }
            else
            {
                _leftHandWeaponManager.damageCollider.EnableDamageCollider();
                _leftHandWeaponManager.ToggleWeaponTrail(true);
                playerManager.characterSoundEffectsManager.PlaySoundFX(
                    WorldSoundFxManager.instance.ChooseRandomClip(
                        playerManager.playerInventoryManager.currentLeftHandWeapon.whooshSounds
                    )
                );
            }
            
        }

        public void CloseDamageColliders()
        {
            
            if (playerManager.characterCombatManager.currentlyUsingRightHand)
            {
                _rightHandWeaponManager.damageCollider.DisableDamageCollider();
                _rightHandWeaponManager.ToggleWeaponTrail(false);
            }
            else
            {
                _leftHandWeaponManager.damageCollider.DisableDamageCollider();
                _leftHandWeaponManager.ToggleWeaponTrail(false);
            }
        }
    }
}

