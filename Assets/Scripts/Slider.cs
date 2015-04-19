using UnityEngine;
using System.Collections;

public class Slider : MonoBehaviour {

	public Camera mainCam;
	public float minVal, maxVal;
	public Transform thumb;
	public Collider sliderArea;
	public float range;
	public Transform root;
	public TextMesh text;

	public float value = 0.5f;
	private string originalText;

	void Start () {
		if (text != null) {
			originalText = text.text;
		}
	}

	void Update () {
		Ray mouse = mainCam.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Input.GetMouseButton (0) && sliderArea.Raycast(mouse, out hit, 100f)) {
			float dist = Mathf.Abs((sliderArea.transform.position - mainCam.transform.position).magnitude);
			Vector3 msp = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist));
			float newX = Mathf.Clamp(msp.x, sliderArea.transform.position.x - range * root.localScale.x, sliderArea.transform.position.x + range * root.localScale.x);
			thumb.position = new Vector3(newX, thumb.position.y, thumb.position.z);
			value = thumb.localPosition.x / range / 2f + 0.5f;
		}
		if (text != null) {
			text.text = originalText + (int) Mathf.Round(GetValue () * 10f) / 10f;
		}
	}

	public float GetValue () {
		return minVal + value * (maxVal - minVal);
	}

}
