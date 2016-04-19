using UnityEngine;
using System.Collections;

public class FartScript : MonoBehaviour {

	AudioSource audio;
	float tempo;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		tempo = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - tempo) > 1)
		{
			audio.PlayOneShot(audio.clip);
			tempo = Time.time;
		}
	}
}
