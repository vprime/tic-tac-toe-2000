using System.Collections;
using UnityEngine;
using App.Components;

namespace App.Systems
{
    public class CelebrateWinnerControl : MonoBehaviour
    {
        [SerializeField] private AppControl appControl;
        private void Awake()
        {
            appControl.OnAppStateChange.AddListener(HandleAppStateChange);
        }

        private void HandleAppStateChange(AppStates prev, AppStates next)
        {
            if (next == AppStates.CelebrateWinner)
            {
                StartCoroutine(SequenceRoutine());
            }
        }

        IEnumerator SequenceRoutine()
        {
            // Trigger an animation according to the winning player, await the animation completion or user input.
            yield return null;
            RoutineComplete();
        }

        void RoutineComplete()
        {
            appControl.CurrentAppState = AppStates.GameResults;
        }
    }
}
