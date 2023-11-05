using System;
using System.Collections;

namespace DefaultNamespace
{
    public interface ISceneLoader
    {
        void Load(string sceneName, Action onLoaded = null);
    }
}