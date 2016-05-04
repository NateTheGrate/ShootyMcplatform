using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
	public float speed = 50;
	private Rigidbody2D rb;
	public GameObject execption;
	public float damage = 1;
	public float angle = 0;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	
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
