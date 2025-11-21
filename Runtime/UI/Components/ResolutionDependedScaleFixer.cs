using UnityEngine;
using UnityEngine.UI;

namespace MewtonGames.UI.Components
{
    [AddComponentMenu("MewtonGames/UI/Components/ResolutionDependedScaleFixer")]
    public class ResolutionDependedScaleFixer : MonoBehaviour
    {
        [Header("For autofill - leave empty fields")] 
        [SerializeField] private CanvasScaler _canvasScaler;
        [SerializeField] private RectTransform _rectTransform;

        [Space]
        [SerializeField] private bool _fixVertical;
        [SerializeField] private bool _fixHorizontal;
        [SerializeField] private bool _autoFixOnStart;


        public void Fix()
        {
            var scale = 1f / GetScale(_canvasScaler);

            if (_fixVertical)
            {
                var newVerticalScale = _rectTransform.rect.height * scale;
                if (newVerticalScale < 1f)
                {
                    newVerticalScale = 1f;
                }

                _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newVerticalScale);
            }

            if (_fixHorizontal)
            {
                var newHorizontalScale = _rectTransform.rect.height * scale;
                if (newHorizontalScale < 1f)
                {
                    newHorizontalScale = 1f;
                }

                _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newHorizontalScale);
            }
        }


        private void Start()
        {
            if (_canvasScaler == null)
            {
                _canvasScaler = GetComponentInParent<CanvasScaler>();
            }

            if (_rectTransform == null)
            {
                _rectTransform = GetComponent<RectTransform>();
            }

            if (_autoFixOnStart)
            {
                Fix();
            }
        }

        private float GetScale(CanvasScaler scaler)
        {
            var screenAspectRatio = Screen.height / (float) Screen.width;
            if (screenAspectRatio < 1)
            {
                screenAspectRatio = 1 / screenAspectRatio;
            }

            var referenceResolution = scaler.referenceResolution;
            var referenceAspectRatio = referenceResolution.y / referenceResolution.x;
            if (referenceAspectRatio < 1)
            {
                referenceAspectRatio = 1 / referenceAspectRatio;
            }

            return (referenceAspectRatio > screenAspectRatio ? Screen.height : Screen.width) / referenceResolution.y;
        }
    }
}