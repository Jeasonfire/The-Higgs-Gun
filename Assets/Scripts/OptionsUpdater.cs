using UnityEngine;
using System.Collections;

public class OptionsUpdater : MonoBehaviour {

	public Slider fovSlider;
	public Slider xAxisSlider;
	public Slider yAxisSlider;

	void Start() {
		fovSlider.SetValue (GameController.fov);
		xAxisSlider.SetValue (GameController.mouseSensitivity.x);
		yAxisSlider.SetValue (GameController.mouseSensitivity.y);
	}

	void Update () {
		GameController.fov = fovSlider.GetValue ();
		GameController.mouseSensitivity.x = xAxisSlider.GetValue ();
		GameController.mouseSensitivity.y = yAxisSlider.GetValue ();
	}

}
