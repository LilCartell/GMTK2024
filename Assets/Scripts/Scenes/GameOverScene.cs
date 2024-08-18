using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    public void Next()
    {
        SceneManager.LoadScene("BuildingShipScene");
    }
}
