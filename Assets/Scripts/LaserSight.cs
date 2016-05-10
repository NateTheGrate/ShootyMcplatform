using UnityEngine;
using System.Collections;

public class LaserSight : MonoBehaviour {
	public LineRenderer laser;
	public GameObject parent;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = parent.transform.eulerAngles;
		Debug.DrawRay (transform.position, direction, Color.red);
		//layer 9 = player

	}
}
