using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    public static int frames;
    static AudioController audioController;

	void Start () {

        audioController = GameObject.FindObjectOfType<AudioController>();

        if(PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            AudioListener.volume = 1;
        } else
        {
            AudioListener.volume = 0;
        }

        GetComponent<AudioSource>().time = frames/60;
        GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
        frames++;
        if (frames > 165 * 60) { frames = 0; }
	}
}
