using System.Collections;
using UnityEngine;
using App.Components;

namespace App.Systems
{
    public class OpeningSequenceControl : MonoBehaviour
    {
        [SerializeField] private AppControl appControl;
        private void Awake()
        {
            appControl.OnAppStateChange.AddListener(HandleAppStateChange);
        }

        private void HandleAppStateChange(AppStates prev, AppStates next)
        {
            if (next == AppStates.OpeningSequence)
            {
                StartCoroutine(SequenceRoutine());
            }
        }

        private IEnumerator SequenceRoutine()
        {
            // Trigger opening sequence animation, and await completion or user input.
            yield return null;
            RoutineComplete();
        }

        private void RoutineComplete()
        {
            appControl.CurrentAppState = AppStates.MainMenu;
        }
    }
}
