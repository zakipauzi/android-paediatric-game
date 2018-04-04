using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class InjectionSceneEditModeTest {

    [Test] // to see that it does not go to DONE straight away
    public void DoesNotGoToDoneStateImmediately() {
        EditorSceneManager.OpenScene("Assets/Scenes/7-injection.unity", OpenSceneMode.Single);

        InjectionGameScript igs = GameObject.Find("GameHandler").GetComponent<InjectionGameScript>();
        bool success = igs.ChangeState(InjectionGameScript.State.DONE);

        Assert.IsFalse(success);
    }

    [Test] // to check that it does go from OPEN_CREAM to APPLY_CREAM
    public void GoesToCorrectStateFromStart() {
        EditorSceneManager.OpenScene("Assets/Scenes/7-injection.unity", OpenSceneMode.Single);

        InjectionGameScript igs = GameObject.Find("GameHandler").GetComponent<InjectionGameScript>();
        bool success = igs.ChangeState(InjectionGameScript.State.APPLY_CREAM);

        Assert.IsTrue(success);
    }

    [Test]
    public void AllObjectsPresent() { // to check that all the objects in the scene are there
        EditorSceneManager.OpenScene("Assets/Scenes/7-injection.unity", OpenSceneMode.Single);

        GameObject child, vein, cream, creamLid, syringe1, syringe2, creamBlob, wellDone, gameHandler, arrow1, arrow2, arrow3, arrow4;

        child = GameObject.Find("Child");
        vein = GameObject.Find("Vein");
        cream = GameObject.Find("Cream");
        creamLid = GameObject.Find("CreamLid");
        syringe1 = GameObject.Find("Syringe1");
        syringe2 = GameObject.Find("Syringe2");
        creamBlob = GameObject.Find("CreamBlob");
        wellDone = GameObject.Find("wellDone");
        gameHandler = GameObject.Find("GameHandler");
        arrow1 = GameObject.Find("Arrow1");
        arrow2 = GameObject.Find("Arrow2");
        arrow3 = GameObject.Find("Arrow3");
        arrow4 = GameObject.Find("Arrow4");

        Assert.IsNotNull(child);
        Assert.IsNotNull(vein);
        Assert.IsNotNull(cream);
        Assert.IsNotNull(creamLid);
        Assert.IsNotNull(syringe1);
        Assert.IsNotNull(syringe2);
        Assert.IsNotNull(creamBlob);
        Assert.IsNotNull(wellDone);
        Assert.IsNotNull(gameHandler);
        Assert.IsNotNull(arrow1);
        Assert.IsNotNull(arrow2);
        Assert.IsNotNull(arrow3);
        Assert.IsNotNull(arrow4);
    }

}
