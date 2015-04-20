using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public CharacterController character;
	public float moveSpeed;
	public float gravity;
	public float jumpForce;
	
	public Transform headTrans, bodyTrans;
	public GameObject spotlight;
	private bool flashlightOn = false;
	private float flashlightToggled = 0;
	private Vector2 rotation;

	public Animator gunAnim;
	public Transform gunTransform;

	public const int NO_AMMO = 0, YELLOW = 1, GREEN = 2, BLUE = 3;
	public float cooldown;
	public float shootForce;
	public GameObject yellowAmmo, greenAmmo, blueAmmo;
	public GUIText equipText;
	private int currentAmmo = NO_AMMO;
	public bool canShootYellow = false, canShootGreen = false, canShootBlue = false;
	private float firingCooldown;
	private string originalEquipText;

	public GameObject pauseMenu;
	private bool paused = false;

	public AudioSource jumpSnd, pickupSnd, shootSnd;

	void Start () {
		rotation.x = bodyTrans.localEulerAngles.y;
		rotation.y = bodyTrans.localEulerAngles.x;
		if (equipText != null) {
			originalEquipText = equipText.text;
		}
	}

	public bool GetPaused () {
		return this.paused;
	}

	public void SetPaused (bool newPaused) {
		this.paused = newPaused;
		if (this.paused) {
			headTrans.localEulerAngles = new Vector3 (-6, 0, 0);
			bodyTrans.localEulerAngles = new Vector3 (0, 0, 0);
			Vector3 newPos = character.gameObject.transform.localPosition;
			newPos.y += 100;
			character.gameObject.transform.localPosition = newPos;
		} else {
			Vector3 newPos = character.gameObject.transform.localPosition;
			newPos.y -= 100;
			character.gameObject.transform.localPosition = newPos;
		}
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag.Contains ("Pickup")) {
			bool noAmmo = !canShootYellow && !canShootGreen && !canShootBlue;
			if (other.tag.Contains ("Yellow")) {
				canShootYellow = true;
				if (noAmmo) {
					currentAmmo = YELLOW;
				}
			}
			if (other.tag.Contains ("Green")) {
				canShootGreen = true;
				if (noAmmo) {
					currentAmmo = GREEN;
				}
			}
			if (other.tag.Contains ("Blue")) {
				canShootBlue = true;
				if (noAmmo) {
					currentAmmo = BLUE;
				}
			}
			if (!pickupSnd.isPlaying) {
				pickupSnd.Play ();
			}
			Destroy (other.gameObject);
		}
	}
	
	void Shoot(GameObject ammo) {
		GameObject bullet = (GameObject) Instantiate (ammo, gunTransform.position, headTrans.rotation);
		bullet.GetComponent<Rigidbody> ().AddForce (bullet.transform.forward * shootForce);
		Physics.IgnoreCollision (GetComponent<Collider> (), bullet.GetComponent<Collider> ());

		if (gunAnim != null) {
			gunAnim.Play("Shoot");
		}
		if (!shootSnd.isPlaying) {
			shootSnd.Play ();
		}
	}
	
	void ResetCooldown () {
		firingCooldown = cooldown;
	}
	
	bool CanShoot () {
		return firingCooldown <= 0;
	}

	void Update () {
		if (paused) {
			pauseMenu.SetActive (true);
			return;
		} else {
			pauseMenu.SetActive (false);
		}

		// Movement
		Vector3 move = new Vector3 ();
		move.x = Input.GetAxis ("Horizontal");
		move.z = Input.GetAxis ("Vertical");
		move = transform.TransformDirection (move);
		move = move.normalized * moveSpeed;
		move.y = character.velocity.y;
		if (!character.isGrounded) {
			move.y -= gravity * Time.deltaTime;
		} else {
			if (Input.GetButton ("Jump")) {
				move.y = jumpForce;
				if (!jumpSnd.isPlaying) {
					jumpSnd.Play ();
				}
			} else {
				move.y = 0;
			}
		}
		character.Move (move * Time.deltaTime);

		// Mouselook
		Vector2 mouse;
		if (Cursor.lockState == CursorLockMode.Locked) {
			mouse = new Vector2 (Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"));
		} else {
			mouse = new Vector2 (0, 0);
		}
		rotation.x += mouse.x * GameController.mouseSensitivity.x;
		rotation.y -= mouse.y * GameController.mouseSensitivity.y;
		rotation.y = Mathf.Clamp (rotation.y, -90, 90);
		bodyTrans.localEulerAngles = new Vector3 (bodyTrans.localEulerAngles.x, rotation.x, bodyTrans.localEulerAngles.z);
		headTrans.localEulerAngles = new Vector3 (rotation.y, headTrans.localEulerAngles.y, headTrans.localEulerAngles.z);

		// Flashlight
		if (Input.GetButton ("Flashlight") && Time.realtimeSinceStartup - flashlightToggled > 0.2) {
			flashlightOn = !flashlightOn;
			spotlight.SetActive(flashlightOn);
			flashlightToggled = Time.realtimeSinceStartup;
		}

		// Scroll through ammo
		if (canShootYellow || canShootGreen || canShootBlue) {
			int mouseScrollWheel = (int) (Input.GetAxis ("Mouse ScrollWheel") * 10);
			currentAmmo -= mouseScrollWheel;
			while (currentAmmo > BLUE) {
				currentAmmo = YELLOW;
			}
			while (currentAmmo < YELLOW) {
				currentAmmo = BLUE;
			}
		}

		// Shooting
		if (!CanShoot ()) {
			firingCooldown -= Time.deltaTime;
		}
		if (Input.GetButton("Shoot") && CanShoot ()) {
			if (currentAmmo == YELLOW && canShootYellow) {
				Shoot (yellowAmmo);
			}
			if (currentAmmo == GREEN && canShootGreen) {
				Shoot (greenAmmo);
			}
			if (currentAmmo == BLUE && canShootBlue) {
				Shoot (blueAmmo);
			}
			ResetCooldown ();
		}

		// Update equip text
		if (equipText != null) {
			switch (currentAmmo) {
			default:
				equipText.text = originalEquipText + "Off";
				break;
			case NO_AMMO:
				equipText.text = originalEquipText + "No ammo";
				break;
			case YELLOW:
				equipText.text = originalEquipText + "Yellow";
				break;
			case GREEN:
				equipText.text = originalEquipText + "Green";
				break;
			case BLUE:
				equipText.text = originalEquipText + "Blue";
				break;
			}
		}
	}

}
