using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextSceneScan : MonoBehaviour {

    public void ChangeScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}
