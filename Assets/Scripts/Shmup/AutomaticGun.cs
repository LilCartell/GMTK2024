using UnityEngine;

public class AutomaticGun : Gun
{
    public float firstShotOffset = 0.0f;

    private bool _firstShotIsDone = false;
    private float _timeBeforeAllowFirstShot;

    private void Awake()
    {
        _timeBeforeAllowFirstShot = firstShotOffset + timeBetweenShots;
    }

    public override void Update()
    {
        base.Update();

        if(!_firstShotIsDone)
        {
            _timeBeforeAllowFirstShot -= Time.deltaTime;
            if(_timeBeforeAllowFirstShot < 0.0f)
            {
                _timeBeforeAllowFirstShot = 0.0f;
                RequestShoot();
                _firstShotIsDone = true;
            }
        }
        else
        {
            RequestShoot();
        }
    }
}
