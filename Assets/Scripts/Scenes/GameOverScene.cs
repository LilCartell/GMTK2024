using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    public void Awake()
    {
        GameSession.Instance.IsLoadingWin = false;
        GameSession.Instance.IsLoadingLose = false;
    }

    public void Start()
    {
        SoundManager.Instance.PlayMusic(SoundManager.Instance.GameOverSceneMusic);
    }

    public void Next()
    {
        SceneManager.LoadScene("BuildingShipScene");
    }
}
