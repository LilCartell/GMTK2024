using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShipPartSpecificationCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    private ShipPartSpecification _specification;

    public void LoadWithSpecification(ShipPartSpecification specification)
    {
        _specification = specification;
        icon.sprite = specification.ShipPartArchetype.Icon;
        icon.transform.localRotation = specification.Orientation.GetRotation();
    }

    public void OnSelectedChanged(bool selected)
    {
        if(selected)
            BuildingShipScene.Instance.SetSelectedSpecification(_specification);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        BuildingShipScene.Instance.shipPartSpecificationDescription.LoadWithSpecification(_specification);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BuildingShipScene.Instance.RefreshSpecificationDescription();
    }
}
