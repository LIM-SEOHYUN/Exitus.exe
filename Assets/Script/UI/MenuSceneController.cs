using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneController : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string gameSceneName = "Map";
    [SerializeField] private string startSceneName = "StartScene";
    [SerializeField] private string clearSceneName = "ClearScene";

    public void StartGame()
    {
        LoadScene(gameSceneName);
    }

    public void GoToMainMenu()
    {
        LoadScene(startSceneName);
    }

    public void OpenClearScene()
    {
        LoadScene(clearSceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        // Unity 에디터에서 테스트할 때 Play 모드를 종료한다.
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 실제 빌드된 게임을 종료한다.
        Application.Quit();
#endif
    }

    private void LoadScene(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError("[MenuSceneController] Scene 이름이 비어 있습니다.");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}