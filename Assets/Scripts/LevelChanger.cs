using UnityEngine;
using System.Collections;

public class LevelChanger : MonoBehaviour {

	public string levelToChangeTo;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag.Equals ("Player")) {
			GameController.SetLoading (2);
			Application.LoadLevel(levelToChangeTo);
		}
	}

}
