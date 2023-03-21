using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class LevelData
{
    [SerializeField] private List<LevelProgress> _progress;

    public int GetLevel(StatId id)
    {
        var stat = _progress.FirstOrDefault(x => x.Id == id);
        return stat?.Level ?? 0; // return obj1!=null ? obj1 : obj2;
    }

    public void LevelUp(StatId id)
    {
        var stat = _progress.FirstOrDefault(x => x.Id == id);

        if (stat == null) _progress.Add(new LevelProgress(id, 1));
        else stat.Level++;
    }

}

[Serializable]
public  class LevelProgress
{
    public StatId Id;
    public int Level;

    public LevelProgress(StatId id, int level)
    {
        Id = id;
        Level = level;
    }
}