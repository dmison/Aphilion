using System;
using System.Collections.Generic;

namespace GameSaving
{
    [Serializable] 
    public class CharacterSaveData
    {
        public float secondsPlayed;
        public int spawnCrystalId;
        
        public int strength;
        public int dexterity;
        public int constitution;
        public int endurance;
        
        public List<int> activatedCrystals = new List<int>();

        public int[] rightHandQuickSlots = new int[3];
        public int[] leftHandQuickSlots = new int[3];
        public int lhQuickSlotIndex;
        public int rhQuickSlotIndex;
        
        public void SetDefaultValuesForDemo()
        {
            secondsPlayed = 0;
            spawnCrystalId = 666;
            strength = 12;
            dexterity = 12;
            constitution = 12;
            endurance = 12;
            activatedCrystals.Clear();
            // activatedCrystals.Add();
            
            rightHandQuickSlots[0] = 2;
            rightHandQuickSlots[1] = 1;
            rightHandQuickSlots[2] = 1;
            
            leftHandQuickSlots[0] = 1;
            leftHandQuickSlots[1] = 1;
            leftHandQuickSlots[2] = 1;
            
            rhQuickSlotIndex = 0;
            lhQuickSlotIndex = 0;
        }   
        
    }
}
