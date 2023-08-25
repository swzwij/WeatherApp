using SingletonBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherCodeLibrary : SingletonBehaviour<WeatherCodeLibrary>
{
    [Serializable]
    public struct WeatherCode 
    {
        public int code;
        public Sprite icon;
    }

    [SerializeField]
    private WeatherCode[] weatherCodes;
    private Dictionary<int, WeatherCode> _codes = new();

    private void Awake()
    {
        foreach (WeatherCode code in weatherCodes)
            _codes.Add(code.code, code);
    }

    public Sprite GetIcon(int code)
    {
        if (_codes.ContainsKey(code))
        {
            return _codes[code].icon;
        }
        else
        {
            Debug.LogError($"Icons doesn't contain the code: {code}");
            return null;
        }

    }
}
