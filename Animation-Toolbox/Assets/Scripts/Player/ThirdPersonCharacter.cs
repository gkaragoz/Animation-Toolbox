using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThirdPersonCharacter : MonoBehaviour {

    public Transform[] upperBodyParts;     // This component is to blending animations of walk and rotation.

    [SerializeField] private float _movementSpeed = 2f;
	[SerializeField] private float _movingTurnSpeed = 360f;
	[SerializeField] private float _stationaryTurnSpeed = 180f;

	private Rigidbody _rigidbody;
	private float _turnAmount;
	private float _forwardAmount;

    private Animation _animation;

	void Start() {
        _animation = GetComponent<Animation>();
		_rigidbody = GetComponent<Rigidbody>();
		_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        InitializeAnimations();
    }

	public void Move(Vector3 move) {
		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.
		if (move.magnitude > 1f) move.Normalize();
		move = transform.InverseTransformDirection(move);

		_turnAmount = Mathf.Atan2(move.x, move.z);
		_forwardAmount = move.z;

		ApplyTurnRotation();

        if (move.magnitude > 0f) {
            _animation.CrossFade("run");
            _rigidbody.velocity = transform.forward * _movementSpeed;

            if (_turnAmount == 0)
                return;

            // turn to left
            if (_turnAmount < 0) {
                _animation.Blend("turn_left");
            }
            // turn to right
            else {
                _animation.Blend("turn_right");
            }
        }
        else {
            _animation.CrossFade("idle_01");
            _rigidbody.velocity = Vector3.zero;
        }
	}

	void ApplyTurnRotation() {
		float turnSpeed = Mathf.Lerp(_stationaryTurnSpeed, _movingTurnSpeed, _forwardAmount);
		transform.Rotate(0, _turnAmount * turnSpeed * Time.deltaTime, 0);
	}

    void InitializeAnimations() {
        _animation["turn_right"].layer = 5;
        foreach (Transform transform in upperBodyParts) {
            _animation["turn_right"].AddMixingTransform(transform);
        }
        _animation["turn_left"].layer = 5;
        foreach (Transform transform in upperBodyParts) {
            _animation["turn_left"].AddMixingTransform(transform);
        }
    }
}
