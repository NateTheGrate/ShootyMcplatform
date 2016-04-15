using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	public float speed = 50;
	private Rigidbody2D rb;
	[HideInInspector]public GameObject parent; // the gun shooting the object
	public GameObject execption;
	public float damage = 1;
	public float angle;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		angle = parent.GetComponent<WeaponController>().angleInDeg;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		rb.velocity = new Vector2 (Mathf.Cos (Mathf.Deg2Rad * angle), Mathf.Sin (Mathf.Deg2Rad * angle)) * speed;
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag != "Player" ) {	//apply damage here
			Destroy (this.gameObject);
		}
	}
}
