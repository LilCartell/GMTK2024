using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShipPartSpecificationCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Image selectionMask;
    private ShipPartSpecification _specification;
    private Toggle _toggleComponent;

    private void Awake()
    {
        _toggleComponent = GetComponent<Toggle>();
    }

    public void Update()
    {
        if(_specification != null)
        {
            bool shouldBeInteractable = GameSession.Instance.CurrentMoney >= _specification.ShipPartArchetype.Cost;
            if (_toggleComponent.isOn)
            {
                _toggleComponent.isOn = shouldBeInteractable;
            }
            _toggleComponent.interactable = shouldBeInteractable;
        }
    }

    public void LoadWithSpecification(ShipPartSpecification specification)
    {
        _specification = specification;
        icon.sprite = specification.ShipPartArchetype.ShopIcon;
        icon.transform.localRotation = specification.Orientation.GetRotation();
    }

    public void OnSelectedChanged(bool selected)
    {
        selectionMask.gameObject.SetActive(selected);
        if (selected)
            BuildingShipScene.Instance.SetSelectedSpecification(_specification);
        else
            BuildingShipScene.Instance.SetSelectedSpecification(null);
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
