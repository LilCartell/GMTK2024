using UnityEngine;

public class Enemy : ShmupCharacter
{
    public float StartingLife;

    private void Awake()
    {
        _life = StartingLife;
    }

    protected override void HandleDeath()
    {
        Debug.Log("Gagné !");
        Destroy(this.gameObject);
    }
}
