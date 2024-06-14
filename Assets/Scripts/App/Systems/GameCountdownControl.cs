using System.Collections;
using UnityEngine;
using App.Components;

namespace App.Systems
{
    public class GameCountdownControl : MonoBehaviour
    {
        [SerializeField] private AppControl appControl;
        private void Awake()
        {
            appControl.OnAppStateChange.AddListener(HandleAppStateChange);
        }

        private void HandleAppStateChange(AppStates prev, AppStates next)
        {
            if (next == AppStates.GameCountdown)
            {
                StartCoroutine(SequenceRoutine());
            }
        }

        IEnumerator SequenceRoutine()
        {
            // Load the game map, play the initialization animation, await animation completion.
            yield return null;
            RoutineComplete();
        }

        void RoutineComplete()
        {
            appControl.CurrentAppState = AppStates.Game;
        }
    }
}
