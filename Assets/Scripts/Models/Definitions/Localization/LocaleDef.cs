using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

[CreateAssetMenu(menuName = "Defs/LocaleDef", fileName = "LocaleDef")]
public class LocaleDef : ScriptableObject
{
    //ru https://docs.google.com/spreadsheets/d/e/2PACX-1vRF4IH5miy1nU68gqtsmHMSKqFlQUrxcxn-lKQX-SYRujlRISSjfQc9IyQ-vWi8sSgyHflzVUXHYn8p/pub?gid=0&single=true&output=tsv
    //ru https://docs.google.com/spreadsheets/d/e/2PACX-1vRF4IH5miy1nU68gqtsmHMSKqFlQUrxcxn-lKQX-SYRujlRISSjfQc9IyQ-vWi8sSgyHflzVUXHYn8p/pub?gid=0&single=true&output=tsv
    //en https://docs.google.com/spreadsheets/d/e/2PACX-1vRF4IH5miy1nU68gqtsmHMSKqFlQUrxcxn-lKQX-SYRujlRISSjfQc9IyQ-vWi8sSgyHflzVUXHYn8p/pub?gid=1460725698&single=true&output=tsv

    [SerializeField] private string _url;
    [SerializeField] private List<LocaleItem> _localeItems;

    private UnityWebRequest _request;

    [ContextMenu("Update locale")]
    public void UpdateLocale()
    {
        if (_request != null) return;

        _request = UnityWebRequest.Get(_url);
        _request.SendWebRequest().completed += OnDataLoaded;
    }

    public Dictionary<string, string> GetData()
    {
        var dictionary = new Dictionary<string, string>();
        foreach (var locale in _localeItems)
        {
            dictionary.Add(locale.Key, locale.Value);
        }
        return dictionary;
    }

    private void OnDataLoaded(AsyncOperation obj)
    {
        if (obj.isDone)
        {
            var rows = _request.downloadHandler.text.Split('\n');

            foreach (var row in rows)
            {
                AddLocaleItem(row);
            }
            _request = null;
        }
    }

    private void AddLocaleItem(string row)
    {
        try
        {
            var parts = row.Split('\t');
            _localeItems.Add( new LocaleItem{Key = parts[0], Value = parts[1]} );
        }
        catch (Exception e)
        {
            Debug.LogError($"Cant parse row: {row} - {e}");
        }
    }

    [Serializable]
    public class LocaleItem {
        public string Key;
        public string Value;
    }
}
