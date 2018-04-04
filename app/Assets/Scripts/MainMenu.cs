using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject choiceCanvas;
    public static bool audioOn = true;

    public GameObject audioButton;
    public Sprite audioOnSprite;
    public Sprite audioOffSprite;

    private SpriteState spriteState;


    void Start() {

        SetNotActiveChoiceCanvas();
    }

    private void Update() {
        
    }

    public void AudioButtonPress() {
        
        Image img = audioButton.GetComponent<Image>();

        if (audioOn) {

            gameObject.GetComponent<AudioSource>().mute = true;
            SetAudioOff();
            img.sprite = audioOffSprite;
        } else {

            gameObject.GetComponent<AudioSource>().mute = false;
            SetAudioOn();
            img.sprite = audioOnSprite;
        }

    }

    public void SetAudioOn() {

        audioOn = true;
    }

    public void SetAudioOff() {

        audioOn = false;
    }

    public void NextScene(int page) {

        SceneManager.LoadScene(page);
    }

    public void InstructionScene(int page) {

        SceneManager.LoadScene(page);
    }

    public void SetActiveChoiceCanvas() {

        choiceCanvas.SetActive(true);
    }

    public void SetNotActiveChoiceCanvas()
    {
        choiceCanvas.SetActive(false);
    }
}
