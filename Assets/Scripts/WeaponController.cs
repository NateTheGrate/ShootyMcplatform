using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
	//refrence to the object that this follows
	[HideInInspector]public GameObject attachedTo;
	[HideInInspector]public float angle = 0;
	[HideInInspector]public bool isAi;
	public float radius = 1;
	public double threshold = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isAi) {
			if (attachedTo != null) {
				float mouseX = Input.mousePosition.x;
				float mouseY = Input.mousePosition.y;
				Vector2 mousePosition = Camera.main.ScreenToWorldPoint (new Vector3 (mouseX, mouseY, 0));
				//angle between attached object and mouse(in radians)
				angle = Mathf.Atan2 (attachedTo.transform.position.y - mousePosition.y, attachedTo.transform.position.x - mousePosition.x) + 135;

				//polar(angle and radius) --> rectangle(x and y) coordinates MUST BE IN RADIANS
				// x = rcos(theta)
				float x = radius * Mathf.Cos (angle);
				// y = rsin(theta)
				float y = radius * Mathf.Sin (angle);
				float angleInDeg = ((angle - 135) * 180 / Mathf.PI) + 185;
				if (Mathf.Abs (this.transform.rotation.eulerAngles.z - angleInDeg) > threshold) {
					transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angleInDeg));
					transform.position = new Vector2 (x + attachedTo.transform.position.x, y + attachedTo.transform.position.y);
				}
			}
		}
	}

}
