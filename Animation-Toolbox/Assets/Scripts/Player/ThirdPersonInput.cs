using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonInput : MonoBehaviour {

    public FixedJoystick joystick;

    private ThirdPersonUserControl _control;

    private void Start() {
        _control = GetComponent<ThirdPersonUserControl>();
    }

    void Update () {
        _control.hInput = joystick.inputVector.x;
        _control.vInput = joystick.inputVector.y;
	}
}
