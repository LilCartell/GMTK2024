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
        GameSession.Instance.Win();
    }

    public Vector3 GetCenterOffset()
    {
        float minX = Mathf.Infinity;
        float maxX = -Mathf.Infinity;
        float minY = Mathf.Infinity;
        float maxY = -Mathf.Infinity;
        foreach(Transform t in transform)
        {
            var positionConsidered = t.localPosition;
            var childSpriteRenderer = t.GetComponentInChildren<SpriteRenderer>();

            if (childSpriteRenderer != null && childSpriteRenderer.transform != t)
            {
                positionConsidered += childSpriteRenderer.transform.localPosition; //Hack to compensate offseted main sprite renderers
            }
            if (positionConsidered.x < minX)
                minX = positionConsidered.x;
            if(positionConsidered.x > maxX)
                maxX = positionConsidered.x;
            if(positionConsidered.y < minY)
                minY = positionConsidered.y;
            if (positionConsidered.y > maxY)
                maxY = positionConsidered.y;
        }
        return new Vector3((minX + maxX) / 2.0f, (minY + maxY) / 2.0f);
    }
}
