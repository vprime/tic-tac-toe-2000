using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Environment.Systems
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera gameCamera;
        [SerializeField] private float landscapeScaling = 0.05f;
        [SerializeField] private float portraitScaling = 0.075f;
        [SerializeField] private float refreshRate = 3f;

        private Vector3? _defaultLocation;

        private void OnEnable()
        {
            if (!gameCamera)
                gameCamera = gameObject.GetComponent<Camera>();
            if (!gameCamera)
                gameCamera = Camera.current;
            Assert.IsNotNull(gameCamera);
            StartCoroutine(RescaleRoutine());
        }

        private void OnDisable()
        {
            StopCoroutine(RescaleRoutine());
        }

        IEnumerator RescaleRoutine()
        {
            while (gameObject.activeSelf)
            {
                UpdateView();
                yield return new WaitForSeconds(refreshRate);
            }
        }

        private void UpdateView()
        {
            _defaultLocation ??= gameCamera.transform.position;
            
            var screenRatio = gameCamera.pixelWidth / (float)gameCamera.pixelHeight;
            var movement = (screenRatio > 1f)
                ? Mathf.Log(screenRatio) * landscapeScaling
                : Mathf.Log(screenRatio) * portraitScaling;
            gameCamera.transform.position = _defaultLocation.Value + gameCamera.transform.forward * movement;
        }
    }
}
