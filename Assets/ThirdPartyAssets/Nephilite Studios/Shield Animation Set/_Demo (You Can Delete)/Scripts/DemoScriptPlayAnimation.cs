using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NS
{
    public class DemoScriptPlayAnimation : MonoBehaviour
    {
        private Animator animator;
        private Vector3 startingPosition;
        private Quaternion startingRotation;

        [Header("Current Animation")]
        public bool isPerformingAnimation = false;
        [SerializeField] string currentAnimation;

        [Header("Animations")]
        [SerializeField] List<string> animations = new List<string>();
        [SerializeField] Text currentAnimationName;
        private int animationListIndex = -1;

        [Header("Autoplay")]
        private bool autoPlay = false;
        [SerializeField] Text autoPlayText;

        [Header("Camera")]
        [SerializeField] Transform[] cameraTransforms;
        private int cameraPositionIndex = 0;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            startingPosition = transform.position;
            startingRotation = transform.rotation;

            foreach (AnimationClip ac in animator.runtimeAnimatorController.animationClips)
            {
                animations.Add(ac.name);
            }
        }

        private void Start()
        {
            autoPlay = false;
            PlayNextAnimation();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                PlayPeviousAimation();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                PlayNextAnimation();
            }

            if (autoPlay && !IsAnimationPlaying())
            {
                PlayNextAnimation();
            }
        }

        public void PlayNextAnimation()
        {
            transform.rotation = startingRotation;
            transform.position = startingPosition;

            animationListIndex++;

            if (animationListIndex >= animations.Count)
            {
                animationListIndex = 0;
            }

            isPerformingAnimation = true;
            animator.Play(animations[animationListIndex]);
            currentAnimationName.text = animations[animationListIndex];
        }

        public void PlayPeviousAimation()
        {
            transform.rotation = startingRotation;
            transform.position = startingPosition;

            animationListIndex--;

            if (animationListIndex < 0)
            {
                animationListIndex = animations.Count - 1;
            }

            isPerformingAnimation = true;
            animator.Play(animations[animationListIndex]);
            currentAnimationName.text = animations[animationListIndex];
        }

        public void ToggleAutoPlay()
        {
            autoPlay = !autoPlay;

            if (autoPlay)
            {
                autoPlayText.text = "Auto Play: ON";
            }
            else
            {
                autoPlayText.text = "Auto Play: OFF";
            }
        }

        private bool IsAnimationPlaying()
        {
            return animator.GetCurrentAnimatorStateInfo(0).length >
           animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }

        public void ToggleCameraPosition()
        {
            cameraPositionIndex += 1;

            if (cameraPositionIndex >= cameraTransforms.Length)
                cameraPositionIndex = 0;

            Camera.main.transform.position = cameraTransforms[cameraPositionIndex].position;
            Camera.main.transform.rotation = cameraTransforms[cameraPositionIndex].rotation;
        }
    }
}
