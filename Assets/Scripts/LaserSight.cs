using UnityEngine;
using System.Collections;

public class LaserSight : MonoBehaviour {
	public LineRenderer laser;
	public GameObject parent;
    private RaycastHit2D hitPoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float angle = Mathf.Deg2Rad * parent.GetComponent<WeaponController>().angleInDeg;
		Vector3 direction = parent.transform.eulerAngles;
        //layer 9 = player
        laser.SetPosition(0, transform.position);
        //laser.SetPosition(1, Physics2D.Raycast(transform.position, transform.forward,)
	}
}
