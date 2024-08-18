using UnityEngine;

public abstract class ShmupCharacter : MonoBehaviour
{
    protected float _life;

    public void TakeDamage(float damage)
    {
        _life -= damage;
        if (_life < 0)
            HandleDeath();
    }

    protected abstract void HandleDeath();
}
