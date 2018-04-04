using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {

	public Transform lightPrefab;
	public Text scoreDisplay;

    private int playerScore = 0;
	private float timeSinceLastSpawn = 0.0f;
	private float timeToSpawn = 0.0f;

    private List<Transform> lights; // refers to the list of lights to be spawned
    private const int LIGHTS_POOL = 35; // the number of light objects

	void Start () {
        // this is during initialisation; the moment the game starts
		lights = new List<Transform>();

        // generate a light object and populate list
        for (int i = 0; i < LIGHTS_POOL; i++) {
			Transform oneLight = Instantiate(lightPrefab) as Transform;
			oneLight.parent = this.transform;
			lights.Add(oneLight);
		}

		SpawnLights();
		GameStart();
	}

    // Update is called once per frame
    void Update () {
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn >= timeToSpawn) {
            // lights will spawn if the time is ready to spawn
			SpawnLights();
		}
    }

	void SpawnLights() {
		timeSinceLastSpawn = 0.0f; // float is used for precision
		timeToSpawn = Random.Range (0.0f, 1.5f); // random time generated to spawn light

        foreach (Transform b in lights) {
			LightScript ls = b.GetComponent<LightScript>(); // getComponent is to retrieve the script for lights
			if (ls && !ls.isActive) {
                // if ls is true and is not active, we will activate the light object
				ls.Activate();
				break;
			}
		}
	}

    public int  GetPlayerScore() {
        return playerScore;
    }

    // method that will add points when player has tapped a green light
    public void AddPoints(int points=1) {
        playerScore += points;
        UpdateScoreDisplay();
	}

	void UpdateScoreDisplay() {
        scoreDisplay.text = playerScore.ToString() + " tapped!";
	}

	public void GameStart() {
		UpdateScoreDisplay();
	}

}
