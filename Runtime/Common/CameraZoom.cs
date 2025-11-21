using UnityEngine;

namespace MewtonGames.Common
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _speed;
        [SerializeField] private float _min;
        [SerializeField] private float _max;

        private Vector2? _firstTouchStartPosition;
        private Vector2? _secondTouchStartPosition;
        private float? _startCameraSize;

        private void Update()
        {
            if (Input.touchCount < 1)
            {
                return;
            }

            var firstTouch = Input.GetTouch(0);
            if (firstTouch.phase == TouchPhase.Began)
            {
                _firstTouchStartPosition = _camera.ScreenToViewportPoint(firstTouch.position);
            }

            if (Input.touchCount < 2)
            {
                return;
            }

            var secondTouch = Input.GetTouch(1);
            if (secondTouch.phase == TouchPhase.Began)
            {
                _secondTouchStartPosition = _camera.ScreenToViewportPoint(secondTouch.position);
            }

            if (firstTouch.phase == TouchPhase.Canceled || firstTouch.phase == TouchPhase.Ended ||
                secondTouch.phase == TouchPhase.Canceled || secondTouch.phase == TouchPhase.Ended)
            {
                _firstTouchStartPosition = null;
                _secondTouchStartPosition = null;
                _startCameraSize = null;
                return;
            }

            if (_firstTouchStartPosition == null || _secondTouchStartPosition == null)
            {
                return;
            }

            _startCameraSize ??= _camera.fieldOfView;
            var currentFirstTouchPosition = _camera.ScreenToViewportPoint(firstTouch.position);
            var currentSecondTouchPosition = _camera.ScreenToViewportPoint(secondTouch.position);
          
            var startDistance = Vector2.Distance(_firstTouchStartPosition.Value, _secondTouchStartPosition.Value);
            var currentDistance = Vector2.Distance(currentFirstTouchPosition, currentSecondTouchPosition);
            var delta = startDistance - currentDistance;
            var newFieldOfView = _startCameraSize.Value + delta * _speed;
            _camera.fieldOfView = Mathf.Clamp(newFieldOfView, _min, _max);
        }
    }
}