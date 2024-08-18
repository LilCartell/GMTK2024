using UnityEngine;

public class ShmupScene : MonoBehaviour
{
    public float PlayerShipPartsSize = 60.0f;

    public static ShmupScene Instance { get { return _instance; } }
    private static ShmupScene _instance;

    private void Awake()
    {
        _instance = this;
    }
}
