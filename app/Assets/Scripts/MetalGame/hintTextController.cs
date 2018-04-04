using UnityEngine;
using UnityEngine.UI;

public class hintTextController : MonoBehaviour {

	private Text hint;
	private int process = 0;	// 0 is inital state, 1 is playing state, 2 is complete state

	// Use this for initialization
	void Start () {
		hint = this.gameObject.GetComponent<Text>();
		hint.text = "Remove the metal items!";	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (process == 0) {
			Invoke ("initText",3);
		}

		if (Difficulty.easyMode) {
			if (process == 1) {

				int temp = CompleteMetalGame.NumberItems;
				if(temp == 1){
					hint.text = temp + " item left!";
				}
				if (temp > 1) {
					hint.text = temp + " items left!";
				}
			}
		} else {
			if (process == 1) {
				Invoke ("cleanText", 1);
				process = 2;
			}
		}

		if (CompleteMetalGame.NumberItems == 0) {
			process = 2;
			hint.text = "Congratulations!";
		}
	}

	// clean the hint text
	private void cleanText(){
		hint.text = "";
	}

	// process from 0 to 1
	private void initText(){
		process = 1;
	}
		
}
