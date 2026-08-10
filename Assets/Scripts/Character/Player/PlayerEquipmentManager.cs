using Items;
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
        
        private WeaponManager leftHandManager;
        private WeaponManager rightHandManager;

        private InputAction _changeItemRight;
        private InputAction _changeItemLeft;
        
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
            _changeItemRight.performed += ctx =>
            {
                 SwitchRightHandWeapon();
            };
            
            _changeItemLeft = InputSystem.actions.FindAction("SwapLeft");
            _changeItemLeft.performed += ctx =>
            {
                SwitchLeftHandWeapon();
            };
            
            InitializeWeaponSlots();
            
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
        }

        public void LoadLeftHandWeapon()
        {
            if (playerManager.playerInventoryManager.currentLeftHandWeapon != null)
            {
                leftHandSlot.UnLoadWeaponModel();
                leftHandWeaponModel =
                    Instantiate(playerManager.playerInventoryManager.currentLeftHandWeapon.weaponModel);
                leftHandSlot.LoadWeaponModel(leftHandWeaponModel);
                leftHandManager = leftHandWeaponModel.GetComponent<WeaponManager>();
                leftHandManager.SetWeaponDamage(playerManager.playerInventoryManager.currentLeftHandWeapon, playerManager);
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
                rightHandManager = rightHandWeaponModel.GetComponent<WeaponManager>();
                rightHandManager.SetWeaponDamage(playerManager.playerInventoryManager.currentRightHandWeapon, playerManager);
            }
        }

        private void SwitchRightHandWeapon()
        {
            if (!playerManager.isPerformingAction)
            {
                playerManager.playerInventoryManager.SwapToNextRhQS();
                LoadRightHandWeapon();
            }
        }

        private void SwitchLeftHandWeapon()
        {
            if (!playerManager.isPerformingAction)
            {
                playerManager.playerInventoryManager.SwapToNextLhQS();
                LoadLeftHandWeapon();
            }
        }
    }
}

