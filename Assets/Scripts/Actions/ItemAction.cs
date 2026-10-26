using Character;
using Character.Player;
using Items;
using UnityEngine;

namespace Actions
{
    [CreateAssetMenu(menuName = "Aphelion/Actions/Item Actions/Abstract Action - DO NOT USE")]
    public class ItemAction : ScriptableObject
    {
        public int actionId;

        public virtual void AttemptToPerformAction(CharacterManager playerWielding, Item itemBeingUsed)
        {
                        
        }

    }
}
