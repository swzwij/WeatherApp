using DTT.UI.ProceduralUI;
using UnityEngine;

namespace WeatherApp.Tabs
{
    public class TempTab : Tab
    {
        [SerializeField]
        private RoundedImage _curveImage;

        public override void Select()
        {
            base.Select();
            _curveImage.color = SelectedColor;
        }

        public override void Deselect()
        {
            base.Deselect();
            _curveImage.color = p_initialColor;
        }
    }
}