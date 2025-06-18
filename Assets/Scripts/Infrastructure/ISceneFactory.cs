using System;

namespace Infrastructure
{
    public interface ISceneFactory
    {
        void LoadScene(string path);
        void LoadScene(string path, Action onLoaded);
        void UnloadScene(string path);
    }
}