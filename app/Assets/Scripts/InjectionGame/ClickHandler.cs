using System.Collections;
using UnityEngine;

public class ClickHandler : MonoBehaviour {

    private InjectionGameScript injectionGameScript;
    private bool playingSound = false;
    private Collider2D veinCollider;
    
    private static GameObject cream, creamLid, syringe1, syringe2, creamBlob;
    private static Vector3 syringeLastGoodPosition;
    private static GameObject draggedObject;

    void Start() {
        injectionGameScript = GameObject.Find("GameHandler").GetComponent<InjectionGameScript>();

        cream = GameObject.Find("Cream");
        creamLid = GameObject.Find("CreamLid");
        syringe1 = GameObject.Find("Syringe1");
        syringe2 = GameObject.Find("Syringe2");
        creamBlob = GameObject.Find("CreamBlob");

        veinCollider = GameObject.Find("Vein").GetComponent<Collider2D>();

        syringeLastGoodPosition = syringe1.transform.position; // for moving the syringe back to where it was
    }

    void OnMouseDrag() {
        if (gameObject.GetComponent<Collider2D>().isTrigger) { // some objects can be dragged, some objects cannot
            OnClickObject();
        } else {
            PickupOrDragObject();
        }
    }

    void OnMouseUp() {
        DropObject();  // drop the currently dragged object, if any
    }

    // Functions concerned with clicking objects

    private void OnClickCreamLid() {
        cream.layer = 0;
        bool success = injectionGameScript.ChangeState(InjectionGameScript.State.APPLY_CREAM);
        if (success) {
            gameObject.GetComponent<Animation>().Play();
        }
    }

    private void OnClickCream() {
        if (injectionGameScript.GetState() == InjectionGameScript.State.APPLY_CREAM) {
            draggedObject = creamBlob;
            PickupOrDragObject();
        }
    }

    private void OnClickSyringe2() { // syringe2 is the "push" part of the syringe
        gameObject.GetComponent<Animation>().Play();
        bool success = injectionGameScript.ChangeState(InjectionGameScript.State.DONE);
    }

    private void OnClickObject() {
        if (gameObject == creamLid) {
            OnClickCreamLid();
        } else if (gameObject == cream) {
            OnClickCream();
        } else if (gameObject == syringe2) {
            OnClickSyringe2();
        }

        AudioSource sound = gameObject.GetComponent<AudioSource>();

        if (sound && !playingSound){
            playingSound = true;
            sound.Play();
        }
    }

    // Functions concerned with dragging objects

    private void PickupOrDragObject() {
        if(draggedObject == null) {
            draggedObject = gameObject;
        }

        draggedObject.transform.position = CurrentTouchPosition;
    }

    private void OnDropSyringe() {
        if (injectionGameScript.GetState() == InjectionGameScript.State.MOVE_SYRINGE && syringe1.GetComponent<Collider2D>().IsTouching(veinCollider)) {
            syringeLastGoodPosition = syringe1.transform.position;
            syringe1.layer = 2;

            injectionGameScript.ChangeState(InjectionGameScript.State.INJECT_SYRINGE);
        } else {
            StartCoroutine(TweenMovement(syringe1, syringe1.transform.position, syringeLastGoodPosition));
        }
    }

    private void OnDropCreamBlob() {
        if (injectionGameScript.GetState() == InjectionGameScript.State.APPLY_CREAM && creamBlob.GetComponent<Collider2D>().IsTouching(veinCollider)) {
            creamBlob.GetComponent<Animation>().Play();
            creamBlob.GetComponent<AudioSource>().Play();
            StartCoroutine(DisableObject(creamBlob));
            injectionGameScript.ChangeState(InjectionGameScript.State.MOVE_SYRINGE);
        }

        else {
            StartCoroutine(TweenMovement(creamBlob, creamBlob.transform.position, creamBlob.transform.position + new Vector3(0, -10, 0)));
        }
    }

    private void DropObject() {
        if(draggedObject == syringe1) {
            OnDropSyringe();
        } else if(draggedObject == creamBlob) {
            OnDropCreamBlob();
        }

        draggedObject = null;
        playingSound = false;
    }

    private Vector2 CurrentTouchPosition {
        get { return Camera.main.ScreenToWorldPoint(Input.mousePosition); }
    }

    // delay disabling of an object
    private IEnumerator DisableObject(GameObject obj) {
        yield return new WaitForSeconds(1);
        obj.SetActive(false);
    }

    // smoothly moves an object from one place to another, used for moving the syringe back to its original position
    private IEnumerator TweenMovement(GameObject obj, Vector3 currentPosition, Vector3 targetPosition) {
        var FRAMES = 30.0f;

        for (var t = 0; t <= FRAMES; t++) {
            obj.transform.SetPositionAndRotation(Vector3.Lerp(currentPosition, targetPosition, (t / FRAMES)), obj.transform.rotation);
            yield return new WaitForEndOfFrame();
        }
    }

}
