using System.Collections;
using GameSaving;
using Interactable;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using World;

namespace Character.Player
{
    public class PlayerManager: CharacterManager
    {
        [HideInInspector] public PlayerMovementManager playerMovementManager;
        [HideInInspector] public PlayerEquipmentManager playerEquipmentManager;
        [HideInInspector] public PlayerInventoryManager playerInventoryManager;
        
        public int currentSpawnCrystalId = 1;
        public bool IsActive;
        public static PlayerManager Instance;
        
        protected override void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
            playerMovementManager = GetComponent<PlayerMovementManager>();
            playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            characterController = GetComponent<CharacterController>();
            characterEffectsManager = GetComponent<PlayerEffectsManager>();
            characterAnimationManager = GetComponent<PlayerAnimationManager>();
            characterStatsManager = GetComponent<PlayerStatsManager>();
            characterCombatManager = GetComponent<PlayerCombatManager>();
            characterSoundEffectsManager = GetComponent<PlayerSoundEffectsManager>();            
            characterCombatManager = GetComponent<PlayerCombatManager>();
            animator = GetComponent<Animator>();
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            PlayerCameraManager.instance.playerManager = this;
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            PlayerCameraManager.instance.HandleCameraActions();
        }
        public override IEnumerator HandleDeath(bool manuallySelectAnimation = false)
        {
            if (IsActive)
            {
                InputSystemManager.Instance.TogglePlayerControls(false);
                InputSystemManager.Instance.ToggleMenuInputs(true);
                
                characterSoundEffectsManager.PlayDie();
                characterAnimationManager.PlayTargetActionAnimation("Dead_01");
                
                yield return new WaitForSeconds(2);
                PlayerUIManager.Instance.deathMessageManager.ShowDeathMessage();
            }
        }

        public void PopulateToSaveData(ref CharacterSaveData data)
        {
            data.spawnCrystalId = currentSpawnCrystalId;
            data.constitution = characterStatsManager.Constitution;
            data.endurance = characterStatsManager.Endurance;
            data.strength = characterStatsManager.Strength;
            data.dexterity = characterStatsManager.Dexterity;
            
            data.rightHandQuickSlots[0] = playerInventoryManager.rightHandQuickSlots[0].ItemId;
            data.rightHandQuickSlots[1] = playerInventoryManager.rightHandQuickSlots[1].ItemId;
            data.rightHandQuickSlots[2] = playerInventoryManager.rightHandQuickSlots[2].ItemId;
            
            data.leftHandQuickSlots[0] = playerInventoryManager.leftHandQuickSlots[0].ItemId;
            data.leftHandQuickSlots[1] = playerInventoryManager.leftHandQuickSlots[1].ItemId;
            data.leftHandQuickSlots[2] = playerInventoryManager.leftHandQuickSlots[2].ItemId;
            
            data.lhQuickSlotIndex = playerInventoryManager.lhQuickSlotIndex;
            data.rhQuickSlotIndex = playerInventoryManager.rhQuickSlotIndex;
        }
        
        public void RestoreFromSaveData(CharacterSaveData data)
        {
            currentSpawnCrystalId = data.spawnCrystalId; 
            
            characterStatsManager.Constitution = data.constitution;
            characterStatsManager.Endurance = data.endurance;
            characterStatsManager.Strength = data.strength;
            characterStatsManager.Dexterity = data.dexterity;
            
            playerInventoryManager.rightHandQuickSlots[0] = WorldItemDatabaseManager.Instance.GetWeaponById(data.rightHandQuickSlots[0]);
            playerInventoryManager.rightHandQuickSlots[1] = WorldItemDatabaseManager.Instance.GetWeaponById(data.rightHandQuickSlots[1]);
            playerInventoryManager.rightHandQuickSlots[2] = WorldItemDatabaseManager.Instance.GetWeaponById(data.rightHandQuickSlots[2]);
            
            playerInventoryManager.leftHandQuickSlots[0] = WorldItemDatabaseManager.Instance.GetWeaponById(data.leftHandQuickSlots[0]);
            playerInventoryManager.leftHandQuickSlots[1] = WorldItemDatabaseManager.Instance.GetWeaponById(data.leftHandQuickSlots[1]);
            playerInventoryManager.leftHandQuickSlots[2] = WorldItemDatabaseManager.Instance.GetWeaponById(data.leftHandQuickSlots[2]);
            
            playerInventoryManager.lhQuickSlotIndex = data.lhQuickSlotIndex;
            playerInventoryManager.rhQuickSlotIndex = data.rhQuickSlotIndex;
            
            
        }

        public void PlaceAtSpawnCrystal()
        {
            SpawnCrystalManager crystal = WorldCrystalSpawnerManager.Instance.GetCrystalById(currentSpawnCrystalId);
            transform.position = crystal.spawnPoint.transform.position;
            transform.rotation = crystal.spawnPoint.transform.rotation;
        }
    }
}