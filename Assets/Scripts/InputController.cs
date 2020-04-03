using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
	
	// All input pases through here, avoids issues where you don't know why something is called;
	void Update () {
        if (Input.GetKey(KeyCode.W))            GlobalVariables.Instance.playerMovCon.rotateUp();
        if (Input.GetKey(KeyCode.S))            GlobalVariables.Instance.playerMovCon.rotateDown();
        if (Input.GetKey(KeyCode.A))            GlobalVariables.Instance.playerMovCon.rotateLeft();
        if (Input.GetKey(KeyCode.D))            GlobalVariables.Instance.playerMovCon.rotateRight();
        if (Input.GetKeyDown(KeyCode.Alpha1))   GlobalVariables.Instance.weaponCon.switchToFlashLight();
        if (Input.GetKeyDown(KeyCode.Alpha2))   GlobalVariables.Instance.weaponCon.switchToShotGun();
        if (Input.GetMouseButtonDown(0))        GlobalVariables.Instance.weaponCon.attemptFire();
        if (Input.GetMouseButtonUp(0))          GlobalVariables.Instance.weaponCon.unFire();
    }
}
