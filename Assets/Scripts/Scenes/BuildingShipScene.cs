using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuildingShipScene : MonoBehaviour
{
    public ShipBuildingGrid buildingGrid;
    public ShipPartSpecificationDescription shipPartSpecificationDescription;
    public ShipStatsPanel shipStatsPanel;
    public Button undoButton;
    public Button redoButton;
    public Transform enemyAnchor;
    public Camera previewCamera;
    public Canvas mainUICanvas;
    public Canvas previewCanvas;

    public Button cheatButton;

    public ShipPartSpecification SelectedSpecification { get; private set; }
    public ShipBuildingActionQueue ShipBuildingActionQueue { get; private set; }
    public bool IsInDeleteMode { get; private set; }

    public static BuildingShipScene Instance { get { return _instance; } }
    private static BuildingShipScene _instance;

    private void Awake()
    {
        _instance = this;
        ShipBuildingActionQueue = new ShipBuildingActionQueue();

    #if UNITY_EDITOR
        cheatButton.gameObject.SetActive(true);
    #else
        cheatButton.gameObject.SetActive(false);
    #endif
        var currentLevelInfo = GameSession.Instance.GetCurrentLevelInfo();
        previewCamera.transform.localPosition = new Vector3(0, 0, -currentLevelInfo.CameraDistance);
        var enemyToPreview = Instantiate(currentLevelInfo.EnemyPrefab);
        enemyToPreview.transform.SetParent(enemyAnchor);
        enemyToPreview.transform.localPosition = -enemyToPreview.GetComponent<Enemy>().GetCenterOffset();
        enemyToPreview.transform.localRotation = Quaternion.identity;
        enemyToPreview.transform.localScale = Vector3.one;
    }

    public void Start()
    {
        SoundManager.Instance.PlayMusic(SoundManager.Instance.BuildingSceneMusic);
    }

    private void Update()
    {
        undoButton.interactable = ShipBuildingActionQueue.CanUndo();
        redoButton.interactable = ShipBuildingActionQueue.CanRedo();
    }

    public void Undo()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.ClicSound);
        ShipBuildingActionQueue.UndoLastAction();
    }

    public void Redo()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.ClicSound);
        ShipBuildingActionQueue.RedoLastAction();
    }

    public void Confirm()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.ClicSound);
        GameSession.Instance.CurrentShipBlueprint = buildingGrid.GetShipBlueprint();
        SceneManager.LoadScene("ShmupScene");
    }

    public void Delete()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.ClicSound);
        IsInDeleteMode = true;
    }

    public void Clear()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.ClicSound);
        ShipBuildingActionQueue.QueueAndDoAction(new ClearGridShipBuildingAction());
    }

    public void Cheat()
    {
        GameSession.Instance.CurrentMoney = 1000000000;
    }

    public void Preview()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.ClicSound);
        mainUICanvas.gameObject.SetActive(false);
        previewCanvas.gameObject.SetActive(true);
    }

    public void CancelPreview()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.ClicSound);
        mainUICanvas.gameObject.SetActive(true);
        previewCanvas.gameObject.SetActive(false);
    }

    public void SetSelectedSpecification(ShipPartSpecification selectedSpecification)
    {
        SelectedSpecification = selectedSpecification;
        IsInDeleteMode = false;
        RefreshSpecificationDescription();
    }
    
    public void RefreshSpecificationDescription()
    {
        shipPartSpecificationDescription.LoadWithSpecification(SelectedSpecification);
    }
}
