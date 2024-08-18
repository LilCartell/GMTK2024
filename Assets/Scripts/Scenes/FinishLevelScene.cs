using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevelScene : MonoBehaviour
{
    public void Next()
    {
        SceneManager.LoadScene("BuildingShipScene");
    }
}
