using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class MetalGameEditModeTest {

	private GameObject bg, child, whistle, brooch, keyHolder, watch, belt, wellDone,hint;
	private GameObject[] items;

	[SetUp]
	public void Init(){
		EditorSceneManager.OpenScene("Assets/Scenes/5-metal-game.unity", OpenSceneMode.Single);

		child = GameObject.Find("child");
		whistle = GameObject.Find("whistle");
		brooch = GameObject.Find("brooch");
		keyHolder = GameObject.Find("key holder");
		watch = GameObject.Find("watch");
		belt = GameObject.Find("belt");
		wellDone = GameObject.Find("wellDone");
		hint = GameObject.Find ("hint");

		items = GameObject.FindGameObjectsWithTag("item");

	}


	[Test]
	// Test all components are in the scene

	public void testAllComponentsPresent(){
		Assert.IsNotNull (child);
		Assert.IsNotNull (whistle);
		Assert.IsNotNull (brooch);
		Assert.IsNotNull (keyHolder);
		Assert.IsNotNull (watch);
		Assert.IsNotNull (belt);
		Assert.IsNotNull (wellDone);
		Assert.IsNotNull (hint);

	}

	[Test]
	// Test all components at the initial position

	public void testComponentsInitPosition(){
		// items are placed mannually, so the position is not that accuate
		approximatelyEqual (-0.0125f,whistle.GetComponent<Transform>().localPosition.x);
		approximatelyEqual (-0.875f,whistle.GetComponent<Transform>().localPosition.y);
		approximatelyEqual (0.8875f,brooch.GetComponent<Transform>().localPosition.x);
		approximatelyEqual (-1.975f,brooch.GetComponent<Transform>().localPosition.y);
		approximatelyEqual (-0.8625f,keyHolder.GetComponent<Transform>().localPosition.x);
		approximatelyEqual (-3.4125f,keyHolder.GetComponent<Transform>().localPosition.y);
		approximatelyEqual (-1.6375f,watch.GetComponent<Transform>().localPosition.x);
		approximatelyEqual (-2.75f,watch.GetComponent<Transform>().localPosition.y);
		approximatelyEqual (0f,belt.GetComponent<Transform>().localPosition.x);
		approximatelyEqual (-3.08f,belt.GetComponent<Transform>().localPosition.y);

		Assert.AreEqual (0,child.GetComponent<Transform>().position.x);
		Assert.AreEqual (0,child.GetComponent<Transform>().position.y);
		Assert.AreEqual (-5,wellDone.GetComponent<Transform>().position.x);
		Assert.AreEqual (2,wellDone.GetComponent<Transform>().position.y);
		Assert.AreEqual (-250,hint.GetComponent<RectTransform>().anchoredPosition.x);
		Assert.AreEqual (-94, hint.GetComponent<RectTransform> ().anchoredPosition.y);

	}

	private void approximatelyEqual(float a, float b){
		UnityEngine.Assertions.Assert.AreApproximatelyEqual (a, b, 1f);
	}



	[Test]
	// Test some gameobjects have their scripts

	public void testGameObjectContainsScripts(){
		Assert.IsNotNull(child.GetComponent<playMetalGame> ());
		Assert.IsNotNull(wellDone.GetComponent<CompleteMetalGame> ());
		Assert.IsNotNull(hint.GetComponent<hintTextController> ());

		foreach(GameObject item in items){
			Assert.IsNotNull (item.GetComponent<DragItem>());
		}
	}
		

}
