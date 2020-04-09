using System;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour {

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private Animator _animator = null;
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterMotor _characterMotor;

    private void Start() {
        _animator = GetComponentInChildren<Animator>();
        _characterMotor = GetComponent<CharacterMotor>();
        _characterMotor.OnAnimationStateChanged += PlayJumpAnimation;
        _characterMotor.OnAnimationStateChanged += PlayComboJumpAnimation;
        _characterMotor.OnAnimationStateChanged += PlayRunAnimation;
    }

    private void PlayJumpAnimation(AnimationState state) {
        if (state == AnimationState.Jump)
            _animator.SetTrigger("Jump");
        else {
            _animator.ResetTrigger("Jump");
        }
    }

    private void PlayComboJumpAnimation(AnimationState state) {
        if (state == AnimationState.ComboJump) {
            _animator.SetTrigger("Combo");
        } else {
            _animator.ResetTrigger("Combo");
        }
    }

    private void PlayRunAnimation(AnimationState state) {
        if (state == AnimationState.RightRun) {
            _animator.SetBool("Run",true);
            _animator.gameObject.transform.localRotation = (Quaternion.Euler(-90,180,-90));
        }else if (state == AnimationState.LeftRun) {
            _animator.SetBool("Run", true);
            _animator.gameObject.transform.localRotation = (Quaternion.Euler(-90, 180, 90));
        }
        else {
            _animator.SetBool("Run", false);
            _animator.gameObject.transform.localRotation = (Quaternion.Euler(0, 180, 0));

        }

    }

}
