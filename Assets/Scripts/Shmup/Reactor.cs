using UnityEngine;

public class Reactor : MonoBehaviour
{
    private SpecialAnimationScript _specialAnimationScript;

    private void Awake()
    {
        _specialAnimationScript = GetComponent<SpecialAnimationScript>();
    }

    public void OnStartMovementInDirection(Directions direction)
    {
        if (_specialAnimationScript.Direction == direction.GetOpposite())
            _specialAnimationScript.StartSpecialAnimation();
    }

    public void OnStopMovementInDirection(Directions direction)
    {
        if(_specialAnimationScript.Direction == direction.GetOpposite())
            _specialAnimationScript.StopSpecialAnimation();
    }
}
