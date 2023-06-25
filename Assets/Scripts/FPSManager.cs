using UnityEngine;

namespace WeatherApp.FPS
{
    /// <summary>
    /// Class to set the target fps.
    /// </summary>
    public class FPSManager : MonoBehaviour
    {
        /// <summary>
        /// The target fps.
        /// </summary>
        private const int TARGET_FPS = 120;

        /// <summary>
        /// Setting the target fps.
        /// </summary>
        private void Awake() => Application.targetFrameRate = TARGET_FPS;
    }
}