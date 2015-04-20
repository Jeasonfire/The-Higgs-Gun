using UnityEngine;
using System.Collections;

public class PlatformReplacer : MonoBehaviour {

	public GameObject replacer;
	public AudioSource effect;

	void Start () {
		if (GameController.IsLoading ()) {
			return;
		}

		if (!effect.isPlaying) {
			effect.Play ();
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag.Equals(gameObject.tag)) {
			GameObject platform = (GameObject) Instantiate (replacer, transform.position, transform.rotation);
			platform.transform.localScale = transform.localScale;
			if (GetComponent<Rigidbody> () != null) {
				platform.AddComponent(GetComponent<Rigidbody> ().GetType());
				platform.GetComponent<Rigidbody> ().constraints = GetComponent<Rigidbody> ().constraints;
				if (platform.GetComponent<Collider> ().isTrigger) {
					platform.GetComponent<Rigidbody> ().isKinematic = true;					
					platform.GetComponent<Rigidbody> ().useGravity = false;
				} else {
					platform.GetComponent<Rigidbody> ().isKinematic = false;					
					platform.GetComponent<Rigidbody> ().useGravity = true;
				}
			}
			Destroy(gameObject);
		}
	}

}
