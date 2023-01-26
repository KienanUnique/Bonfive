using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesSwitcher : MonoBehaviour
{
    private const string MainLevelSceneName = "Main Level";
    private const string WinSceneName = "Win";
    private const string LooseDeadSceneName = "Loose Dead";
    private const string LooseTimeOutSceneName = "Loose Time Out";
    private const string TutorialSceneName = "Tutorial";
    private const string BackstorySceneName = "Backstory";
    private bool _isAlreadyLoading = false;

    public void LoadMainLevelScene() => LoadScene(MainLevelSceneName);
    public void LoadWinScene() => LoadScene(WinSceneName);
    public void LoadLooseDeadScene() => LoadScene(LooseDeadSceneName);
    public void LoadLooseTimeOutScene() => LoadScene(LooseTimeOutSceneName);
    public void LoadTutorialScene() => LoadScene(TutorialSceneName);
    public void LoadBackstoryScene() => LoadScene(BackstorySceneName);
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
