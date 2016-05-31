using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour {
	public GameObject singlePlayerButtonText;
	public GameObject multiplayerButtonText;
	public GameObject optionsButtonText;

	// Use this for initialization
	void Start () {
	
	}
	
	public void startSinglePlayer(){
		SceneManager.LoadScene ("test_flat");
	}

	public void startMultiplayer(){
		multiplayerButtonText.GetComponent<Text>().text = "Under Construction";
	}

	public void startOptions(){
		optionsButtonText.GetComponent<Text>().text = "Under Construction";
	}
}
