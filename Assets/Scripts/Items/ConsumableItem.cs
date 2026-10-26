using Actions;
using Character;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu (fileName = "ConsumableItem", menuName = "Aphelion/Items/Consumable Item")]
    public class ConsumableItem : Item
    {
        [SerializeField] public int MaxCharges; 
        public int currentCharges = 3;
        [SerializeField] private ConsumableItemAction _itemAction;
        [SerializeField] public bool rechargesAtRest = false;

        public void UseItem(CharacterManager characterUsing)
        {
            Debug.Log("useItem");
            bool success = _itemAction.AttemptToPerformAction(characterUsing, this);
            if(success) currentCharges--;
        }
        public void RecoverAtRest()
        {
            if(rechargesAtRest) currentCharges = MaxCharges;
        }
         

    }
}