using System;
using Environment.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Environment.Systems
{
    public class InteractionBehavior : MonoBehaviour
    {
        [SerializeField] private Camera raycastCamera;
        [SerializeField] private InputActionReference click;
        [SerializeField] private LayerMask interactableLayers;
        

        private void Start()
        {
            if (!raycastCamera)
                raycastCamera = Camera.main;
            if (!raycastCamera)
                throw new Exception("No raycast Camera assigned in the InteractionBehavior!");

            if (!click)
                throw new Exception("No InputActionRefrence assigned for InteractionBehavior!");
            
            click.action.performed += HandlePress;
        }


        private void HandlePress(InputAction.CallbackContext ctx)
        {
            var value = Vector2.zero;
            if (Touchscreen.current is not null)
            {
                value = Touchscreen.current.primaryTouch.position.ReadValue();
            }
            else if (Mouse.current is not null)
            {
                value = Mouse.current.position.ReadValue();
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
