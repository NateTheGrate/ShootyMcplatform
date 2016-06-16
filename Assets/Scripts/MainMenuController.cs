using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour {
	public GameObject singlePlayerButtonText;
	public GameObject multiplayerButtonText;
	public GameObject optionsButtonText;
	public GameObject titleText;
	public GameObject creditText;
	public GameObject camera;
	public GameObject mouse;
	public GameObject canvas;
	public GameObject canvas2;

	public GameObject[] weapons;

	private AudioSource source;
	public AudioClip nope;
	public AudioClip start;
	public Gradient g;

	private float spawnDelay = 0;
	private float startDelay;
	private bool nextLevel = false;
	private float strobeDuration = 4f;
	private Vector3 velocity = Vector3.zero;
	// Use this for initialization

	void Start () {
		source = GetComponent<AudioSource> ();

	}


	void Update(){

		if (mouse != null) {
			mouse.transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		}
		if ( nextLevel && startDelay <= Time.time ) {
			SceneManager.LoadScene ("test_flat");
		}
		//changing title colors	
		float t = Mathf.PingPong(Time.time/ strobeDuration, 1f);
		titleText.GetComponent<Text> ().color = g.Evaluate (t);
		creditText.GetComponent<Text> ().color = g.Evaluate (t);
		//rotating title
		titleText.GetComponent<RectTransform> ().eulerAngles = new Vector3( 0.0f, 0.0f, (t - 0.5f) * 30);
		creditText.GetComponent<RectTransform> ().eulerAngles = new Vector3( 0.0f, 0.0f, (t - 0.5f) * 30);
		//falling weapons
		if (spawnDelay <= Time.time) {
			int index = Random.Range (0, weapons.Length);
			Instantiate (weapons[index], new Vector3 (Random.Range (-10.3f,  31.74f), 7, 0.0f), Quaternion.Euler(new Vector3(0.0f, 0.0f, Random.Range(-170, 170)) ) );
			index = Random.Range (0, weapons.Length);
			Instantiate (weapons[index], new Vector3 (Random.Range (-10.3f,  31.74f), 7, 0.0f), Quaternion.Euler(new Vector3(0.0f, 0.0f, Random.Range(-170, 170)) ) );
			spawnDelay = 0.1f + Time.time;
		}	
	}

	public void startSinglePlayer(){
		source.PlayOneShot (start, 2);
		//start transition into next scene
		nextLevel = true;
		startDelay = start.length + Time.time;
	}

	public void startMultiplayer(){
		multiplayerButtonText.GetComponent<Text>().text = "Under Construction";
			source.PlayOneShot (nope, 1);

	}

	public void startOptions(){
		optionsButtonText.GetComponent<Text>().text = "Under Construction";
			source.PlayOneShot (nope, 1);
	}

	public void startCredits(){
		//canvas.GetComponent<Canvas> ().renderMode = RenderMode.WorldSpace;
		camera.transform.position = new Vector3 (21.29f, 0.0f, camera.transform.position.z);
		//canvas2.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceCamera;
	}
	public void toggleCredits(){
		//canvas2.GetComponent<Canvas> ().renderMode = RenderMode.WorldSpace;
		camera.transform.position = new Vector3 (0.0f, 0.0f, camera.transform.position.z);
		//canvas.GetComponent<Canvas> ().renderMode = RenderMode.ScreenSpaceCamera;
	}

}
