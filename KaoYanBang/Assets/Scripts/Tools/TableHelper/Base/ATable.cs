using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ATable
{
    Dictionary<string, IRecord> table = new Dictionary<string, IRecord>();

    protected IRecord this[string key]
    {
        get
        {
            if (table.ContainsKey(key))
            {
                return table[key];
            }
            return null;
        }
        set
        {
            table[key] = value;
        }
    }
    protected List<IRecord> GetAllRecord()
    {
        var list = new List<IRecord>();
        foreach(var values in table.Values)
        {
            list.Add(values);
        }
        return list;
    }
}