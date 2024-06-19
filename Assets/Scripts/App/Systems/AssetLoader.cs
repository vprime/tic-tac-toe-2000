using System.Collections;
using UnityEngine;
using App.Components;

namespace App.Systems
{
    /// <summary>
    /// Asset loader fetches assets bundles and loads them into memory.
    /// For this example, AssetLoader should not be needed. 
    /// </summary>
    public class AssetLoader : MonoBehaviour
    {
        [SerializeField] private AppControl appControl;
        [SerializeField] private float sequenceTime = 3f;
        [SerializeField] private GameObject sequenceRenderRoot;
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

        private IEnumerator LoadAssets()
        {
            sequenceRenderRoot.SetActive(true);
            // Trigger opening sequence animation, and await completion or user input.
            yield return new WaitForSeconds(sequenceTime);
            sequenceRenderRoot.SetActive(false);
            LoadingComplete();
        }

        private void LoadingComplete()
        {
            appControl.CurrentAppState = AppStates.OpeningSequence;
        }
    }
}
