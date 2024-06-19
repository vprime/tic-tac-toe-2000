using System;
using System.Collections;
using Environment.Components;
using Environment.Interfaces;
using UnityEngine;

namespace Environment.Systems
{
    public class ClickableObject : MonoBehaviour, IInteractableObject
    {
        public Action OnPress { get; set; }
        
        [SerializeField] private Transform buttonIndicator;
        [SerializeField] private ButtonConfig buttonConfig;
        
        // Button Control
        private bool _buttonActivated;
        private float _buttonPressTime;
        private float _buttonEnableTime;

        private Vector2 _buttonCenter;
        private float _buttonXRadius;
        private float _buttonYRadius;
        
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
            while (_buttonPressTime > Time.time || _buttonEnableTime > Time.time)
            {
                if (_buttonPressTime >= Time.time)
                {
                    var depth = (_buttonPressTime - Time.time)/buttonConfig.buttonSnapTime * buttonConfig.maxPressDepth;
                    buttonIndicator.localPosition = new Vector3(buttonIndicator.localPosition.x, -depth, buttonIndicator.localPosition.z);
                }
                yield return null;
            }
            buttonIndicator.localPosition = new Vector3(buttonIndicator.localPosition.x, 0f, buttonIndicator.localPosition.z);
            _buttonActivated = false;
        }
    }
}
