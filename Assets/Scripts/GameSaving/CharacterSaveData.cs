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

        public void SetDefaultValuesForDemo()
        {
            secondsPlayed = 0;
            spawnCrystalId = 1;
            strength = 12;
            dexterity = 12;
            constitution = 12;
            endurance = 12;
            activatedCrystals.Clear();
            activatedCrystals.Add(1);
        }   
        
    }
}
