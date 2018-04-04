using UnityEngine;
using System.Collections.Generic;

public class LightScript : MonoBehaviour {

	public List<Sprite> lightImages;
    public Sprite greenLightImage;
    public AudioClip sound;

    private bool easyMode = Difficulty.easyMode;

    // lights will be by default, inactive and not green
    public bool isActive = false;
	public bool isGreen = false;
    public bool greenPopped = false;

	// This will be for initialisation
	void Start () {
		ResetPosition();
        gameObject.AddComponent<AudioSource>();
        Source.clip = sound;
        Source.playOnAwake = false;
    }

    private AudioSource Source {
        get {
          return GetComponent<AudioSource>();
        }
    }

    public void MakeGreen() {
		isGreen = true;
        this.GetComponent<SpriteRenderer>().sprite = greenLightImage;
    }
    
	public void MakeNotGreen() {
		isGreen = false;
		int lightChoice = Random.Range (0, lightImages.Count);
		this.GetComponent<SpriteRenderer>().sprite = lightImages[lightChoice];
	}
    
	void ResetPosition() {
        // the position that is outside of the screen
		this.transform.position = new Vector3(0.0f, -10.0f, 0.0f);
	}

	void Update() {
		if (this.transform.position.y > 6.0f && isActive) {
            // this is when the light has went out of the screen
			Deactivate();
		}
	}

	public void Activate() {
        float upSpeed;
        isActive = true;

        // speed of the light depends on the difficulty
        if (easyMode) {
            upSpeed = Random.Range(1.0f, 2.5f); // light speeds are random from 1 to 2.5
        } else {
            upSpeed = Random.Range(2.0f, 6.5f); // light speeds are random from 2 to 6.5
        }
		
		this.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, upSpeed, 0.0f);
		this.transform.position = new Vector3(Random.Range (-7.4f, 5.45f), -6.0f, 0.0f); 
        // this makes the lights look like they're moving by changing the position
        
        // the condition to either make the light green or otherwise
		if (Random.Range(0,4) == 0) {
			MakeGreen();
		} else {
			MakeNotGreen ();
		}
	}

	public void Deactivate() {
        // make the light disappear
		isActive = false;
		this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		ResetPosition();
	}

	void OnMouseDown() {
		Pop ();
	}

	public void Pop() {
        Source.PlayOneShot(sound);
        Deactivate();
        if (isGreen) {
            // this calls the game controller in the parent to add a point
            this.transform.parent.GetComponentInParent<GameControllerScript>().AddPoints(1);
        } 
	}
}
