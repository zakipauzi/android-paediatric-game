using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// NB: UnityEditor will be used to create a Prefab instance but should
// not be a part of the build.

public class GreenLightPlayModeTest {

    [SetUp]
    public void Init() {
        SceneManager.LoadScene("3-light-game", LoadSceneMode.Single);
    }

    [UnityTest]
    // Test for loading the scene successfully 
    public IEnumerator testLoadScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        Assert.AreEqual("3-light-game", currentScene.name);
        yield return null;
    }

    [UnityTest]
    [Timeout(180000)] 
    // Sets the timeout of the test in milliseconds (if the test hangs, this will ensure 
    // it closes after 3 minutes).
    public IEnumerator testLightPrefab() {

        // Remove the default skybox then creating a new game object
        RenderSettings.skybox = null;
        var root = new GameObject();

        // Attach a camera to our root game object.
        root.AddComponent(typeof(Camera));
        var camera = root.GetComponent<Camera>();
        camera.backgroundColor = UnityEngine.Color.white;

        // Add our game objects (with the camera included) to the scene by instantiating it.
        root = GameObject.Instantiate(root);

#if UNITY_EDITOR

        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Light.prefab");

        // Instantiate the prefab (by adding it to the scene).
        prefab = GameObject.Instantiate(prefab, new Vector3(0, 0, 10), new Quaternion(0, 180, 0, 0));
        
        // Wait for three seconds (this gives us time to see the prefab in the scene).
        yield return new WaitForSeconds(3f);

        // Get the LightScript from the Prefab
        var script = prefab.gameObject.GetComponentInChildren<LightScript>();

        // Assert that the script exists on our prefab.
        Assert.IsTrue(script != null, "LightScript must be set on Light.prefab.");
        
        // Destroy the temporary objects
        GameObject.Destroy(prefab);
        GameObject.Destroy(root);

#endif

        yield return null;
    }

    [UnityTest]
    // Test for player to go next scene once game has finished
    public IEnumerator testClickButtonToNextScene() {

        GameObject obj = GameObject.Find("GameController");
        GameControllerScript gcs = obj.GetComponent<GameControllerScript>();

        // adds 10 points immediately to end game
        gcs.AddPoints(10);

        // this is to allow the animation of well done to end
        yield return new WaitForSeconds(5f);

        // checks if the scene has changed
        Scene currentScene = SceneManager.GetActiveScene();
        Assert.AreEqual("4-cutscene", currentScene.name);  // the next scene is 4-cutscene

        yield return null;
    }

}
