using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateScanner : MonoBehaviour {
	
	private int flag = 0; 

	// flag = 0 stop all actions
	// flag = 1 close the scanner
	// flag = 2 open the scanner
	// flag = 3 rotate the scanner;

//	private float speed = 0.1f;
//	private Transform tr;
	private string upDown = "";

	// Use this for initialization
	void Start () {
		upDown = gameObject.name;
//		tr = GameObject.Find (upDown).GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
//		float ypos = tr.position.y;
		if (flag == 1) {
//			if (upDown.Equals("up")) {
//				objectMoveDown (ypos, 0.22f);
//			} else if (upDown.Equals("down")) {
//				objectMoveUp (ypos, -2.8f);
//				Debug.Log (ypos);
//			}

			Animator aim = GetComponent<Animator> ();
			if (upDown.Equals("up")) {
				aim.SetInteger ("state", 1);
			} else if (upDown.Equals("down")) {
				aim.SetInteger("state",1);
			}
		} else if (flag == 2) {
//			if (upDown.Equals("up")) {
//				objectMoveUp (ypos, 0.6f);
//			} else if (upDown.Equals("down")) {
//				objectMoveDown (ypos, -3.2f);
//			}
			Animator aim = GetComponent<Animator> ();
			if (upDown.Equals("up")) {
				aim.SetInteger ("state", 3);
			} else if (upDown.Equals("down")) {
				aim.SetInteger("state",3);
			}


		} else if (flag == 3) {
			Animator aim = GetComponent<Animator> ();
			if (upDown.Equals("up")) {
				aim.SetInteger ("state", 2);
			} else if (upDown.Equals("down")) {
				aim.SetInteger("state",2);
			}	
		}
		
	}

//	void objectMoveUp(float pos,float limit){
//		if (pos < limit) {
//			tr.Translate (Vector3.up * Time.deltaTime * speed);
//		}
//	}
//
//	void objectMoveDown(float pos,float limit){
//		if (pos > limit) {
//			tr.Translate (Vector3.down * Time.deltaTime * speed);
//		}
//	}


	public void setScannerFlag(int i){
		flag = i;
	}
}
