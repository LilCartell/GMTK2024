using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelInfo")]
public class LevelsInfo : ScriptableObject
{
    [SerializeField]
    public List<LevelInfo> LevelsList;
}

[Serializable]
public class LevelInfo
{
    public float Money;
    public float Timer;
    public float CameraDistance;
    public GameObject EnemyPrefab;
}