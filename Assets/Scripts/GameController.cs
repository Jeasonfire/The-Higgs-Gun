using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static float fov = 90;
	public static Vector2 mouseSensitivity = new Vector2(2, 2);
	public static float music = 50, sound = 50;

	private static float loading = 1;
	public PlayerController player;
	private float pauseTime = 0;

	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public static void SetLoading (int seconds) {
		loading = seconds;
	}

	public static bool IsLoading () {
		return loading > 0;
	}
	
	void Update () {
		if (loading > 0) {
			loading -= Time.deltaTime;
		}

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
				Cursor.visible = true;
			} else {
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}
	}

}
