using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 1.0f;
    public float Damage = 1.0f;
    public float LifeTime = 1.0f;

    private Vector3 _travelDirection;
    private float _timeSinceSpawn = 0.0f;

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
        float damage = Damage;
        var weakpointTouched = collision.GetComponent<WeakPoint>();
        if (weakpointTouched != null)
            damage *= weakpointTouched.DamageFactor;
        var enemyTouched = collision.GetComponentInParent<Enemy>();
        if(enemyTouched != null)
        {
            enemyTouched.TakeDamage(damage);
            Destroy(gameObject);
        }

        var playerTouched = collision.GetComponentInParent<PlayerShip>();
        if(playerTouched != null)
        {
            playerTouched.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
