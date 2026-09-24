using System;
using System.Collections.Generic;
using UnityEngine;

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
        
        public List<int> activatedCrystals;

        
    }
}
