using System;
using UnityEngine;
using WeatherApp.Loading;
using WeatherApp.Tabs;
using WeatherApp.WeatherSystem;

public class ContentManager : MonoBehaviour
{
    [SerializeField]
    private TabManager _tabManager;

    [SerializeField]
    private HourlyWeatherSystem _weatherSystem;

    private void OnEnable()
    {
        _tabManager.onSwitchTab += SwitchTab;
        _weatherSystem.onGetHourlyRain += Init;
    }

    private void OnDisable()
    {
        _tabManager.onSwitchTab -= SwitchTab;
        _weatherSystem.onGetHourlyRain -= Init;
    }

    private void SwitchTab(TabType tabType)
    {
        ShowTempContent(tabType == TabType.Temp);
    }

    private void Init()
    {
        ShowTempContent(true);
        LoadingScreen.Instance.ShowLoadingScreen(false);
    }

    private void ShowTempContent(bool show)
    {
        _weatherSystem.DisableTempItems(!show);
    }
}
