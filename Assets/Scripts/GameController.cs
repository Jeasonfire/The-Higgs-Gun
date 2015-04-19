using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static float fov = 90;
	public static Vector2 mouseSensitivity = new Vector2(2, 2);

	void Start () {
	}
	
	void Update () {
		if (Camera.main.fieldOfView != fov) {
			Camera.main.fieldOfView = fov;
		}

		if (Input.GetKey ("e")) {
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

}
