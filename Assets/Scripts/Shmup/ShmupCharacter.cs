using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShmupCharacter : MonoBehaviour
{
    public const float BLINK_ITERATION_DURATION = 0.2f;
    public const int BLINK_ITERATIONS_TO_DO = 3;

    public AudioSource TakeDamageAudioSource;

    private bool _nextBlinkingSpritesAreWeakpoints;
    private List<SpriteRenderer> _currentlyBlinkingWeakpoints = new List<SpriteRenderer>();
    private List<SpriteRenderer> _currentlyBlinkingOtherSprites = new List<SpriteRenderer>();
    protected List<SpriteRenderer> _spritesToStartBlinkNextFrame = new List<SpriteRenderer>();
    protected float _life;

    public void TakeDamage(float damage, GameObject damageOrigin)
    {
        if(BuildingShipScene.Instance == null)
        {
            if (TakeDamageAudioSource != null)
                TakeDamageAudioSource.Play();
            float realDamage = damage;
            var weakpointTouched = damageOrigin.GetComponent<WeakPoint>();
            if (weakpointTouched != null)
            {
                damage *= weakpointTouched.DamageFactor;
                if (!_nextBlinkingSpritesAreWeakpoints)
                {
                    _nextBlinkingSpritesAreWeakpoints = true;
                    _spritesToStartBlinkNextFrame.Clear(); //Weakpoints blink alone if touched
                }
                var spriteRenderer = weakpointTouched.GetComponent<SpriteRenderer>();
                if (!_currentlyBlinkingWeakpoints.Contains(spriteRenderer))
                {
                    _spritesToStartBlinkNextFrame.Add(spriteRenderer);
                }
            }
            else
            {
                if (_currentlyBlinkingWeakpoints.Count == 0 && _currentlyBlinkingOtherSprites.Count == 0 && _spritesToStartBlinkNextFrame.Count == 0)
                {
                    var allSpriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
                    foreach (var spriteRenderer in allSpriteRenderers)
                    {
                        if (spriteRenderer.GetComponentInParent<WeakPoint>() == null && !_currentlyBlinkingOtherSprites.Contains(spriteRenderer))
                            _spritesToStartBlinkNextFrame.Add(spriteRenderer);
                    }
                }
            }

            _life -= damage;
            if (_life < 0)
                HandleDeath();
        }
    }

    public virtual void Update()
    {
        if (_spritesToStartBlinkNextFrame.Count > 0)
        {
            StartCoroutine(BlinkSpritesCoroutine(_spritesToStartBlinkNextFrame));
            _nextBlinkingSpritesAreWeakpoints = false;
            _spritesToStartBlinkNextFrame.Clear();
        }
    }

    private IEnumerator BlinkSpritesCoroutine(List<SpriteRenderer> sprites)
    {
        var spritesToBlink = new List<SpriteRenderer>(sprites);
        for (int i = 0; i < BLINK_ITERATIONS_TO_DO; ++i)
        {

            foreach (var spriteRenderer in spritesToBlink)
            {
                spriteRenderer.enabled = false;
            }
            yield return new WaitForSeconds(BLINK_ITERATION_DURATION / 2.0f);
            foreach (var spriteRenderer in spritesToBlink)
            {
                spriteRenderer.enabled = true;
            }
            yield return new WaitForSeconds(BLINK_ITERATION_DURATION / 2.0f);
        }

        foreach (var sprite in spritesToBlink)
        {
            _currentlyBlinkingWeakpoints.Remove(sprite);
            _currentlyBlinkingOtherSprites.Remove(sprite);
        }
    }

    protected abstract void HandleDeath();
}