using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clockdeath : MonoBehaviour {

    public int deathtime = 120;
    int time;

	void Start () {
        time = 0;
	}

	void Update () {
		if (time > deathtime)
        {
            gameObject.SetActive(false);
        }
        else
        {
            time++;
        }
	}
}
