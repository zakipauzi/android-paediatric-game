using UnityEngine.SceneManagement;
using UnityEngine;

public class CompleteLightGame : MonoBehaviour {

	Animation anim;
	AudioSource successSource; 
    GameControllerScript gc;
    SpriteRenderer sprender;
	bool played = false;
	bool showIcon = true;

	void Start(){
        // linking the variables to the components
		anim = GetComponent<Animation> ();
		successSource = GetComponent<AudioSource> ();
        gc = gameObject.GetComponentInParent<GameControllerScript>();
        sprender = GetComponent<SpriteRenderer>();
        // ensuring the well done is not played yet
        sprender.enabled = false;
		successSource.playOnAwake = false;
	}

	void Update(){
        // this will always check if player has won the game
        successDelegate methodToUse = CheckSuccess;
        played = methodToUse.Invoke();

        if (played) {
            if (showIcon) {
                // makes the sprite renderer visible
                sprender.enabled = true;
                // play the animation
                anim.Play("wellDone");
                // stop the animation after once played
                showIcon = false;
				// play the success audio
				successSource.Play ();

            }
            // Invoke allows a certain waiting time
            Invoke("PlayerWon", 3);
        }
    }

    void PlayerWon() {
        SceneManager.LoadScene("4-cutscene");
    }

    delegate bool successDelegate();

    bool CheckSuccess() {
        if (gc.GetPlayerScore() == 10) {
            return true;
        }
        return false;
    }
}
