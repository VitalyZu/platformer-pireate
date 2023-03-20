using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Defs/PlayerDef", fileName = "PlayerDef")]
public class PlayerDef : ScriptableObject
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private StatDef[] _stats;

    public int MaxHealth => _maxHealth;
    public StatDef[] Stats => _stats;

    public StatDef GetStat(StatId id) => _stats.FirstOrDefault<StatDef>(x => x.Id == id);   //LINQ
    /*{
        foreach (var item in _stats)
        {
            if (item.Id == id)
                return item;
        }
        return default;
    }*/
}
