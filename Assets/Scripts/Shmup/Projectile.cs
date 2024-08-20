using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 1.0f;
    public float Damage = 1.0f;
    public float LifeTime = 1.0f;
    public AudioSource audioSource;

    private Vector3 _travelDirection;
    private float _timeSinceSpawn = 0.0f;

    private void Awake()
    {
        if (audioSource != null && BuildingShipScene.Instance != null)
            audioSource.volume = 0;
    }

    public void SetTravelDirection(Vector3 direction)
    {
        _travelDirection = direction;
        _travelDirection.Normalize();
    }

    public void Update()
    {
        transform.position += _travelDirection * Speed * Time.deltaTime;
        _timeSinceSpawn += Time.deltaTime;
        if(_timeSinceSpawn > LifeTime)
            Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var characterTouched = collision.GetComponentInParent<ShmupCharacter>();
        if(characterTouched != null)
        {
            characterTouched.TakeDamage(Damage, collision.gameObject);
            Destroy(gameObject);
        }
    }
}
