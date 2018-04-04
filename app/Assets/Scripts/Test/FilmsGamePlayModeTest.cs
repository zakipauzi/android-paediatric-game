using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FilmsGamePlayModeTest {

	[SetUp]
	public void Init(){
		
		SceneManager.LoadScene("11-films-game", LoadSceneMode.Single);
	}

	[UnityTest]
	// Test for loading the current scene 

	public IEnumerator testLoadScene(){
		Scene currentScene = SceneManager.GetActiveScene ();
		Assert.AreEqual ("11-films-game",currentScene.name);
		yield return null;
	}


	[UnityTest]
	// Test for scrollPanel movement 

	public IEnumerator testScrollPanelToPosition(){ 

		GameObject scrollPanel = GameObject.Find("scrollPanel");

		RectTransform scrollPanelPositionNow = scrollPanel.GetComponent<RectTransform>();
		scrollPanelPositionNow.anchoredPosition = new Vector2(-1100, scrollPanelPositionNow.anchoredPosition.y);

		Assert.AreEqual (-1100, scrollPanelPositionNow.anchoredPosition.x); // now scrollPanel is located at -1100

		yield return null;

		ScrollRectSnap_CS srs = GameObject.Find("GameController").GetComponent<ScrollRectSnap_CS> ();

		srs.EndDrag (); // if we drag panel at -1100 and we end dragging.

		yield return new WaitForSeconds (2); // It will slowly move to -1200, not directly

		UnityEngine.Assertions.Assert.AreApproximatelyEqual (-1200,scrollPanelPositionNow.anchoredPosition.x,2f);

		yield return null;

	}


	[UnityTest]
	// Test for clicking the button with sound 

	public IEnumerator testClickButtonWithSound(){		

		GameObject obj = GameObject.Find ("Button (1)");	// test Button 1 works
		Button bc =  obj.GetComponent<Button> ();

		bc.onClick.Invoke ();

		ClickSound cs = obj.GetComponent<ClickSound> ();

		Assert.AreEqual (true, cs.isActiveAndEnabled);	// the ClickSound script is actived

		yield return null;
	}


	[UnityTest]
	// Test for clicking the button and move to next scene

	public IEnumerator testClickButtonToNextScene(){

		GameObject obj = GameObject.Find ("Button (2)");	// test Button 2 works
		Button bc =  obj.GetComponent<Button> ();

		bc.onClick.Invoke ();

		yield return null;

		Scene currentScene = SceneManager.GetActiveScene ();
		Assert.AreEqual ("12-cutscene",currentScene.name);	// the next scene is 12-cutscene

		yield return null;
	}

}
