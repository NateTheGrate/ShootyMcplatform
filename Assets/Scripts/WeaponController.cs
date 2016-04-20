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
	public GameObject bullet;
	public float bulletOffsetX = 0;
	public float bulletOffsetY = 0;
	public float fireRate = 30;
	public float radius = 1;
	public double threshold = 1;

	private int ticks;
	// Use this for initialization
	void Start () {
		ticks = 0;
	}
	
	// Update is called once per frame
	void Update () {
			if (attachedTo != null) {
				float mouseX = Input.mousePosition.x;
				float mouseY = Input.mousePosition.y;
				Vector2 mousePosition = Camera.main.ScreenToWorldPoint (new Vector3 (mouseX, mouseY, 0));
				//angle between attached object and mouse(in radians)
				angle = Mathf.Atan2 (attachedTo.transform.position.y - mousePosition.y, attachedTo.transform.position.x - mousePosition.x) + 135;
				
				//polar(angle and radius) --> rectangle(x and y) coordinates MUST BE IN RADIANS
				// x = rcos(theta)
				float x = radius * Mathf.Cos (angle) ;
				// y = rsin(theta)
				float y = radius * Mathf.Sin (angle) ;
				angleInDeg = ((angle - 135) * 180 / Mathf.PI) + 185;
				if (Mathf.Abs (this.transform.rotation.eulerAngles.z - angleInDeg) > threshold) {
				
					//flip weapon
				if ( (angleInDeg > 90) && (angleInDeg < 270) ) {
					GetComponent<SpriteRenderer> ().flipY = true;
					GetComponent<SpriteRenderer> ().sortingOrder = 0;
					flipped = true;
				}else { 
					GetComponent<SpriteRenderer> ().flipY = false; 
					flipped = false;
					GetComponent<SpriteRenderer> ().sortingOrder = 1;
				}
					
					transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angleInDeg));
			
					transform.position = new Vector2 (x + attachedTo.transform.position.x, y + attachedTo.transform.position.y);

				}
				if (Input.GetMouseButton (0)) {
				
					if (ticks >= fireRate) {
						bullet.GetComponent<BulletController> ().parent = this.gameObject;
						Vector2 spawnPosition = new Vector2 (0, 0, 0);
						spawnPosition = new Vector2(transform.position.x +  (bulletOffsetX * Mathf.Sign( Mathf.Cos(angle) ) + Mathf.Cos(angle) )  , transform.position.y  + ( bulletOffsetY * Mathf.Sign( Mathf.Sin(angle) ) + Mathf.Sin(angle) ) );
						Instantiate (bullet, spawnPosition, Quaternion.Euler (new Vector3 (0, 0, angleInDeg)));
				
						ticks = 0;
					}
				}
			}
			ticks++;
		}

	}
