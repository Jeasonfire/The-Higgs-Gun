using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public Collider playButton;
	public Collider optionsButton;
	public Collider backButton;
	public Collider fovButton;
	public Collider xAxisButton;
	public Collider yAxisButton;
	public Collider musicButton;
	public Collider soundButton;

	public float cameraRotationSpeed, errorMargin;
	public Transform spotlight;
	public Camera mainCam;

	private const int NOTHING = 0, PLAY = 1, OPTIONS = 2, BACK = 3, FOV = 4, XAXIS = 5, YAXIS = 6, MUSIC = 7, SOUND = 8;
	private int currentButton = NOTHING;
	private Vector3 mainCamRot, optionsCamRot;
	private Vector3 targetCameraRotation;

	void Start () {
		mainCamRot = mainCam.transform.localEulerAngles + new Vector3(0, 360, 0);
		optionsCamRot = mainCam.transform.localEulerAngles + new Vector3(0, 180, 0);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	void Play () {
		Application.LoadLevel ("Level1");
	}

	void ChangeToOptionsMenu () {
		targetCameraRotation = optionsCamRot;
	}

	void ChangeToMainMenu () {
		targetCameraRotation = mainCamRot;
	}

	void Update () {
		// Camera rotation
		Vector3 dir = targetCameraRotation - mainCam.transform.localEulerAngles;
		if (dir.magnitude < errorMargin || dir.magnitude > 360 - errorMargin) {
			mainCam.transform.localEulerAngles = targetCameraRotation;
		} else {
			dir = dir.normalized * cameraRotationSpeed * Time.deltaTime;
			mainCam.transform.localEulerAngles = mainCam.transform.localEulerAngles + dir;
		}

		// Hovers & clicks
		Ray mouseRay = mainCam.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (playButton.Raycast (mouseRay, out hit, 100f)) {
			if (currentButton != PLAY) {
				spotlight.gameObject.SetActive(true);
			}
			spotlight.LookAt (playButton.transform);
			currentButton = PLAY;
			
			if (Input.GetMouseButton (0)) {
				Play ();
			}
		} else if (optionsButton.Raycast (mouseRay, out hit, 100f)) {
			if (currentButton != OPTIONS) {
				spotlight.gameObject.SetActive(true);
			}
			spotlight.LookAt (optionsButton.transform);
			currentButton = OPTIONS;

			if (Input.GetMouseButton (0)) {
				ChangeToOptionsMenu ();
			}
		} else if (backButton.Raycast (mouseRay, out hit, 100f)) {
			if (currentButton != BACK) {
				spotlight.gameObject.SetActive(true);
			}
			spotlight.LookAt (backButton.transform);
			currentButton = BACK;
			
			if (Input.GetMouseButton (0)) {
				ChangeToMainMenu ();
			}
		} else if (fovButton.Raycast (mouseRay, out hit, 100f)) {
			if (currentButton != FOV) {
				spotlight.gameObject.SetActive(true);
			}
			spotlight.LookAt (fovButton.transform);
			currentButton = BACK;
		} else if (xAxisButton.Raycast (mouseRay, out hit, 100f)) {
			if (currentButton != XAXIS) {
				spotlight.gameObject.SetActive(true);
			}
			spotlight.LookAt (xAxisButton.transform);
			currentButton = XAXIS;
		} else if (yAxisButton.Raycast (mouseRay, out hit, 100f)) {
			if (currentButton != YAXIS) {
				spotlight.gameObject.SetActive(true);
			}
			spotlight.LookAt (yAxisButton.transform);
			currentButton = YAXIS;
		} else if (musicButton.Raycast (mouseRay, out hit, 100f)) {
			if (currentButton != MUSIC) {
				spotlight.gameObject.SetActive(true);
			}
			spotlight.LookAt (musicButton.transform);
			currentButton = MUSIC;
		} else if (soundButton.Raycast (mouseRay, out hit, 100f)) {
			if (currentButton != SOUND) {
				spotlight.gameObject.SetActive(true);
			}
			spotlight.LookAt (soundButton.transform);
			currentButton = SOUND;
		} else {
			if (currentButton != NOTHING) {
				spotlight.gameObject.SetActive(false);
			}
			currentButton = NOTHING;
		}
	}
}
