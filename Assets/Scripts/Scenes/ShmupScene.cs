using UnityEngine;

public class ShmupScene : MonoBehaviour
{
    public float PlayerShipPartsSize = 60.0f;
    public float PlayerSpeedFactor = 1.0f;
    public Camera sceneCamera;
    public Transform enemyAnchor;
    public PlayerShip playerShip;

    public static ShmupScene Instance { get { return _instance; } }
    private static ShmupScene _instance;

    private void Awake()
    {
        _instance = this;
        foreach(Transform child in enemyAnchor)
        {
            Destroy(child.gameObject);
        }
        var currentLevelInfo = GameSession.Instance.GetCurrentLevelInfo();
        playerShip.gameObject.SetActive(false); //Creates a collision bug during enemy instantiation otherwise
        var enemyForThisLevel = Instantiate(currentLevelInfo.EnemyPrefab);
        enemyForThisLevel.transform.SetParent(enemyAnchor);
        enemyForThisLevel.transform.localPosition = Vector3.zero;
        enemyForThisLevel.transform.localRotation = Quaternion.identity;
        enemyForThisLevel.transform.localScale = Vector3.one;
        float baseCameraDistance = Mathf.Abs(sceneCamera.transform.localPosition.z);
        sceneCamera.transform.localPosition = new Vector3(0, 0, -Mathf.Abs(currentLevelInfo.CameraDistance));
        float displacementRatio = Mathf.Abs(currentLevelInfo.CameraDistance / baseCameraDistance);
        enemyAnchor.transform.localPosition *= displacementRatio;
        playerShip.transform.localPosition *= displacementRatio;
        playerShip.gameObject.SetActive(true);
    }
}
