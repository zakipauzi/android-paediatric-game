using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class TracerGamePlayModeTest {

    [SetUp]
    public void Init()
    {
        SceneManager.LoadScene("9-tracer-game", LoadSceneMode.Single);
    }

    [UnityTest]
    public IEnumerator testLoadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Assert.AreEqual("9-tracer-game", currentScene.name);
        yield return null;
    }

    [UnityTest]
    [Timeout(180000)]
    // Sets the timeout of the test in milliseconds (if the test hangs, this will ensure 
    // it closes after 3 minutes).
    public IEnumerator testLightPrefab()
    {

        // Remove the default skybox then creating a new game object
        RenderSettings.skybox = null;
        var root = new GameObject();

        // Attach a camera to our root game object.
        root.AddComponent(typeof(Camera));
        var camera = root.GetComponent<Camera>();
        camera.backgroundColor = Color.white;

        // Add our game objects (with the camera included) to the scene by instantiating it.
        root = GameObject.Instantiate(root);
        
        GameObject obj = GameObject.Find("GameController");

        // Wait for three seconds (this gives us time to see the prefab in the scene).
        yield return new WaitForSeconds(3f);

        // Get the TracerMinigameScript from the GameController
        var script = obj.gameObject.GetComponentInChildren<TracerMinigameScript>();

        // Assert that the script exists
        Assert.IsTrue(script != null, "TracerMinigameScript must be set on GameController.");

        // Destroy the temporary objects
        GameObject.Destroy(root);

        yield return null;
    }

    [UnityTest]
    // Test for player to go next scene once game has finished
    public IEnumerator testClickButtonToNextScene()
    {
        GameObject obj = GameObject.Find("GameController");
        TracerMinigameScript script = obj.gameObject.GetComponentInChildren<TracerMinigameScript>();

        // adds 10 points immediately to end game
        for (int i=0; i<10; i++)
        {
            script.SpotTapped();
        }

        // this is to allow the animation of well done to end
        yield return new WaitForSeconds(5f);

        // checks if the scene has changed
        Scene currentScene = SceneManager.GetActiveScene();
        Assert.AreEqual("10-cutscene", currentScene.name); 

        yield return null;
    }

}
