using UnityEngine;
using System.Collections;

public class MusicUpdater : MonoBehaviour {
	
	private AudioSource audio;
	
	void Start () {
		this.audio = GetComponent<AudioSource> ();
	}
	
	void Update () {
		this.audio.volume = GameController.music / 100;
	}

}
