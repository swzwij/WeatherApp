using UnityEngine;
using WeatherApp.Loading;
using WeatherApp.Tabs;

namespace WeatherApp.WeatherSystem
{
    /// <summary>
    /// Class to handle loading of the hourly content.
    /// </summary>
    public class ContentManager : MonoBehaviour
    {
        /// <summary>
        /// Reference to the tab manager.
        /// </summary>
        [SerializeField]
        private TabManager _tabManager;

        /// <summary>
        /// Reference to the hourly weather system;
        /// </summary>
        [SerializeField]
        private HourlyWeatherSystem _weatherSystem;
        
        /// <summary>
        /// Set listeners.
        /// </summary>
        private void OnEnable()
        {
            _tabManager.onSwitchTab += SwitchTab;
            _weatherSystem.onGetHourlyRain += Init;
        }

        /// <summary>
        /// Remove listeners.
        /// </summary>
        private void OnDisable()
        {
            _tabManager.onSwitchTab -= SwitchTab;
            _weatherSystem.onGetHourlyRain -= Init;
        }

        /// <summary>
        /// Show the correct content when tabs get switched.
        /// </summary>
        /// <param name="tabType">The type of tab its getting switched to.</param>
        private void SwitchTab(TabType tabType) => ShowTempContent(tabType == TabType.Temp);

        /// <summary>
        /// Showing the temperature content and removing the loading screen.
        /// </summary>
        private void Init()
        {
            ShowTempContent(true);
            LoadingScreen.Instance.ShowLoadingScreen(false);
        }

        /// <summary>
        /// Show the temperature content.
        /// </summary>
        /// <param name="show">Whether it needs to show.</param>
        private void ShowTempContent(bool show) => _weatherSystem.DisableTempItems(!show);
    }
}