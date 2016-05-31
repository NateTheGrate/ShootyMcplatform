using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour {
	public GameObject singlePlayerButtonText;
	public GameObject multiplayerButtonText;
	public GameObject optionsButtonText;

	private AudioSource source;
	public AudioClip nope;
	public AudioClip success;

	// Use this for initialization

	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	public void startSinglePlayer(){
		source.PlayOneShot (success, 1);
		SceneManager.LoadScene ("test_flat");

	}

	public void startMultiplayer(){
		multiplayerButtonText.GetComponent<Text>().text = "Under Construction";
		source.PlayOneShot (nope, 1);
	}

	public void startOptions(){
		optionsButtonText.GetComponent<Text>().text = "Under Construction";
		source.PlayOneShot (nope, 1);
	}
}
