using DTT.UI.ProceduralUI;
using UnityEngine;
using UnityEngine.UI;

namespace WeatherApp.Tabs
{
    /// <summary>
    /// Class to handle the tab itself.
    /// </summary>
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(RoundedImage))]
    public class Tab : MonoBehaviour
    {
        /// <summary>
        /// Whether the tab starts on.
        /// </summary>
        [SerializeField]
        private bool _startsOn;

        /// <summary>
        /// The color of the tab when it's selected.
        /// </summary>
        [SerializeField]
        private Color _selectedColor;

        /// <summary>
        /// The type of tab it is.
        /// </summary>
        [SerializeField]
        private TabType _tabType;

        /// <summary>
        /// Reference to the button of the tab.
        /// </summary>
        private Button _button;

        /// <summary>
        /// Reference to the image of the tab.
        /// </summary>
        private RoundedImage _roundedImage;

        /// <summary>
        /// The initial color of the tab.
        /// </summary>
        private Color _initialColor;

        /// <summary>
        /// Reference to the tab manager holding this tab.
        /// </summary>
        private TabManager _tabManager;

        /// <summary>
        /// Setting references and set it to selected is startsOn is true.
        /// </summary>
        private void Awake()
        {
            _button = GetComponent<Button>();
            _roundedImage = GetComponent<RoundedImage>();
            _initialColor = _roundedImage.color;

            if (_startsOn)
                Select();
        }

        /// <summary>
        /// Setting Click to the onClick of the button.
        /// </summary>
        private void OnEnable() => _button.onClick.AddListener(Click);

        /// <summary>
        /// Removing Click to the onClick of the button.
        /// </summary>
        private void OnDisable() => _button.onClick.RemoveListener(Click);

        /// <summary>
        /// Setting the tabManager.
        /// </summary>
        /// <param name="tabManager"></param>
        public void Init(TabManager tabManager) => _tabManager = tabManager;

        /// <summary>
        /// Setting this button as selected.
        /// </summary>
        private void Click() => _tabManager.SelectThis(this, _tabType);
        
        /// <summary>
        /// Setting the color of this tab to selected.
        /// </summary>
        public void Select() => _roundedImage.color = _selectedColor;
        
        /// <summary>
        /// Setting the color of this tab to deselected.
        /// </summary>
        public void Deselect() => _roundedImage.color = _initialColor;
    }
}