using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseOverButton : MonoBehaviour {

    private Vector3 newScale;
    private Vector3 normalScale;

    private void Start() {
        normalScale = transform.localScale;
        ScaleFigure();
    }

    private void OnMouseOver() {
        

        transform.localScale = newScale;
        Debug.Log("Mouse Over");
    }

    private void OnMouseExit() {

        transform.localScale = normalScale;
        Debug.Log("Mouse Exit");
    }

    private void ScaleFigure() {

        float originalX = transform.localScale.x;
        float originalY = transform.localScale.y;
        float originalZ = transform.localScale.z;
        newScale = new Vector3(originalX * 1.2f, originalY * 1.2f, originalZ * 1.2f);
    }
}
