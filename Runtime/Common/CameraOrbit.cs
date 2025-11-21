using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MewtonGames.Common
{
    public class CameraOrbit : MonoBehaviour, IInitializable
    {
        public bool isInitialized { get; private set; }

        [SerializeField] [Range(-90f, 90f)] private float _xMin = -90f;
        [SerializeField] [Range(-90f, 90f)] private float _xMax = 90f;
        [SerializeField] [Range(1f, 30f)] private float _rotationSpeed = 15f;

        private Transform _target;
        private bool _isScrollStarted;
        private float _y;
        private float _x;
        private float _distance;

        public void Initialize(Vector3 startPosition, Transform target, Action onComplete = null)
        {
            transform.position = startPosition;
            _target = target;
            transform.LookAt(_target);
            Initialize(onComplete);
        }

        public void Initialize(Action onComplete = null)
        {
            var initialVector = transform.position - (_target != null ? _target.position : Vector3.zero);
            _y = transform.eulerAngles.y;
            _x = transform.eulerAngles.x;
            _distance = Vector3.Magnitude(initialVector);

            isInitialized = true;
            onComplete?.Invoke();
        }

        private void Update()
        {
            if (!isInitialized)
            {
                return;
            }

            if (Input.touchCount > 1)
            {
                _isScrollStarted = false;
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
#if UNITY_EDITOR
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    _isScrollStarted = false;
                    return;
                }
#else
                if (EventSystem.current.IsPointerOverGameObject(0))
                {
                    _isScrollStarted = false;
                    return;
                }
#endif
                _isScrollStarted = true;
            }
            else if (Input.GetMouseButton(0) && _isScrollStarted)
            {
                _y += Input.GetAxis("Mouse X") * _rotationSpeed;
                _x -= Input.GetAxis("Mouse Y") * _rotationSpeed;

                _x = ClampAngle(_x, _xMin, _xMax);

                var rotation = Quaternion.Euler(_x, _y, 0);
                var distance = new Vector3(0, 0, -_distance);
                var position = rotation * distance + (_target != null ? _target.position : Vector3.zero);

                transform.rotation = rotation;
                transform.position = position;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isScrollStarted = false;
            }
        }

        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
            {
                angle += 360F;
            }

            if (angle > 360F)
            {
                angle -= 360F;
            }

            return Mathf.Clamp(angle, min, max);
        }
    }
}