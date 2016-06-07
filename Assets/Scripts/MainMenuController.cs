using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour {
	public GameObject singlePlayerButtonText;
	public GameObject multiplayerButtonText;
	public GameObject optionsButtonText;
	public GameObject titleText;

	private float colorChangeDelay = 0;

	private AudioSource source;
	public AudioClip nope;
	public AudioClip start;

	private float startDelay;
	private bool nextLevel = false;
	// Use this for initialization

	void Start () {
		source = GetComponent<AudioSource> ();
	}


	void Update(){
		
		if ( nextLevel && startDelay <= Time.time ) {
			SceneManager.LoadScene ("test_flat");
		}

		//change letter color in title
		if (colorChangeDelay <= Time.time) {
			//titleText.GetComponent<Text> ().color
			colorChangeDelay = 1 + Time.time;
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


	//color stuff for title
	Color nextTitleColor(){
		
		if (titleText.GetComponent<Text> ().color == Color.red) {
			return Color.Lerp (Color.red, Color.blue, 2);	
		}

		if (titleText.GetComponent<Text> ().color == Color.blue) {
			return Color.Lerp (Color.red, Color.green, 2);
		}

		if (titleText.GetComponent<Text> ().color == Color.green) {
			return Color.Lerp (Color.red, Color.red, 2);
		} 
		return titleText.GetComponent<Text>().color;
	}

}
