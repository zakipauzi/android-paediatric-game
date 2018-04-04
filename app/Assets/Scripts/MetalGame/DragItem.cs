using UnityEngine;

public class DragItem : MonoBehaviour {
	private float decaySecond = 1f;
	private float elapsedSecond = 0;
	private float distance = 10;
	private bool flag = false;
	private AudioSource clickSource;
	public AudioClip clickSound;


	// Use this before initialization to add AudioSouce to every item
	void Awake(){
		this.gameObject.AddComponent<AudioSource> ();
		clickSource = this.gameObject.GetComponent<AudioSource> ();
		clickSource.clip = clickSound;
		clickSource.playOnAwake = false;
	}


	// active when drag the items
	void OnMouseDrag(){
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distance);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);
		transform.position = objPosition;
	}


	// click the item, then play the sound
	void OnMouseDown(){
		clickSource.Play ();
	}


	// When we relase the mouse, then the item is going to destroy
	void OnMouseUp () {
		flag = true;
		Destroy(this.gameObject, decaySecond); // destroy the object once it is dragged off
		CompleteMetalGame.NumberItems --;

	}

	// Update is called once per frame
	void Update(){
		if (flag) {
			float scaleRate = (decaySecond - elapsedSecond) / decaySecond;
			transform.localScale *= scaleRate;
			elapsedSecond += Time.deltaTime;
		}
			
	}
}
