using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesSwitcher : MonoBehaviour
{
    private const string MainLevelSceneName = "Main Level";
    private const string WinSceneName = "Win";
    private const string LooseSceneName = "Loose";
    public void LoadMainLevelScene()
    {
        SceneManager.LoadScene(MainLevelSceneName);
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene(WinSceneName);
    }

    public void LoadLooseScene()
    {
        SceneManager.LoadScene(LooseSceneName);
    }
}
