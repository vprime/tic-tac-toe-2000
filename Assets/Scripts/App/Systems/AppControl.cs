using System.Collections;
using App.Components;
using UnityEngine;
using UnityEngine.Events;

namespace App.Systems
{
    public class AppStateChangeEvent: UnityEvent<AppStates, AppStates>{}
    public class AppControl : MonoBehaviour
    {
        private AppStates _appStates = AppStates.Init;

        public AppStates CurrentAppState
        {
            get => _appStates;
            set
            {
                var prev = _appStates;
                var next = value;
                BeforeAppStateChange.Invoke(prev, next);
                _appStates = next;
                OnAppStateChange.Invoke(prev, next);
            }
        }

        public AppStateChangeEvent BeforeAppStateChange = new AppStateChangeEvent();
        public AppStateChangeEvent OnAppStateChange = new AppStateChangeEvent();

        private IEnumerator Start()
        {
            yield return null;
            CurrentAppState = AppStates.LoadAssets;
        }
    }
}
