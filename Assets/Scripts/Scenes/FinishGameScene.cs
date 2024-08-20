using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGameScene : MonoBehaviour
{
    public void Awake()
    {
        GameSession.Instance.IsLoadingWin = false;
        GameSession.Instance.IsLoadingLose = false;
    }

    public void BackToBeginning()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.ClicSound);
        GameSession.Instance.CurrentBriefing = 0;
        GameSession.Instance.CurrentLevel = 0;
        GameSession.Instance.CurrentShipBlueprint = null;
        SceneManager.LoadScene("TitleScene");
    }

    public void CloseGame()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.ClicSound);
    #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
    #else
                        Application.Quit();
    #endif
    }
}
