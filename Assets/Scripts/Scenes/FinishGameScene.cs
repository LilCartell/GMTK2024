using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGameScene : MonoBehaviour
{
    public void BackToBeginning()
    {
        GameSession.Instance.CurrentLevel = 0;
        GameSession.Instance.CurrentShipBlueprint = null;
        SceneManager.LoadScene("BuildingShipScene");
    }

    public void CloseGame()
    {
        #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                        Application.Quit();
        #endif
    }
}
