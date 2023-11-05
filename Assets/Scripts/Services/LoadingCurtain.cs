using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class LoadingCurtain:MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private static readonly int Disapearing = Animator.StringToHash("Disapearing");

        public void Remove()
        {
            if (_animator != null)
            {
                _animator.SetBool(Disapearing, true);
            } 
            Destroy(gameObject,1f);
        }
    }
}