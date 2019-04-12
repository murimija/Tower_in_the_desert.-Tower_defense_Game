using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private Animator curtainForScenesTransition;
    private string nextScene;

    public void GoToScene(string nameOfScene)
    {
        nextScene = nameOfScene;
        onTransitionComplete();
    }
    // ReSharper disable once UnusedMember.Global
    public void onTransitionComplete()
    {
        SceneManager.LoadScene(nextScene);
    }
}