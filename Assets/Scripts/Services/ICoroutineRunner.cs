using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator enumerator);
        void StopAllCoroutines();
    }
}