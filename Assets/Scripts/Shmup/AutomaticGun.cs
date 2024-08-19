using UnityEngine;

public class AutomaticGun : Gun
{
    public float firstShotOffset = 0.0f;
    public float rotationAmplitude = 0.0f;
    public float rotationSpeed = 0.0f;

    private bool _firstShotIsDone = false;
    private float _timeBeforeAllowFirstShot;
    private bool _isRotationIncreasing;
    private float _baseRotation;
    private float _currentRotationDegree;

    private void Awake()
    {
        _timeBeforeAllowFirstShot = firstShotOffset + timeBetweenShots;
        _baseRotation = transform.rotation.eulerAngles.z;
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

        if(rotationSpeed > 0.0f &&  rotationAmplitude > 0.0f)
        {
            if(_isRotationIncreasing)
            {
                _currentRotationDegree +=  rotationSpeed * Time.deltaTime;
                if(_currentRotationDegree >= rotationAmplitude)
                {
                    _currentRotationDegree = rotationAmplitude;
                    _isRotationIncreasing = false;
                }
            }
            else
            {
                _currentRotationDegree -=  rotationSpeed * Time.deltaTime;
                if(_currentRotationDegree <= -rotationAmplitude)
                {
                    _currentRotationDegree = -rotationAmplitude;
                    _isRotationIncreasing = true;
                }
            }
            this.transform.localRotation = Quaternion.Euler(0, 0, _baseRotation + _currentRotationDegree);
        }
    }
}
