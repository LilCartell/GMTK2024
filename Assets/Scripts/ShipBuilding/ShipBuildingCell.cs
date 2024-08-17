using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShipBuildingCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public GameObject greenMask;
    public GameObject redMask;
    public Image shipPartSprite;

    public ShipPartSpecification LoadedSpecification { get; private set; }
    public Coordinates Coordinates { get; private set; }

    private bool _canPlace = false;

    private void Awake()
    {
        ResetUI();
    }

    public void SetCoordinates(Coordinates coordinates)
    {
        Coordinates = coordinates;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_canPlace)
        {
            BuildingShipScene.Instance.ShipBuildingActionQueue.QueueAndDoAction(new PlaceSpecInCellsShipBuildingAction(BuildingShipScene.Instance.SelectedSpecification, this));
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(BuildingShipScene.Instance.SelectedSpecification != null)
        {
            shipPartSprite.gameObject.SetActive(true);
            shipPartSprite.sprite = BuildingShipScene.Instance.SelectedSpecification.ShipPartArchetype.Icon;
            shipPartSprite.transform.rotation = BuildingShipScene.Instance.SelectedSpecification.Orientation.GetRotation();

            if (GetComponentInParent<ShipBuildingGrid>().CanBuildSpecificationsOnCoordinates(BuildingShipScene.Instance.SelectedSpecification, Coordinates))
            {
                _canPlace = true;
                greenMask.SetActive(true);
                redMask.SetActive(false);
            }
            else
            {
                _canPlace = false;
                greenMask.SetActive(false);
                redMask.SetActive(true);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetUI();
    }

    public void PlaceSpecInCell(ShipPartSpecification specifications)
    {
        LoadedSpecification = specifications;
        ResetUI();
    }

    public void RemoveSpecFromCell()
    {
        LoadedSpecification = null;
        ResetUI();
    }

    private void ResetUI()
    {
        greenMask.SetActive(false);
        redMask.SetActive(false);
        if(LoadedSpecification != null)
        {
            shipPartSprite.gameObject.SetActive(true);
            shipPartSprite.sprite = LoadedSpecification.ShipPartArchetype.Icon;
            shipPartSprite.transform.rotation = LoadedSpecification.Orientation.GetRotation();
        }
        else
        {
            shipPartSprite.gameObject.SetActive(false);
        }
    }
}
