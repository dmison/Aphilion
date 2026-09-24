using System.Collections.Generic;
using Items;
using UnityEngine;

namespace World
{
    public class WorldItemDatabaseManager : MonoBehaviour
    {
        public static WorldItemDatabaseManager Instance;
        [SerializeField] public List<WeaponItem> weapons = new List<WeaponItem>();
        [SerializeField] public WeaponItem unarmedWeapon;
        
        private List<Item> items = new List<Item>();
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitLists();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitLists()
        {
            foreach (WeaponItem weapon in weapons )
            {
                items.Add(weapon);
            }

            for (int i =0; i<items.Count; i++)
            {
                items[i].ItemId = i;
            }
        }

        public WeaponItem GetWeaponById(int id)
        {
            return weapons.Find(weapon => weapon.ItemId == id);
        }
        
        // private void Start()
        // {
        //     DontDestroyOnLoad(gameObject);
        // }
    }
}
