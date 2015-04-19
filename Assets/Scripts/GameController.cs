using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static float fov = 90;
	public static Vector2 mouseSensitivity = new Vector2(2, 2);

	public PlayerController player;
	private float pauseTime = 0;

	void Start () {
	}
	
	void Update () {
		if (Camera.main.fieldOfView != fov && !player.GetPaused ()) {
			Camera.main.fieldOfView = fov;
		}
		if (player.GetPaused ()) {
			Camera.main.fieldOfView = 60;
		}

		if (Input.GetKey (KeyCode.Tab) && Time.realtimeSinceStartup - pauseTime > 0.2) {
			pauseTime = Time.realtimeSinceStartup;
			player.SetPaused(!player.GetPaused ());
			if (player.GetPaused ()) {
				Cursor.lockState = CursorLockMode.None;
			} else {
				Cursor.lockState = CursorLockMode.Locked;
			}
		}
	}

}
