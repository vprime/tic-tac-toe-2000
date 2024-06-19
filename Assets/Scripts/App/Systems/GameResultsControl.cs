using System.Collections;
using UnityEngine;
using App.Components;

namespace App.Systems
{
    public class GameResultsControl : MonoBehaviour
    {
        [SerializeField] private AppControl appControl;
        private void Awake()
        {
            appControl.OnAppStateChange.AddListener(HandleAppStateChange);
        }

        private void HandleAppStateChange(AppStates prev, AppStates next)
        {
            if (next == AppStates.GameResults)
            {
                StartCoroutine(SequenceRoutine());
            }
        }

        private IEnumerator SequenceRoutine()
        {
            // Display the game result UI, await user input
            yield return null;
            RoutineComplete();
        }

        private void RoutineComplete()
        {
            appControl.CurrentAppState = AppStates.MainMenu;
        }
    }
}
