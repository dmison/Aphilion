using System;
using UnityEngine;

namespace Character
{
    public class CharacterEquipmentManager: MonoBehaviour
    {
        protected CharacterManager _characterManager;

        protected virtual void Awake()
        {
            _characterManager = GetComponent<CharacterManager>();
        }
        
        protected virtual void Start(){ }
    }


}