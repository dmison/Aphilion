using System.Data;
using Character.Player;
using UnityEngine;
using World;

namespace Interactable
{
    public class PlayerStart : SpawnCrystalManager
    {
        
        private void Start()
        {
            crystalId = 666;
            crystalName = "PlayerStart";
            WorldCrystalSpawnerManager.Instance.ReportIn(this);
        }
        
        private void OnDestroy()
        {
            WorldCrystalSpawnerManager.Instance.RemoveMe(this);
        }
        
    }
}
