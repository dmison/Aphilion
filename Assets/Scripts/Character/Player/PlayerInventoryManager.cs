using System.Linq;
using Items;
using UnityEngine;
using World;

namespace Character.Player
{
    public class PlayerInventoryManager : CharacterInventoryManager
    {
        private PlayerManager _player;
        
        public WeaponItem[] rightHandQuickSlots = new WeaponItem[3];
        public WeaponItem[] leftHandQuickSlots = new WeaponItem[3];
        
        public int lhQuickSlotIndex;
        public int rhQuickSlotIndex;
        
        public WeaponItem currentRightHandWeapon => rightHandQuickSlots[rhQuickSlotIndex];
        public WeaponItem currentLeftHandWeapon => leftHandQuickSlots[lhQuickSlotIndex];

        
        protected override void Awake()
        {
            _player = GetComponent<PlayerManager>();
        }

        private bool WeaponIsUnarmed(WeaponItem weapon)
        {
            return weapon.ItemId == WorldItemDatabaseManager.Instance.unarmedWeapon.ItemId;
        }

        private bool moreThanOneUnarmed(WeaponItem[] slots)
        {
            int count = slots.Count(WeaponIsUnarmed); 
            return  count > 1;
        }

        private int SelectNextFromSlot(WeaponItem[] slots, int currentIndex, bool armament = false)
        {
            int index = currentIndex;
            for (int i = 0; i < 3; i++)
            {
                index++;
                if (index > 2) index = 0;
                if (armament)
                {
                    if (slots[index].ItemId != WorldItemDatabaseManager.Instance.unarmedWeapon.ItemId)
                    {
                        break;
                    }
                }
                else
                {
                    if (slots[index].ItemId == WorldItemDatabaseManager.Instance.unarmedWeapon.ItemId)
                    {
                        break;
                    }
                }
                
            }

            return index;
        }

        private bool AllSlotsUnarmed(WeaponItem[] slots)
        {
            return slots.Count(WeaponIsUnarmed) == 3;
        }

        private int NextSlotIndex(WeaponItem[] slots, int currentIndex)
        {
            int nextIndex = currentIndex;
            
            // if all slots are unarmed, then no point doing anything, just return current slot
            if(AllSlotsUnarmed(slots)) return nextIndex;
            
            // if there is more than one unarmed slot & we are currently with weapon
            // then get the next unarmed slot
            if (moreThanOneUnarmed(slots) && !WeaponIsUnarmed(WorldItemDatabaseManager.Instance.GetWeaponById(slots[currentIndex].ItemId)))
            {
                nextIndex = SelectNextFromSlot(slots, currentIndex, false);
            }
            else
            {
                // otherwise just get next item that is a WEAPON
                nextIndex = SelectNextFromSlot(slots, currentIndex, true);
            }
            
            return nextIndex;
        }

        public void SwapToNextRhQS()
        {
            rhQuickSlotIndex = NextSlotIndex(rightHandQuickSlots, rhQuickSlotIndex);
            
        }

        public void SwapToNextLhQS()
        {
            lhQuickSlotIndex = NextSlotIndex(leftHandQuickSlots, lhQuickSlotIndex);   
        }
        
    }
}
