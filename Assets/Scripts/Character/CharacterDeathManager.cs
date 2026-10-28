using System;
using System.Collections;
using System.Collections.Generic;
using Colliders;
using UnityEngine;

namespace Character
{
    public class CharacterDeathManager: MonoBehaviour
    {
        protected CharacterAnimationManager _characterAnimationManager;
        protected CharacterSoundEffectsManager _characterSoundEffectsManager;
        protected CharacterManager _characterManager;
        
        protected virtual void Awake()
        {
            _characterManager = GetComponent<CharacterManager>();
            _characterAnimationManager = GetComponent<CharacterAnimationManager>();
            _characterSoundEffectsManager = GetComponent<CharacterSoundEffectsManager>();
            
        }

        public virtual IEnumerator Drown(DrowningCollider source)
        {
            yield return null;
        }   
        
        // public virtual IEnumerator Death()
        // {
        //     
        // }
        
        
    }
}