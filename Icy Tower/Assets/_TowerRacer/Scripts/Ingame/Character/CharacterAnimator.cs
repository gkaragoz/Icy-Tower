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
        _characterMotor.OnAnimationStateChanged += PlayJumpMiddleAnimation;
        _characterMotor.OnAnimationStateChanged += PlayFallAnimation;
        _characterMotor.OnAnimationStateChanged += PlayComboJumpAnimation;
        _characterMotor.OnAnimationStateChanged += PlayRunAnimation;
        GameManager.instance.OnGameStateChanged += OnGameStateChanged;

    }

    private void OnGameStateChanged(GameState arg1, GameState arg2)
    {
        if (arg2== GameState.GameOver)
        {
            _animator.SetTrigger("IdleStart");
        }
    }

    private void PlayJumpMiddleAnimation(AnimationState state)
    {
        if (state == AnimationState.Fall)
            _animator.SetTrigger("Fall");
        else
        {
            _animator.ResetTrigger("Fall");
        }
    }
    private void PlayFallAnimation(AnimationState state)
    {
        if (state == AnimationState.JumpMiddle)
            _animator.SetTrigger("JumpMiddle");
        else
        {
            _animator.ResetTrigger("JumpMiddle");
        }
    }
    private void PlayJumpAnimation(AnimationState state) {
        if (state == AnimationState.JumpBegin)
        {

              _animator.SetTrigger("Jump");
        }
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
