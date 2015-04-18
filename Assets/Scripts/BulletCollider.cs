using UnityEngine;
using System.Collections;

public class BulletCollider : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag.Equals(gameObject.tag)) {
			Destroy(gameObject);
		}
	}

}
