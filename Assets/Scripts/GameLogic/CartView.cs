using System;
using System.Linq;
using Extencions;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameLogic
{
    
    public class CartView:MonoBehaviour
    {
        [SerializeField] private Image[] _suitObjects;
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text[] _nominalText;
        [SerializeField] private GameObject _trumpIndicator;
        private bool _isTrump;
        private bool _isActive;
        private int _suit;
        private ICartSkinProvider _skins;
        private int _nominal;

        public void Construct(ICartSkinProvider skinProvider,int nominal,int suit)
        {
            _trumpIndicator.SetActive(false);
            _nominal = nominal;
            _skins = skinProvider;
            _suit = suit;
            _suitObjects.ToList().ForEach(s => s.GetComponent<MaterialGradient>().InitColors(_skins.GetSuitColors(_suit)));
            UpdateSprites();
        }

        public void Activate()
        {
            _isActive = true;
            UpdateSprites();
        }

        public void Deactivate()
        {
            _isActive = false;
            UpdateSprites();
        }

        public void SetTramp(int trampSuit)
        {
            _isTrump = trampSuit == _suit;
        }

        private void UpdateSprites()
        {
            if (_isActive)
            {
                _background.sprite = _skins.GetCartFace();
                _suitObjects.ToList().ForEach(s => s.sprite =  _skins.GetSuit(_suit));
                foreach (var text in _nominalText)
                {
                    if (_nominal < 5)
                    {
                        text.text = (_nominal + 6).ToString();
                    }
                    else
                    {
                        switch (_nominal)
                        {
                            case(5):
                                text.text = "В";
                                break;
                            case(6):
                                text.text = "Д";
                                break;
                            case(7):
                                text.text = "К";
                                break;
                            case(8):
                                text.text = "Т";
                                break;
                        }
                    }
                }
                _trumpIndicator.SetActive(_isTrump);
            }
            else
            {
                _background.sprite = _skins.GetCartBack();
                _suitObjects.ToList().ForEach(s => s.sprite = _skins.GetEmptySprite());
                _nominalText.ToList().ForEach(t => t.text = "");
                _trumpIndicator.SetActive(false);
            }
        }
        
    }
}