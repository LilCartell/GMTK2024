using TMPro;
using UnityEngine;

public class ShmupScene : MonoBehaviour
{
    public Camera sceneCamera;
    public Transform enemyAnchor;
    public PlayerShip playerShip;
    public GameObject rightBorder;
    public GameObject leftBorder;
    public GameObject topBorder;
    public GameObject bottomBorder;
    public GameObject background;
    public TextMeshProUGUI timerText;
    public GameObject cheatButton;

    public static ShmupScene Instance { get { return _instance; } }
    private static ShmupScene _instance;

    private float _remainingTime;

    private void Awake()
    {
        _instance = this;

        #if UNITY_EDITOR
                cheatButton.gameObject.SetActive(true);
        #else
                cheatButton.gameObject.SetActive(false);
        #endif

        foreach (Transform child in enemyAnchor)
        {
            Destroy(child.gameObject);
        }
        var currentLevelInfo = GameSession.Instance.GetCurrentLevelInfo();
        playerShip.gameObject.SetActive(false); //Creates a collision bug during enemy instantiation otherwise
        var enemyForThisLevel = Instantiate(currentLevelInfo.EnemyPrefab);
        enemyForThisLevel.transform.SetParent(enemyAnchor);
        enemyForThisLevel.transform.localPosition = -enemyForThisLevel.GetComponent<Enemy>().GetCenterOffset();
        enemyForThisLevel.transform.localRotation = Quaternion.identity;
        enemyForThisLevel.transform.localScale = Vector3.one;
        float baseCameraDistance = Mathf.Abs(sceneCamera.transform.localPosition.z);
        sceneCamera.transform.localPosition = new Vector3(0, 0, -Mathf.Abs(currentLevelInfo.CameraDistance));
        float displacementRatio = Mathf.Abs(currentLevelInfo.CameraDistance / baseCameraDistance);
        enemyAnchor.transform.localPosition *= displacementRatio;
        playerShip.transform.localPosition *= displacementRatio;
        rightBorder.transform.localPosition *= displacementRatio;
        leftBorder.transform.localPosition *= displacementRatio;
        topBorder.transform.localPosition *= displacementRatio;
        bottomBorder.transform.localPosition *= displacementRatio;
        background.transform.localScale *= displacementRatio;
        playerShip.gameObject.SetActive(true);
        
        _remainingTime = currentLevelInfo.Timer;
    }
    public void Start()
    {
        SoundManager.Instance.PlayMusic(SoundManager.Instance.ShmupSceneMusic);
    }

    public void CheatWin()
    {
        GameSession.Instance.Win();
    }

    private void Update()
    {
        _remainingTime -= Time.deltaTime;
        timerText.text = _remainingTime.ToString("00");
        if(_remainingTime <= 0)
        {
            _remainingTime = 0;
            GameSession.Instance.Lose();
        }
    }
}
