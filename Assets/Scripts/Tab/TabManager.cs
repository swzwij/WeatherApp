using System;
using System.Collections.Generic;
using UnityEngine;

namespace WeatherApp.Tabs
{
    /// <summary>
    /// Class to manage all the tabs.
    /// </summary>
    public class TabManager : MonoBehaviour
    {
        /// <summary>
        /// List of tabs that will be managed.
        /// </summary>
        [SerializeField]
        private List<Tab> _tabs = new();

        /// <summary>
        /// Action for when a tab gets switched.
        /// </summary>
        public Action<TabType> onSwitchTab;

        /// <summary>
        /// Calling Init for each tab in the tab list.
        /// </summary>
        private void Awake()
        {
            foreach(Tab tab in _tabs)
                tab.Init(this);
        }

        /// <summary>
        /// Setting the given tab to selected.
        /// </summary>
        /// <param name="currentTab">The tab that will be set as selected.</param>
        /// <param name="tabType">The type of the given tab.</param>
        public void SelectThis(Tab currentTab, TabType tabType)
        {
            currentTab.Select();

            foreach(Tab tab in _tabs)
            {
                if (tab == currentTab)
                    continue;
                
                tab.Deselect();
            }

            onSwitchTab?.Invoke(tabType);
        }
    }
}