using GameLogic;
using StaticData;

namespace Services
{
    public interface ISettingsProvider
    {
        public PerformanceSettings GetPerformanceSettings();
        public CartsData GetCartData();
    }
}