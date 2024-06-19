using System;
using Environment.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Environment.Systems
{
    public class InteractionBehavior : MonoBehaviour
    {
        [SerializeField] private Camera raycastCamera;
        [SerializeField] private LayerMask interactableLayers;
        

        private void Start()
        {
            Debug.Log($"Interaction Behavior initialized");
            if (!raycastCamera)
                raycastCamera = Camera.main;
            if (!raycastCamera)
                throw new Exception("No raycast Camera assigned in the InteractionBehavior!");
        }

        private void Update()
        {
            if (Pointer.current.press.wasReleasedThisFrame)
            {
                HandlePress();
            }
        }

        void HandlePress()
        {
            var value = Vector2.zero;
            if (Pointer.current is not null)
            {
                value = Pointer.current.position.ReadValue();
            }
            else if (Touchscreen.current is not null)
            {
                value = Touchscreen.current.primaryTouch.position.ReadValue();
            }
            else if (Mouse.current is not null)
            {
                value = Mouse.current.position.ReadValue();
            }
            else
            {
                Debug.LogError("No pointer position source");
            }
            var ray = raycastCamera.ScreenPointToRay(value);
            if (Physics.Raycast(ray, out var hit, float.PositiveInfinity, interactableLayers))
                ProcessInteraction(hit);
        }

        private static void ProcessInteraction(RaycastHit hit)
        {
            var clickableObj = hit.transform.GetComponent<IInteractableObject>();
            if (clickableObj is null)
            {
                Debug.LogWarning($"Object in Interactable layer does not contain a IInteractableObject: {hit.transform.gameObject}", hit.transform.gameObject);
                return;
            }
            clickableObj.Trigger();
        }
    }
}
