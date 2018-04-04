using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[TestFixture]
public class ScanGamePlayModeTest {

	private GameObject cover, buttonContinue, panel;


	[SetUp]
	public void Init(){
		SceneManager.LoadScene("13-scan-game", LoadSceneMode.Single);
	}

	[UnityTest]
	// Test for loading the current scene 

	public IEnumerator testLoadScene(){
		Scene currentScene = SceneManager.GetActiveScene ();
		Assert.AreEqual ("13-scan-game",currentScene.name);
		yield return null;
	}


	[UnityTest]
	// Test for drawing on the canvas and update percentage

	public IEnumerator testDrawOnTheCanvas(){
		DrawCanvas drawCanvas = GameObject.Find ("Cover").GetComponent<DrawCanvas> ();

		Assert.AreNotEqual("Scan Complete!", drawCanvas.textPercent.text);

		for (float i = -2.3f; i < 1.4f; i += 0.4f) {
			for (float j = 1.6f; j > -2f; j -= 0.4f) {
				drawCanvas.BrushPlace (new Vector2 (i,j));
				yield return new WaitForSecondsRealtime (0.05f);
				drawCanvas.Invoke ("UpdatePercent",0);
			}
		}

		Assert.AreEqual("Scan Complete!", drawCanvas.textPercent.text);	// When game is completed, textPercentage shows "Scan Complete!"

		yield return null;
	}



	[UnityTest]
	// Test for activing the continue Button and move to next scene 

	public IEnumerator testClickButtonToNextScene(){
//		panel = GameObject.Find ("Panel");
//		buttonContinue = panel.transform.Find ("ButtonContinue").gameObject;
//		buttonContinue.SetActive (true);
//		buttonContinue.GetComponent<Button> ().onClick.Invoke();
		DrawCanvas drawCanvas = GameObject.Find ("Cover").GetComponent<DrawCanvas> ();
		drawCanvas.Invoke ("NextScene",0);
		yield return null;

		Scene currentScene = SceneManager.GetActiveScene ();
		Assert.AreEqual ("14-cutscene",currentScene.name);

		yield return null;
	}
		
}
