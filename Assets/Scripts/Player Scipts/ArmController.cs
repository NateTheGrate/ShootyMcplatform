using UnityEngine;
using System.Collections;

public class ArmController : MonoBehaviour {
	[HideInInspector] public WeaponController weapon;
	private float angle;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (weapon != null) {
			angle = weapon.angleInDeg + 70;
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
		}

	}
}
