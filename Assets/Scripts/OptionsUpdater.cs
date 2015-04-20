using UnityEngine;
using System.Collections;

public class OptionsUpdater : MonoBehaviour {

	public Slider fovSlider;
	public Slider xAxisSlider;
	public Slider yAxisSlider;
	public Slider musicSlider;
	public Slider soundSlider;

	void Start() {
		fovSlider.SetValue (GameController.fov);
		xAxisSlider.SetValue (GameController.mouseSensitivity.x);
		yAxisSlider.SetValue (GameController.mouseSensitivity.y);
		musicSlider.SetValue (GameController.music);
		soundSlider.SetValue (GameController.sound);
	}

	void Update () {
		GameController.fov = fovSlider.GetValue ();
		GameController.mouseSensitivity.x = xAxisSlider.GetValue ();
		GameController.mouseSensitivity.y = yAxisSlider.GetValue ();
		GameController.music = musicSlider.GetValue ();
		GameController.sound = soundSlider.GetValue ();
	}

}
