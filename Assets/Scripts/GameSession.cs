using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession
{
	public ShipBlueprint CurrentShipBlueprint;
	public float CurrentMoney = 0;
	public int CurrentLevel = 0;
	public int CurrentBriefing = 0;
	public bool IsLoadingWin = false;
	public bool IsLoadingLose = false;

	private List<LevelInfo> _levelsInfo;

	private static GameSession _instance;
	public static GameSession Instance
	{
		get
		{
			if (_instance == null)
				_instance = new GameSession();
			return _instance;
		}
	}

	public LevelInfo GetCurrentLevelInfo()
	{
		if (_levelsInfo == null)
			_levelsInfo = Resources.LoadAll<LevelsInfo>("").First().LevelsList;
        return _levelsInfo[CurrentLevel];
	}

	public void Lose()
	{
		if(!IsLoadingLose && !IsLoadingWin)
		{
            IsLoadingLose = true;
            SceneManager.LoadScene("GameOverScene");
        }
	}

	public void Win()
	{
		if(!IsLoadingWin && !IsLoadingLose)
		{
            IsLoadingWin = true;
            ++CurrentLevel;
            SceneManager.LoadScene("BriefingScene");
        }
	}

	public void GoToNextSceneAfterBriefing()
	{
        if (_levelsInfo == null)
            _levelsInfo = Resources.LoadAll<LevelsInfo>("").First().LevelsList;

        if (CurrentLevel >= _levelsInfo.Count)
		{
			SceneManager.LoadScene("FinishGameScene");
		}
		else
		{
			SceneManager.LoadScene("BuildingShipScene");
		}
	}
}