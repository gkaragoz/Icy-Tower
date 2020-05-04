using System;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterMotor : MonoBehaviour, IHaveSingleSound {

    [Header("Initializations")]
    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField]
    private float _rayDistance = 0.5f;
    [SerializeField]
    private Transform[] _rayPoints = null;

    public Action<AnimationState> OnAnimationStateChanged;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private Rigidbody _rb;
    [SerializeField]
    [Utils.ReadOnly]
    private BoxCollider _boxCollider;
    [SerializeField]
    [Utils.ReadOnly]
    private CharacterStats _characterStats;
    [SerializeField]
    [Utils.ReadOnly]
    private AnimationState _animationState = AnimationState.Idle;
    [SerializeField]
    private ParticleSystem _comboJumpVFX = null;

    public AnimationState AnimationStateEnum {
        get {
            return _animationState;
        }
        set {
            _animationState = value;
            OnAnimationStateChanged?.Invoke(_animationState);
        }
    }

    public bool IsFalling {
        get {
            if (_rb.velocity.y < 0) {
                return true;
            } else
                return false;
        }
    }

    private void Awake() {
        _rb = GetComponent<Rigidbody>();

        _boxCollider = GetComponentInChildren<BoxCollider>();
        _characterStats = GetComponent<CharacterStats>();

        Stop();
    }

    private void Update() {
        if (IsFalling) {
            _boxCollider.enabled = true;
        } else {
            _boxCollider.enabled = false;
        }
    }

    private void FixedUpdate() {
        if (IsFalling) {
            Vector3 dir;
            Vector3 origin;
            bool isHit = false;

            for (int ii = 0; ii < _rayPoints.Length; ii++) {
                origin = _rayPoints[ii].position;
                dir = new Vector3(origin.x, transform.position.y, origin.z) - origin;
                isHit = Physics.Raycast(origin, dir.normalized, _rayDistance, _layerMask);

                if (isHit) {
                    break;
                }
            }

            if (isHit) {
                _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
                if (Mathf.Abs(_rb.velocity.x) >= _characterStats.GetRequiredVelocityForComboJump()) {
                    ComboJump();
                } else {
                    Jump();
                }
            }
        }
    }

    private void OnDrawGizmos() {
        Vector3 dir = Vector3.zero;
        Vector3 origin = Vector3.zero;
        bool isHit = false;

        for (int ii = 0; ii < _rayPoints.Length; ii++) {
            origin = _rayPoints[ii].position;
            dir = new Vector3(origin.x, transform.position.y, origin.z) - origin;
            isHit = Physics.Raycast(origin, dir.normalized, _rayDistance, _layerMask);

            if (isHit) {
                break;
            }
        }

        for (int ii = 0; ii < _rayPoints.Length; ii++) {
            if (_rayPoints[ii].position == origin && isHit) {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(origin, dir.normalized * _rayDistance);
            } else {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(_rayPoints[ii].position, dir.normalized * _rayDistance);
            }
        }
    }

    public void Run() {
        _rb.isKinematic = false;

        this.enabled = true;
    }

    public void Stop() {
        _rb.velocity = Vector3.zero;
        _rb.isKinematic = true;

        this.enabled = false;
    }

    public void Jump() {
        _rb.AddForce(Vector3.up * (_characterStats.GetJumpPower() + (Mathf.Abs(_rb.velocity.x) / 3f)), ForceMode.Impulse);
        AnimationStateEnum = AnimationState.Jump;
        PlayVFX();
        PlaySFX(SoundFXTypes.InGame_Player_Jump);
    }

    private void PlayVFX() {
        ObjectPooler.instance.SpawnFromPool(VFXTypes.VFXJump.ToString(), transform.position);
    }

    public void ComboJump() {
        _rb.AddForce(Vector3.up * _characterStats.GetComboJumpPower(), ForceMode.Impulse);
        AnimationStateEnum = AnimationState.ComboJump;
        _comboJumpVFX.Play();
        PlaySFX(SoundFXTypes.InGame_Player_Jump_Combo);

        Account.instance.AddCombo();
    }

    public void Move(float horizontal) {
        if (horizontal > 0) {
            horizontal = 1;
        }
        if (horizontal < 0) {
            horizontal = -1;
        }

        _rb.AddForce(new Vector3(horizontal * _characterStats.GetMovementSpeed(), 0));
    }

    public void PlaySFX(SoundFXTypes sfxType) {
        ObjectPooler.instance.SpawnFromPool(sfxType.ToString(), transform.position);
    }
}
