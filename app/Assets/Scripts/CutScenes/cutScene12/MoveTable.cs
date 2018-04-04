using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTable : MonoBehaviour {
	private int flag = 0; 
	private float speed = 1;
	private Transform tr;
//	private float distance;

	// Use this for initialization

	void Start () {
//		distance = 4.2f - 0.5f;
		tr = gameObject.GetComponent<Transform> ();
	}

	// Update is called once per frame

	void Update () {
		float xpos = tr.localPosition.x;
		if (flag == 1) {
			if (xpos <= 1.53f) {
				gameObject.transform.Translate (Vector3.right * Time.deltaTime * speed);
//			} else {
//				new WaitForSecondsRealtime (2);
//				flag = 2;
			}
		} else if (flag == 2) {
			if (xpos >= -2.2f) {
				tr.Translate (Vector3.left * Time.deltaTime * speed);
			}
		}
	}

	delegate void setTableFlagDelegate();

	public void setTableFlag(int i){
		flag = i;
	}
		
}
