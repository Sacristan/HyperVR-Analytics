using System;
using UnityEngine.SceneManagement;

namespace HyperVR.Analytics
{
    public static class SceneFlowAnalytics
    {
        private const string SceneStartEventName = "scene_start";
        private const string SceneExitEventName = "scene_exit";

        private const string SceneNameParamName = "scene_name";
        private const string SceneTimeParamName = "time_in_scene_s";

        private static string _previousSceneName;
        private static DateTime _startSceneTime;

        public static void Init()
        {
            OnSceneLoaded(SceneManager.GetActiveScene());
            SubscribeToEvents();
        }

        private static void SubscribeToEvents()
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        private static void OnActiveSceneChanged(Scene oldScene, Scene newScene)
        {
            TrackSceneExit(_previousSceneName, _startSceneTime);
            OnSceneLoaded(newScene);
        }

        private static void OnSceneLoaded(Scene scene)
        {
            _startSceneTime = TimeUtils.NowUtc;
            TrackSceneStart(scene.name);
        }

        private static void TrackSceneStart(string sceneName)
        {
            _previousSceneName = sceneName;
            AppAnalytics.TrackEvent(
                SceneStartEventName,
                AppAnalytics.GetParam().Set(SceneNameParamName, sceneName)
            );
        }

        private static void TrackSceneExit(string sceneName, DateTime startSceneTime)
        {
            AppAnalytics.TrackEvent(
                SceneExitEventName,
                AppAnalytics.GetParam().Set(SceneNameParamName, sceneName),
                AppAnalytics.GetParam().Set(SceneTimeParamName, TimeUtils.SecondsSinceTimeUtc(startSceneTime))
            );
        }
    }
}