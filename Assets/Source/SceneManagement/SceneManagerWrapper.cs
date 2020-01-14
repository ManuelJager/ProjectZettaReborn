using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zetta.Generics;
using UnityEngine.SceneManagement;

namespace Zetta.SceneManagement
{
    public enum SpecialScene
    {
        MainMenu,
        GameWorld,
        Editor
    }

    public class SceneManagerWrapper : AutoInstanceMonoBehaviour<SceneManagerWrapper>
    {
        private SpecialScene? activeScene = null;

        private Dictionary<SpecialScene, string> specialScenePaths = new Dictionary<SpecialScene, string>
        {
            { SpecialScene.MainMenu, "MainMenu"},
            { SpecialScene.GameWorld, "GameWorld"},
            { SpecialScene.Editor, "Editor"}
        };

        public new void Awake()
        {
            base.Awake();
        }

        public void Start()
        {
            LoadScene(SpecialScene.MainMenu);
        }

        public void LoadScene(SpecialScene specialScene)
        {
            // Unload active scene
            if (activeScene != null)
            {
                var activeScenePath = specialScenePaths[specialScene];
                SceneManager.UnloadSceneAsync(activeScenePath);
            }

            // Load scene
            var newScenePath = specialScenePaths[specialScene];
            activeScene = specialScene;
            SceneManager.LoadSceneAsync(newScenePath, LoadSceneMode.Additive);
        }
    }
}

