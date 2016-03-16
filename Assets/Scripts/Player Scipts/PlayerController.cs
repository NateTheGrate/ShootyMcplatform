using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float moveForce = 360f;
	public float jumpForce = 50f;
	public float maxSpeed = 15f;
	public int airDragMultiplier = 2;
	public float maxJumpSpeed = 50f;
	public float yForceAwayFromWall = 400;
	public float xForceAwayFromWall = 600;

	[HideInInspector]public bool jump;
	/** [HideInInspector] **/public bool grounded;
	[HideInInspector]public int jumpLimit = 2;
	/** [HideInInspector] **/public int jumpNumber = 0;

	private Rigidbody2D rb;
	private GameObject wallObject;

	[HideInInspector]public bool againstWall = false;
	[HideInInspector]public Transform groundCheck;
	[HideInInspector]public bool movementControl = true;

	private int ticks;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();	

	}
	
	// Update is called once per frame
	// Controlls
	void Update () {
		//cast a line to see wether or not it hit the ground
		grounded = Physics2D.Linecast (transform.position, groundCheck.position );

		// jumping on the ground and off of walls and other objects
		if ((Input.GetButtonDown ("Jump")) && (jumpNumber < (jumpLimit + 1) && againstWall)) {
			jump = true;
			jumpNumber++;
		}
		else if( (Input.GetButtonDown ("Jump") )  && (jumpNumber < jumpLimit) ) {
			jump = true;
			jumpNumber++;
		}
		// go back through platforms
		if (Input.GetKeyDown (KeyCode.S)) {
			// playerlayer, groundlayer, ignore collision?
			Physics2D.IgnoreLayerCollision (9, 13, rb.velocity.y < 0.05);
		}

		/* if (Input.GetKeyDown(KeyCode.W) ) {
			rb.gravityScale = -rb.gravityScale;
		}*/
		ticks++;
	}

	void FixedUpdate(){
		
		if (ticks >= 6) {
			movementControl = true;
		}

		if (movementControl) {
			
			//horizontal movement
			float moveHorizontal = Input.GetAxis ("Horizontal");
			Vector2 movement = new Vector2 (moveHorizontal, 0.0f);

			//add some air drag
			if (!againstWall && !grounded) {
				movement /= airDragMultiplier;
			}

			//force upward can be implemented to reverse gravity
			Vector2 jumpVector = new Vector2 (0.0f, ((jumpForce - rb.velocity.y * 45) * Mathf.Sign (rb.gravityScale) ) );

			//max speeds
			if (Mathf.Abs (rb.velocity.x) > maxSpeed) {
				rb.velocity = new Vector2 (Mathf.Sign (rb.velocity.x) * maxSpeed, rb.velocity.y)  ;
			}

			if (Mathf.Abs (rb.velocity.y) > maxJumpSpeed) {
				rb.velocity = new Vector2 (rb.velocity.x, Mathf.Sign (rb.velocity.y * maxJumpSpeed));
			}

			//reverse direction and disable movement for a moment
			if (jump && againstWall) {
				rb.AddForce( new Vector2(-xForceAwayFromWall  * Mathf.Sign(wallObject.transform.position.x - this.transform.position.x) , jumpVector.y + yForceAwayFromWall)  ) ;
				movementControl = false;
				jump = false;
				ticks = 0;

			}else if (jump) {
				rb.AddForce (jumpVector ); 
				jump = false;

			}if (movementControl) {
				rb.AddForce (movement * (moveForce * Mathf.Sign(rb.gravityScale) )  );
			}
		}
	} 
		
	 void OnCollisionStay2D(Collision2D other){
		//if not moving vertically and not against a wall then reset the jump number
		if ((grounded && rb.velocity.y < 0.05) && (!againstWall)) {
			jumpNumber = 0;

		// jumping off of other objects( just one for now )
		} else if (grounded && (!againstWall) && (!other.gameObject.tag.Equals ("Platform") || !other.gameObject.tag.Equals ("Wall") )   ) {
			jumpNumber--;	
		}
		if (other.gameObject.tag.Equals ("Wall")) {
			againstWall = true;
			wallObject = other.gameObject;
		} 
			
	}

	void OnCollisionEnter2D(Collision2D other){
		//Weapon Pickups
		if(other.gameObject.tag.Equals("Weapon") ){
			other.transform.SetParent (this.transform);
			//destroy rigibbody and disable collider components
			//you can only destroy and recreate rigidbodies :/
			Destroy( other.gameObject.GetComponent<Rigidbody2D>() );
			foreach (BoxCollider2D collisionBox in other.gameObject.GetComponents<BoxCollider2D>()) {
				collisionBox.enabled = false;
			}

			other.gameObject.GetComponent<WeaponController> ().attachedTo = this.gameObject;
		}
	}

	void OnCollisionExit2D(Collision2D other){
		againstWall = false;
	}
}
