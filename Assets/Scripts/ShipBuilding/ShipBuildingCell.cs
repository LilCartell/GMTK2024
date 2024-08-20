using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShipBuildingCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public GameObject deleteIcon;
    public GameObject greenMask;
    public GameObject redMask;
    public Image shipPartSprite;
    public bool isDeletable;

    public ShipPartSpecification LoadedSpecification { get; private set; }
    public Coordinates Coordinates { get; private set; }

    private bool _canPlace = false;
    private bool _canDelete = false;

    private void Awake()
    {
        ResetUI();
    }

    public void SetCoordinates(Coordinates coordinates)
    {
        Coordinates = coordinates;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(BuildingShipScene.Instance.IsInDeleteMode)
        {
            _canDelete = GetComponentInParent<ShipBuildingGrid>().CanDeleteCell(this);
            if (isDeletable && _canDelete)
            {
                deleteIcon.SetActive(true);
            }
        }
        else
        {
            if (BuildingShipScene.Instance.SelectedSpecification != null)
            {
                shipPartSprite.gameObject.SetActive(true);
                var orientation = BuildingShipScene.Instance.SelectedSpecification.Orientation;
                var specialAnimationSprites = BuildingShipScene.Instance.SelectedSpecification.ShipPartArchetype.GetSpecialAnimationSpritesByOrientation(orientation);
                if (specialAnimationSprites != null && specialAnimationSprites.Count > 0)
                {
                    shipPartSprite.sprite = specialAnimationSprites.Last();
                }
                else
                {
                    shipPartSprite.sprite = BuildingShipScene.Instance.SelectedSpecification.ShipPartArchetype.GetSpriteByOrientation(orientation);
                }
                shipPartSprite.transform.rotation = orientation.GetRotation();

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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetUI();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (BuildingShipScene.Instance.IsInDeleteMode)
        {
            if (isDeletable && _canDelete && LoadedSpecification != null)
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.ClicSound);
                BuildingShipScene.Instance.ShipBuildingActionQueue.QueueAndDoAction(new DeleteSpecsFromCellShipBuildingAction(LoadedSpecification, this));
            }
        }
        else
        {
            if (_canPlace)
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.PlaceCellSound);
                BuildingShipScene.Instance.ShipBuildingActionQueue.QueueAndDoAction(new PlaceSpecInCellsShipBuildingAction(BuildingShipScene.Instance.SelectedSpecification, this));
            }
        }
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
            var specialAnimationSprites = LoadedSpecification.ShipPartArchetype.GetSpecialAnimationSpritesByOrientation(LoadedSpecification.Orientation);
            if (specialAnimationSprites != null && specialAnimationSprites.Count > 0)
            {
                shipPartSprite.sprite = specialAnimationSprites.Last();
            }
            else
            {
                shipPartSprite.sprite = LoadedSpecification.ShipPartArchetype.GetSpriteByOrientation(LoadedSpecification.Orientation);
            }
            shipPartSprite.transform.rotation = LoadedSpecification.Orientation.GetRotation();
        }
        else
        {
            shipPartSprite.gameObject.SetActive(false);
        }
        deleteIcon.SetActive(false);
    }
}
