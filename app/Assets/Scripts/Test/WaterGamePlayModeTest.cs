using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class WaterGamePlayModeTest {

    [SetUp]
    public void Init() {
        SceneManager.LoadScene("15-water-game", LoadSceneMode.Single);

    }

    [UnityTest] // Test for loading the scene successfully 
    public IEnumerator TestLoadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Assert.AreEqual("15-water-game", currentScene.name);
        yield return null;
    }

    [UnityTest]  //Checks to see whether the glass is draggable at the beginning of the game
    public IEnumerator GlassIsDraggableAtStartOfGame() {

        GameObject glass = GameObject.Find("glass");
        GlassDrag gd = glass.GetComponent<GlassDrag>();

        Assert.IsTrue(gd.ReturnCanDrag());
        yield return null;
    }

    [UnityTest] //Check to see whether all water objects are not find-able
                //and therefore not in the scene
    public IEnumerator WaterObjectsAreNotVisibleAtTheBeginning() {

        GameObject water1 = GameObject.Find("water1");
        GameObject water2 = GameObject.Find("water2");
        GameObject water3 = GameObject.Find("water3");
        GameObject water4 = GameObject.Find("water4");
        GameObject water5 = GameObject.Find("water5");
        GameObject water6 = GameObject.Find("water6");

        Assert.IsNull(water1);
        yield return null;
        Assert.IsNull(water2);
        yield return null;
        Assert.IsNull(water3);
        yield return null;
        Assert.IsNull(water4);
        yield return null;
        Assert.IsNull(water5);
        yield return null;
        Assert.IsNull(water6);
        yield return null;
    }

    [UnityTest] //Check All GameObjects that should be in the scene are
    public IEnumerator AllGameObjectsBesidesWaterObjectsAreInScene() {

        GameObject glassCollider = GameObject.Find("glassCollider");
        GameObject waterDispenser = GameObject.Find("water_dispenser");
        GameObject button = GameObject.Find("button");
        GameObject rectangle = GameObject.Find("rectangle");
        GameObject glass = GameObject.Find("glass");
        GameObject wellDone = GameObject.Find("wellDone");
        GameObject waterBackground = GameObject.Find("water_B");

        Assert.IsNotNull(glassCollider);
        yield return null;
        Assert.IsNotNull(waterDispenser);
        yield return null;
        Assert.IsNotNull(button);
        yield return null;
        Assert.IsNotNull(rectangle);
        yield return null;
        Assert.IsNotNull(glass);
        yield return null;
        Assert.IsNotNull(wellDone);
        yield return null;
        Assert.IsNotNull(waterBackground);
        yield return null;
    }
}
