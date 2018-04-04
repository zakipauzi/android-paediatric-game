using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InjectionGameScript : MonoBehaviour {

	public enum State {
		OPEN_CREAM, APPLY_CREAM, MOVE_SYRINGE, INJECT_SYRINGE, DONE
	};

	private State currentState = State.OPEN_CREAM;
    private bool easyMode = Difficulty.easyMode;
    private GameObject arrow1, arrow2, arrow3, arrow4;

    private Animation wellDoneAnimation;
    private AudioSource wellDoneSound, music;

    // execute at start of game
	void Start () {
        arrow1 = GameObject.Find("Arrow1");
        arrow2 = GameObject.Find("Arrow2");
        arrow3 = GameObject.Find("Arrow3");
        arrow4 = GameObject.Find("Arrow4");

        if (easyMode) {
            ShowArrow();
        } else {
            arrow1.SetActive(false);
            arrow2.SetActive(false);
            arrow3.SetActive(false);
            arrow4.SetActive(false);
        }

        wellDoneAnimation = GameObject.Find("wellDone").GetComponent<Animation>();
        wellDoneSound = GameObject.Find("wellDone").GetComponent<AudioSource>();
        music = GameObject.Find("GameHandler").GetComponent<AudioSource>();

        music.Play();
    }

    // delay setting of state
    private IEnumerator SetState(State targetState, float time = 0.1f) {
        yield return new WaitForSeconds(time);
        currentState = targetState;

        if (easyMode) {
            ShowArrow();
        }
    }

    public void HideArrowsExcept(GameObject arrowToKeep) {
        GameObject[] arrows = { arrow1, arrow2, arrow3, arrow4 };

        if (arrowToKeep == null) {
            foreach (GameObject arrow in arrows) {
                arrow.SetActive(false);
            }
        } else {
            foreach (GameObject arrow in arrows) {
                if (!(arrow.Equals(arrowToKeep))){
                    arrow.SetActive(false);
                }
            }
        }
    }

    public void ShowArrow() {
        GameObject[] arrows = { arrow1, arrow2, arrow3, arrow4, null };
        GameObject arrow = arrows[(int)currentState];

        if(arrow == null) {
            HideArrowsExcept(null);
        } else {
            arrow.SetActive(true);
            arrow.GetComponent<Animation>().Play();
            HideArrowsExcept(arrow);
        }
    }

    public State GetState() {
        return currentState;
    }

    // attempt to change the state and return if it was a success
    public bool ChangeState(State targetState) {
        // can only to this state if it directly precedes the current one
        if((int)currentState == (int)targetState - 1) {
            StartCoroutine(SetState(targetState));
            if(targetState == State.DONE) {
                StartCoroutine(Done());
            }
            return true;
        }

        return false;
    }

    // executed when the game is done
	private IEnumerator Done() {
        yield return new WaitForSeconds(1);
        wellDoneAnimation.Play();
        wellDoneSound.Play();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("8-cutscene");
	}
}
