using UnityEngine;
using System.Collections;

public class SelfDestructor : MonoBehaviour {

	public float time;

	private float endTime;

	void Start() {
		endTime = Time.realtimeSinceStartup + time;
	}

	void Update () {
		if (endTime <= Time.realtimeSinceStartup) {
			Destroy(gameObject);
		}
	}

}
