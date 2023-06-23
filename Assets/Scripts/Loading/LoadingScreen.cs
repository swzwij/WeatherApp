using SingletonBehaviour;
using UnityEngine;

namespace WeatherApp.Loading
{
    /// <summary>
    /// Class to handle the loading screen.
    /// </summary>
    public class LoadingScreen : SingletonBehaviour<LoadingScreen>
    {
        /// <summary>
        /// The loading screen object.
        /// </summary>
        [SerializeField]
        private GameObject _loadingScreen;

        /// <summary>
        /// Whether the loading screen starts on.
        /// </summary>
        [SerializeField]
        private bool _startOn;

        /// <summary>
        /// Either enable or disable the loading screen on start.
        /// </summary>
        private void Awake() => ShowLoadingScreen(_startOn);

        /// <summary>
        /// Show or hide the loading screen.
        /// </summary>
        /// <param name="show">Whether the loading screen should be shown.</param>
        public void ShowLoadingScreen(bool show) => _loadingScreen.SetActive(show);
    }
}
