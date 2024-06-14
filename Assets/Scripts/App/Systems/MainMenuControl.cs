using System.Collections;
using UnityEngine;
using App.Components;

namespace App.Systems
{
    public class MainMenuControl : MonoBehaviour
    {
        [SerializeField] private AppControl appControl;
        private void Awake()
        {
            appControl.OnAppStateChange.AddListener(HandleAppStateChange);
        }

        private void HandleAppStateChange(AppStates prev, AppStates next)
        {
            if (next == AppStates.MainMenu)
            {
                StartCoroutine(SequenceRoutine());
            }
        }

        IEnumerator SequenceRoutine()
        {
            // Load main menu and await user input
            yield return null;
            RoutineComplete();
        }

        void RoutineComplete()
        {
            appControl.CurrentAppState = AppStates.GameCountdown;
        }
    }
}
