using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class scanningController : MonoBehaviour {

	MoveTable table;
	GameObject upScanner;
	GameObject downScanner;
	// Use this for initialization
	void Start () {
		upScanner = GameObject.Find ("up");
		downScanner = GameObject.Find ("down");
		StartCoroutine (processing());
	}


	private IEnumerator processing(){
		yield return new WaitForSeconds(3);
		moveTableInto ();
		yield return new WaitForSeconds(5);
		closeUpDownScanner ();
		yield return new WaitForSeconds(5);
		rotateScanner ();
		yield return new WaitForSeconds(16);
		openUpDownScanner ();
		yield return new WaitForSeconds(5);
		moveTableBack ();
		yield return new WaitForSeconds(5);
		loadNextScene ();

	}

	private void moveTableInto(){
		table = GameObject.Find ("table").GetComponent<MoveTable> ();
		table.setTableFlag(1);
	}

	private void closeUpDownScanner(){
		if (upScanner != null && downScanner != null) {
			upScanner.GetComponent<rotateScanner> ().setScannerFlag (1);
			downScanner.GetComponent<rotateScanner> ().setScannerFlag (1);
		}
	}

	private void rotateScanner(){
		if (upScanner != null && downScanner != null) {
			upScanner.GetComponent<rotateScanner> ().setScannerFlag (3);
			downScanner.GetComponent<rotateScanner> ().setScannerFlag (3);
		}
	}

	private void openUpDownScanner(){
		if (upScanner != null && downScanner != null) {
			upScanner.GetComponent<rotateScanner> ().setScannerFlag (2);
			downScanner.GetComponent<rotateScanner> ().setScannerFlag (2);
		}
	}


	private void moveTableBack(){
		table = GameObject.Find ("table").GetComponent<MoveTable> ();
		table.setTableFlag(2);
	}
		
	private void loadNextScene(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
