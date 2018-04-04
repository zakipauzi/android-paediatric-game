using UnityEngine;

public class Difficulty : MonoBehaviour {

    static Difficulty instance = null;
    public static bool easyMode = true;

    //UpHolds Singleton pattern
    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    public void SetEasyMode() {

        PlayerPrefs.SetInt("EasyMode", 0);
        easyMode = true;
    }

    public void SetHardMode() {

        PlayerPrefs.SetInt("EasyMode", 1);
        easyMode = false;
    }

}
