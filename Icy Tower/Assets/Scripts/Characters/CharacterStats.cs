﻿using UnityEngine;

public class CharacterStats : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    private CharacterStats_SO _characterDefinition_Template = null;

    [Header("Debug")]
    [SerializeField]
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

    #endregion

    #region Reporters

    public string GetName() {
        return _characterDefinition_Template.Name;
    }

    public GameObject GetPrefab() {
        return _characterDefinition_Template.Prefab;
    }

    public float GetMovementSpeed() {
        return _character.MovementSpeed;
    }

    public float GetJumpPower() {
        return _character.JumpPower;
    }

    #endregion

    #region Custom Methods
    #endregion

}