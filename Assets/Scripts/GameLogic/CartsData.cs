using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    [CreateAssetMenu(menuName = "Create CartsData", fileName = "CartsData", order = 0)]
    public class CartsData:ScriptableObject
    {
        public Sprite[] Suits;
        public int CartForEachSuitCount;
        public Sprite[] Backs;
        public Sprite[] Faces;
        public Sprite[] SuitSprites;
        public SuitColors[] SuitColors;
        public Sprite EmptySprite;
    }
}