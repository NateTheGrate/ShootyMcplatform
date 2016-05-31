using UnityEngine;
using System.Collections;
/**
 * A general class for player controlled weapons 
 **/
public class WeaponController : MonoBehaviour {
	//refrence to the object that this follows
	[HideInInspector]public GameObject attachedTo;
	[HideInInspector]public float angle = 0;
	[HideInInspector]public float angleInDeg = 0;
	[HideInInspector]public bool flipped = false;
	[HideInInspector]public double threshold = 1; //how much an angle(in degrees) it takes to cause rotation could reduce the jitter

	public GameObject bullet;
	public Transform bulletSpawn;
	public float fireRate = 30;
	public bool semiautomatic = false; //semiautomatic in this game means fire on click, a rocket launcher for example could be semiautomatic but you shouldn't be able to fire it every click

    public float mass = 1;
	public float radius = 1;
	public float scale = 1;
    //private bool reactivateHitBox = false;
	public float teleportDistance = 1;
	private float shootCoolDown = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
			if (attachedTo != null) {
				float mouseX = Input.mousePosition.x;
				float mouseY = Input.mousePosition.y;
				Vector2 mousePosition = Camera.main.ScreenToWorldPoint (new Vector3 (mouseX, mouseY, 0));
				
				//angle between attached object and mouse(in radians)
				angle = coordsToAngle(attachedTo.transform.position.y, mousePosition.y, attachedTo.transform.position.x, mousePosition.x);
				float x = polarToRectangular (angle, radius).x;
				float y = polarToRectangular (angle, radius).y;
				
				angleInDeg = radToDeg(angle);
				if (Mathf.Abs (this.transform.rotation.eulerAngles.z - angleInDeg) > threshold) {
					
					//flip weapon when above or below the player
					if ( (angleInDeg > 90) && (angleInDeg < 270) ) {
						flip ();
						
					}else { 
						unflip ();
					}
						
					transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angleInDeg));
					
					transform.position = new Vector2 (x + attachedTo.transform.position.x, y + attachedTo.transform.position.y);
					
				}

				////////////////Bullet Stuff////////////////
				if (Input.GetMouseButton (0)) {

				//checks wether its semiautomatic or not
				if ( (shootCoolDown <= Time.time) || (Input.GetMouseButtonDown(0) && semiautomatic) ) {
						//spawn the bullet
						bullet.GetComponent<BulletController> ().angle = angleInDeg;
						Instantiate (bullet, bulletSpawn.position, Quaternion.Euler (new Vector3 (0, 0, angleInDeg)));
						shootCoolDown = Time.time + fireRate; 
					}
				}
			}
            //time to reactivate hitboxes inorder to collide with the player
	        /**if (reactivateHitBox && (Time.time % 1 == 0) ) {
	            foreach (Collider2D collisionBox in GetComponents<Collider2D>())
	            {
	                collisionBox.enabled = true;
	            }
	            reactivateHitBox = false;
	        }**/
	
		}
		
		////////////////Math Stuff////////////////
		Vector2 polarToRectangular( float angle, float radius){
			//polar(angle and radius) --> rectangle(x and y) coordinates MUST BE IN RADIANS
			// x = rcos(theta)
			float x = radius * Mathf.Cos (angle) ;
			// y = rsin(theta)
			float y = radius * Mathf.Sin (angle) ;
			Vector2 result = new Vector2 (x, y);
			return result;  
		}


        public void pickup() {
            //destroy rigibbody and disable collider components
            //you can only destroy and recreate rigidbodies :/
			Destroy(GetComponent<Rigidbody2D>());
            foreach (Collider2D collisionBox in GetComponents<Collider2D>())
            {
                collisionBox.enabled = false;
            }
            
        }


        public void drop() {
           
            float angle2 = angleInDeg * Mathf.Deg2Rad;
            //throwing physics
            GameObject parent = transform.parent.gameObject;
            PlayerController parentController = parent.GetComponent<PlayerController>();
            gameObject.AddComponent<Rigidbody2D>();
            GetComponent<Rigidbody2D>().mass = mass;
        	
			GetComponent<Rigidbody2D>().AddForce(new Vector2(parentController.moveForce * 1.5f * (Mathf.Cos(angle2) ) , 
			parentController.moveForce * 1.5f * (Mathf.Sin(angle2)  ) ) );
			transform.position = new Vector3(transform.position.x + (teleportDistance * Mathf.Cos(angle)), transform.position.y + (teleportDistance * Mathf.Sin(angle)), 0);
			foreach (Collider2D collisionBox in GetComponents<Collider2D>())
			{
				collisionBox.enabled = true;
			}
            //reactivate colliders
            //reactivateHitBox = true;
			GetComponent<SpriteRenderer>().sortingOrder = 1;
            transform.parent = null;
   		}

		void flip(){
			//flip over y axis, also flips children
			transform.localScale = new Vector3( scale, -scale, scale );
			//character turns to the other side
			GetComponent<SpriteRenderer> ().sortingOrder = 0;
			flipped = true;
		}

		void unflip(){
			//flip back over y axis, also flips children
			transform.localScale = new Vector3( scale, scale, scale ); 
			flipped = false;
			GetComponent<SpriteRenderer> ().sortingOrder = 1;
		}

		float radToDeg(float rads){
			return ((angle - 135) * 180 / Mathf.PI) + 180.5F;
		} 
		
		float coordsToAngle( float y2, float y1, float x2, float x1){
			return Mathf.Atan2 (y2 - y1, x2 - x1) + 135;
		}
	}
