using System;
using System.Collections;
using Environment.Components;
using Environment.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Environment.Systems
{
    public class ClickableObject : MonoBehaviour, IInteractableObject
    {
        public Action OnPress { get; set; }
        
        [SerializeField] private Transform buttonIndicator;
        [SerializeField] private ButtonConfig buttonConfig;
        [SerializeField] private Vector3 pressAxis;
        [SerializeField] private AudioSource audioSource;
        
        // Button Control
        private bool _buttonActivated;
        private float _buttonPressTime;
        private float _buttonEnableTime;

        private void Start()
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
        }

        public void Trigger()
        {
            if (_buttonActivated) return;
            _buttonActivated = true;
            StartCoroutine(ActivateButton());
        }
        

        private IEnumerator ActivateButton()
        {
            _buttonPressTime = Time.time + buttonConfig.buttonSnapTime;
            _buttonEnableTime = Time.time + buttonConfig.buttonCooldownTime;
            OnPress?.Invoke();
            audioSource.Play();
            while (_buttonPressTime > Time.time || _buttonEnableTime > Time.time)
            {
                if (_buttonPressTime >= Time.time)
                {
                    var depth = (_buttonPressTime - Time.time)/buttonConfig.buttonSnapTime;
                    buttonIndicator.localPosition =
                        Vector3.Lerp(pressAxis * buttonConfig.maxPressDepth, Vector3.zero, 1f - depth);
                }
                yield return null;
            }
            _buttonActivated = false;
        }
    }
}
