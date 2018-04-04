using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class FilmsGameEditModeTest {

	private GameObject b1, b2, b3, b4, b5, b6, b7, b8, script, panel, scrollPanel, gameController;
	private GameObject[] buttons;

	[SetUp]
	public void Init() {
		EditorSceneManager.OpenScene("Assets/Scenes/11-films-game.unity", OpenSceneMode.Single);

		b1 = GameObject.Find("Button");
		b2 = GameObject.Find("Button (1)");
		b3 = GameObject.Find("Button (2)");
		b4 = GameObject.Find("Button (3)");
		b5 = GameObject.Find("Button (4)");
		b6 = GameObject.Find("Button (5)");
		b7 = GameObject.Find("Button (6)");
		b8 = GameObject.Find("Button (7)");
		script = GameObject.Find("Script");
		panel = GameObject.Find("Panel");
		scrollPanel = GameObject.Find("scrollPanel");
		gameController = GameObject.Find ("GameController");

		buttons = new GameObject[]{b1,b2,b3,b4,b5,b6,b7,b8};

	}

    [Test]
	// Test all components are in the scene

    public void testAllComponentsPresent(){ 
        foreach(GameObject b in buttons) {
            Assert.IsNotNull(b);
        }
        Assert.IsNotNull(script);
        Assert.IsNotNull(panel);
        Assert.IsNotNull(scrollPanel);
		Assert.IsNotNull(gameController);
    }

	[Test]
	// Test scrollPanel initial position

	public void testScrollPanelInitPosition(){
		
		Assert.AreEqual (-600, scrollPanel.GetComponent<RectTransform> ().anchoredPosition.x);
		Assert.AreEqual (0, scrollPanel.GetComponent<RectTransform> ().anchoredPosition.y);

	}

	[Test]
	// Test all buttons initial position

	public void testAllButtonsInitPosition(){
		int i = 0;
		foreach(GameObject bttn in buttons){
			Assert.AreEqual (i, bttn.GetComponent<RectTransform> ().anchoredPosition.x);
			Assert.AreEqual (0, bttn.GetComponent<RectTransform> ().anchoredPosition.y);
			i += 600;
		}
	}

	[Test]
	// Test some gameobjects have their scripts

	public void testGameObjectContainsScripts(){
		Assert.IsNotNull(script.GetComponent<GoToNextSceneFilm> ());
		Assert.IsNotNull(gameController.GetComponent<ScrollRectSnap_CS> ());
		foreach(GameObject bttn in buttons){
			Assert.IsNotNull (bttn.GetComponent<ClickSound>());
		}
	}

}
