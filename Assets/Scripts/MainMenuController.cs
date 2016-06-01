﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour {
	public GameObject singlePlayerButtonText;
	public GameObject multiplayerButtonText;
	public GameObject optionsButtonText;

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
}