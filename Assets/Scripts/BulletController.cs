using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	public float speed = 50;
	private Rigidbody2D rb;
	[HideInInspector]public GameObject parent; // the gun shooting the object
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		float angle = parent.GetComponent<WeaponController>().angle + 0.25f;
		rb.velocity = new Vector2 (Mathf.Cos (angle), Mathf.Sin (angle)) * speed;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other){
		Destroy (this.gameObject);
	}
}
