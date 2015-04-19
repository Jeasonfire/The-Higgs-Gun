using UnityEngine;
using System.Collections;

public class LevelChanger : MonoBehaviour {

	public string levelToChangeTo;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag.Equals ("Player")) {
			Debug.Log ("Change level to " + levelToChangeTo + "!");
			Application.LoadLevel(levelToChangeTo);
		}
	}

}
