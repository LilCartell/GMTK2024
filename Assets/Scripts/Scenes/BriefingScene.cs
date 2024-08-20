using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class Briefing
{
    public List<String> SpeechBubbles;
}

public class BriefingScene : MonoBehaviour
{
    public List<Briefing> Briefings;

    public TextMeshProUGUI BriefingText;

    private Briefing _briefingToPlay;
    private int _currentBriefingIndex;

    private void Awake()
    {
        GameSession.Instance.IsLoadingWin = false;
        GameSession.Instance.IsLoadingLose = false;

        if (GameSession.Instance.CurrentBriefing >= Briefings.Count)
        {
            Debug.LogError("Not enough briefings");
            GameSession.Instance.CurrentBriefing = Briefings.Count - 1;
        }
        _briefingToPlay = Briefings[GameSession.Instance.CurrentBriefing++];
        _currentBriefingIndex = -1;
        NextSpeechBubble();
    }

    public void Start()
    {
        SoundManager.Instance.PlayMusic(SoundManager.Instance.BriefingSceneMusic);
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            NextSpeechBubble();
        }
    }

    private void NextSpeechBubble()
    {
        if(++_currentBriefingIndex < _briefingToPlay.SpeechBubbles.Count) 
        {
            BriefingText.text = _briefingToPlay.SpeechBubbles[_currentBriefingIndex];
        }
        else
        {
            GameSession.Instance.GoToNextSceneAfterBriefing();
        }
    }
}
