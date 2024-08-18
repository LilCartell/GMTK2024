using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    public void Awake()
    {
        GameSession.Instance.IsLoadingWin = false;
        GameSession.Instance.IsLoadingLose = false;
    }

    public void Next()
    {
        SceneManager.LoadScene("BuildingShipScene");
    }
}
