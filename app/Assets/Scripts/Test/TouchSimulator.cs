using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSimulator : MonoBehaviour {

    // simulate a touch for some seconds (default 0.25)
	public IEnumerator SimulateTouch(Vector3 position, float seconds = 0.25f) {

        Debug.Log("Touch started");

        Touch touch = new Touch
        {
            position = position,
            phase = TouchPhase.Began
        };

        yield return new WaitForEndOfFrame();

        touch.phase = TouchPhase.Moved;

        yield return new WaitForSeconds(seconds);

        touch.phase = TouchPhase.Ended;

        Debug.Log("Touch ended");
    }

    // simulate a drag for some frames (default 30)
    public IEnumerator SimulateDrag(Vector3 start, Vector3 goal, float frames = 30.0f)
    {

        Debug.Log("Drag started");

        Touch touch = new Touch
        {
            position = goal,
            phase = TouchPhase.Began
        };

        for(float i = 0.0f; i < frames; i++)
        {
            touch.position = Vector3.Lerp(start, goal, i/frames);
            touch.phase = TouchPhase.Moved;
            yield return new WaitForEndOfFrame();
        }

        touch.phase = TouchPhase.Ended;

        Debug.Log("Drag ended");
    }
}
