using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	void Start () {
	}
	
	void Update () {
		if (Input.GetKey ("e")) {
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

}
