using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesSwitcher : MonoBehaviour
{
    private const string MainLevelSceneName = "Main Level";
    private const string WinSceneName = "Win";
    private const string LooseSceneName = "Loose";
    private const string TutorialSceneName = "Tutorial";
    private bool _isAlreadyLoading = false;

    public void LoadMainLevelScene() => LoadScene(MainLevelSceneName);
    public void LoadWinScene() => LoadScene(WinSceneName);
    public void LoadLooseScene() => LoadScene(LooseSceneName);
    public void LoadTutorialScene() => LoadScene(TutorialSceneName);
    private void LoadScene(string sceneName)
    {
        if (_isAlreadyLoading)
        {
            return;
        }
        _isAlreadyLoading = true;
        SceneManager.LoadSceneAsync(sceneName);
    }
}
