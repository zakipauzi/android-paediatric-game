using UnityEngine;
using NUnit.Framework;
using UnityEditor.SceneManagement;

public class GreenLightEditModeTest {

    private GameObject background, gc, wellDone, scoreDisplay, instructions, canvas, child;
    private GameObject[] gameObjArray;

    [SetUp]
    public void Init()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/3-light-game.unity", OpenSceneMode.Single);

        background = GameObject.Find("Background");
        gc = GameObject.Find("GameController");
        wellDone = GameObject.Find("WellDone");
        scoreDisplay = GameObject.Find("ScoreDisplay");
        instructions = GameObject.Find("Instructions");
        canvas = GameObject.Find("Canvas");
        child = GameObject.Find("Child");

        gameObjArray = new GameObject[] {background, gc, wellDone, scoreDisplay, instructions, canvas, child};

    }

    [Test]
    // Test all components are in the scene
    public void testAllComponentsPresent() {
        foreach(GameObject go in gameObjArray) {
            Assert.IsNotNull(go);
        }
    }

    [Test]
    // Test if the components have their scripts
    public void testGameObjectContainsScripts() {
        Assert.IsNotNull(gc.GetComponent<GameControllerScript>());
        Assert.IsNotNull(wellDone.GetComponent<CompleteLightGame>());
    }

}
