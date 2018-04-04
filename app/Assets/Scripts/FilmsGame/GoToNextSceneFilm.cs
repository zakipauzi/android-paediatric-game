using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextSceneFilm : MonoBehaviour {

    public void ChangeScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}
