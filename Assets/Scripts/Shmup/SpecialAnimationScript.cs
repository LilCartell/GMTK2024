using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAnimationScript : MonoBehaviour
{
    public Directions Direction { get; private set; }

    public float TimeBetweenSpecialSprites = 0.1f;

    private Sprite _spriteBeforeAnimationTriggered;
    private List<Sprite> _spritesList = new List<Sprite>();
    private SpriteRenderer _spriteRenderer;
    private bool _coroutineIsRunning;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void LoadWithSpritesAndDirection(List<Sprite> sprites, Directions direction)
    {
        _spritesList = sprites;
        Direction = direction;
    }

    public void StartSpecialAnimation()
    {
        if (!_coroutineIsRunning)
        {
            _spriteBeforeAnimationTriggered = _spriteRenderer.sprite;
            _coroutineIsRunning = true;
            StartCoroutine("SpecialAnimationRoutine");
        }
    }

    public void StopSpecialAnimation()
    {
        if (_coroutineIsRunning) 
        {
            _coroutineIsRunning = false;
            StopCoroutine("SpecialAnimationRoutine");
            _spriteRenderer.sprite = _spriteBeforeAnimationTriggered;
        }
    }

    public IEnumerator SpecialAnimationRoutine()
    {
        int index = 0;
        while(true)
        {
            _spriteRenderer.sprite = _spritesList[index];
            ++index;
            if(index >= _spritesList.Count)
            {
                index = 0;
            }
            yield return new WaitForSeconds(TimeBetweenSpecialSprites);
        }
    }
}
