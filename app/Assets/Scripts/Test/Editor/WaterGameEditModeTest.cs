using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class WaterGameEditModeTest {

    private GameObject glassCollider, water_dispenser, button, rectangle, glass, water1,
            water2, water3, water4, water5, water6, wellDone, water_B;
    private GameObject[] gameobjArray;

    [SetUp] //Initialises all objects and the scene for rest of the tests
    public void Init() {
        EditorSceneManager.OpenScene("Assets/Scenes/15-water-game.unity", OpenSceneMode.Single);

        glassCollider = GameObject.Find("glassCollider");
        water_dispenser = GameObject.Find("water_dispenser");
        button = GameObject.Find("button");
        rectangle = GameObject.Find("rectangle");
        glass = GameObject.Find("glass");
        water1 = GameObject.Find("water1");
        water2 = GameObject.Find("water2");
        water3 = GameObject.Find("water3");
        water4 = GameObject.Find("water4");
        water5 = GameObject.Find("water5");
        water6 = GameObject.Find("water6");
        wellDone = GameObject.Find("wellDone");
        water_B = GameObject.Find("water_B");

        gameobjArray = new GameObject[] {glassCollider, water_dispenser, button, rectangle, glass, water1,
            water2, water3, water4, water5, water6, wellDone, water_B};
    }

    [Test] // Test to check for the game not to start in a completed state
    public void GameDoesNotGoToWellDoneStraightAway() {

        ButtonClick script = GameObject.Find("wellDone").GetComponent<ButtonClick>();

        Assert.IsFalse(script.GetGameComplete());
    }

    [Test]
    public void AllObjectsPresent() { // to check that all the objects in the scene are there

        foreach (GameObject gameobject in gameobjArray) {

            Assert.IsNotNull(gameobject);
        }

    }

    [Test]  //Checks each object contains their scripts
    public void AllObjectsContainTheirScripts() {

        Assert.IsNotNull(button.GetComponent<OnMouseOverButton>());
        Assert.IsNotNull(button.GetComponent<ButtonClick>());
        Assert.IsNotNull(glass.GetComponent<GlassDrag>());
        Assert.IsNotNull(wellDone.GetComponent<ButtonClick>());
        Assert.IsNotNull(wellDone.GetComponent<GameComplete>());
    }

    [Test]  //Checks each object contain their sound files
    public void AllAudioSourcesArePresent() {

        Assert.IsNotNull(button.GetComponent<AudioSource>());
        Assert.IsNotNull(glass.GetComponent<AudioSource>());
    }

}
