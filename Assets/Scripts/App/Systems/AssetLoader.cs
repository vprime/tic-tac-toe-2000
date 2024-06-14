using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.Components;

namespace App.Systems
{
    /// <summary>
    /// Asset loader fetches assetsbundles and loads them into memory.
    /// For this example, AssetLoader should not be needed. 
    /// </summary>
    public class AssetLoader : MonoBehaviour
    {
        [SerializeField] private AppControl appControl;
        private void Awake()
        {
            appControl.OnAppStateChange.AddListener(HandleAppStateChange);
        }

        private void HandleAppStateChange(AppStates prev, AppStates next)
        {
            if (next == AppStates.LoadAssets)
            {
                StartCoroutine(LoadAssets());
            }
        }

        IEnumerator LoadAssets()
        {
            yield return null;
            LoadingComplete();
        }

        void LoadingComplete()
        {
            appControl.CurrentAppState = AppStates.OpeningSequence;
        }
    }
}
