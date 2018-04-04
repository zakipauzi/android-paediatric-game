using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundScript : MonoBehaviour {

    private AudioController audioController;

    public GameObject audioButton;
    public Sprite audioOnSprite;
    public Sprite audioOffSprite;

    // Use this for initialization
    void Start () {

        audioController = GameObject.FindObjectOfType<AudioController>();
        UpdateIcon();
	}
	
    public void ChangeMusic() {

        audioController.ToggleSound();
        UpdateIcon();
    }

	void UpdateIcon() {
        if (PlayerPrefs.GetInt("Muted") == 0)
        {
            AudioListener.volume = 1;
            audioButton.GetComponent<Image>().sprite = audioOnSprite;
        } else
        {
            AudioListener.volume = 0;
            audioButton.GetComponent<Image>().sprite = audioOffSprite;
        }

    }
}
