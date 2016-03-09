using UnityEngine;
using System.Collections;

public class BoxSpawner : MonoBehaviour {
	public int amount = 10;
	public float xRange;
	public float yRange;
	public GameObject cameraGameObject;
	public GameObject objectToSpawn;
	// Use this for initialization
	void Start () {
		
		for (int i = 0; i < amount; i++) {
			Instantiate(objectToSpawn, new Vector2( this.transform.position.x + Random.Range(-xRange, xRange), this.transform.position.y + Random.Range(-yRange, yRange) ), Quaternion.identity  );
		} 
	}
	
	// Update is called once per frame
	void Update () {
		Camera camera = cameraGameObject.GetComponent<Camera> ();
		Vector2 mousePosition = camera.ScreenToWorldPoint (Input.mousePosition);
		if (Input.GetButton("Fire1")) {
				Instantiate(objectToSpawn, mousePosition, Quaternion.identity  );
		}
	}
}
