using UnityEngine;

namespace Items
{
    public class Item : ScriptableObject
    {
        [Header("Item Information")] 
        public string ItemName;
        [TextArea] public string ItemDescription;
        public Sprite ItemIcon;
        public int ItemId;
        


    }
}