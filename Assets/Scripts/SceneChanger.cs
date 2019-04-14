using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private string nextScene;

    public void GoToScene(string nameOfScene)
    {
        nextScene = nameOfScene;
        onTransitionComplete();
    }

    private void onTransitionComplete()
    {
        SceneManager.LoadScene(nextScene);
    }
}