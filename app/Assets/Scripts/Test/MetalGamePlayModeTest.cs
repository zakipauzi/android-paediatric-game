using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class MetalGamePlayModeTest {

	private GameObject whistle, brooch, keyHolder, watch, belt, wellDone;
	private GameObject[] items;

	[SetUp]
	public void Init(){
		SceneManager.LoadScene("5-metal-game", LoadSceneMode.Single);

	}


	[UnityTest]
	// Test for loading the current scene 

	public IEnumerator testLoadScene(){
		Scene currentScene = SceneManager.GetActiveScene ();
		Assert.AreEqual ("5-metal-game",currentScene.name);
		yield return null;
	}

	[UnityTest]
	// Test for remove all items with click sound

	public IEnumerator testRemoveAllItems()
	{
		items = GameObject.FindGameObjectsWithTag("item");
		whistle = GameObject.Find("whistle");
		brooch = GameObject.Find("brooch");
		keyHolder = GameObject.Find("key holder");
		watch = GameObject.Find("watch");
		belt = GameObject.Find("belt");

		foreach (GameObject obj in items) {
			obj.GetComponent<DragItem> ().Invoke("OnMouseDown",0);
			obj.GetComponent<DragItem> ().Invoke("OnMouseUp",0);
		};

		GameObject.Find("wellDone").GetComponent<AudioSource> ().volume = 0;	// mute the audioSouce

		yield return new WaitForSeconds (1f);

		Assert.IsTrue (brooch.Equals(null));
		Assert.IsTrue (watch.Equals(null));
		Assert.IsTrue (whistle.Equals(null));
		Assert.IsTrue (keyHolder.Equals(null));
		Assert.IsTrue (belt.Equals(null));

		yield return null;
	}

	[UnityTest]
	// Test for play WellDone animation & audio, and check if game is success when all items are removed

	public IEnumerator testPlayWellDone(){
		items = GameObject.FindGameObjectsWithTag("item");
		wellDone = GameObject.Find("wellDone");
		wellDone.GetComponent<AudioSource> ().volume = 0;	// mute the audioSouce

		foreach (GameObject obj in items) {
			obj.GetComponent<DragItem> ().Invoke("OnMouseUp",0);
		};
		yield return new WaitForSeconds (0.5f);
		Assert.IsTrue(CompleteMetalGame.checkSuccess ());
		Assert.IsTrue(wellDone.GetComponent<Animation> ().IsPlaying("wellDone"));
		Assert.IsTrue(wellDone.GetComponent<AudioSource> ().isPlaying);
		yield return null;
	}



	[UnityTest]
	//Test for complete the game and move to next scene

	public IEnumerator testCompleteGameToNextScene(){
		GameObject.Find("wellDone").GetComponent<CompleteMetalGame> ().Invoke("nextScene",0);
		yield return null;
		Scene currentScene = SceneManager.GetActiveScene ();
		Assert.AreEqual ("6-cutscene",currentScene.name);	// the next scene is 12-cutscene

		yield return null;
	}
}
