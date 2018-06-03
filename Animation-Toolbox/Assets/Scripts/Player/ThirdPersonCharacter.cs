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

    private Animator _anim;

	void Start() {
        _anim = GetComponent<Animator>();
		_rigidbody = GetComponent<Rigidbody>();
		_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
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
            _rigidbody.velocity = transform.forward * _movementSpeed;
        }
        else {
            _rigidbody.velocity = Vector3.zero;
        }

        // update animation parameters
        _anim.SetFloat("velocity", _rigidbody.velocity.magnitude);
        _anim.SetFloat("rotation", _turnAmount * 2f);
	}

	void ApplyTurnRotation() {
		float turnSpeed = Mathf.Lerp(_stationaryTurnSpeed, _movingTurnSpeed, _forwardAmount);
		transform.Rotate(0, _turnAmount * turnSpeed * Time.deltaTime, 0);
	}
}
