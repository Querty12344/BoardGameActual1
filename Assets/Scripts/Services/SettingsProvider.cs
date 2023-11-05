using GameLogic;
using StaticData;
using UnityEngine;

namespace Services
{
    public class SettingsProvider : MonoBehaviour,ISettingsProvider
    {
        [SerializeField] private PerformanceSettings _performanceSettings;
        [SerializeField] private CartsData _cartsData; 

        public PerformanceSettings GetPerformanceSettings() => _performanceSettings;
        public CartsData GetCartData() => _cartsData;
    }
}