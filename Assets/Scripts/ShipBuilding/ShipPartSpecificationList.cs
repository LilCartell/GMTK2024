using UnityEngine;
using UnityEngine.UI;

public class ShipPartArchetypeList : MonoBehaviour
{
    public GameObject listRoot;
    public GameObject cellPrefab;

    void Start()
    {
        foreach(Transform child in listRoot.transform)
        {
            Destroy(child.gameObject);
        }

        foreach(var shipPartArchetype in Resources.LoadAll<ShipPartArchetype>(""))
        {
            foreach(var direction in shipPartArchetype.PossibleDirections)
            {
                var newCell = Instantiate(cellPrefab);
                var specification = new ShipPartSpecification() { Orientation = direction, ShipPartArchetype = shipPartArchetype };
                newCell.GetComponent<ShipPartSpecificationCell>().LoadWithSpecification(specification);
                newCell.transform.SetParent(listRoot.transform);
                newCell.transform.localPosition = Vector3.zero;
                newCell.transform.localScale = Vector3.one;
                newCell.GetComponent<Toggle>().group = listRoot.GetComponent<ToggleGroup>();
            }
        }
    }
}
