using Character.Player;
using Items;
using UnityEngine;

namespace WeaponActions
{
    [CreateAssetMenu(menuName = "Aphelion/Weapon Item Action/Test Action")]
    public class WeaponItemAction : ScriptableObject
    {
        public int actionId;

        public virtual void AttemptToPerformAction(PlayerManager playerWielding, WeaponItem weaponBeingWielded, bool rightHand, AttackTypes attackType)
        {
            
        }

    }
}
