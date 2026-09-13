
using Items;
using UnityEngine;

namespace Character.AICharacters
{
    public class AIEquipmentManager: CharacterEquipmentManager
    {
        private AICharacterManager _aiCharacter;
        
        public WeaponModelInstanceSlot rightHandSlot;
        public WeaponModelInstanceSlot leftHandSlot;
        public GameObject rightHandWeaponModel;
        public GameObject leftHandWeaponModel;
        
        private WeaponManager _leftHandManager;
        private WeaponManager _rightHandManager;

        [SerializeField] WeaponItem rightHandWeaponItem;
        
        protected override void Start()
        {
            base.Start();
            LoadAllWeaponSlots();
        }
        
        protected override void Awake()
        {
            base.Awake();
            _aiCharacter = GetComponent<AICharacterManager>();
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
            // LoadLeftHandWeapon();
            LoadRightHandWeapon();
        }

        public void LoadRightHandWeapon()
        {
            // if (playerManager.playerInventoryManager.currentRightHandWeapon != null)
            // {
            rightHandSlot.UnLoadWeaponModel();
            rightHandWeaponModel = Instantiate(rightHandWeaponItem.weaponModel);
            rightHandSlot.LoadWeaponModel(rightHandWeaponModel);
            _rightHandManager = rightHandWeaponModel.GetComponent<WeaponManager>();
            _rightHandManager.SetWeaponDamage(rightHandWeaponItem, _aiCharacter);
        }
        
        
        public void OpenDamageColliders()
        {
            _rightHandManager.damageCollider.EnableDamageCollider();
            _rightHandManager.ToggleWeaponTrail(true);
            // play whoosh SFX
            _aiCharacter.characterSoundEffectsManager.PlaySoundFX(
                WorldSoundFxManager.instance.ChooseRandomClip(
                    rightHandWeaponItem.whooshSounds
                )
            );
        }

        public void CloseDamageColliders()
        {
            _rightHandManager.damageCollider.DisableDamageCollider();
            _rightHandManager.ToggleWeaponTrail(false);
        }
    }

    
    
    
}