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
    public int heal;
    public Vector3 position;
    public List<int> relate;
}

[Serializable]
public class Player
{
    public int heal;
}

public class Target
{
    public GameObject target;
    public Dictionary<int, List<Transform>> path;
}