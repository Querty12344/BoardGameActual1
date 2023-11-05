using UnityEditor.Profiling;
using UnityEngine;

namespace GameLogic
{
    [RequireComponent(typeof(CartMover))]
    [RequireComponent(typeof(CartView))]
    public class Cart:MonoBehaviour
    {
        private int _nominal;
        private int _suit;
        
        public int GetNominal() => _nominal;
        public int GetSuit() => _suit;
        public IPlayer Owner { get; set; }
        public CartMover Mover { get;private set; }
        public CartView View { get; private set; }
        
        public void Construct(int nominal,int suit)
        {
            _nominal = nominal;
            _suit = suit;
            Mover = GetComponent<CartMover>();
            View = GetComponent<CartView>();
        }

        public void Remove()
        {
            Mover.Remove();
            Destroy(gameObject);
        }
    }
}