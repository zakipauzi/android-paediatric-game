using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class InjectionScenePlayModeTest {

    [UnityTest]
    public IEnumerator ChronologicalStateTest() // to check the states go in chronological order
    {
        SceneManager.LoadScene("7-injection", LoadSceneMode.Single);

        yield return null;

        InjectionGameScript igs = GameObject.Find("GameHandler").GetComponent<InjectionGameScript>();
        bool success = igs.ChangeState(InjectionGameScript.State.APPLY_CREAM);

        yield return new WaitForSeconds(0.25f);

        success = igs.ChangeState(InjectionGameScript.State.MOVE_SYRINGE);

        yield return new WaitForSeconds(0.25f);

        success = igs.ChangeState(InjectionGameScript.State.INJECT_SYRINGE);

        yield return new WaitForSeconds(0.25f);

        success = igs.ChangeState(InjectionGameScript.State.DONE);

        yield return new WaitForSeconds(0.25f);

        Assert.IsTrue(success);
    }


    [UnityTest]
    public IEnumerator WrongChronologicalStateTest() // to check the state order does not go an incorrect way
    {
        SceneManager.LoadScene("7-injection", LoadSceneMode.Single);

        yield return null;

        InjectionGameScript igs = GameObject.Find("GameHandler").GetComponent<InjectionGameScript>();
        bool success = igs.ChangeState(InjectionGameScript.State.APPLY_CREAM);

        yield return new WaitForSeconds(0.25f);

        success = igs.ChangeState(InjectionGameScript.State.INJECT_SYRINGE);

        yield return new WaitForSeconds(0.25f);

        success = igs.ChangeState(InjectionGameScript.State.MOVE_SYRINGE);

        yield return new WaitForSeconds(0.25f);

        success = igs.ChangeState(InjectionGameScript.State.DONE);

        yield return new WaitForSeconds(0.25f);

        Assert.IsFalse(success);
    }

    // now we attempt to simulate a typical game playthrough
    [UnityTest]
    public IEnumerator InjectionGameIntegrationTest()
    {
        SceneManager.LoadScene("7-injection", LoadSceneMode.Single);

        yield return null;

        InjectionGameScript igs = GameObject.Find("GameHandler").GetComponent<InjectionGameScript>();
        GameObject creamLid, cream, vein, syringe1, syringe2, creamBlob, wellDone;

        creamLid = GameObject.Find("CreamLid");
        cream = GameObject.Find("Cream");
        vein = GameObject.Find("Vein");
        syringe1 = GameObject.Find("Syringe1");
        syringe2 = GameObject.Find("Syringe2");
        creamBlob = GameObject.Find("CreamBlob");
        wellDone = GameObject.Find("wellDone");

        Vector3 creamLidPosition = creamLid.transform.position;
        Vector3 syringePosition = syringe1.transform.position;
        Vector3 wellDoneScale = wellDone.transform.localScale;

        yield return null;

        creamLid.GetComponent<ClickHandler>().Invoke("OnMouseDrag", 0);
        creamLid.GetComponent<ClickHandler>().Invoke("OnMouseUp", 0);

        yield return new WaitForSeconds(0.3f);

        Assert.AreNotEqual(creamLidPosition, creamLid.transform.position);
        Assert.AreEqual(igs.GetState(), InjectionGameScript.State.APPLY_CREAM);

        // move the syringe to check it goes back
        syringe1.GetComponent<ClickHandler>().Invoke("OnMouseDrag", 0);

        yield return null;
        syringe1.transform.position = vein.transform.position;
        syringe1.GetComponent<ClickHandler>().Invoke("OnMouseUp", 0);

        // wait 30 frames
        for(int i = 0; i < 31; i++)
        {
            yield return new WaitForEndOfFrame();
        }

        Assert.AreEqual(syringe1.transform.position, syringePosition);

        cream.GetComponent<ClickHandler>().Invoke("OnMouseDrag", 0);
        yield return null;

        creamBlob.transform.position = vein.transform.position;

        yield return null;
        creamBlob.GetComponent<ClickHandler>().Invoke("OnMouseUp", 0);

        yield return new WaitForSeconds(0.3f);

        Assert.AreEqual(igs.GetState(), InjectionGameScript.State.MOVE_SYRINGE);

        syringe1.GetComponent<ClickHandler>().Invoke("OnMouseDrag", 0);

        yield return null;
        syringe1.transform.position = vein.transform.position;

        yield return null;
        syringe1.GetComponent<ClickHandler>().Invoke("OnMouseUp", 0);

        yield return new WaitForSeconds(0.5f);

        Assert.AreNotEqual(syringe1.transform.position, syringePosition);
        Assert.AreEqual(syringe1.transform.position, vein.transform.position);
        Assert.AreEqual(igs.GetState(), InjectionGameScript.State.INJECT_SYRINGE);

        syringe2.GetComponent<ClickHandler>().Invoke("OnMouseDrag", 0);

        yield return new WaitForSeconds(0.3f);
        Assert.AreEqual(igs.GetState(), InjectionGameScript.State.DONE);

        // check that the well done animation plays

        yield return new WaitForSeconds(1.5f);
        Assert.AreNotEqual(wellDone.transform.localScale, wellDoneScale);

    }
}
