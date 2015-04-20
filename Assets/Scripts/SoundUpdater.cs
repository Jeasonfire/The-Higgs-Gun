using UnityEngine;
using System.Collections;

public class SoundUpdater : MonoBehaviour {

	private AudioSource audio;

	void Start () {
		this.audio = GetComponent<AudioSource> ();
	}

	void Update () {
		this.audio.volume = GameController.sound / 100;
	}

}
