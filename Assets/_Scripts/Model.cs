using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Map
{
    public List<Note> notes;
}

[Serializable]
public class Note
{
    public TargetType type;
    public int heal;
    public Vector3 position;
    public List<int> relate;
}

[Serializable]
public class Player
{
    public int heal;

    public Player()
    {
        heal = 10;
    }
}

public class Target
{
    public ObstacleHandler target;
    public Dictionary<int, List<Transform>> path;
}


public enum TargetType
{
    StartPoint,
    Path,
    FinishPoint
}