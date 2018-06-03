using UnityEngine;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class ThirdPersonUserControl : MonoBehaviour
{
    public FixedJoystick joystick;

    [HideInInspector]
    public float hInput;

    [HideInInspector]
    public float vInput;

    private ThirdPersonCharacter _character; // A reference to the ThirdPersonCharacter on the object
    private Transform _cam;                  // A reference to the main camera in the scenes transform
    private Vector3 _camForward;             // The current forward direction of the camera
    private Vector3 _move;

    private void Start() {
        if (Camera.main != null) {
            _cam = Camera.main.transform;
        } else {
            Debug.LogWarning("Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
        }

        _character = GetComponent<ThirdPersonCharacter>();
    }

    private void FixedUpdate() {
        // get input from joystick
        if (joystick != null) {
            hInput = joystick.inputVector.x;
            vInput = joystick.inputVector.y;
        }

        // calculate move direction to pass to character
        if (_cam != null) {
            // calculate camera relative direction to move:
            _camForward = Vector3.Scale(_cam.forward, new Vector3(1, 0, 1)).normalized;
            _move = vInput * _camForward + hInput * _cam.right;
        } else {
            // we use world-relative directions in the case of no main camera
            _move = vInput * Vector3.forward + hInput * Vector3.right;
        }

        _character.Move(_move);
    }
}
