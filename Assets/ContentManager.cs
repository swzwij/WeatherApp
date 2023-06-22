using System.Collections;
using UnityEngine;
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
        _weatherSystem.onGetHourlyTemperature += () => StartCoroutine(Init());
    }

    private void OnDisable() => _tabManager.onSwitchTab -= SwitchTab;

    private void SwitchTab(TabType tabType)
    {
        ShowTempContent(tabType == TabType.Temp);
    }

    private IEnumerator Init()
    {
        yield return new WaitForSeconds(0.5f);
        ShowTempContent(true);
    }

    private void ShowTempContent(bool show)
    {
        _weatherSystem.DisableTempItems(!show);
    }
}
