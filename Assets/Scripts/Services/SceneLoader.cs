using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        
        public void Load(string sceneName, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(sceneName, onLoaded));
        }

        private IEnumerator LoadScene(string sceneName, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onLoaded?.Invoke();
                yield break; 
            }
            var loadScene = SceneManager.LoadSceneAsync(sceneName);
            while (!loadScene.isDone)
            {
                Debug.Log("false");
                yield return null;
            }
            
            Debug.Log("menuStart");
            onLoaded?.Invoke();
        }
            
    }
}