using System;
using GameStates;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class SceneFactory : ISceneFactory
    {
        public void LoadScene(string path)
        {
            LoadScene(path, () => { });
        }

        public void LoadScene(string path, Action onLoaded)
        {
            int buildIndex = SceneUtility.GetBuildIndexByScenePath(path);

            SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Single)!
                .completed += _ =>
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(buildIndex));
                onLoaded();
            };
        }

        public void UnloadScene(string path)
        {
            SceneManager.UnloadSceneAsync(path);
        }
    }
}