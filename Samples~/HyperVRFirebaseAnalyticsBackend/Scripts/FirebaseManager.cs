using Firebase;
using UnityEngine;

namespace HyperVR.Analytics
{
    public class FirebaseManager : MonoBehaviour
    {
        private static FirebaseManager instance;

        public static FirebaseManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject("FirebaseManager");
                    DontDestroyOnLoad(go);
                    instance = go.AddComponent<FirebaseManager>();
                    instance.Init();
                }

                return instance;
            }
        }

        public bool IsReady { get; private set; }

        private FirebaseManager()
        {
        }

        private void Init()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    // Create and hold a reference to your FirebaseApp,
                    // where app is a Firebase.FirebaseApp property of your application class.
                    var app = FirebaseApp.DefaultInstance;
                    Debug.Log(">>> Firebase enabled");
                    // Set a flag here to indicate whether Firebase is ready to use by your app.
                    IsReady = true;
                }
                else
                {
                    Debug.LogError($">>> Could not resolve all Firebase dependencies: {dependencyStatus}");
                    // Firebase Unity SDK is not safe to use here.
                }
            });
        }
    }
}