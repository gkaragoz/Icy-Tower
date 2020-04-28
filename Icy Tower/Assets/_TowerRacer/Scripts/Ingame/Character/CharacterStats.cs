﻿using UnityEngine;

public class CharacterStats : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private CharacterStats_SO _characterDefinition_Template = null;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterStats_SO _character = null;

    #region Initializations

    private void Awake() {
        if (_characterDefinition_Template != null) {
            _character = Instantiate(_characterDefinition_Template);
        }
    }

    #endregion

    #region Increasers

    public void IncreaseMovementSpeed(float amount) {
        _character.MovementSpeed += amount;

        // TODO: check maximum values to not go over it.
    }

    public void IncreaseJumpPower(float amount) {
        _character.JumpPower += amount;

        // TODO: check maximum values to not go over it.
    }

    #endregion

    #region Decreasers

    public void DecreaseMovementSpeed(float amount) {
        _character.MovementSpeed -= amount;

        // TODO: check default values to not go below zero.
    }

    public void DecreaseJumpPower(float amount) {
        _character.JumpPower -= amount;

        // TODO: check default values to not go below zero.
    }

    #endregion

    #region Setters

    public void SetMovementSpeed(float speed) {
        _character.MovementSpeed = speed;
    }

    public void SetJumpPower(float power) {
        _character.JumpPower = power;
    }
    public void SetComboJumpPower(float power) {
        _character.ComboJumpPower = power;
    }

    public void SetMaxVelocityX(float power) {
        _character.MaxVelocityX = power;
    }

    public void SetRequiredVelocityForComboJump(float power) {
        _character.RequiredVelocityForComboJump = power;
    }

    public void SetFootPositionOffset(float amount) {
        _character.FootPositionOffset = amount;
    }

    public void SetFallMultiplier(float value) {
        _character.LocalGravity = value;
    }
    #endregion

    #region Reporters

    public string GetName() {
        return _character.name;
    }

    public GameObject GetPrefab() {
        return _character.Prefab;
    }

    public float GetMovementSpeed() {
        return _character.MovementSpeed;
    }

    public float GetJumpPower() {
        return _character.JumpPower;
    }

    public float GetComboJumpPower() {
        return _character.ComboJumpPower;
    }

    public float GetMaxVelocityX() {
        return _character.MaxVelocityX;
    }

    public float GetRequiredVelocityForComboJump() {
        return _character.RequiredVelocityForComboJump;
    }

    public float GetFootPositionOffset() {
        return _character.FootPositionOffset;
    }

    public float GetLocalGravity() {
        return _character.LocalGravity;
    }

    #endregion

    #region Custom Methods
    #endregion

}