using System.Collections.Generic;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    [RequireComponent(typeof(Image))]
    public class MaterialGradient:MonoBehaviour
    {
        private Color[] _colors;
        private Image _image;
        private void Start()
        {
            _image = GetComponent<Image>();
        }

        private void FixedUpdate()
        {
            float progress = GlobalGradient.GetGradientState();
            int colorIndex = (int)(_colors.Length * progress); 
            _image.color = Color.Lerp(_image.color,_colors[colorIndex],GlobalGradient.GetLerpSpeed());
        }

        public void InitColors(Color[] colors)
        {
            _colors = colors;
        }
    }
}