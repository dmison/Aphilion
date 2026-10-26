using Character;
using Effects;
using Items;
using UnityEngine;

namespace Actions
{
    [CreateAssetMenu(menuName = "Aphelion/Actions/Item Actions/Consumable Item Action")]
    public class ConsumableItemAction: ItemAction
    {
        [SerializeField] public InstantCharacterEffect _instantEffect; 
        
        public virtual bool AttemptToPerformAction(CharacterManager playerWielding, ConsumableItem itemBeingUsed)
        {
            // === STOPS ===
            if (playerWielding.isPerformingAction || !playerWielding.isGrounded) return false;
            if (!(itemBeingUsed.currentCharges > 0)) return false;
            
            // =============
            
            return playerWielding.characterEffectsManager.ProcessInstantEffect(_instantEffect);
            
        }
    }
}