using UnityEngine;
using NUnit.Framework;
using UnityEditor.SceneManagement;

public class TracerGameEditModeTest {

    private GameObject background, gc, wellDone, scoreDisplay, instructions, canvas, child;
    private GameObject[] gameObjArray;

    [SetUp]
    public void Init()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/9-tracer-game.unity", OpenSceneMode.Single);

        background = GameObject.Find("Background");
        gc = GameObject.Find("GameController");
        wellDone = GameObject.Find("wellDone");
        scoreDisplay = GameObject.Find("ScoreDisplay");
        instructions = GameObject.Find("Instructions");
        canvas = GameObject.Find("Canvas");
        child = GameObject.Find("Child");

        gameObjArray = new GameObject[] { background, gc, wellDone, scoreDisplay, instructions, canvas, child };

    }

    [Test]
    // Test all components are in the scene
    public void testAllComponentsPresent()
    {
        foreach (GameObject go in gameObjArray)
        {
            Assert.IsNotNull(go);
        }
    }

    [Test]
    // Test if the game controller component contains the game script
    public void testGameObjectContainsScript()
    {
        Assert.IsNotNull(gc.GetComponent<TracerMinigameScript>());
    }

}
