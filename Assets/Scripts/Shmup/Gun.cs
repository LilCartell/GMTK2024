using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileStartingPoint;
    public float timeBetweenShots = 1.0f;

    private float _timeSinceLastShot = 0.0f;

    public virtual void Update()
    {
        _timeSinceLastShot += Time.deltaTime;   
    }

    public void RequestShoot()
    {
        if (_timeSinceLastShot >= timeBetweenShots)
            Shoot();
    }

    protected void Shoot()
    {
        _timeSinceLastShot = 0.0f;
        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = projectileStartingPoint.position;
        projectile.transform.localRotation = this.transform.localRotation;
        projectile.transform.localScale = Vector3.one;
        projectile.GetComponent<Projectile>().SetTravelDirection(projectileStartingPoint.position - this.transform.position);
    }
}
