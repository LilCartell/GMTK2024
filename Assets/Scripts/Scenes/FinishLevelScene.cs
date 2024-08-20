using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevelScene : MonoBehaviour
{
    public void Awake()
    {
        GameSession.Instance.IsLoadingWin = false;
        GameSession.Instance.IsLoadingLose = false;
    }

    public void Next()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.ClicSound);
        SceneManager.LoadScene("BuildingShipScene");
    }
}
