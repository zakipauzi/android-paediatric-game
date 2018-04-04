using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlassDrag : MonoBehaviour {

    public bool canDrag = true;
    public AudioClip Glass_drag;
    public AudioClip Glass_drop;
    public Text hint;

    private bool atPosition = false;
    private float distance = 10;
    private GameObject obj = null;
    private GameObject glassToBe;
    private Collider2D glassCollider;
    private bool didAudioPlay = false;

    private bool easyMode = Difficulty.easyMode;

    void Start() {

        hint.text = "Drag the glass to the Water Dispenser";
        glassToBe = GameObject.Find("glassCollider");
        glassCollider = glassToBe.GetComponent<Collider2D>();
    }

    void OnMouseDrag() {

        if (canDrag) { 
            obj = GetComponent<GameObject>();
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;

            if (!didAudioPlay) {
                AudioSource sound = gameObject.GetComponent<AudioSource>();
                sound.PlayOneShot(Glass_drag, 0.1F);
                didAudioPlay = true;
            }
        }
    }

    void OnMouseUp() {

        if (canDrag) {
            AudioSource sound = gameObject.GetComponent<AudioSource>();
            sound.PlayOneShot(Glass_drop, 0.4F);
            didAudioPlay = false;
        }

        if (gameObject.GetComponent<Collider2D>().IsTouching(glassCollider)) {

            hint.text = "Press the Button to pour water";

            canDrag = false;
            atPosition = true;
            gameObject.transform.position = new Vector3(glassToBe.transform.position.x, glassToBe.transform.position.y, gameObject.transform.position.z);
        }
    }

    public bool IsAtPosition() {

        return atPosition;
    }

    public bool ReturnCanDrag() {
        return canDrag;
    }
}
